using UnityEngine;
using UnityEngine.InputSystem;

namespace Vocario.GameFlow
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerControllerBehaviour : ContextMonoBehaviour
    {
        protected PlayerInput _playerInput = null;
        protected PlayerPawnBehaviour _playerPawnBehaviour = null;

        protected virtual void Awake()
        {
            if (_context != null)
            {
                _context.PlayerController.ControllerBehaviour = this;
            }

            _playerPawnBehaviour = GetComponent<PlayerPawnBehaviour>();
            _playerInput = GetComponent<PlayerInput>();
        }

        protected virtual void OnEnable()
        {
            if (_playerInput == null)
            {
                return;
            }
            _playerInput.ActivateInput();
        }

        protected virtual void OnDisable()
        {
            if (_playerInput == null)
            {
                return;
            }
            _playerInput.DeactivateInput();
        }

        public void SetEnableInput(bool isEnabled)
        {
            if (isEnabled == true)
            {
                _playerInput.ActivateInput();
                return;
            }

            _playerInput.DeactivateInput();
        }
    }
}
