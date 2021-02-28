using System;
using System.Threading;
// todo: After running the normal version, uncomment the deadlock version.
using DiningNormal;
//using DiningDeadlock;

namespace DiningPhilosophers
{
    class Program
    {
        static void Main(string[] args)
        {
            int numPhilosphers = 5 , iteration = 1000;
            Table table = new Table(numPhilosphers);
            // todo: run the program with both sequential and concurrent eatings ... see the behaviours
            //table.startSequentialDining(iteration);
            table.startConcurrentDining(iteration);

            Console.WriteLine("[Dinining Philospher] End of the dinner ...");
        }
    }
}
