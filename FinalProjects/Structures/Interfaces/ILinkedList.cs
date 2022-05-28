

namespace FinalProjects.Structures.Interfaces
{
    public interface ILinkedList<E>
    {
        int GetSize();
        E Get(int index);
        E GetFirst();
        E GetLast();

        void Set(int index, E element);
        void SetFirst(E element);
        void SetLast(E element);

        void Add(int index, E element);
        void AddFirst(E element);
        void AddLast(E element);

        void Remove(int index);
        void RemoveFirst();
        void RemoveLast();

        E FindItem(int index);
        int FindIndex(E element);

        void Clear();
    }
}
