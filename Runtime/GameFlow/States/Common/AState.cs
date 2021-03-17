using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Vocario.GameFlow
{
    public abstract class AState : ScriptableObject
    {
        [SerializeField]
        protected AGameContext _context = null;
        [SerializeField]
        protected AState[] _states = null;

        public Dictionary<Type, AState> States { get; protected set; } = new Dictionary<Type, AState>();

        public UnityEvent OnEnterAction = new UnityEvent();
        public UnityEvent OnExitAction = new UnityEvent();

        protected virtual void OnValidate()
        {
            if (_states == null)
            {
                return;
            }
            States = _states.ToDictionary(x => x.GetType(), x => x);
        }

        protected virtual void Awake()
        {
            OnValidate();
        }

        public virtual void OnEnter()
        {
            OnEnterAction?.Invoke();
        }

        public virtual void OnExit()
        {
            OnExitAction?.Invoke();
        }
    }
}
