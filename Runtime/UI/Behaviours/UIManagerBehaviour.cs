using System;
using System.Collections.Generic;

namespace Vocario.GameFlow.UI
{
    public class UIManagerBehaviour : ContextMonoBehaviour
    {
        protected virtual void Awake()
        {
            Dictionary<Type, IUIScreen> screens = new Dictionary<Type, IUIScreen>();

            foreach (var screen in GetComponentsInChildren<IUIScreen>(true))
            {
                screen.GO.SetActive(false);
                screens.Add(screen.GetType(), screen);
            }

            _context.UIManager.UIBehaviour = this;
            _context.UIManager.SetScreens(screens);
        }
    }
}
