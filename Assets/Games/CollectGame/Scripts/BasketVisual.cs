using System;
using UnityEngine;

namespace Games.CollectGame
{
    public class BasketVisual : MonoBehaviour
    {
        private static readonly int FreezeHash = Animator.StringToHash("freeze");
        private static readonly int UnfreezeHash = Animator.StringToHash("unfreeze");
        private static readonly int DieHash = Animator.StringToHash("die");
        private static readonly int IsMovingHash = Animator.StringToHash("isMoving");


        [SerializeField] private Basket basket;
        [SerializeField] private Animator animator;
        [SerializeField] private Freezer freezer;
        [SerializeField] private GameObject frozenVisual;


        private void Awake()
        {
            basket.OnDead += OnDead;
            
            freezer.OnFreeze += OnFreeze;
            freezer.OnUnfreeze += OnUnfreeze;
        }

        private void OnDestroy()
        {
            basket.OnDead -= OnDead;
            
            freezer.OnFreeze -= OnFreeze;
            freezer.OnUnfreeze -= OnUnfreeze;
        }

        private void Update()
        {
            SetIsMoving(basket.IsMoving());
        }


        private void OnDead()
        {
            animator.SetTrigger(DieHash);
        }

        private void OnFreeze()
        {
            animator.SetTrigger(FreezeHash);
        }

        private void OnUnfreeze()
        {
            animator.SetTrigger(UnfreezeHash);
        }

        private void SetIsMoving(bool value)
        {
            animator.SetBool(IsMovingHash, value);
        }
    }
}