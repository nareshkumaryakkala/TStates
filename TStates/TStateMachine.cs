using System;
using System.Collections;
using System.Collections.Generic;
using YakkalaTech.TMessages;

namespace YakkalaTech.TStates
{
    public partial class TState
    {

        public class TStateMachine
        {
            public string Name
            {
                get;
                set;
            }

            protected Dictionary<string, TState> m_States = new Dictionary<string, TState>();
            protected Dictionary<int, TStateTransition> m_Transitions = new Dictionary<int, TStateTransition>();

            protected TState m_defaultSate = null;
            protected TState m_currentSate = null;

            public TStateMachine()
            {
                CreateState("Default");
                m_defaultSate = m_States["Default"];
            }

            public void Run()
            {
                GoToState(m_defaultSate);
            }

            public string CreateState(string name)
            {
                string temp = name;
                int i = 0;
                while(m_States.ContainsKey(temp))
                {
                    temp = name + i.ToString();
                    i++;
                }
                TState state = new TState(temp);
                m_States.Add(temp, state);
                return temp;
            }

            public void CreateState(string name, StateAction action)
            {
                name = CreateState(name);
                m_States[name].AddStateAction(action);
            }

            public TStateTransitionInfo AddStateTransition(string source, string destination, string triggerName = "")
            {
                if(m_States.ContainsKey(source) && m_States.ContainsKey(destination))
                {
                    TStateTransition transition = new TStateTransition(m_States[source], m_States[destination]);
                    m_States[source].AddTransitionOut(transition);
                    m_States[destination].AddTransitionIn(transition);
                    m_Transitions.Add(transition.ID, transition);

                    if(triggerName.Trim() != "")
                    {
                        transition.AddTrigger(triggerName);
                    }
                    return transition.Info;
                }
                return null;
            }

            public void RemoveStateTransition(TStateTransitionInfo transitionInfo)
            {
                TStateTransition transition = m_Transitions[transitionInfo.ID];
                m_Transitions.Remove(transition.ID);
                transition.Source.RemoveTransitionOut(transition);
                transition.Destination.RemoveTransitionIn(transition);
            }

            public void RemoveState(string name)
            {
                if(m_States.ContainsKey(name))
                {
                    TState state = m_States[name];
                    m_States.Remove(name);
                    while(state.m_OutTransitions.Count > 0)
                    {
                        RemoveStateTransition(state.m_OutTransitions[0].Info);                        
                    }

                    while(state.m_InTransitions.Count > 0)
                    {
                        RemoveStateTransition(state.m_InTransitions[0].Info);                        
                    }
                }
            }
        
            protected void GoToState(TState state)
            {
                state.Execute();
                if(state.AutoForwardState != null)
                {
                     GoToState(state.AutoForwardState);
                }
            }

            /*public void AddStateTransition(string source, string destination)
            {
                m_States[source].AutoForwardState = m_States[destination];
            }*/

            public bool Trigger(string trigger)
            {
                if(m_currentSate.TriggerTransition(trigger) != null)
                {
                    return true;
                }
                return false;
            }

            public string[] GetStateNames()
            {
                string[] keys = new string[m_States.Keys.Count];
                m_States.Keys.CopyTo(keys, 0);
                return keys;
            }

            public List<TStateTransitionInfo> GetTransitions()
            {
                List<TStateTransitionInfo> infos = new List<TStateTransitionInfo>();
                foreach(KeyValuePair<int, TStateTransition> item in m_Transitions)
                {
                    infos.Add(item.Value.Info);
                }
                return infos;
            }
        }
    }
}
