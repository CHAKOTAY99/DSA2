using System;
using System.Collections.Generic;

namespace DSA2ChakotayIncorvaia
{
    class Program
    {
        static void Main(string[] args)
        {
            DFACreate q1 = new DFACreate();
            List<NodeState> question1 = q1.CreateAutomata();
            q1.DisplayAutomata(question1);
            //q1.EnterString();
            q1.Bsearch(question1);
            q1.DisplayList(question1);
            List<List<NodeState>> NewList = new List<List<NodeState>>();
            List<NodeState> question3 = q1.MinimizeMoore(NewList, question1, false);
            q1.DisplayAutomata(question3);
            Console.ReadLine();
        }
    }
}
