using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    static class Program
    {
        static void Main(string[] args)
        {
            var vals = new int[] { 5, 9, 4, 7, 15, 16, 10, 11, 14, 19, 18 };
            var result1 = vals.MaxOverPrevious();
            Console.WriteLine("Values greater than all previous values:");
            foreach (var element in result1)
            {
                Console.WriteLine(element.ToString());
            }

            var result2 = vals.LocalMaxima();
            Console.WriteLine("Values greater than previous and next value in list");
            foreach (var element in result2)
            {
                Console.WriteLine(element.ToString());
            }
            var result3 = vals.AtLeastK(3, i => i >= 16 && i < 20);
            Console.WriteLine("Do 3 elements pass the test?");
            if (result3)
            {
                Console.WriteLine("Yes");
            }
            else
            {
                Console.WriteLine("No");
            }
            var result4 = vals.AtLeastHalf(i => i >= 16 && i < 20);
            Console.WriteLine("Do half the elements pass the test?");
            if (result4)
            {
                Console.WriteLine("Yes");
            }
            else
            {
                Console.WriteLine("No");
            }
            Console.WriteLine("Overloaded results:");
            var result5 = vals.MaxOverPrevious(i => i / 2 + 7);
            Console.WriteLine("Values greater than all previous values:");
            foreach (var element in result5)
            {
                Console.WriteLine(element.ToString());
            }

            var result6 = vals.LocalMaxima(i => i % vals.Length);
            Console.WriteLine("Values greater than previous and next value in list");
            foreach (var element in result6)
            {
                Console.WriteLine(element.ToString());
            }
            var result7 = vals.AtLeastK(3, i => i >= 16 && i < 20, i => i * 2);
            Console.WriteLine("Do 3 elements pass the test?");
            if (result7)
            {
                Console.WriteLine("Yes");
            }
            else
            {
                Console.WriteLine("No");
            }
            var result8 = vals.AtLeastHalf(i => i >= 16 && i < 20, i => i + 3);
            Console.WriteLine("Do half the elements pass the test?");
            if (result8)
            {
                Console.WriteLine("Yes");
            }
            else
            {
                Console.WriteLine("No");
            }
            Console.ReadKey();
        }
        //The entire list does need to be enumerated so we do not save with deferred execution.
        public static IEnumerable<int> MaxOverPrevious(this int[] list)
        {
            int compare = list[0];
            foreach (var val in list)
            {
                //if val is greater than all previous values
                if (val >= compare)
                {
                    compare = val;
                    yield return val;
                }
            }
        }
        //Here also, the entire list does need to be enumerated so we do not save with deferred execution. 
        public static IEnumerable<int> LocalMaxima(this int[] list)
        {
            int prev = list[0];
            int next = list[2];
            if (list[0] > list[1])
            {
                yield return list[0];
            }
            for (int i = 1; i < (list.Length - 1); i++)
            {
                if (list[i] > prev && list[i] > next)
                {
                    //TODO index out of bounds exception

                    yield return list[i];
                }
                prev = list[i];
                if (i + 2 < list.Length)
                {
                    next = list[i + 2];
                }
            }
            if (list[list.Length - 1] > list[list.Length - 2])
            {
                yield return list[list.Length - 1];
            }
        }
        //With this method, we return true as soon as the condition is satisfied,
        //so we don't enumerate through the whole list necessarily.
        //I used the Count() method even though it has to iterate over the list 
        //every time to check te count, because I did not want to add another variable
        public static bool AtLeastK(this int[] list, int k, Func<int, bool> include)
        {
            int counter = 0;
            foreach (var val in list)
            {
                if (include(val))
                {
                    counter++;
                }
                if (counter >= k)
                {
                    return true;
                }
            }

            return false;
        }
        //Same as above method -  we iterate until cond is true and use Count() method
        public static bool AtLeastHalf(this int[] list, Func<int, bool> include)
        {
            int counter = 0;
            foreach (var val in list)
            {
                if (include(val))
                {
                    counter++;
                }
                if (counter >= list.Count() / 2)
                {
                    return true;
                }
            }

            return false;
        }

        public static IEnumerable<int> MaxOverPrevious(this int[] list, Func<int, int> transformValue)
        {
            int compare = list[0];
            foreach (var val in list)
            {
                //if val is greater than all previous values
                if (val >= compare)
                {
                    compare = val;
                    transformValue(val);
                    yield return val;
                }
            }
        }

        public static IEnumerable<int> LocalMaxima(this int[] list, Func<int, int> transformValue)
        {
            int prev = list[0];
            int next = list[2];
            if (list[0] > list[1])
            {
                transformValue(list[0]);
                yield return list[0];
            }
            for (int i = 1; i < list.Length - 1; i++)
            {
                if (list[i] > prev && list[i] > next)
                {
                    transformValue(list[i]);
                    yield return list[i];
                }
                prev = list[i];
                if (i + 2 < list.Length)
                {
                    next = list[i + 2];
                }
            }
            if (list[list.Length - 1] > list[list.Length - 2])
            {
                transformValue(list[list.Length - 1]);
                yield return list[list.Length - 1];
            }
        }

        public static bool AtLeastK(this int[] list, int k, Func<int, bool> include, Func<int, int> transformValue)
        {
            int counter = 0;
            foreach (var val in list)
            {
                if (include(val))
                {
                    counter++;
                }
            }
            if (counter >= k)
            {
                return true;
            }
            return false;
        }

        public static bool AtLeastHalf(this int[] list, Func<int, bool> include, Func<int, int> transformValue)
        {
            int counter = 0;
            foreach (var val in list)
            {
                if (include(val))
                {
                    counter++;
                }
            }
            if (counter >= list.Count() / 2)
            {
                return true;
            }
            return false;
        }

    }
}
