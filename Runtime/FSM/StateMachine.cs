using System;
using System.Collections.Generic;

namespace Vocario.FSM
{
    public class StateMachine
    {
        private IState _currentState = null;

        private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        private List<Transition> _currentTransitions = new List<Transition>();
        private List<Transition> _anyTransitions = new List<Transition>();

        private static List<Transition> EmptyTransitions = new List<Transition>(0);

        public void Tick()
        {
            Transition transiton = GetTransition();
            if (transiton != null)
            {
                SetState(transiton.To);
            }

            _currentState?.Tick();
        }

        public void SetState(IState state)
        {
            if (state == _currentState)
            {
                return;
            }

            _currentState?.OnExit?.Invoke();
            _currentState = state;

            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
            if (_currentTransitions == null)
            {
                _currentTransitions = EmptyTransitions;
            }

            _currentState?.OnEnter?.Invoke();
        }

        public void AddTransition(IState from, IState to, Func<bool> predicate)
        {
            if (_transitions.TryGetValue(from.GetType(), out List<Transition> transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }

            transitions.Add(new Transition(to, predicate));
        }

        public void AddAnyTransition(IState state, Func<bool> predicate)
        {
            _anyTransitions.Add(new Transition(state, predicate));
        }

        private Transition GetTransition()
        {
            foreach (Transition transiton in _anyTransitions)
            {
                if (transiton.Condition())
                {
                    return transiton;
                }
            }

            foreach (Transition transition in _currentTransitions)
            {
                if (transition.Condition())
                {
                    return transition;
                }
            }

            return null;
        }
    }
}
