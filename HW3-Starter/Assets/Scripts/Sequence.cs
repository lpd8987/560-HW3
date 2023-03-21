using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    [Serializable]
    public class Sequence : Task
    {
        // An ordered list of this Sequence's children tasks.
        public List<Task> children { get; set; } = new List<Task>();

        // This method implements the sequence behavior.
        public override bool run(WorldState state)
        {
            foreach(Task t in children)
            {
                if (!t.run(state)) return false;
            }
            return true;
        }
    }
}
