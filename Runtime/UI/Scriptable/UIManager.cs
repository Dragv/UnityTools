using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vocario.GameFlow.UI
{
    public class UIManager : ScriptableObject
    {
        [HideInInspector]
        public UIManagerBehaviour UIBehaviour = null;
        protected IUIScreen _currentScreen;
        protected Dictionary<Type, IUIScreen> _screens = null;

        public void SetScreens(Dictionary<Type, IUIScreen> screens)
        {
            _screens = screens;
        }

        public void ShowScreen<T>() where T : IUIScreen
        {
            var screenType = typeof(T);
            if (_currentScreen != null)
            {
                _currentScreen.OnExit(() => EnterNewScreen(screenType));
            }

            EnterNewScreen(screenType);
        }

        protected void EnterNewScreen(Type screenType)
        {
            if (_screens.ContainsKey(screenType) == false)
            {
                return;
            }

            _currentScreen = _screens[screenType];
            _currentScreen.OnEnter();
        }
    }
}
