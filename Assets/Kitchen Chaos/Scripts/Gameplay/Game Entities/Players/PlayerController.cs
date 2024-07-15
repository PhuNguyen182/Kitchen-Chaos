using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenChaos.Scripts.Inputs;

namespace KitchenChaos.Scripts.Gameplay.GameEntities.Players
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 6f;
        [SerializeField] private Transform playerGraphic;
        [SerializeField] private Transform groundPoint;
        [SerializeField] private Rigidbody playerBody;
        [SerializeField] private CapsuleCollider playerCollider;
        [SerializeField] private LayerMask checkGroundMask;
        [SerializeField] private PlayerAnimation playerAnimation;

        private Vector3 _inputVector;
        private Vector2 _movement;

        private void Awake()
        {

        }

        private void FixedUpdate()
        {
            HandleInput();
            MovePlayer();
        }

        private void HandleInput()
        {
            _movement = InputActionController.Instance.Movement;
        }

        private void MovePlayer()
        {
            _inputVector = new(_movement.x, 0, _movement.y);
            playerBody.MovePosition(playerBody.position + _inputVector * moveSpeed * Time.fixedDeltaTime);

            Vector3 playerForward = _inputVector;
            Vector3 forward = Vector3.Slerp(playerGraphic.forward, playerForward, Time.deltaTime * 18f);
            playerGraphic.forward = forward;
        }
    }
}
