using UnityEngine;
using UnityEngine.InputSystem;

namespace Vocario.GameFlow
{
    [RequireComponent(typeof(PlayerInput))]
    public abstract class APlayerControllerBehaviour : ContextMonoBehaviour
    {
        protected PlayerInput _playerInput = null;
        protected APlayerPawnBehaviour _playerPawnBehaviour = null;

        protected virtual void Awake()
        {
            if (_context != null)
            {
                _context.APlayerController.ControllerBehaviour = this;
            }

            _playerPawnBehaviour = GetComponent<APlayerPawnBehaviour>();
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
