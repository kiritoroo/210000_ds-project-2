using System;
using System.Drawing;
using System.Windows.Forms;
using FinalProjects.Structures;
using FinalProjects.Objects;
using Bunifu.UI.WinForms.BunifuButton;
using FinalProjects.Properties;

namespace FinalProjects.Handler
{
    public class NodeObjectHandler<E>
    {
        //Controls
        Panel pnContain;
        frmSimulator frmContain = new frmSimulator();
        public LinkedList<NodeObject<E>> nodeObjList = new LinkedList<NodeObject<E>>();
        public int size;

        //Components
        BunifuIconButton btnAdd_First;
        BunifuIconButton btnAdd_Last;
        BunifuIconButton btnCreat_New;

        //Coordinate
        private const int xPOS_CONST = 100;
        private const int yPOS_CONST = 100;
        private const int distance = 180;
        private int xPOS;
        private int yPOS;
        private const int waitTime_CONST = 5;

        //Event
        private Timer timer;

        //Check Variables
        int timeSleep;
        bool isRemove;
        bool isLinkRemove;
        bool isLinkAdd;
        int currentScrollValue;
        private NodeObject<E> currentNewNodeObj;
        private Node<NodeObject<E>> currentNewNode;
        public NodeObject<E> currentNodeObj;
        public Node<NodeObject<E>> currentNode;

        public NodeObjectHandler(Panel _pn, frmSimulator _frm)
        {
            this.pnContain = _pn;
            this.frmContain = _frm;
            Loading();
        }

        private void Loading()
        {
            //Init Value
            xPOS = xPOS_CONST;
            yPOS = yPOS_CONST;
            size = 0;
            isRemove = false;
            isLinkRemove = false;
            isLinkAdd = false;
            buttonPropertie();
            pnContain.Controls.Add(btnCreat_New);

            timer = new Timer();
            timer.Interval = 100;
            timer.Start();

            //Event Active
            timer.Tick += timer_Tick;
            btnAdd_First.Click += AddFirst_Click;
            btnAdd_Last.Click += AddLast_Click;
            btnCreat_New.Click += Create_Click;
        }

        //----------PROPERTIES
        private void buttonPropertie()
        {
            btnAdd_First = new BunifuIconButton()
            {
                Image = Resources.icon_add,               
                BorderColor = Color.White,
                BackgroundColor = Color.White,
            };

            btnAdd_Last = new BunifuIconButton()
            {
                Image = Resources.icon_add,
                BorderColor = Color.White,
                BackgroundColor = Color.White
            };

            btnCreat_New = new BunifuIconButton()
            {
                Size = new Size(50, 50),
                Location = new Point(420, 130),
                Image = Resources.icon_add,
                BorderColor = Color.LawnGreen,
                BackgroundColor = Color.White
            };
        }

        //----------CONTROLS AREA
        public E GetItem(int index)
        {
            if (isRemove == true || isLinkAdd == true || isLinkRemove == true)
            {
                MessageBox.Show("Đợi xíu...");
            }
             
            E item = nodeObjList.Get(index).item;
            setAllBlue();
            currentNodeObj = nodeObjList.Get(index);
            currentNode = nodeObjList.Node(index);

            nodeObjList.Get(index).ico_Node.Image = Resources.icon_nodeBlue;

            return item;
        }
        public void GetNodePrev()
        {
            if (currentNodeObj == null || currentNode == null)
                return;
            if (currentNode.GetNodePrev() == null)
                return;

            currentNode = currentNode.GetNodePrev();
            currentNodeObj = currentNode.GetItem();
            setAllBlue();
            currentNodeObj.ico_Node.Image = Resources.icon_nodeBlue;
            frmContain.GetItem(currentNodeObj.index);
        }

        public void GetNodeNext()
        {
            if (currentNodeObj == null || currentNode == null)
                return;
            if (currentNode.GetNodeNext() == null)
                return;

            currentNode = currentNode.GetNodeNext();
            currentNodeObj = currentNode.GetItem();
            setAllBlue();
            currentNodeObj.ico_Node.Image = Resources.icon_nodeBlue;
            frmContain.GetItem(currentNodeObj.index);
        }

        //Thêm tại vị trí click 
        public void Add(E item, int index)
        {
            if (isLinkAdd == true)
            {
                MessageBox.Show("Đợi xíu...");
            }

            LockPanel();
            setAllBlue();
            currentNode = null;
            currentNodeObj = null;

            xPOS = nodeObjList.Node(index - 1).GetItem().xPos + distance; //tọa độ của node trước đó + khoảng cách

            NodeObject<E> newObj = new NodeObject<E>(xPOS, yPOS, pnContain, frmContain);
            newObj.ico_Node.Image = Resources.icon_nodeGreen;
            newObj.item = item;

            size++;

            nodeObjList.Add(index, newObj);
            currentNewNode = nodeObjList.Node(index);
            currentNewNodeObj = newObj;

            //fix position all
            for (int i = index + 1; i < size; i++)
            {
                nodeObjList.Get(i).ChangePos(distance);
            }

            _Update();

            //Cuộn chuột tại điểm add
            pnContain.HorizontalScroll.Value = currentScrollValue;

            //Ẩn link để làm animation link
            currentNewNodeObj.ico_LinkPrev.Visible = false;
            currentNewNodeObj.ico_LinkNext.Visible = false;
            if (currentNewNode.GetNodePrev() != null)
                currentNewNode.GetNodePrev().GetItem().ico_LinkNext.Visible = false;
            if (currentNewNode.GetNodeNext() != null)
                currentNewNode.GetNodeNext().GetItem().ico_LinkPrev.Visible = false;
            isLinkAdd = true;
        }

        //Thêm đuôi
        public void AddFirst(E item)
        {
            LockPanel();
            setAllBlue();
            currentNode = null;
            currentNodeObj = null;

            xPOS = xPOS_CONST;

            NodeObject<E> newObj = new NodeObject<E>(xPOS, yPOS, pnContain, frmContain);
            newObj.ico_Node.Image = Resources.icon_nodeGreen;
            newObj.item = item;

            size++;

            nodeObjList.AddFirst(newObj);
            currentNewNode = nodeObjList.Node(0);
            currentNewNodeObj = newObj;

            //fix position all
            for (int i = 1; i < size; i++)
            {
                nodeObjList.Get(i).ChangePos(distance);
            }

            _Update();

            //Ẩn link để làm animation link
            currentNewNodeObj.ico_LinkPrev.Visible = false;
            currentNewNodeObj.ico_LinkNext.Visible = false;
            if (currentNewNode.GetNodePrev() != null)
                currentNewNode.GetNodePrev().GetItem().ico_LinkNext.Visible = false;
            if (currentNewNode.GetNodeNext() != null)
                currentNewNode.GetNodeNext().GetItem().ico_LinkPrev.Visible = false;
            isLinkAdd = true;
        }

        //Thêm đầu
        public void AddLast(E item)
        {
            LockPanel();
            setAllBlue();
            currentNode = null;
            currentNodeObj = null;

            if (nodeObjList.GetLast() != null)
                xPOS = nodeObjList.GetLast().xPos + distance;

            NodeObject<E> newObj = new NodeObject<E>(xPOS, yPOS, pnContain, frmContain);
            newObj.ico_Node.Image = Resources.icon_nodeGreen;
            newObj.item = item;

            size++;

            nodeObjList.AddLast(newObj);
            currentNewNode = nodeObjList.Node(size - 1);
            currentNewNodeObj = newObj;


            _Update();

            //Cuộn chuột lên maximum
            pnContain.HorizontalScroll.Value = pnContain.HorizontalScroll.Maximum;

            //Ẩn link để làm animation link
            currentNewNodeObj.ico_LinkPrev.Visible = false;
            currentNewNodeObj.ico_LinkNext.Visible = false;
            if (currentNewNode.GetNodePrev() != null)
                currentNewNode.GetNodePrev().GetItem().ico_LinkNext.Visible = false;
            if (currentNewNode.GetNodeNext() != null)
                currentNewNode.GetNodeNext().GetItem().ico_LinkPrev.Visible = false;
            isLinkAdd = true;
        }

        //Xóa Node tại node được chọn
        public void Remove()
        {
            if (isRemove == true || isLinkRemove == true)
            {
                MessageBox.Show("Đợi xíu...");
            }
            if (currentNode == null || currentNodeObj == null || size == 0)
                return;

            NodeObject<E> xObj = nodeObjList.Get(currentNodeObj.index);
            Node<NodeObject<E>> x = nodeObjList.Node(currentNodeObj.index);

            xObj.ico_Node.Image = Resources.icon_nodeRed;
            xObj.ico_LinkPrev.Visible = false;
            xObj.ico_LinkNext.Visible = false;

            if (x.GetNodePrev() != null)
                x.GetNodePrev().GetItem().ico_LinkNext.Visible = false;
            if (x.GetNodeNext() != null)
                x.GetNodeNext().GetItem().ico_LinkPrev.Visible = false;

            size--;
            isRemove = true;
        }

        //Set item, index tại node được chọn
        public void SetItem(E item)
        {
            if (currentNodeObj == null || currentNode == null || size == 0)
                return;

            currentNodeObj.item = item;
            frmContain.GetItem(currentNodeObj.index);
        }

        //Find Item theo index
        public void FindItem(int index)
        {
            E item = nodeObjList.FindItem(index).item;
            MessageBox.Show($"Item tại vị trí {index}: {item.ToString()}","Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Find index theo Item
        public void FindIndex(E item)
        {
            for (int i = 0; i < nodeObjList.GetSize(); i++)
            {
                if (nodeObjList.Get(i).item.Equals(item))
                {
                    MessageBox.Show($"Item tại vị trí: {i}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            MessageBox.Show("Không tìm thấy", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //Clear List
        public void Clear()
        {
            nodeObjList.Clear();

            currentNewNode = null;
            currentNewNodeObj = null;
            currentNode = null;
            currentNodeObj = null;

            xPOS = xPOS_CONST;
            yPOS = yPOS_CONST;
            size = 0;

            isRemove = false;
            isLinkRemove = false;
            isLinkAdd = false;

            _Update();

            MessageBox.Show("Dọn dẹp thành công !", "Clear", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        //----------HANDLING AREA
        private void LockPanel()
        {
            currentScrollValue = pnContain.HorizontalScroll.Value;
            pnContain.HorizontalScroll.Value = 0;
            pnContain.AutoScroll = false;
        }

        private void _Update()
        {
            //TODO: Update Logic here.
            pnContain.Controls.Clear();
            pnContain.Refresh();
            if (nodeObjList.GetSize() == 0 || size == 0)
            {
                pnContain.Controls.Add(btnCreat_New); //nếu list rỗng tạo button create
                return;
            }

            for (int i = 0; i < size; i++)
            {
                NodeObject<E> nodeObj = nodeObjList.Get(i);
                Node<NodeObject<E>> node = nodeObjList.Node(i);
                
                nodeObj.index = i;
                nodeObj.Show();

                if (node.GetNodePrev() == null)
                {
                    nodeObj.ico_LinkPrev.Visible = false;
                }
                else
                {
                    nodeObj.ico_LinkPrev.Visible = true;
                }

                if (node.GetNodeNext() == null)
                {
                    nodeObj.ico_LinkNext.Visible = false;
                    nodeObj.pnButton.Visible = false;
                }
                else
                {
                    nodeObj.ico_LinkNext.Visible = true;
                    nodeObj.pnButton.Visible = true;
                }
            }

            setPosButton();
            pnContain.Controls.Add(btnAdd_First); btnAdd_First.BringToFront();
            pnContain.Controls.Add(btnAdd_Last); btnAdd_Last.BringToFront();

            pnContain.AutoScroll = true;


        }

        private void _Refresh()
        {
            //TODO: Refresh Logic here.
            pnContain.Controls.Clear();
            pnContain.Refresh();

            for(int i = 0; i < nodeObjList.GetSize(); i++)
            {
                nodeObjList.Get(i).Show();
            }

            setPosButton();
            pnContain.Controls.Add(btnAdd_First);
            pnContain.Controls.Add(btnAdd_Last);

            pnContain.AutoScroll = true;
        }

        private void setAllBlue()
        {
            for (int i = 0; i < nodeObjList.GetSize(); i++)
            {
                nodeObjList.Get(i).ico_Node.Image = Resources.icon_nodeLightBlue;
            }
        }

        private void setPosButton()
        {
            if (nodeObjList.GetFirst() == null || nodeObjList.GetLast() == null)
                return;

            btnAdd_First.Location = new Point(nodeObjList.GetFirst().xPos - 60, nodeObjList.GetLast().yPos + 30);
            btnAdd_Last.Location = new Point(nodeObjList.GetLast().xPos + 120, nodeObjList.GetLast().yPos + 30);
        }


        //----------EVENT AREA
        private void timer_Tick(object sender, EventArgs e)
        {
            //TODO: Time Tick logic here.
            //Remove rùi Link
            if (isRemove == true)
            {
                timeSleep++;
                if (timeSleep == waitTime_CONST)
                {
                    LockPanel();
                    setAllBlue();

                    nodeObjList.Remove(currentNodeObj.index);

                    timeSleep = 0;
                    isRemove = false;
                    isLinkRemove = true;

                    _Update();
                }
            }

            if (isLinkRemove == true)
            {
                timeSleep++;
                if (timeSleep == waitTime_CONST)
                {
                    for (int i = currentNodeObj.index; i < size; i++)
                    {
                        nodeObjList.Get(i).ChangePos(-distance);
                    }

                    pnContain.AutoScroll = false;
                    setPosButton();
                    pnContain.AutoScroll = true;
                    pnContain.HorizontalScroll.Value = currentScrollValue;

                    currentNode = null;
                    currentNodeObj = null;

                    timeSleep = 0;
                    isLinkRemove = false;
                }
            }

            if (isLinkAdd == true)
            {
                timeSleep++;
                if (timeSleep ==waitTime_CONST )
                {
                    if (currentNewNode == null || currentNewNodeObj == null)
                        return;

                    if (currentNewNode.GetNodePrev() != null)
                    {
                        currentNewNode.GetNodePrev().GetItem().ico_LinkNext.Visible = true;
                        currentNewNodeObj.ico_LinkPrev.Visible = true;
                    }
                    if (currentNewNode.GetNodeNext() != null)
                    {
                        currentNewNode.GetNodeNext().GetItem().ico_LinkPrev.Visible = true;
                        currentNewNodeObj.ico_LinkNext.Visible = true;
                    }

                    currentNewNode = null;
                    currentNewNodeObj = null;

                    timeSleep = 0;
                    isLinkAdd = false;
                }
            }
        }

        private void Create_Click(object sender, EventArgs e)
        {
            if (isLinkAdd == true)
            {
                MessageBox.Show("Đợi xíu...");
            }
            frmContain.NewList();
        }

        private void AddFirst_Click(object sender, EventArgs e)
        {
            if (isLinkAdd == true)
            {
                MessageBox.Show("Đợi xíu...");
            }
            frmContain.AddFirst();
        }

        private void AddLast_Click(object sender, EventArgs e)
        {
            if (isLinkAdd == true)
            {
                MessageBox.Show("Đợi xíu...");
            }
            frmContain.AddLast();
        }

    }
}
