using System;

namespace YakkalaTech.TStates
{
    public class TStateTrigger
    {
        public string Name = ""; 

        public TStateTrigger(string name = "")
        {
            Name = name;
        }

        public bool Validate(string name)
        {
            bool result = false;

            if(Name == name && Name != "")
            {
                result = true;
            }
            return result;
        }
    }
}

