using System;
using System.Collections.Generic;
using System.Linq;
using Vocario.GameFlow.UI;
using UnityEngine;

namespace Vocario.GameFlow
{
    public class GameContext : ScriptableObject
    {
        [SerializeField]
        protected UIManager _UIManager = null;

        [SerializeField]
        protected PlayerController _playerController = null;

        [SerializeField]
        protected AState _currentState = null;

        [SerializeField]
        protected AState[] _states = null;

        protected Dictionary<Type, AState> _allStates = null;
        protected Dictionary<Type, AState> _allowedStates = null;

        public UIManager UIManager { get => _UIManager; }
        public PlayerController PlayerController { get => _playerController; }

        protected virtual void OnValidate()
        {
            if (_states == null)
            {
                return;
            }

            _allStates = _states.ToDictionary(x => x.GetType(), x => x);

            if (_currentState == null || _allStates.ContainsKey(_currentState.GetType()) == false)
            {
                _currentState = null;
                Debug.LogError("Current state is not in allowed states list");
                return;
            }

            _allowedStates = _currentState.States;
        }

        protected virtual void Awake()
        {
            OnValidate();
        }

        public void SetState<T>() where T : AState
        {
            Type stateID = typeof(T);

            if (_allowedStates.ContainsKey(stateID) == false)
            {
                return;
            }

            if (_currentState != null)
            {
                _currentState.OnExit();
            }

            _currentState = _allowedStates[stateID];
            _currentState.OnEnter();
            _allowedStates = _currentState.States;
        }

        public void AddStateOnEnterEffect<T>(Action callback) where T : AState
        {
            Type stateID = typeof(T);

            if (_allStates.ContainsKey(stateID) == false)
            {
                return;
            }
            AState desiredState = _allStates[stateID];
            desiredState.OnEnterAction.AddListener(() => callback?.Invoke());
        }

        public void AddStateOnExitEffect<T>(Action callback) where T : AState
        {
            Type stateID = typeof(T);

            if (_allStates.ContainsKey(stateID) == false)
            {
                return;
            }
            AState desiredState = _allStates[stateID];
            desiredState.OnExitAction.AddListener(() => callback?.Invoke());
        }

        public void ShowScreen<T>() where T : IUIScreen => _UIManager.ShowScreen<T>();
    }
}
