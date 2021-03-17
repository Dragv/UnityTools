using UnityEngine;

namespace Vocario.GameFlow
{
    public abstract class APlayerController : ScriptableObject
    {
        [HideInInspector]
        public APlayerControllerBehaviour ControllerBehaviour = null;

        [SerializeField]
        protected APlayerPawnBehaviour _playerPawnPrefab = null;
    }
}
