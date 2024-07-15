using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos.Scripts.Gameplay.GameEntities.Players
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator characterAnimator;

        private readonly int _isWalkingHash = Animator.StringToHash("IsWalking");

        public void SetWalkingState(bool isWalking)
        {
            characterAnimator.SetBool(_isWalkingHash, isWalking);
        }
    }
}
