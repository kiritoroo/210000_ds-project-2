using System;
using System.Drawing;
using System.Windows.Forms;
using FinalProjects.Handler;
using Microsoft.VisualBasic;

namespace FinalProjects
{
    public partial class frmSimulator : Form
    {
        private NodeObjectHandler<int> nodeObjHandler;
        private Timer timer;

        private bool isInfoOpen;

        public frmSimulator()
        {
            InitializeComponent();
        }

        private void frmSimulator_Load(object sender, EventArgs e)
        {
            nodeObjHandler = new NodeObjectHandler<int>(pnContain, this);

            //Init value
            isInfoOpen = false;
            pnInfo.Size = new Size(pnInfo.Size.Width, 30);

            timer = new Timer();
            timer.Interval = 100;
            timer.Start();

            //Event Active
            timer.Tick += timer_Tick;
        }

        private void _Refresh()
        {
            lblIndex.Text = "";
            lblItem.Text = "";
            lblDatatype.Text = "";
        }


        //-----MAIN AREA
        public void GetItem(int index)
        {
            int value = nodeObjHandler.GetItem(index);
            lblIndex.Text = "Index: " + index.ToString();
            lblItem.Text = "Value: " + value.ToString();
            lblDatatype.Text = "Data type: " + value.GetType().ToString();
        }

        public void NewList()
        {
            //TODO: New List logic here.
            string value = Interaction.InputBox($"Tạo mới", " ", "Nhập giá trị...", Screen.PrimaryScreen.Bounds.Width / 2 - 300, Screen.PrimaryScreen.Bounds.Height / 2 - 100);
            if (value == "")
                return;
            try
            {
                int.Parse(value);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không đúng kiểu dữ liệu", "Thêm thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine(ex);
                return;
            }

            nodeObjHandler.AddFirst(int.Parse(value));
        }

        public void AddFirst()
        {
            string value = Interaction.InputBox($"Thêm vào first", "Thêm mới", "Nhập giá trị...", Screen.PrimaryScreen.Bounds.Width / 2 - 300, Screen.PrimaryScreen.Bounds.Height / 2 - 100);
            if (value == "")
                return;
            try
            {
                int.Parse(value);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không đúng kiểu dữ liệu", "Thêm thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine(ex);
                return;
            }

            _Refresh();
            nodeObjHandler.AddFirst(int.Parse(value));
        }

        public void AddLast()
        {
            string value = Interaction.InputBox($"Thêm vào last", "Thêm mới", "Nhập giá trị...", Screen.PrimaryScreen.Bounds.Width / 2 - 300, Screen.PrimaryScreen.Bounds.Height / 2 - 100);
            if (value == "")
                return;
            try
            {
                int.Parse(value);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không đúng kiểu dữ liệu", "Thêm thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine(ex);
                return;
            }

            _Refresh();
            nodeObjHandler.AddLast(int.Parse(value));
        }

        public void Add(int index)
        {
            string value = Interaction.InputBox($"Thêm tại Index = {index}", "Thêm mới", "Nhập giá trị...", Screen.PrimaryScreen.Bounds.Width / 2 - 300, Screen.PrimaryScreen.Bounds.Height / 2 - 100);
            if (value == "")
                return;
            try
            {
                int.Parse(value);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không đúng kiểu dữ liệu", "Thêm thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine(ex);
                return;
            }

            _Refresh();
            nodeObjHandler.Add(int.Parse(value), index);
        }

        //-----EVENT AREA
        private void btnRemove_Click(object sender, EventArgs e)
        {
            _Refresh();
            nodeObjHandler.Remove();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //TODO: Time Tick logic here.
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            if (isInfoOpen == false)
            {
                pnInfo.Size = new Size(pnInfo.Size.Width, 100);
                isInfoOpen = true;
            }
            else
            {
                pnInfo.Size = new Size(pnInfo.Size.Width, 30);
                isInfoOpen = false;
            }
        }

        private void btnPrevNode_Click(object sender, EventArgs e)
        {
            nodeObjHandler.GetNodePrev();
        }

        private void btnNextNode_Click(object sender, EventArgs e)
        {
            nodeObjHandler.GetNodeNext();
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Giảng viên hướng dẫn: Cô Lê Thị Minh Châu \n\nThành viên nhóm: \n20110587 Lê Kiên Trung \n20110568 Nguyễn Hữu Thắng \n18110137 Điều Thị Diễm Kiều ", "Projects cuối kì", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSetItem_Click(object sender, EventArgs e)
        {
            if (nodeObjHandler.currentNodeObj == null || nodeObjHandler.currentNode == null || nodeObjHandler.size == 0)
                return;

            string value = Interaction.InputBox($"Thay đổi giá trị mới", "Change", $"{lblItem.Text}", Screen.PrimaryScreen.Bounds.Width / 2 - 300, Screen.PrimaryScreen.Bounds.Height / 2 - 100);
            if (value == "")
                return;
            try
            {
                int.Parse(value);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không đúng kiểu dữ liệu", "Thay đổi thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine(ex);
                return;
            }

            nodeObjHandler.SetItem(int.Parse(value));
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            nodeObjHandler.Clear();
        }
        private void btnFindItem_Click(object sender, EventArgs e)
        {
            if (nodeObjHandler.size == 0)
                return;

            string value = Interaction.InputBox($"Tìm kiếm giá trị theo index", "Find", $"Nhập Index...", Screen.PrimaryScreen.Bounds.Width / 2 - 300, Screen.PrimaryScreen.Bounds.Height / 2 - 100);
            if (value == "")
                return;
            try
            {
                int.Parse(value);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không đúng kiểu dữ liệu", "Tìm thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine(ex);
                return;
            }

            if (int.Parse(value) > nodeObjHandler.size - 1 || int.Parse(value) < 0)
            {
                MessageBox.Show("Index không hợp lệ", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            nodeObjHandler.FindItem(int.Parse(value));
        }

        private void bunifuIconButton2_Click(object sender, EventArgs e)
        {
            if (nodeObjHandler.size == 0)
                return;

            string value = Interaction.InputBox($"Tìm kiếm index theo giá trị", "Find", $"Nhập giá trị...", Screen.PrimaryScreen.Bounds.Width / 2 - 300, Screen.PrimaryScreen.Bounds.Height / 2 - 100);
            if (value == "")
                return;
            try
            {
                int.Parse(value);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không đúng kiểu dữ liệu", "Tìm thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine(ex);
                return;
            }

            nodeObjHandler.FindIndex(int.Parse(value));
        }

        //End Game
        private void bunifuIconButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
