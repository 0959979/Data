using System;
using System.Threading;
using System.Threading.Tasks;

// todo: This example is taken from https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/
// Read the whole document with more detail explanation.
namespace BreakfastExample
{
    public class BreakfastItem
    {
        public BreakfastItem()
        {
            int time = new Random().Next(1000,5000);
            Console.WriteLine("*** Started preparing {0}", this.GetType().ToString());
            Thread.Sleep(time); // Time needed to do the task.
            Console.WriteLine("+++ {0} is ready ... ", this.ToString());
        }
    }
    public class Coffee : BreakfastItem{   public Coffee() : base() { } }
    public class Toast : BreakfastItem { public Toast() : base() { } }
    public class Juice : BreakfastItem { public Juice() : base() { } }
    public class Bacon : BreakfastItem { public Bacon() : base() { } }
    public class Breakfast
    {
        public void Prepare()
        {
            Console.WriteLine("[Synchronous] We are going to prepare the breakfast ...");
            Coffee c = new Coffee();
            Toast t = new Toast();
            Juice j = new Juice();
            Bacon b = new Bacon();
            Console.WriteLine("[Synchronous] The breakfast is ready ...");
        }

        public async Task PrepareAsync()
        {
            Console.WriteLine("[Asynchronous] We are going to prepare the breakfast ...");

            Task prepareCoffee = new Task(()=> new Coffee());
            Task prepareToast = new Task(()=> new Toast());
            Task prepareJuice = new Task(()=> new Juice());
            Task prepareBacon = new Task(()=> new Bacon());

            prepareCoffee.Start();
            prepareToast.Start();
            prepareJuice.Start();
            prepareBacon.Start();

            await prepareCoffee;
            await prepareJuice;
            await prepareToast;
            await prepareBacon;

            Console.WriteLine("[Asynchronous] The breakfast is ready ...");
        }
    }
}