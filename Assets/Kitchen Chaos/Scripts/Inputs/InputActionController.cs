using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KitchenChaos.Scripts.Inputs
{
    public class InputActionController : MonoBehaviour
    {
        private PlayerControllerInputAction _inputActions;

        public bool Interact => _inputActions.PlayerController.Interact.WasPressedThisFrame();
        public bool InteractAlternative => _inputActions.PlayerController.InteractAlternative.WasPressedThisFrame();
        public Vector2 Movement { get; private set; }

        private void Awake()
        {
            _inputActions = new();

            _inputActions.PlayerController.Movement.started += MovementHandle;
            _inputActions.PlayerController.Movement.performed += MovementHandle;
            _inputActions.PlayerController.Movement.canceled += MovementHandle;
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void MovementHandle(InputAction.CallbackContext context)
        {
            Movement = context.ReadValue<Vector2>();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        private void OnDestroy()
        {
            _inputActions?.Dispose();
        }
    }
}
