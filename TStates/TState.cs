using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YakkalaTech.TStates
{
    public partial class TState
    {

        public delegate void StateAction();
        //TODO: Multi parameter state actions
        //public delegate void StateAction<T>(T param);
        //public delegate void StateAction<T, U>(T param, U param2);
        //public delegate void StateAction<T, U, V>(T param, U param2, V param3);

        protected List<StateAction> m_StateActions = new List<StateAction>();
        protected List<TStateTransition> m_InTransitions = new List<TStateTransition>();
        protected List<TStateTransition> m_OutTransitions = new List<TStateTransition>();

        public string Name
        {
            get
            {
                return m_Name;
            }

            set
            {
                m_Name = value;
            }
        }

        string m_Name;

        protected TState AutoForwardState = null;


        public TState(string name)
        {
            Name = name;
        }

        protected void Execute()
        {
           foreach(StateAction action in m_StateActions)
           {
               action.Invoke();
           }

        }

        protected TStateTransition TriggerTransition(string message)
        {
            foreach(TStateTransition transition in m_OutTransitions)
            {
                if(transition.Validate(message))
                {
                   return transition;
                }
            }
            return null;
        }

        protected void AddStateAction(StateAction action)
        {
           m_StateActions.Add(action);
        }

        protected void RemoveStateAction(StateAction action)
        {
           m_StateActions.Remove(action);
        }

        protected void AddTransitionOut(TStateTransition transition)
        {
            m_OutTransitions.Add(transition);
        }

        protected void AddTransitionIn(TStateTransition transition)
        {
            m_InTransitions.Add(transition);
        }

        protected void RemoveTransitionOut(TStateTransition transition)
        {
            m_OutTransitions.Remove(transition);
        }

        protected void RemoveTransitionIn(TStateTransition transition)
        {
            m_InTransitions.Remove(transition);
        }

    }
}
