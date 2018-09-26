using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    class Program
    {
        static void Main(string[] args)
        {
            const int workers_count = 10;
            Worker[] workers = new Worker[workers_count];
            String[] name_chars = new String[] { "a", "b", "c", "e", "p", "f", "y", "g", "r", "i"};

            for (var i = 0; i < workers_count; i++)
            {
                if (i % 2 == 0)
                {
                    workers[i] = new PartTimeWorker();
                    workers[i].Pay = i * new Random().Next(100, 500);
                }
                else
                {
                    workers[i] = new FullTimeWorker();
                    workers[i].Pay = i * new Random().Next(1000, 5000);
                }
                int name_length = new Random().Next(3, 7);
                for (var j = 0; j < name_length; j++)
                {
                    workers[i].Name += name_chars[new Random().Next(name_chars.Length)];
                    //Sleep for random
                    System.Threading.Thread.Sleep(10);
                }
                int surname_length = new Random().Next(3, 7);
                for (var j = 0; j < surname_length; j++)
                {
                    workers[i].Surname += name_chars[new Random().Next(name_chars.Length)];
                    System.Threading.Thread.Sleep(10);

                }
            }

            foreach (var worker in workers)
            {
                Console.WriteLine($"{worker.Type} {worker.Name} {worker.Surname}: {worker.Calculate()}");
            }
            Console.ReadLine();
        }
    }
}
