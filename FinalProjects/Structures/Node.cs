

namespace FinalProjects.Structures
{
    public class Node<E>
    {
        private E item { get; set; }
        private Node<E> next { get; set; }
        private Node<E> prev { get; set; }

        public Node(Node<E> _prev, E _item, Node<E> _next)
        {
            item = _item;
            next = _next;
            prev = _prev;
        }

        public E GetItem()
        {
            return item;
        }

        public Node<E> GetNodeNext()
        {
            return next;
        }

        public Node<E> GetNodePrev()
        {
            return prev;
        }

        public void SetItem(E _item)
        {
            item = _item;
        }

        public void SetNodeNext(Node<E> _next)
        {
            next = _next;
        }

        public void SetNodePrev(Node<E> _prev)
        {
            prev = _prev;
        }
    }
}
