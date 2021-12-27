using System;
using static System.Console;

namespace Lab4
{
    class Program
    {
        static void Main()
        {
            Random rnd = new Random();
            SLList list = new SLList(rnd.Next(0, 50));

            for (int i = 0; i < 10; i++)
            {
                list.AddLast(rnd.Next(0, 50));
            }
            commands();

            while (true)
            {
                try
                {
                    string enterCommand = ReadLine();
                    switch (enterCommand)
                    {
                        case "/list":
                            commands();
                            break;
                        case "/addlast":
                            Write("Введiть бажаємий елемент: ");
                            list.AddLast(int.Parse(ReadLine()));
                            list.Print();
                            break;
                        case "/addfirst":
                            Write("Введiть бажаємий елемент: ");
                            list.AddFirst(int.Parse(ReadLine()));
                            list.Print();
                            break;
                        case "/dellast":
                            list.DeleteLast();
                            list.Print();
                            break;
                        case "/delfirst":
                            list.DeleteFirst();
                            list.Print();
                            break;
                        case "/addpos":
                            Write("Введiть бажаний елемент: "); int tempVal = int.Parse(ReadLine());
                            Write("Введiть бажану позицiю: "); int tempPos = int.Parse(ReadLine());
                            list.AddToPosition(tempVal, tempPos);
                            list.Print();
                            break;
                        case "/delpos":
                            Write("Введiть бажану позицiю: "); tempPos = int.Parse(ReadLine());
                            list.DeleteFromPosition(tempPos);
                            list.Print();
                            break;
                        case "/findadd":
                            Write("Введiть бажаємий елемент: ");  int vall = int.Parse(ReadLine());
                            list.Findsm(vall);
                            commands();
                            break;
                        case "/print":
                            list.Print();
                            break;
                        case ("/exit"):
                            System.Environment.Exit(1);
                            break;
                        case "/clear":
                            Clear();
                            commands();
                            break;
                        default:
                            WriteLine("Невiрна команда!");
                            break;
                    }
                }
                catch
                {
                    WriteLine("Помилка");
                }
            }
            static void commands()
            {
                WriteLine("/list - Список команд;");
                WriteLine("/addlast - Додати елемент в кiнець списку;");
                WriteLine("/addfirst - Додати елемент в початок списку;");
                WriteLine("/dellast - видалити останiй елемент списку;");
                WriteLine("/delfirst - видалити перший елемент списку;");
                WriteLine("/addpos - додати елемент до обранної позицiї;");
                WriteLine("/delpos - видалити елемент до обранної позицiї;");
                WriteLine("/findadd - знайти найменший елемент та додати пiсля нього новий(задача);");
                WriteLine("/print - Вивести список елементiв");
                WriteLine("/clear - Очищення консолi;");
                WriteLine("/exit - Вихiд.");
            }
        }
    }


    class SLList
    {
        public Node head;
        public int small;
        public class Node
        {
            public int data;
            public Node next;
            public Node(int data)
            {
                this.data = data;
            }
            public Node(int data, Node next)
            {
                this.data = data;
                this.next = next;
            }
        }
        public SLList(int data)
        {
            head = new Node(data);
        }

        public void AddFirst(int data)
        {
            Node current = new Node(data);
            current.next = head;
            head = current;
        }
        public void AddToPosition(int data, int position)
        {
            Node current = head;
            for (int i = 0; i < position - 3; i++)
            {
                current = current.next;
            }
            Node addedNode = new Node(data);
            addedNode.next = current.next.next;
            current.next.next = addedNode;

        }
        public void AddLast(int data)
        {
            Node current = head;
            while (current.next != null)
            {
                current = current.next;
            }
            current.next = new Node(data);
        }
        public void DeleteFirst()
        {
            Node current = head;
            head = head.next;
        }
        public void DeleteFromPosition(int position)
        {
            Node current = head;
            int indexCounter = 0;
            Node TempNode = current;
            Node PreviousNode = null;
            while (TempNode.next != null)
            {
                if (indexCounter == position - 1)
                {
                    PreviousNode.next = TempNode.next;
                    break;
                }
                indexCounter++;
                PreviousNode = TempNode;
                TempNode = TempNode.next;
            }
        }
        public int DeleteLast()
        {
            Node current = head;
            while (current != null)
            {
                var curNode = head;

                while (current.next?.next != null)
                {
                    current = current.next;
                }

                var lastNodeValue = current.next?.data ?? -1;
                current.next = null;
                
                return lastNodeValue;
            }
            return -1;
        }
        
        public void Findsm(int data)
        {
            Node current = head.next;
            small = current.data;
            int min = 0;
            
            for (int i = 0; current.next != null; i++)
            {
                if (current.next.data < small)
                {
                    current = current.next;
                    small = current.data;
                    min = i + 4;
                }
                else
                {
                    current = current.next;
                }
                
            }
            AddToPosition(data, min);
            WriteLine(small);
            WriteLine();
            Print();
            WriteLine();

        }
        public void Print()
        {
            while (head == null)
            {
                WriteLine("Список порожній");
            }
            Node current = head;
            while (current != null)
            {
                WriteLine(current.data);
                current = current.next;
            }
        }


    }
}

