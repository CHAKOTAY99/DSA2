using System;
using System.Collections.Generic;
using System.Linq;
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

        // Method to display DFA with an adjacency list
        public void DisplayList()
        {
            Console.WriteLine("\nPrinting the Adjancency List");
            var item = states[states.Count - 1];
            Console.Write("V = {");
            foreach (NodeState aState in states)
            {

                if (aState == item)
                {
                    Console.Write(aState.IdNum);
                } else
                {
                    Console.Write(aState.IdNum + ",");
                }
            }
            Console.Write("}\n");
            Console.Write("E = {");
            foreach (NodeState aState in states)
            {

                if (aState == item)
                {
                    Console.Write("({" + aState.IdNum + "},a)=" + aState.a.IdNum);
                    Console.Write("({" + aState.IdNum + "},b)=" + aState.b.IdNum);
                }
                else
                {
                    Console.Write("({" + aState.IdNum + "},a)=" + aState.a.IdNum + ",");
                    Console.Write("({" + aState.IdNum + "},b)=" + aState.b.IdNum + ",");
                }
            }
            Console.Write("}\n");
            foreach (NodeState aState in states)
            {
                Console.WriteLine("{"+aState.IdNum + "} -->" + " ({" + aState.IdNum + "},a)="+ aState.a.IdNum + " --> " + " ({" + aState.IdNum + "},b)="+aState.b.IdNum + " --> null");
            }
            Console.Write("\n");
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

        /* Find the depth of the automata
         * Use breadth-first search to find the last and shortes possible state
         * then calculate on how to get to it by using nulls to calculate the level */
        public void Bsearch()
        {
            int depth = 0;
            /* Find start state */
            NodeState currentNode = states.Find(e => e.StartState == 1);
            NodeState lastState = null;
            /* Create the list to be used for the states */
            Queue<NodeState> bState = new Queue<NodeState>();
            /* Create the list to be used for the visited states */
            Queue<NodeState> visitedState = new Queue<NodeState>();

            Console.WriteLine("\nCalculating Depth: ");
            /* Start */
            bState.Enqueue(currentNode);
            bState.Enqueue(null);
            visitedState.Enqueue(currentNode);
            while(bState.Count != 0)
            {
                currentNode = bState.Dequeue();
                if(currentNode == null)
                {
                    depth++;
                    bState.Enqueue(null);
                    if(bState.Peek() == null)
                    {
                        depth--;
                        break;
                    }
                }
                if (currentNode != null)
                {
                    if (!visitedState.Contains(currentNode.a))
                    {
                        bState.Enqueue(currentNode.a);
                        visitedState.Enqueue(currentNode.a);
                    }
                    if (!visitedState.Contains(currentNode.b))
                    {
                        bState.Enqueue(currentNode.b);
                        visitedState.Enqueue(currentNode.b);
                    }
                }
            }
            foreach(NodeState aState in visitedState)
            {
                Console.Write("{0} ",aState.IdNum);
                lastState = aState;
            }
            Console.WriteLine("\nThe Depth of the Automaton is " + depth + ".");
        }
        /*
        // Testing the newly understood Hopcroft algorithm
        public void MinimizeHopcroft(List<NodeState>MainList)
        {
            List<NodeState> accepting = new List<NodeState>();
            List<NodeState> rejecting = new List<NodeState>();
            // Seperate the accepting states and rejecting states
            foreach (NodeState aState in MainList)
            {
                if(aState.StateType == 0)
                {
                    accepting.Add(aState);
                } else
                {
                    rejecting.Add(aState);
                }
            }
            // P = {F, Q\F}
            List<List<NodeState>> P = new List<List<NodeState>>();
            P.Add(accepting);
            P.Add(rejecting);
            // W = {F}
            List<List<NodeState>> W = new List<List<NodeState>>();
            W.Add(accepting);

            while (P.Any()) {
                foreach(List<NodeState> tinyList in W)
                {
                    W.Remove(tinyList);
                    List<NodeState> X = new List<NodeState>();
                    foreach (NodeState aState in states)
                    {
                        if (tinyList.Contains(aState.a) && tinyList.Contains(aState.b))
                        {
                            X.Add(aState);
                        }
                    }
                    List<List<NodeState>> NewList = new List<List<NodeState>>(P);
                    foreach(List<NodeState> tinyP in NewList)
                    {
                        List<NodeState> Y = new List<NodeState>(tinyP);
                        List<NodeState> intersection = new List<NodeState>(X);
                        foreach(NodeState removal in intersection)
                        {
                            if (!Y.Contains(removal))
                            {
                                intersection.Remove(removal);
                            }
                        }
                        List<NodeState> difference = new List<NodeState>(Y);
                        foreach(NodeState removal in difference)
                        {
                            if (X.Contains(removal))
                            {
                                difference.Remove(removal);
                            }
                        }
                        if(intersection.Any() && difference.Any())
                        {
                            P.Remove(Y);
                            P.Add(intersection);
                            P.Add(difference);
                            if (W.Contains(Y))
                            {
                                W.Remove(Y);
                                W.Add(intersection);
                                W.Add(difference);
                            } else
                            {
                                if(intersection.Count <= difference.Count)
                                {
                                    W.Add(intersection);
                                } else
                                {
                                    W.Add(difference);
                                }
                            }
                        }
                    }
                }
            }
            List<NodeState> finalList = new List<NodeState>();
        }

        //2nd attempt
        // Testing the newly understood Hopcroft algorithm
        public void MinimizeHopcroftTwo(List<NodeState> MainList)
        {
            HashSet<NodeState> accepting = new HashSet<NodeState>();
            HashSet<NodeState> rejecting = new HashSet<NodeState>();
            // Seperate the accepting states and rejecting states
            foreach (NodeState aState in MainList)
            {
                if (aState.StateType == 0)
                {
                    accepting.Add(aState);
                }
                else
                {
                    rejecting.Add(aState);
                }
            }
            // P = {F, Q\F}
            HashSet<HashSet<NodeState>> P = new HashSet<HashSet<NodeState>>();
            P.Add(accepting);
            P.Add(rejecting);
            // W = {F}
            HashSet<HashSet<NodeState>> W = new HashSet<HashSet<NodeState>>();
            W.Add(accepting);
            
            // While W is not empty do
            while (W.Any())
            {
                // for each c in Σ do
                foreach (NodeState listStates in states)
                {

                    // choose and remove a set A from W
                    foreach (HashSet<NodeState> tinyList in W)
                    {
                        W.Remove(tinyList);

                        HashSet<NodeState> X = new HashSet<NodeState>();
                        foreach (NodeState aState in states)
                        {
                            if (tinyList.Contains(aState.a) && tinyList.Contains(aState.b))
                            {
                                X.Add(aState);
                            }
                        }
                        HashSet<HashSet<NodeState>> NewList = new HashSet<HashSet<NodeState>>(P);
                        foreach (HashSet<NodeState> tinyP in NewList)
                        {
                            List<NodeState> Y = new List<NodeState>(tinyP);
                            List<NodeState> intersection = new List<NodeState>(X);
                            foreach (NodeState removal in intersection)
                            {
                                if (!Y.Contains(removal))
                                {
                                    intersection.Remove(removal);
                                }
                            }
                            List<NodeState> difference = new List<NodeState>(Y);
                            foreach (NodeState removal in difference)
                            {
                                if (X.Contains(removal))
                                {
                                    difference.Remove(removal);
                                }
                            }
                            if (intersection.Any() && difference.Any())
                            {
                                P.Remove(Y);
                                P.Add(intersection);
                                P.Add(difference);
                                if (W.Contains(Y))
                                {
                                    W.Remove(Y);
                                    W.Add(intersection);
                                    W.Add(difference);
                                }
                                else
                                {
                                    if (intersection.Count <= difference.Count)
                                    {
                                        W.Add(intersection);
                                    }
                                    else
                                    {
                                        W.Add(difference);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            List<NodeState> finalList = new List<NodeState>();
        }
        */

        // Old Minimize Moore Algorithm - coudln't get trough actual partitioning without mixing up the lists
        public void MinimizeMoore(List<List<NodeState>> OldEquivalency, bool Zeropass)
        {
            List<List<NodeState>> NewEquivalency = new List<List<NodeState>>();
            if(Zeropass == false) // Else check if it seperated its accepting and rejecting states
            {
                Console.WriteLine("Make the 0 lvl lists");
                List<NodeState> accepting = new List<NodeState>();
                List<NodeState> rejecting = new List<NodeState>();
                foreach(NodeState aState in states)
                {
                    if(aState.StateType == 0)
                    {
                        accepting.Add(aState);
                    } else
                    {
                        rejecting.Add(aState);
                    }
                }
                NewEquivalency.Add(new List<NodeState>(accepting));
                NewEquivalency.Add(new List<NodeState>(rejecting));
                foreach (List<NodeState> SubList in NewEquivalency)
                {
                    Console.WriteLine("NEW FAT LIST");
                    foreach(NodeState aState in SubList)
                    {
                        Console.WriteLine(aState);
                    }
                }
                MinimizeMoore(NewEquivalency, true);
            } else
            {
                foreach (List<NodeState> OldSubList in OldEquivalency)
                {
                    if(OldSubList.Count == 1)
                    {
                        // You can stop right here
                        break;
                    }
                    bool doFirst = false;
                    NodeState LastItem = OldSubList[OldSubList.Count - 1];
                    NodeState FirstItem = OldSubList[0];
                    if (doFirst == false)
                    {
                        if(CheckSubset(FirstItem, LastItem, OldEquivalency))
                        {
                            NewEquivalency.Add(new List<NodeState>(new NodeState[] { LastItem, FirstItem }));
                        } else
                        {
                            NewEquivalency.Add(new List<NodeState>(new NodeState[] { FirstItem }));
                            NewEquivalency.Add(new List<NodeState>(new NodeState[] { LastItem }));
                        }
                        // Started off the first sets
                        doFirst = true;
                    }
                    foreach(NodeState aState in OldSubList)
                    {
                        if(aState == LastItem || aState == FirstItem)
                        {
                            // They were added
                            break;
                        }
                        foreach(List<NodeState> NewSubList in NewEquivalency)
                        {
                            // Check state type from first object in new list
                            LastItem = NewSubList[NewSubList.Count - 1];
                            FirstItem = NewSubList[0];
                            if(aState.StateType != FirstItem.StateType)
                            {
                                break;
                            }
                            if(CheckSubset(aState, LastItem, OldEquivalency)){
                                NewSubList.Add(aState);
                            }
                        }
                        NewEquivalency.Add(new List<NodeState>(new NodeState[] { aState }));
                    }
                }
            }
            //states.Add(new NodeState() { IdNum = i, StateType = y, StartState = 0 });
        }

        // Method to check if both nodes are within the same list
        public bool CheckSubset(NodeState x, NodeState y, List<List<NodeState>> OldList)
        {
            foreach(List<NodeState>SubList in OldList)
            {
                if(SubList.Contains(x.a) && SubList.Contains(y.a))
                {
                    if(SubList.Contains(x.b) && SubList.Contains(y.b))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}