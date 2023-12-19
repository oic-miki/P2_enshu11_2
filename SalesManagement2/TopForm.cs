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
        //���b�Z�[�W�\���p�N���X�̃C���X�^���X��
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
        //���\�b�h���FfncFormOpen()
        //���@��   �F�t�H�[�����̂������l
        //�߂�l   �F�Ȃ�
        //�@�@�\   �F���j���[�𓧖������t�H�[�����J��
        ///////////////////////////////
        private void fncFormOpen(int n)
        {
            Form frm;
            //�������A�J���t�H�[����ݒ�
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

            //�t�H�[���𓧖���
            Opacity = 0;

            //�I�����ꂽ�t�H�[�����J��
            frm.ShowDialog();

            //�J�����t�H�[������߂��Ă�����
            //���������������
            frm.Dispose();
        }

        ///////////////////////////////
        //���\�b�h���FfncTableImport()
        //���@��   �F�Ȃ�
        //�߂�l   �F�Ȃ�
        //�@�@�\   �F���b�Z�[�W�e�[�u�����m�F���f�[�^��
        //          �F���݂��Ă��Ȃ���΃C���|�[�g
        ///////////////////////////////
        private void fncTableImport(string strTbl)
        {
            try
            {
                using (var context = new SalesContext())
                {
                    //DB��M_Message�e�[�u���f�[�^�L���`�F�b�N
                    //�f�[�^�����݂��Ă��Ȃ��΃f�[�^���C���|�[�g
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

        private void �I��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // �t�H�[�������m�F���b�Z�[�W�̕\��
            DialogResult result = msg.MsgDsp("M00001");

            if (result == DialogResult.OK)
            {
                // OK�̎��̏���
                Close();
            }
            else
            {
                // �L�����Z���̎��̏���
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
