using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Day4Exercises
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Exe1.Start();
            // Exe2.Start();
            // Exe3.Start();
            Console.ReadLine();
        }
    }

    //Exercise1
    public static class Exe1
    {
        public static void Start()
        {
            int[] numbers = new int[11] { 4, 9, 16, 25, 36, 49, 64, 81, 100, 121, 1764 };
            int[] numbers2 = new int[11] { 1, 2, -8, 77, 11, 5, 1, 88, 0, 10, 1764 };

            rootPrinterParallel(numbers);
            //Calling the same method with 'bad' numbers
            rootPrinterParallel(numbers2);

            //Another solution
            RootPrinter2(numbers);
        }
        public static void rootPrinterParallel(int[] numbers)
        {
            try
            {
                var rootNumbers =
                    from number in numbers.AsParallel()
                    select new
                    {
                        number = number,
                        root = Math.Sqrt(number)
                    };

                foreach (var numbEntety in rootNumbers)
                {
                    Console.WriteLine($"rot for {numbEntety.number} is: {numbEntety.root}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("exception: " + e);
            }

        }
        //Another solution
        public static void RootPrinter2(int[] numbers)
        {
            Parallel.ForEach(numbers, number =>
            {
                Console.WriteLine($"{number}, is the root of: {Math.Sqrt(number)}");
            });
        }

    }
    //Exercise2
    public static class Exe2
    {
        public static void Start()
        {
            //Gets a great array of horses
            var horses = getHorses();

            //get the same amount of tasks as the number of horses
            Task[] horseTasks = new Task[horses.Length];
            for (int ii = 0; ii < horses.Length; ii++)
            {
                //Gets each task a uniqe number that stays the same after the for-loop continues
                int runningNumber = ii;
                horseTasks[runningNumber] = Task.Run(() => TheBigRace(horses[runningNumber]));
            }

            //logs the first horse that reaches the goal
            int index = Task.WaitAny(horseTasks);

            //Wait untils all horses reaches the goal
            Task.WaitAll(horseTasks);

            //prints the winner
            Console.WriteLine("And the winner is: ****" + horses[index].Name + "****");
        }
        static void TheBigRace(Horse horse)
        {
            Random random = new Random();
            int running = random.Next(5000, 10000);
            Console.WriteLine("{0} starts!", horse.Name);
            Thread.Sleep(running);
            Console.WriteLine("{0} passes the goal!", horse.Name);
        }

        public static Horse[] getHorses()
        {
            Horse[] horses = new Horse[]
            {
                new Horse{Name="Ludwig"},
                new Horse{Name="Running Thunder"},
                new Horse{Name="Running Flounder"},
                new Horse{Name="Thundering Flounder"},
                new Horse{Name="Floundering Thunder"},
                new Horse{Name="Floundering Runner"},
                new Horse{Name="Thundering Runner"},
            };
            return horses;
        }


    }

    public class Horse
    {
        public string Name { get; set; }
    }

    //Exercise3
    public static class Exe3
    {
        public static void Start()
        {
            var warm = OvenAsync();
            var greaced = GreacingAsync();
            var mixed = MixAsync();

            mixed.Wait();
            var wMixed = WaterMixAsync();

            wMixed.Wait();
            warm.Wait();
            greaced.Wait();
            var waitingForCakeinOven = WaitingForCakeInOvenAsync();

            var icing = PreperingIcingAsync();
            waitingForCakeinOven.Wait();

            icing.Wait();
            var done = LastTouchesAsync();
            done.Wait();

            Console.WriteLine("Cake is good, life is good!");



        }
        //Async methods
        private static async Task LastTouchesAsync()
        {
           // await Task.Run(() => LastTouches());
            await Task.Run(() => GenericBake("Aplying Icing to the cake", 3000));
            Console.WriteLine("The cake is beautiful!");
        }

        private static async Task PreperingIcingAsync()
        {
            await Task.Run(() => PrepareIcing());
            Console.WriteLine("The icing is done!");
        }

        private static async Task WaitingForCakeInOvenAsync()
        {
            await Task.Run(() => FillUppTinsAndPutInOven());
            Console.WriteLine("The cake is done in the oven!");
        }
        private static async Task WaterMixAsync()
        {
            await Task.Run(() => MixInWater());
            Console.WriteLine("The mixture is smooth and watery!");
        }

        private static async Task MixAsync()
        {
            await Task.Run(() => MixingIngredince());
            Console.WriteLine("The mixture is smooth (but chuncky)");
        }

        private static async Task GreacingAsync()
        {
            await Task.Run(()=> GreaceTin());
            Console.WriteLine("The tins are greaced");
        }

        private static async Task OvenAsync()
        {
            await Task.Run(() => PreheatOven());
            Console.WriteLine("Oven is warm");
        }


        //Non asynch methods
        public static void PreheatOven()
        {
            Console.WriteLine("Preheating the oven");
            Thread.Sleep(15000);
        }

        public static void GreaceTin()
        {
            Console.WriteLine("Greacing the tins");
            Thread.Sleep(5000);
        }

        public static void MixingIngredince() 
        {
            Console.WriteLine("Mixing everthing (except boilng water)");
            Thread.Sleep(6000);
        }
        public static void MixInWater()
        {
            Console.WriteLine("Mixing in boiling water (where did it come from?!)");
            Thread.Sleep(800);
        }

        public static void FillUppTinsAndPutInOven()
        {
            Console.WriteLine("Filling up tins and putting them in the warm oven");
            Thread.Sleep(15000);
        }

        public static void PrepareIcing()
        {
            Console.WriteLine("Mixing and fixing the icing...");
            Thread.Sleep(2000);
        }

        public static void LastTouches()
        {
            Console.WriteLine("Aplying Icing to the cake");
            Thread.Sleep(3000);
        }

        public static void GenericBake(string message, int time)
        {
            Console.WriteLine(message);
            Thread.Sleep(time);
        }
    }
}
