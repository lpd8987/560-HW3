using System;
using UnityEngine;

namespace BehaviorTree
{
    [Serializable]
    public class IsHere : Task
    {
        //The Character looking for the thing
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
        public IsHere() : this(Character.None, Thing.None) { }

        // This constructor takes a parameter for the thing.
        public IsHere(Character targetChar, Thing collectible)
        {
            TargetChar = targetChar;
            Collectible = collectible;
        }

        // This method runs the IsOpen condition on the given WorldState object.
        public override bool run(WorldState state)
        {
            Location charLoc = state.CharacterPosition[TargetChar];
            Location thingLoc = state.ThingPosition[Collectible];

            if(charLoc == thingLoc)
            {
                if (state.Debug) Debug.Log(ToString() + " It is!");
                return true;
            }
            else
            {
                if (state.Debug) Debug.Log(ToString() + " It is not!");
                return false;
            }
        }

        // Creates and returns a string describing the IsOpen condition.
        public override string ToString()
        {
            return "Is " + collectible + " here?";
        }
    }
}
