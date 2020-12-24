using System;
using System.Collections.Generic;
using System.Threading;

namespace Categories
{
    class PartOneChapterOneTwo
    {
        public static int AddOne(int n) => n + 1;
        public static int Double(int n) => n * 2;

        public static int SlowAddOne(int n)
        {
            Thread.Sleep(1000);
            return n + 1;
        }

        public static Random random1 = new Random(50);
        public static int Random1(int n) => new Random(n).Next(n);
        public static Random random2 = new Random(50);
        public static int Random2(int n) => new Random(n).Next(n);

        public static T Id<T>(T x) => x;

        public static A3 Compose<A1, A2, A3>(A1 x, Func<A1, A2> f1, Func<A2, A3> f2) => f2(f1(x));
        static Func<int, int> AddDouble = (int x) => Compose(x, AddOne, Double);

        public static Func<T1, T3> Compose<T1, T2, T3>(Func<T1, T2> f1, Func<T2, T3> f2) => x => f2(f1(x));

        public static int StupidCompose(int n)
        {
            return AddOne(Double(n));
        }

        public static Func<T, R> Memorise<T, R>(Func<T, R> f)
        {
            var cache = new Dictionary<T, R>();
            return x =>
            {
                if (cache.TryGetValue(x, out var value))
                    return value;
                else
                    return cache[x] = f(x);
            };
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("You're ....");

            // // var test = Compose<>(x => 2 * x, x => x + 2);
            // var n = 5;

            // var x1 = AddDouble(n);
            // var x2 = Compose<int, int, int>(x => x, x => n + x * 2)(n);

            // var n1 = AddOne(Id(n));
            // var n2 = Compose<int, int, int>(Id, AddOne)(n);
            // var test = n1 == n2;
            // if (test) Console.WriteLine("Super");


            // var MemorizedSlowAddOne = Memorise<int, int>(SlowAddOne);
            // var MemorizedRandom = Memorized(Random);

            // foreach (var value in new List<int> { 1, 2, 3, 1, 2, 3 })
            // {
            //     Console.Write(Random(value))
            //     Console.WriteLine(MemorizedRandom(value));
            // }

            // x => 1
            // x => 0
            // x => x
            // x => !x

            var memorize = Memorise<int, int>(Random2);
            foreach (var value in new List<int> { 10, 20, 30, 10, 20, 30 })
            {
                Console.WriteLine($"Original: {Random1(value)}, Cached: {memorize(value)}");
            }
        }
    }
}