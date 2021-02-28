using System;
using BreakfastExample;
using TaskExamples;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Asynchrounous
{
    class Program
    {
        static void ExampleTasks()
        {
            new SynchronousTasks().PrintNewId();
            new SynchronousTasks().RunSomeTasks();

            Stopwatch sw = new Stopwatch();
            int result = 0;

            ConcurrentTasks ct = new ConcurrentTasks();
            Console.WriteLine("Different ways of concurrent tasks are going to start (ENTER to continue)");
            Console.ReadLine();

            sw.Start();
            result = ct.InvokeAnInefficientAsyncTask().Result;
            sw.Stop();
            Console.WriteLine("Inefficient Async ... {0} elapsed {1} ms (ENTER to continue)", result, sw.ElapsedMilliseconds);
            Console.ReadLine();

            sw.Reset();
            sw.Start();
            result = ct.InvokeAnEfficientAsyncTask().Result;
            sw.Stop();
            Console.WriteLine("Efficient Async ... {0} elapsed {1} ms (ENTER to continue)", result, sw.ElapsedMilliseconds);
            Console.ReadLine();

            sw.Reset();
            sw.Start();
            result = ct.InvokeMultithreadedTasks();
            sw.Stop();
            Console.WriteLine("Multithreaded operations ... {0} elapsed {1} ms (ENTER to continue)", result, sw.ElapsedMilliseconds);

        }
        static void ExampleBreakfast()
        {

            Breakfast bf = new Breakfast();

            bf.Prepare();

            Task tbf = bf.PrepareAsync();
            // todo: what should be here?
            // do something esle
            // todo: Is this a correct invocation? What is the problem? Fix it.
        }
        static void Main(string[] args)
        {
            Program.ExampleTasks();
            Console.WriteLine("\n Next example is about running some tasks to prepare a breakfast (ENTER to continue)");
            Console.ReadLine();
            Program.ExampleBreakfast();
        }

    }
}