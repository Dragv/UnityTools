using UnityEngine;

namespace Vocario.GameFlow.UI
{
    public interface IUIScreen
    {
        GameObject GO { get; }
        void OnEnter();
        void OnExit(System.Action onFinish);
    }
}
