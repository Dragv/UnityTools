using UnityEngine;

namespace Vocario.GameFlow
{
    public abstract class APlayerController : ScriptableObject
    {
        [HideInInspector]
        public PlayerControllerBehaviour ControllerBehaviour = null;

        [SerializeField]
        protected PlayerPawnBehaviour _playerPawnPrefab = null;
    }
}
