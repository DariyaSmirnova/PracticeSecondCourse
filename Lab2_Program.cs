using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Xml;

using Lab2;

namespace Lab2
{
    class Program
    {

        static void Main(string[] args)
        {

            try
            {
                var result1 = new int[] { 1, 2 }
                    .Combinations(2, Lab2.IEqualityComparer.Comp);

                foreach (var setArr in result1)
                {
                    Console.WriteLine($"[{string.Join(", ", setArr.Select(x => x.ToString()))}]");
                }
            }
            catch (ArgumentException @a)
            {
                Console.WriteLine(@a.Message);
            }
            Console.WriteLine(string.Empty);

            try
            {
                var result2 = new int[] { 1, 0, 2 }
                    .Subsets(Lab2.IEqualityComparer.Comp);

                foreach (var setArr in result2)
                {
                    Console.WriteLine($"[{string.Join(", ", setArr.Select(x => x.ToString()))}]");
                }
            }
            catch (ArgumentException @a)
            {
                Console.WriteLine(@a.Message);
            }
            Console.WriteLine(string.Empty);

            try
            {
                var result3 = new int[] {}
                    .Reshuffles(Lab2.IEqualityComparer.Comp);
                foreach (var setArr in result3)
                {
                    Console.WriteLine($"[{string.Join(", ", setArr.Select(x => x.ToString()))}]");
                }
            }
            catch (ArgumentException @a)
            {
                Console.WriteLine(@a.Message);
            }
        }
    }
}
