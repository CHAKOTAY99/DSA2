using System;
using System.Collections.Generic;

namespace DSA2ChakotayIncorvaia
{
    class Program
    {
        static void Main(string[] args)
        {
            // Question 1
            DFACreate q1 = new DFACreate();
            List<NodeState> question1 = q1.CreateAutomata();
            q1.DisplayAutomata(question1, "DISPLAYING AUTOMATA A");
            // Question 2
            q1.Bsearch(question1, "\nDISPLAYING DEPTH OF AUTOMATA A");
            q1.DisplayListA();
            // Question 3
            List<List<NodeState>> NewList = new List<List<NodeState>>();
            List<NodeState> question3 = q1.MinimizeMoore(NewList, question1, false);
            q1.DisplayAutomata(question3,"DISPLAYING AUTOMATA M");
            // Quesetion 4
            q1.Bsearch(question3, "\nDISPLAYING DEPTH OF AUTOMATA M");
            q1.DisplayListM();
            // Question 5
            q1.EnterString(question1,"\nDISPLAYING AUTOMATA A\n");
            q1.EnterString(question3, "\nDISPLAYING AUTOMATA M\n");
            // Question 6
            q1.tarjanAlgorithm(question3);
            Console.ReadLine();
        }
    }
}
