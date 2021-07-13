using System;
using System.Threading;

namespace SemaphoreTest
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i < 6; i++)
            {
                Reader reader = new Reader(i);
                Console.WriteLine($"Current reader iteration {i}");
            }

            Console.ReadLine();
        }
    }

    class Reader
    {
        static Semaphore sem = new Semaphore(3, 3);
        int count = 3;
        Thread myThread;

        public Reader(int i)
        {
            myThread = new Thread(Read);
            myThread.Name = $"Reader {i.ToString()}";
            myThread.Start();
        }

        public void Read()
        {
            while (count > 0)
            {
                sem.WaitOne();

                Console.WriteLine($"{Thread.CurrentThread.Name} входит в библиотеку");

                Console.WriteLine($"{Thread.CurrentThread.Name} читает");
                Thread.Sleep(5000);

                Console.WriteLine($"{Thread.CurrentThread.Name} покидает библиотеку");

                sem.Release();

                count--;
                Console.WriteLine($"{Thread.CurrentThread.Name} и его каунт = {count}");
                Thread.Sleep(1000);
            }
        }
    }
}
