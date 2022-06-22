using System;
using static System.Console;

class Program
{
    Random rnd = new Random();
    static void Main()
    {
        Queue<string> a = new Queue<string>(5);
        WriteLine("Введiть рядок для його запису в чергу: ");
        WriteLine("Введiть out для виводу першого i останього елементу черги: ");
        WriteLine("Введiть quit для кiнця програми: ");
        WriteLine();

        while (true)
        {
            string line = ReadLine();
         
            if (line == "quit") 
            {
                break; 
            }

            if (line == "out")
            {
                WriteLine("Перший елемент черги: " + a.GetElement(a.Head()) + "\nОстаннiй елемент черги: " + a.GetElement((a.Tail())));
                Write("\nВведiть наступний рядок: ");
            }
            else
            {
                if (a.Enqueue(line))
                {
                    WriteLine("Довжну черги подвоєно. Поточна довжина: " + a.Length());
                }
                Print_Queue(a);
            }
        }
    }
    static void Print_Queue(Queue<string> a)
    {
        Write("Черга: ");
        for (int i = a.Head(); i <= a.Tail(); i++)
        {
            if (i == a.Tail()) { Write(a.GetElement(i)); }
            else { Write(a.GetElement(i) + ",  "); }
        }
        Write("\nВведiть наступний рядок: ");
    }

}
public class Queue<T>
{
    private int length;
    private int count;
    private T[] items;
    private int head;
    private int tail;
    public Queue(int Length)
    {
        length = Length;
        items = new T[Length];
        head = 0;
        tail = 0;
        count = 0;
    }
    public int Head() => head;
    public int Length() => length;
    public int Tail() => tail;
    public int Count() => count;
    public bool IsEmpty() => count == 0;
    public bool IsFull() => count == length;
    public T GetElement(int index) => items[index % length];
    public bool Enqueue(T elem)
    {
        bool expand = false;

        if (IsFull()) 
        { 
            Expand(); expand = true; 
        }
        if (IsEmpty()) 
        {
            items[tail] = elem; count++; return expand; 
        }

        tail = (tail + 1) % length;
        items[tail] = elem;
        count++;

        return expand;
    }
    public void Dequeue()
    {
        if (IsEmpty()) 
            throw new InvalidOperationException("Queue exhausted");
        else
        {
            items[head] = default(T);
            head = (head + 1) % length;
            count--;
        }
    }
    public void Expand()
    {
        length *= 2;
        T[] expanded = new T[length];
        for (int i = 0; i < length / 2; i++)
        {
            expanded[i] = items[(head + i) % (length / 2)];
        }
        head = 0;
        tail = length / 2 - 1;
        items = expanded;
    }
}