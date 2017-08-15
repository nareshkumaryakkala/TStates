using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using YakkalaTech.TMessages;

namespace YakkalaTech.TStates
{
    public class TStateTransition
    {
        static int nextID = 0;

        public TState Source
        {
            get;
            private set;
        }

        public TState Destination
        {
            get;
            private set;
        }

        public int ID
        {
            get;
            private set;
        }

        public TStateTransitionInfo Info
        {
            get
            {
                if(info == null)
                {
                    info = new TStateTransitionInfo(ID, Source.Name, Destination.Name);
                }
                return info;
            }
        }

        List<TStateTrigger> triggers = new  List<TStateTrigger>();

        TStateTransitionInfo info;

        public TStateTransition(TState source, TState destination)
        {
            Source = source;
            Destination = destination;
            ID = nextID;
            nextID++;
        }

        public TStateTrigger AddTrigger(string name)
        {
            TStateTrigger trigger = new TStateTrigger(name);
            triggers.Add(trigger);
            return trigger;
        }

        public bool Validate(string name)
        {
            foreach(TStateTrigger trigger in triggers)
            {
                if(!trigger.Validate(name))
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class TStateTransitionInfo
    {
        public string Source
        {
            get;
            private set;
        }

        public string Destination
        {
            get;
            private set;
        }

        public int ID
        {
            get;
            private set;
        }

        public TStateTransitionInfo(int id, string source, string destination)
        {
            ID = id;
            Source = source;
            Destination = destination;
        }
    }

}
