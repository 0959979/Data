using System;
using System.Threading;

namespace DiningDeadlockSol
{
    class Fork
    {

    }
    class Philosopher
    {
        public Fork rightFork { get; set; }
        public Fork leftFork { get; set; }
        public int maxTime = 100;
        public int number;
        public Philosopher(int n)
        {
            number = n;
        }
        public void eat()
        {

            Console.WriteLine("[{0} waiting for the right fork ...]", number);
            lock (rightFork)
            {
                Console.WriteLine("[{0} waiting for the left fork ...]", number);
                lock (leftFork)
                {
                    Console.WriteLine("[{0} started eating ...]", number);
                    Thread.Sleep(new Random().Next(10, maxTime));
                    Console.WriteLine("[{0} finished eating ...]", number);
                }
            }
        }

        public void startEating(Object it)
        {
            Thread.Sleep(new Random().Next(10, maxTime));
            int iterations = (int)it;
            for (int i = 0; i < iterations; i++)
            {
                this.eat();
            }
        }

    }
    class Table
    {
        public Fork[] forks;
        public Philosopher[] philosophers;
        public Thread[] threads;
        public Table(int num)
        {
            forks = new Fork[num];
            philosophers = new Philosopher[num];
            threads = new Thread[num];

            for (int i = 0; i < num; i++)
            {
                forks[i] = new Fork();
                philosophers[i] = new Philosopher(i);
            }
            int rightIndex = 0, leftIndex = 0;
            for (int i = 0; i < num; i++)
            {
                rightIndex = (i) % (num);
                leftIndex = ((i + num - 1) % (num));
                philosophers[i].rightFork = forks[rightIndex];
                Console.WriteLine("[Philosopher {0}] got right fork {1}",i, rightIndex);
                philosophers[i].leftFork = forks[leftIndex];
                Console.WriteLine("[Philosopher {0}] got left fork {1}", i, leftIndex);
            }
        }
        public void startConcurrentDining(int it)
        {
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(philosophers[i].startEating);
            }
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start(it);
            }
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
        }
    }
}
