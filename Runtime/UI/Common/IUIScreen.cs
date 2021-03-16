using UnityEngine;

namespace Flushed.GameFlow.UI
{
    public interface IUIScreen
    {
        GameObject GO { get; }
        void OnEnter();
        void OnExit(System.Action onFinish);
    }
}
