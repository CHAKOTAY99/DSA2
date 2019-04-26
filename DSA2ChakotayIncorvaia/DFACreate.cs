using System;
using System.Collections.Generic;
using System.Text;

namespace DSA2ChakotayIncorvaia
{
    class DFACreate
    {
        // Create a list of States
        List<NodeState> states = new List<NodeState>();

        // Method to create DFA
        public void CreateAutomata()
        {
            /* Variables */
            int randNum, startState;
            // Instantite random number generator
            Random rand = new Random();
            // Generate the random number between 16 and 64 inclusive
            randNum = rand.Next(15, 65);
            //Console.WriteLine("Random number is: {0}", randNum);
            /* Display the start state*/
            startState = rand.Next(15, randNum);
            Console.WriteLine("Start state is: {0}", startState);
            /* Add basic stuff to create the entire list */
            for (int i = 1; i <= randNum; i++)
            {
                int y = rand.Next(0, 2);

                if (i == startState)
                {
                    states.Add(new NodeState() { IdNum = i, StateType = y, StartState = 1 });
                }
                else
                {
                    states.Add(new NodeState() { IdNum = i, StateType = y, StartState = 0 });
                }
            }
            /* Fill out the transitions by ensuring later
             to ensure that the other Node States exist */
            foreach (NodeState aState in states)
            {
                int aNum = rand.Next(randNum);
                int bNum = rand.Next(randNum);
                aState.a = states[aNum];
                aState.b = states[bNum];
            }
        }

        // Method to Display DFA
        public void DisplayAutomata()
        {
            Console.WriteLine("We randomly have {0} states in total.", states.Count);
            foreach (NodeState aState in states)
            {
                Console.WriteLine(aState);
            }
        }

        /* Method to enter strings into the automata
         * and see if its rejected or accepted */
         public void EnterString()
        {
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("Enter a string (Alphabet = a,b): ");
            String words = Console.ReadLine();
            NodeState tempState = states.Find(e => e.StartState == 1);
            Console.WriteLine(tempState);
            foreach (char c in words)
            {
                if(c == ' ') {
                    tempState = tempState;
                } else if (c == 'a')
                {
                    tempState = tempState.a;
                    Console.WriteLine(tempState);
                } else if (c == 'b')
                {
                    tempState = tempState.b;
                    Console.WriteLine(tempState);
                } else
                {
                    //return "Invalid String!";
                    Console.WriteLine("Invalid String!");
                    return;
                }
            }
            if(tempState.StateType == 0)
            {
                /* Accepted */
                //return "String has been accepted";
                Console.WriteLine("ACCEPTED STRING");
            } else
            {
                /* Rejected */
                //return "String has been rejected";
                Console.WriteLine("REJECTED STRING");
            }
        }
    }
}