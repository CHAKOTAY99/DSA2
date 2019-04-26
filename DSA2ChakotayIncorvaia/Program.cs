using System;

namespace DSA2ChakotayIncorvaia
{
    class Program
    {
        static void Main(string[] args)
        {
            DFACreate q1 = new DFACreate();
            q1.CreateAutomata();
            q1.DisplayAutomata();
            q1.EnterString();
            Console.ReadLine();
        }
    }
}
