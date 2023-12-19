using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SalesManagement2.Common;
using SalesManagement2.Model;
using System.Data;
using System.Text;

namespace SalesManagement2
{
    public partial class TopForm : Form
    {
        //メッセージ表示用クラスのインスタンス化
        MessageDsp msg = new MessageDsp();

        public TopForm()
        {
            //fncTableImport("M_Division");
            //fncTableImport("M_Position");
            //fncTableImport("M_Store");
            //fncTableImport("M_Staff");
            fncTableImport("M_Message");

            InitializeComponent();
        }

        private void buttonDivision_Click(object sender, EventArgs e)
        {
            fncFormOpen(1);
        }

        private void ToolStripMenuItemDivisionM_Click(object sender, EventArgs e)
        {
            fncFormOpen(1);
        }

        ///////////////////////////////
        //メソッド名：fncFormOpen()
        //引　数   ：フォーム名称を示す値
        //戻り値   ：なし
        //機　能   ：メニューを透明化しフォームを開く
        ///////////////////////////////
        private void fncFormOpen(int n)
        {
            Form frm;
            //引数より、開くフォームを設定
            switch (n)
            {
                case 1:
                    frm = new DivisionForm();
                    break;
                case 2:
                    frm = new PositionForm();
                    break;
                default:
                    return;
            }

            //フォームを透明化
            Opacity = 0;

            //選択されたフォームを開く
            frm.ShowDialog();

            //開いたフォームから戻ってきたら
            //メモリを解放する
            frm.Dispose();
        }

        ///////////////////////////////
        //メソッド名：fncTableImport()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：メッセージテーブルを確認しデータが
        //          ：存在していなければインポート
        ///////////////////////////////
        private void fncTableImport(string strTbl)
        {
            try
            {
                using (var context = new SalesContext())
                {
                    //DBのM_Messageテーブルデータ有無チェック
                    //データが存在していなけばデータをインポート
                    int cntMsg = context.Database.SqlQueryRaw<int>($"SELECT count(*) AS VALUE FROM {strTbl}").First();
                    if (cntMsg > 0)
                    {
                        return;
                    }
                }
                TableDataImport TblImport = new TableDataImport();
                TblImport.DataImport(strTbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // フォームを閉じる確認メッセージの表示
            DialogResult result = msg.MsgDsp("M00001");

            if (result == DialogResult.OK)
            {
                // OKの時の処理
                Close();
            }
            else
            {
                // キャンセルの時の処理
            }
        }

        private void TopForm_Activated(object sender, EventArgs e)
        {
            if (Opacity == 0)
            {
                Opacity = 1;
            }
        }

        private void buttonPosition_Click(object sender, EventArgs e)
        {
            fncFormOpen(2);
        }

        private void ToolStripMenuItemPositionM_Click(object sender, EventArgs e)
        {
            fncFormOpen(2);
        }
    }
}
