using System;

namespace node
{
    class Program
    {
        class Node
        {
            public int Data;
            public Node Next;
            public Node(int data, Node next = null)
            {
                Data = data;
                Next = next;
            }
        }

        class LinkList
        {
            public Node Init;
            public LinkList(Node first = null) 
            {
                Init = first;
                // Last
            }

            public void AddNodeAtHead(Node nd)
            {
                /*if (Init == null)
                    Init = nd;
               */
                nd.Next = Init;
                Init = nd;
            }

            public void AddNodeAtEnd(Node nd)
            {
                if (Init == null)
                {
                    Init = nd;
                }
                else
                {
                    Node current = Init;
                    while(current.Next != null)
                    {
                        current = current.Next;
                    }
                    current.Next = nd;
                }
            }

            public Node FindNode(int value)
            {
                Node current = Init;
                while (current != null)
                {
                    if (value == current.Data)
                    {
                        return current;
                    }
                    current = current.Next;
                }
                return null;
            }

            public bool Remove(int value)
            {
                if (Init == null) return false;
                if (value == Init.Data)
                {
                    Init = Init.Next;
                    return true;
                }
                Node current = Init;
                while (current.Next != null)
                {
                    if (current.Next.Data == value)
                    {
                        current.Next = current.Next.Next;
                        return true;
                    }
                    current = current.Next;
                }
                return false;

            }


            public string Traverse()
            {
                string ret = "";
                Node current = Init;
                while (current != null)
                {
                    ret += current.Data + ", ";
                    current = current.Next;
                }
                return ret;
            }
        }

        static void Main(string[] args)
        {
            LinkList list = new LinkList();
            list.AddNodeAtEnd(new Node(1));
            list.AddNodeAtEnd(new Node(2));
            list.AddNodeAtEnd(new Node(3));

            Console.WriteLine(list.Remove(4));
            Console.WriteLine(list.Traverse());
            Console.WriteLine(list.Remove(2));
            Console.WriteLine(list.Traverse());
            Console.WriteLine(list.Remove(1));
            Console.WriteLine(list.Traverse());

            Console.WriteLine(list.Remove(3));
            Console.WriteLine(list.Traverse());
            Console.WriteLine(list.Remove(1));
            Console.WriteLine(list.Traverse());
            Console.WriteLine(list.Remove(1));
            Console.WriteLine(list.Traverse());
        }
    }
}
