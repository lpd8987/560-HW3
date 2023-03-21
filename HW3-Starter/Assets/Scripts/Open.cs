using System;
using UnityEngine;

namespace BehaviorTree
{
    [Serializable]
    public class Open : Task
    {
        // The character that is opening the thing.
        private Character opener;
        public Character Opener
        {
            get { return opener; }
            set { opener = value; }
        }

        // The thing that is being opened.
        private Thing openThis;
        public Thing OpenThis
        {
            get { return openThis; }
            set { openThis = value; }
        }

        // This constructor defaults the character and thing to None.
        public Open() : this(Character.None, Thing.None) { }

        // This constructor takes parameters for the character and thing.
        public Open (Character opener, Thing openThis)
        {
            Opener = opener;
            OpenThis = openThis;
        }

        // This method runs the Open action on the given WorldState object.
        public override bool run(WorldState state)
        {
            //check if the character and thing are in the same location, if they are, continue, otherwise, return false
            //local var for code legibility
            Location currentCharLoc = state.CharacterPosition[Opener];
            Location thingLoc = state.ThingPosition[OpenThis];

            if (currentCharLoc != thingLoc)
            {
                if (state.Debug) Debug.Log(Opener + " cannot open " + OpenThis + " because they are not in the same location!");
                return false;
            }

            //check if the thing is open or closed
            if(!new IsOpen(OpenThis).run(state))
            {
                //if it is closed, open it
                if (state.Debug) Debug.Log(ToString());
                state.Open[OpenThis] = true;
                return true;
            }

            //if thing is already open, or is not possible to be opened, return false
            if (state.Debug) Debug.Log(Opener + " cannot open " + OpenThis + " because it is already open!");
            return false;
        }

        // Creates and returns a string describing the Open action.
        public override string ToString()
        {
            return opener + " opens " + openThis;
        }
    }
}
