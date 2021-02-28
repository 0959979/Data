using System;
using System.Threading.Tasks;
using System.Threading;

namespace TaskExamples
{
    class Operations
    {
        // This is an example of mostly IO-bound operation
        public static void PrintConsole(int iteration, int wait_time)
        {
            for (int i = 0; i < iteration; i++)
            {
                Console.WriteLine("Aynch. prints {0} ", i);
                Thread.Sleep(wait_time);
            }
        }

        // This is an example of mixed IO-bound and CPU-bound operation 
        public static int FindPrimes(int m, int M)
        {
            Boolean isPrime = true;
            int count = 0;

            if (m > M)
            {
                Console.WriteLine("invalid inputs");
                return -1; // invalid parameters
            }

            for (int n = m; n <= M; n++)
            {
                if (n % 1000 == 0) // This condition fakes IO operations.
                    Thread.Sleep(200);

                for (int i = 2; i < n && isPrime; i++)
                    if (n % i == 0)
                    {
                        isPrime = false;
                        break;
                    }

                if (!isPrime)
                {
                    isPrime = true;
                    Console.Write("{0} is a prime",n);
                    count++;
                }
            }
            return count;
        }
    }
    class SynchronousTasks
    {
        // todo: check how different instances of Task can be executed 
        public void RunSomeTasks()
        {
            Task printingTask = Task.Run(() => { Console.WriteLine("\n A separate task is printing this ..."); });

            Task<string> taskWithStringResult = Task.Run(() => { return "\n Hello World from a task."; });
            Console.WriteLine("\n task with string result returns {0} ", taskWithStringResult.Result);

            Task<int> timeConsumingTask = Task.Run(() =>
            {
                // Assume a very heavy calculation is happening here ...
                int r = new Random().Next();
                Thread.Sleep(3000); // wait for a few seconds ...
                return r;
            });
            Console.WriteLine("\n task with a heavy calculation returns {0}", timeConsumingTask.Result);

        }

        // todo: check how a function can return a task with an int result
        public Task<int> GetNewId()
        {
            Task<int> newIdTask = new Task<int>(() => new Random().Next());
            return newIdTask;
        }

        // todo: check how a defined task is starting.
        public void PrintNewId()
        {
            var idTask = this.GetNewId();
            idTask.Start();
            // todo: check how the result of the task is used
            // Below the task will be called. The operation will block until the result is available.
            Console.WriteLine("[Tasks] New id is {0}",idTask.Result); 
        }
    }

    class ConcurrentTasks
    {
        int wait_time = 20, iterations = 1000, min_prime = 1, max_parime = 50000;

        // todo: Why this is an inefficient design of asynchronous?
        public async Task<int> InvokeAnInefficientAsyncTask()
        {
            int c = 0;
            Console.WriteLine(" A normal task is going to start ... ");
            Console.WriteLine(" Now an Async task is going to be called ...");
            Task printTask = new Task(() => Operations.PrintConsole(iterations, wait_time));
            printTask.Start();
            await printTask;
            c = Operations.FindPrimes(min_prime, max_parime);
            Console.WriteLine(" All the tasks are ready here ...");
            return c;
        }


        public async Task<int> InvokeAnEfficientAsyncTask()
        {
            int c = 0;
            Console.WriteLine(" A normal task is going to start ... ");
            Console.WriteLine(" Now an Async task is going to be called ...");
            Task printTask = new Task(()=>Operations.PrintConsole(iterations,wait_time));
            printTask.Start();
            c = Operations.FindPrimes(min_prime, max_parime);
            // todo: what will be the result if we do not await for printTask? Comment this line and check the output.
            await printTask;
            Console.WriteLine(" All the tasks are ready here ...");
            return c;
        }

        public int InvokeMultithreadedTasks()
        {
            int c = 0;
            Thread printingThread = new Thread(() => Operations.PrintConsole(iterations, wait_time));
            // todo: check how we get return value of a thread
            Thread primeNumThread = new Thread(()=> { c = Operations.FindPrimes(min_prime, max_parime); });

            printingThread.Start();
            primeNumThread.Start();

            printingThread.Join();
            primeNumThread.Join();

            return c;
        }
    }

}