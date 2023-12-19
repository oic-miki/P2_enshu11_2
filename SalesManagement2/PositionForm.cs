using Microsoft.EntityFrameworkCore;
using SalesManagement2.Common;
using SalesManagement2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement2
{
    public partial class PositionForm : Form
    {
        //メッセージ表示用クラスのインスタンス化
        MessageDsp msg = new MessageDsp();

        public PositionForm()
        {
            InitializeComponent();
        }

        private void PositionForm_Load(object sender, EventArgs e)
        {
            //ボタンの初期設定
            fncButtonEnable(1);

            //データグリッドビューの列設定
            //列数の指定
            dataGridViewDsp.ColumnCount = 4;
            //0番目（左端）の列幅を設定
            dataGridViewDsp.Columns[0].Width = 70;
            //0番目（左端）の項目名を設定
            dataGridViewDsp.Columns[0].HeaderText = "役職ID";
            dataGridViewDsp.Columns[1].Width = 130;
            dataGridViewDsp.Columns[1].HeaderText = "役職名";
            dataGridViewDsp.Columns[2].Width = 70;
            dataGridViewDsp.Columns[2].HeaderText = "表示";
            dataGridViewDsp.Columns[3].Width = 250;
            dataGridViewDsp.Columns[3].HeaderText = "備考";
            //選択モードを行単位
            dataGridViewDsp.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //読み取り専用
            dataGridViewDsp.ReadOnly = true;
            // 行の追加と削除を禁止
            dataGridViewDsp.AllowUserToAddRows = false;
            dataGridViewDsp.AllowUserToDeleteRows = false;
            //全データ表示
            fncAllSelect();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {

        }

        private void fncAllSelect()
        {
            //全データの表示
            dataGridViewDsp.Rows.Clear();
            try
            {
                using var context = new SalesContext();
                foreach (var p in context.MPositions)
                {
                    dataGridViewDsp.Rows.Add(p.MPositionId, p.PositionName, p.DspFlg, p.Comments);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //データグリッドビューからクリックされたデータをテキストボックスへ
            textBoxPositionID.Text = dataGridViewDsp.Rows[dataGridViewDsp.CurrentRow.Index].Cells[0].Value.ToString();
            textBoxPositionName.Text = dataGridViewDsp.Rows[dataGridViewDsp.CurrentRow.Index].Cells[1].Value.ToString();
            checkBoxDispFLG.Checked = (bool)dataGridViewDsp.Rows[dataGridViewDsp.CurrentRow.Index].Cells[2].Value;
            textBoxComments.Text = dataGridViewDsp.Rows[dataGridViewDsp.CurrentRow.Index].Cells[3].Value.ToString();
        }

        private void buttonRegist_Click(object sender, EventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {

        }

        private void buttonFormPositionClose_Click(object sender, EventArgs e)
        {
            // フォームを閉じる確認メッセージの表示
            DialogResult result = msg.MsgDsp("M20008");

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

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxPositionID.Text = "";
            textBoxPositionName.Text = "";
            checkBoxDispFLG.Checked = true;
            textBoxComments.Text = "";
            dataGridViewDsp.Rows.Clear();
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            string strNo = Microsoft.VisualBasic.Interaction.InputBox("役職マスタを新規に作成する場合は「1」\r\n" +
                                                            "役職マスタにデータを追加する場合は「2」\r\n" +
                                                            "役職マスタへの追加をキャンセルする場合は「3」\r\n" +
                                                            "を入力してください", "役職マスタデータ追加確認", "3", -1, -1);
            //入力データのチェック
            if (strNo != "1" && strNo != "2" && strNo != "3")
            {
                //エラーメッセージ表示
                _ = msg.MsgDsp("M20011");
                return;
            }

            if (strNo == "3")
            {
                return;
            }

            try
            {
                if (strNo == "1")
                {
                    using var context = new SalesContext();
                    //M_Positionテーブルのデータを全削除
                    context.Database.ExecuteSqlRaw("DELETE FROM M_Position;");
                    //自動採番を初期化
                    context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('M_Position', RESEED, 0);");
                    context.SaveChanges();
                }

                TableDataImport TblImport = new TableDataImport();
                TblImport.DataImport("M_Position");

                //データグリッドに全データ表示
                fncAllSelect();
                //完了メッセージの表示
                _ = msg.MsgDsp("M20007");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///////////////////////////////
        //メソッド名：fncButtonEnable()
        //引　数   ：int型：chk　（役職IDの入力状況）
        //戻り値   ：なし
        //機　能   ：役職IDの入力状況に応じてボタンの
        //          ：enableプロパティの設定
        ///////////////////////////////
        private void fncButtonEnable(int chk)
        {
            //chkが１ならば、役職IDが空、２ならば有
            if (chk == 1)
            {
                //Enableは、コントロールの使用可否
                buttonRegist.Enabled = true;
                buttonSearch.Enabled = true;
                buttonPrint.Enabled = true;
                buttonClear.Enabled = true;
                buttonClose.Enabled = true;
                buttonLogout.Enabled = true;
                buttonImport.Enabled = true;
                buttonExport.Enabled = true;
                buttonUpdate.Enabled = false;
                buttonDelete.Enabled = false;
            }
            else if (chk == 2)
            {
                buttonRegist.Enabled = false;
                buttonSearch.Enabled = true;
                buttonPrint.Enabled = true;
                buttonClear.Enabled = true;
                buttonClose.Enabled = true;
                buttonLogout.Enabled = true;
                buttonImport.Enabled = true;
                buttonExport.Enabled = true;
                buttonUpdate.Enabled = true;
                buttonDelete.Enabled = true;
            }
        }

        ///////////////////////////////
        //メソッド名：fncInputNameCheck()
        //引　数   ：なし
        //戻り値   ：true(エラーなし） Or false（エラーあり）
        //機　能   ：入力データのチェック
        ///////////////////////////////
        private bool fncInputNameCheck()
        {
            //役職名の入力データ有無チェック
            if (string.IsNullOrWhiteSpace(textBoxPositionName.Text))
            {
                //入力無しエラーメッセージの表示
                _ = msg.MsgDsp("M20009");
                textBoxPositionName.Focus();
                return false;
            }

            //役職名の文字数チェック
            if (textBoxPositionName.Text.Length > 50)
            {
                //文字数エラーメッセージの表示
                _ = msg.MsgDsp("M20010");
                textBoxPositionName.Focus();
                return false;
            }

            return true;
        }

        ///////////////////////////////
        //メソッド名：fncExistenceIdCheck()
        //引　数   ：なし
        //戻り値   ：true(データあり） Or false（データなし）
        //機　能   ：役職IDの存在チェック
        ///////////////////////////////
        private bool fncExistenceIdCheck()
        {
            bool flg = false;
            //役職IDの存在チェック
            if (int.TryParse(textBoxPositionID.Text, out int positionId))
            {
                try
                {
                    using var context = new SalesContext();
                    //役職IDが存在するか
                    flg = context.MPositions.Any(x => x.MPositionId == positionId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //役職IDが存在しなかった場合のメッセージ表示
            if (!flg)
            {
                _ = msg.MsgDsp("M20012");
                textBoxPositionID.Focus();
            }
            return flg;
        }

        private void textBoxPositionID_TextChanged(object sender, EventArgs e)
        {
            //役職IDの有無をチェック
            if (string.IsNullOrEmpty(textBoxPositionID.Text))
            {
                fncButtonEnable(1);
            }
            else
            {
                fncButtonEnable(2);
            }
        }
    }
}
