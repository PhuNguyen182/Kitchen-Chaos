using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenChaos.Scripts.Inputs;

namespace KitchenChaos.Scripts.Gameplay.GameEntities.Players
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 6f;
        [SerializeField] private float wallDistance = 0.15f;
        [SerializeField] private float wallCheckRadius = 1f;
        [SerializeField] private float rotateInterpolate = 18f;
        [SerializeField] private float playerHeight = 2f;
        [SerializeField] private Rigidbody playerBody;

        [Header("Physics Checking")]
        [SerializeField] private Transform playerGraphic;
        [SerializeField] private LayerMask checkObstacleMask;
        [SerializeField] private PlayerAnimation playerAnimation;

        private const float WallHitCheckThreshold = 0.5f;

        private float _hitNormalAngleSine;

        private Vector2 _movement;
        private Vector3 _inputVector;
        private RaycastHit _kitchenCounterHit;

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

            Vector3 playerForward = _inputVector;
            Vector3 forward = Vector3.Slerp(playerGraphic.forward, playerForward, Time.deltaTime * rotateInterpolate);
            playerGraphic.forward = forward;

            if (IsMovementInputActive())
            {
                if (!CheckPlayerCollision())
                {
                    playerBody.MovePosition(playerBody.position + _inputVector * moveSpeed * Time.fixedDeltaTime);
                }

                else
                {
                    Vector3 newInput = Vector3.zero;
                    if (Mathf.Abs(_hitNormalAngleSine) >= WallHitCheckThreshold && _inputVector.x != 0)
                        newInput = new(_inputVector.x, 0, 0);

                    else if (Mathf.Abs(_hitNormalAngleSine) <= WallHitCheckThreshold && _inputVector.z != 0)
                        newInput = new(0, 0, _inputVector.z);

                    playerBody.MovePosition(playerBody.position + newInput.normalized * moveSpeed * Time.fixedDeltaTime);
                }
            }
            
            playerAnimation.SetWalkingState(IsMovementInputActive());
        }

        private bool CheckPlayerCollision()
        {
            Vector3 heightPoint = transform.position + Vector3.up * playerHeight;
            bool isCollided = Physics.CapsuleCast(transform.position, heightPoint, wallCheckRadius, _inputVector
                                                  , out _kitchenCounterHit, wallDistance, checkObstacleMask);
            float angle = Mathf.Atan2(_kitchenCounterHit.normal.z, _kitchenCounterHit.normal.x);
            _hitNormalAngleSine = Mathf.Sin(angle);
            return isCollided;
        }

        private void HandleKitchenCounter()
        {

        }

        private bool IsMovementInputActive()
        {
            return _inputVector.sqrMagnitude > 0;
        }
    }
}
