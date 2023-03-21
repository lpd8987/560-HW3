using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    [Serializable]
    public class Selector : Task
    {
        // An ordered list of this Selector's children tasks.
        public List<Task> children { get; set; } = new List<Task>();

        // This method implements the selector behavior.
        public override bool run (WorldState state)
        {
            foreach (Task t in children)
            {
                //Selector returns true as soon as one of its children returns true.
                if (t.run(state)) return true;
            }
            return false;
        }
    }
}
