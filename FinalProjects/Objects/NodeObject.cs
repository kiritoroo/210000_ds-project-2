using Bunifu.UI.WinForms.BunifuButton;
using FinalProjects.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FinalProjects.Objects
{
    public class NodeObject<E>
    {
        //Controls
        private Panel pnContain;
        private frmSimulator frmContain;

        //Components
        public PictureBox ico_Node  { get; set; }
        public PictureBox ico_LinkPrev { get; set; }
        public PictureBox ico_LinkNext { get; set; }

        public E item;
        public int index;

        public int xPos;
        public int yPos;

        //Event Area Components
        private Timer timer;
        public Panel pnButton;
        public BunifuIconButton btnAdd;

        //Check Variables
        bool cursor_InArea;
        int timeSleep;

        public NodeObject(int _xPos, int _yPos, Panel _pn, frmSimulator _frm)
        {
            this.xPos = _xPos;
            this.yPos = _yPos;
            this.pnContain = _pn;
            this.frmContain = _frm;

            Loading();
        }

        private void Loading()
        {
            //Init Value
            item = default;
            index = 0;
            timeSleep = 0;
            cursor_InArea = false;


            timer = new Timer();
            timer.Interval = 100;
            timer.Start();

            nodePropertie();
            linkPrevPropertie();
            linkNextPropertie();
            AreaPropertie();

            //Event Active
            timer.Tick += timer_Tick;
            ico_Node.MouseClick += Node_Click;
            ico_LinkNext.MouseEnter += Mouse_Enter;
            ico_LinkNext.MouseLeave += Mouse_Leave;
            pnButton.MouseEnter += Mouse_Enter;
            pnButton.MouseLeave += Mouse_Leave;
            btnAdd.MouseEnter += Mouse_Enter;
            btnAdd.MouseLeave += Mouse_Leave;
            btnAdd.Click += ButtonAdd_Click;

        }


        //----------PROPERTIES
        void nodePropertie()
        {
            ico_Node = new PictureBox()
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(100, 100),
                Location = new Point(xPos, yPos),
                Image = Resources.icon_nodeLightBlue,
            };
        }

        void linkPrevPropertie()
        {
            ico_LinkPrev = new PictureBox()
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(50, 50),
                Location = new Point(xPos - 55, yPos + 50),
                Image = Resources.icon_nodePrev
            };
        }

        void linkNextPropertie()
        {
            ico_LinkNext = new PictureBox()
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(50, 50),
                Location = new Point(xPos + 105, yPos),
                Image = Resources.icon_nodeNext
            };
        }

        void AreaPropertie()
        {
            btnAdd = new BunifuIconButton()
            {
                Visible = false,
                BackgroundColor = Color.White,
                BorderColor = Color.White,
                Location = new Point(20, 10),
                Image = Resources.icon_add
            };

            pnButton = new Panel()
            {
                Size = new Size(80, 100),
                Location = new Point(xPos + 100, yPos - 60),
            };

            pnButton.Controls.Add(btnAdd);
        }


        public void Show()
        {
            {
                pnContain.Controls.Add(pnButton);
                pnContain.Controls.Add(ico_Node); ico_Node.BringToFront();
                pnContain.Controls.Add(ico_LinkPrev); ico_LinkPrev.BringToFront();
                pnContain.Controls.Add(ico_LinkNext); ico_LinkNext.BringToFront();
            }
        }

        public void SetPos(Point newPos)
        {
            xPos = newPos.X;
            ico_Node.Location = newPos;
            ico_LinkPrev.Location = newPos;
            ico_LinkNext.Location = newPos;
            pnButton.Location = newPos;
            btnAdd.Location = newPos;
        }

        public void ChangePos(int offset)
        {
            xPos += offset;
            ico_Node.Location = new Point(ico_Node.Location.X + offset, ico_Node.Location.Y);
            ico_LinkPrev.Location = new Point(ico_LinkPrev.Location.X + offset, ico_LinkPrev.Location.Y);
            ico_LinkNext.Location = new Point(ico_LinkNext.Location.X + offset, ico_LinkNext.Location.Y);
            pnButton.Location = new Point(pnButton.Location.X + offset, pnButton.Location.Y);
            btnAdd.Location = new Point();
        }

        //----------EVENTS
        private void timer_Tick(object sender, EventArgs e)
        {
            //TODO: Time Tick logic here.
            if (cursor_InArea == false && btnAdd.Visible == true)
            {
                timeSleep++;
                if (timeSleep == 2)
                {
                    btnAdd.Visible = false;
                    timeSleep = 0;
                }
            }
        }

        private void Node_Click(object sender, EventArgs e)
        {
            //TODO: Node Click logic here.
            frmContain.GetItem(this.index);
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            //TODO: Button Add logic here.
            frmContain.Add(this.index + 1);
        }

        private void Mouse_Enter(object sender, EventArgs e)
        {
            btnAdd.Visible = true;
            cursor_InArea = true;
        }

        private void Mouse_Leave(object sender, EventArgs e)
        {
            cursor_InArea = false;
        }

    }
}
