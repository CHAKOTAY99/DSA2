using System;
using System.Collections.Generic;

namespace DSA2ChakotayIncorvaia
{
    class Program
    {
        static void Main(string[] args)
        {
            DFACreate q1 = new DFACreate();
            q1.CreateAutomata();
            q1.DisplayAutomata();
            //q1.EnterString();
            q1.Bsearch();
            q1.DisplayList();
            List<List<NodeState>> NewList = new List<List<NodeState>>();
            q1.MinimizeMoore(NewList, false);
            Console.ReadLine();
        }
    }
}
