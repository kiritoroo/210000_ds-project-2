using System;
using FinalProjects.Structures.Interfaces;

namespace FinalProjects.Structures
{
    public class LinkedList<E> : ILinkedList<E>
    {
        private int size { get; set; } //kích thước 
        private Node<E> first { get; set; } //Node đầu tiên của list
        private Node<E> last { get; set; } //Node cuối cùng của list


        //Khởi tạo
        /* Kích thước = 0
         * Node đầu rỗng
         * Node đầu rỗng
         */
        public LinkedList()
        {
            size = 0;
            first = null;
            last = null;
        }

        //-------------------------------------------------- PRIVATE
        //Kiểm tra list rỗng
        private bool IsEmpty()
        {
            //Trả về true nếu {Node đầu rỗng, Node cuối rỗng và size = 0}
            return first == null && last == null && size == 0;
        }

        //Kiểm tra index theo giá trị (index có thể từ 0 đến size - 1)
        private bool IsElementIndex(int index)
        {
            //Trả về true nếu index thuộc {0, size - 1} 
            return index >= 0 && index < size;
        }

        //Kiểm tra index theo vị trí (index có thể từ 0 đến size)
        private bool IsPositionIndex(int index)
        {
            //Trả về true nếu index thuộc {0, size}
            return index >= 0 && index <= size;
        }

        //Trả về string (Exception)
        private string OutOfBoundsMsg(int index)
        {
            return $"[Out of Bound] Index: {index}, Size: {size}";
        }

        //Hàm kiểm tra index theo giá trị
        private void CheckElementIndex(int index)
        {
            if (!IsElementIndex(index)) //Ném ngoại lệ nếu index không hợp lệ
                throw new Exception(OutOfBoundsMsg(index));
        }

        //Hàm kiểm tra index theo vị trí
        private void CheckPositionIndex(int index)
        {
            if (!IsPositionIndex(index)) //Ném ngoại lệ nếu index không hợp lệ
                throw new Exception(OutOfBoundsMsg(index));
        }

        //TODO: Link
        //Liên kết |Node mới| với node succ
        /* Chèn |Node mới|, thay thế vị trí Node succ, Node succ lùi về sau + 1
         * ?---------- Case 0: Node _prev của succ = null
         * Begin: firt = null <- succ <-> _next <-> ...
         * Final: firt = null <- |Node mới| <-> succ <-> _next <-> ...
         * Task:
         *  + Tạo Node mới có item = e, Node prev = null, Node next = succ
         *  + Thay Node prev của succ = Node mới
         *  + Gán Node first = Node mới
         * 
         * ?---------- Case 1: other
         * Begin: ... <-> _prev <-> succ <-> _next <-> ...
         * Final: ... <-> _prev <-> |Node mới| <-> succ <-> _next <-> ... 
         * Task:
         *  + Tạo Node mới có item = e, Node prev = Node _prev của Node succ, Node next = succ 
         *  + Thay Node prev của succ = Node mới
         *  + Thay Node next của Node _prev = Node mới
         */
        private void Link(E e, Node<E> succ)
        {
            Node<E> x = succ.GetNodePrev();
            Node<E> newNode = new Node<E>(x, e, succ);
            succ.SetNodePrev(newNode);
            if (x == null)
                first = newNode;
            else
                x.SetNodeNext(newNode);
            size++;
        }

        //Liên kết |Node mới| vào đầu
        /*
         * ?---------- Case 0: Node first = null (list rỗng)
         * Begin: first = null = last
         * Final: first = null <- |Node mới| -> null = last
         * Task:
         *  + Tạo Node mới có item = e, Node prev = null, Node next = null
         *  + Gán Node first = Node mới
         *  + Gán Node last = Node mới
         *  
         * ?---------- Case 1: other
         * Begin: first = null <- _node <-> next <-> ...
         * Final: first = null <- |Node mới| <-> _node <-> next <-> ...
         * Task:
         *  + Tạo Node mới có item = e, Node prev = null, Node next = _node
         *  + Gán Node first = Node mới
         *  + Gán Node prev của _node = Node mới
         */
        private void LinkFirst(E e)
        {
            Node<E> x = first;
            Node<E> newNode = new Node<E>(null, e, x);
            first = newNode;
            if (x == null)
                last = newNode;
            else
                x.SetNodePrev(newNode);
            size++;
        }

        //Liên kết |Node mới| vào cuối
        /*
         * ?---------- Case 0: Node last = null (list rỗng)
         * Begin: first = null = last
         * Final: first = null <- |Node mới| -> null = last
         * Task:
         *  + Tạo Node mới có item = e, Node prev = null, Node next = null
         *  + Gán Node last = Node mới
         *  + Gán Node first = Node mới
         *  
         * ?---------- Case 1: other
         * Begin: ... prev <-> _node -> null = last
         * Final: ... prev <-> _node <-> |Node mới| -> null = last
         * Task:
         *  + Tạo Node mới có item = e, Node prev = _node, Node next = null
         *  + Gán Node last = Node mới
         *  + Gán Node next của _node = Node mới 
         */
        private void LinkLast(E e)
        {
            Node<E> x = last;
            Node<E> newNode = new Node<E>(x, e, null);
            last = newNode;
            if (x == null)
                first = newNode;
            else
                x.SetNodeNext(newNode);
            size++;
        }

        //TODO: UnLink
        //Bỏ Liên kết Node x
        /*
         * 1. Bỏ liên kết Node prev
         * ?---------- Case 0: Node prev của x rỗng => x là Node đầu
         * Begin: first = null <- x <-> _next <-> ...
         * Final: first =  null <- _next <-> ... 
         * Task:
         *  + Gán Node first = _next
         * ?---------- Case 1: Node prev của x khác rỗng
         * Begin: ... <-> _prev <-> x <-> _next <-> ...
         * Final: ... <-> _prev <-> _next <-> ...
         * Task:
         *  + Gán Node next của _prev = _next
         *  + Gán Node prev của x = null
         *  
         *  2. Bỏ liên kết Node next
         * ?---------- Case 0: Node next của x rỗng => x là Node cuối
         * Begin: ... <-> _prev <-> x -> null = last
         * Final: --- <-> _prev -> null = last
         * Task: 
         *  + Gán Node last = _prev
         * ?---------- Case 1: Node next của x khác rỗng
         * Begin: ... <-> _prev <-> x <-> _next <-> ...
         * Final: ... <-> _prev <-> _next <-> ...
         * Task:
         *  + Gán Node prev của _next = _prev
         *  + Gán Node next của x = null
         */
        private void Unlink(Node<E> x)
        {
            Node<E> _next = x.GetNodeNext();
            Node<E> _prev = x.GetNodePrev();

            if (_prev == null)
            {
                first = _next;
            }
            else
            {
                _prev.SetNodeNext(_next);
                x.SetNodePrev(null);
            }

            if (_next == null)
            {
                last = _prev;
            }
            else
            {
                _next.SetNodePrev(_prev);
                x.SetNodeNext(null);
            }

            x.SetItem(default);
            size--;
        }

        //Bỏ liên kết Node đầu tiên
        /*
         * ?---------- Case 0: List chỉ có 1 Node
         * Begin: first = null <- f -> null = last
         * Final: first = null = last
         * Task:
         *  + Gán Node last = null
         *  + Gán Node first = null
         * ?---------- Case 1: Other
         * Begin: first = null <- f <-> _next <-> ...
         * Final: first = null <- _next <-> ...
         * Task:
         *  + Gán Node next của f = null
         *  + Gán Node prev của _next = null
         *  + Gán Node first = _next
         */
        private void UnlinkFirst()
        {
            Node<E> f = first;
            Node<E> next = f.GetNodeNext();
            f.SetItem(default);
            first = next;
            if (next == null)
                last = null;
            else
            {
                f.SetNodeNext(null);
                next.SetNodePrev(null);
            }
            size--;
        }

        //Bỏ liên kết Node cuối cùng
        /*
         * ?---------- Case 0: List chỉ có 1 Node
         * Begin: first = null <- l -> null = last
         * Final: first = null = last
         * Task:
         *  + Gán Node first = null
         *  + Gán Node last = null
         * ?---------- Case 1: Other
         * Begin: ... <-> _prev <-> l -> null = last
         * Final: ... <-> _prev -> null = last
         * Task:
         *  + Gán Node prev của l = null
         *  + Gán Node next của _prev = null
         *  + Gán Node last = _prev
         */
        private void UnlinkLast()
        {
            Node<E> l = last;
            Node<E> prev = l.GetNodePrev();
            l.SetItem(default);
            last = prev;
            if (prev == null)
                first = null;
            else
            {
                l.SetNodePrev(null);
                prev.SetNodeNext(null);
            }
            size--;
        }

        //-------------------------------------------------- PUBLIC

        //TODO: Get
        //Lấy Node tại vị trí index
        public Node<E> Node(int index)
        {
            Node<E> x = first;
            for (int i = 0; i < index; i++)
                x = x.GetNodeNext();
            return x;
        }
        public int GetSize()
        {
            return this.size;
        }

        //Lấy item của Node tại vị trí index
        public E Get(int index)
        {
            CheckElementIndex(index);

            return Node(index).GetItem();
        }

        //Lấy item của Node đầu tiên
        public E GetFirst()
        {
            if (first == null)
                return default;
            return first.GetItem();
        }

        //Lấy item của Node cuối cùng
        public E GetLast()
        {
            if (last == null)
                return default;
            return last.GetItem();
        }


        //TODO: Set
        //Gán giá trị mới cho Node tại vị trí index
        public void Set(int index, E element)
        {
            CheckElementIndex(index);

            Node(index).SetItem(element);
        }

        //Gán giá trị mới cho Node đầu tiên
        public void SetFirst(E element)
        {
            if (first == null)
                return;

            first.SetItem(element);
        }

        //Gán giá trị mới cho Node cuối cùng
        public void SetLast(E element)
        {
            if (last == null)
                return;

            last.SetItem(element);
        }


        //TODO: Add
        //Hàm thêm Node tại vị trí bất kì
        /*
         * Kiểm tra vị trí chèn hợp lệ (0 -> size)
         * + Case 0: index = size
         * + Case 1: other
         */
        public void Add(int index, E element)
        {
            CheckPositionIndex(index);

            if (index == size)
                LinkLast(element);
            else
                Link(element, Node(index));
        }

        //Hàm thêm Node tại vị trí đầu tiên
        public void AddFirst(E element)
        {
            LinkFirst(element);
        }

        //Hàm thêm Node tại vị trí cuối cùng
        public void AddLast(E element)
        {
            LinkLast(element);
        }


        //TODO: Remove
        public void Remove(int index)
        {
            CheckElementIndex(index);
            Unlink(Node(index));
        }

        public void RemoveFirst()
        {
            if (first != null)
                UnlinkFirst();
        }

        public void RemoveLast()
        {
            if (last != null)
                UnlinkLast();
        }

        //Tìm Item theo index
        public E FindItem(int index)
        {
            CheckElementIndex(index);

            return Node(index).GetItem();
        }

        //Tìm index theo Item
        public int FindIndex(E item)
        {
            Node<E> x = first;
            for (int i = 0; i < size; i++)
            {
                if (x.GetItem().Equals(item))
                    return i;
                x = x.GetNodeNext();
            }
            return default;
        }

        //TODO: Clear
        public void Clear()
        {
            Node<E> x = first;
            while (x != null)
            {
                Node<E> next = x.GetNodeNext();
                x.SetItem(default(E));
                x.SetNodeNext(null);
                x.SetNodePrev(null);
                x = next;
            }
            first = last = null;
            size = 0;
        }
    }
}

