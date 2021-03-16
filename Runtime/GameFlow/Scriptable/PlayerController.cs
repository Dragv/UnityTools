using UnityEngine;

namespace Vocario.GameFlow
{
    public class PlayerController : ScriptableObject
    {
        [HideInInspector]
        public PlayerControllerBehaviour ControllerBehaviour = null;

        [SerializeField]
        protected PlayerPawnBehaviour _playerPawnPrefab = null;
    }
}
