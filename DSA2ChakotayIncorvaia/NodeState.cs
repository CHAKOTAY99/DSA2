using System;
using System.Collections.Generic;
using System.Text;

namespace DSA2ChakotayIncorvaia
{
    class NodeState : IEquatable<NodeState>
    {
        public int IdNum { get; set; }
        public int StateType { get; set; } /* 0 = accepting 1 = rejecting */
        public int startState { get; set; } /* 0 = not start 1 = start */
        public NodeState a, b = null; /* Transitions */

        public override string ToString()
        {
            /* Marker if it is a start state or not */
            String startOrNot = "";
            if(startState == 1)
            {
                startOrNot = " And it is also the Start State";
            }


            /* Return statements */
            if (StateType == 0)
            {
                return "State ID: " + IdNum + " is an accepting state with transition A: ( " + IdNum + "," + a.IdNum + ") and transition " +
                    "B: (" + IdNum + "," + b.IdNum + ")." +startOrNot;

            }
            else
            {
                return "State ID: " + IdNum + " is a rejecting state with transition A: ( " + IdNum + "," + a.IdNum + ") and transition " +
                    "B: (" + IdNum + "," + b.IdNum + ")." + startOrNot;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            NodeState objAsNodeState = obj as NodeState;
            if (objAsNodeState == null)
            {
                return false;
            }
            else
            {
                return Equals(objAsNodeState);
            }
        }

        public bool Equals(NodeState other)
        {
            if (other == null)
            {
                return false;
            }
            return (this.IdNum.Equals(other.IdNum));
        }
    }
}