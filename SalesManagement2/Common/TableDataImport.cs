using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement2.Common
{
    internal class TableDataImport
    {
        ///////////////////////////////
        //メソッド名：DataImport()
        //引　数   ：string型：strTbl（インポートテーブル名）
        //戻り値   ：なし
        //機　能   ：引数で指定されたテーブルへのデータインポート
        ///////////////////////////////
        public void DataImport(string strTbl)
        {
            try
            {
                //インポートするCSVファイルの指定
                string csvpth = Path.Combine(Environment.CurrentDirectory, strTbl + ".csv");
                //データテーブルの設定
                DataTable dt = new DataTable();
                dt.TableName = strTbl;

                //csvファイルの内容をDataTableへ
                // 全行読み込み
                var rows = File.ReadAllLines(csvpth, Encoding.GetEncoding("Shift-JIS")).Select(x => x.Split(','));
                // 列設定
                dt.Columns.AddRange(rows.First().Select(s => new DataColumn(s)).ToArray());
                // 行追加
                foreach (var row in rows.Skip(1))
                {
                    dt.Rows.Add(row);
                }

                //DB接続情報の取得
                var dbpth = System.Configuration.ConfigurationManager.ConnectionStrings["SalesContext"].ConnectionString;
                //DataTableの内容をDBへ追加
                using (var bulkCopy = new SqlBulkCopy(dbpth))
                {
                    bulkCopy.DestinationTableName = dt.TableName;
                    bulkCopy.WriteToServer(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
