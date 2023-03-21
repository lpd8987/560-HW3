using System;
using UnityEngine;

namespace BehaviorTree
{
    [Serializable]
    public class MoveTo : Task
    {
        // The character that is moving to a different location.
        private Character mover;
        public Character Mover
        {
            get { return mover; }
            set { mover = value; }
        }

        // The location the character is moving to.
        private Location where;
        public Location Where
        {
            get { return where; }
            set { where = value; }
        }

        // This constructor defaults the character and location to None.
        public MoveTo() : this(Character.None, Location.None) { }

        // This constructor takes parameters for the character and location.
        public MoveTo (Character mover, Location where)
        {
            Mover = mover;
            Where = where;
        }

        // This method runs the MoveTo action on the given WorldState object.
        public override bool run (WorldState state)
        {
            if (state.Debug) Debug.Log("Attempting to move to " + Where.ToString());

            //local var declared for legibility
            Location currentCharLoc = state.CharacterPosition[Mover];

            //check if character is already in new location, if so return false, otherwise, return true
            if (currentCharLoc == Where) 
            {
                if (state.Debug) Debug.Log("Failed to move to " + Where.ToString() + ", character already at location.");
                return false; 
            }

            //check if character location is connected to new location, if not, return false, otherwise continue
            if (!state.ConnectedLocations[currentCharLoc].Contains(Where))
            {
                if (state.Debug) Debug.Log("Failed to move to " + Where.ToString() + ", location not connected to current location");
                return false;
            }


            //check if there is a thing between the current location and new location
            if (!state.BetweenLocations.ContainsKey(currentCharLoc))
            {
                state.CharacterPosition[Mover] = Where;
                if (state.Debug) Debug.Log(ToString());
                return true;
            }

            //local vars declared for legibility
            Thing thingBetween = state.BetweenLocations[currentCharLoc].Item1;
            Location targetRoom = state.BetweenLocations[currentCharLoc].Item2;

            if (thingBetween == Thing.Gate && targetRoom == Where)
            {
                //if there is, check if that door is open
                if (new IsOpen(thingBetween).run(state))
                {
                    state.CharacterPosition[Mover] = Where;
                    if (state.Debug) Debug.Log(ToString());
                    return true;
                }
                else {
                    if (state.Debug) Debug.Log("Failed to move to " + Where.ToString() + ", " + thingBetween.ToString() + " blocking path");
                    return false; 
                }
            }

            //return false if somehow nothing has been returned yet
            return false;
        }

        // Creates and returns a string describing the MoveTo action.
        public override string ToString()
        {
            return mover + " moves to " + where;
        }
    }
}
