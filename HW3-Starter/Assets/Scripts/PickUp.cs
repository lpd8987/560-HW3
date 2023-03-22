using System;
using UnityEngine;

namespace BehaviorTree
{
    [Serializable]
    public class PickUp : Task
    {
        //The character that is picking something up
        private Character targetChar;
        public Character TargetChar
        {
            get { return targetChar; }
            set { targetChar = value; }
        }

        //The thing to be picked up
        private Thing collectible;
        public Thing Collectible
        {
            get { return collectible; }
            set { collectible = value; }
        }

        // This constructor defaults the thing to None.
        public PickUp() : this(Character.None, Thing.None) { }

        // This constructor takes a parameter for the thing.
        public PickUp(Character targetChar, Thing collectible)
        {
            TargetChar = targetChar;
            Collectible = collectible;
        }

        //
        public override bool run(WorldState state)
        {
            if (!new IsHere(TargetChar, Collectible).run(state))
            {
                if (state.Debug) Debug.Log("Could not pick up " + Collectible + " because it is not in the same location!");
            }

            if (!state.Inventories[TargetChar].Contains(Collectible))
            {
                if (state.Debug) Debug.Log(ToString());
                state.Inventories[targetChar].Add(Collectible);
                return true;
            }
            else
            {
                if (state.Debug) Debug.Log("Could not pick up " + Collectible + " because it is already in character's inventory!");
                return false;
            }

        }

        // Creates and returns a string describing the IsOpen condition.
        public override string ToString()
        {
            return targetChar + " picks up " + collectible;
        }
    }
}
