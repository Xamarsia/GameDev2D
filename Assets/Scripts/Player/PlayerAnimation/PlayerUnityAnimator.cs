using UnityEngine;

namespace Player.PlayerAnimation
{
    public class PlayerUnityAnimator : PlayerAnimator
    {
        [SerializeField] private Animator _animator;

        protected override void PlayAnimation(AnimationType animationType)
        {
            ResetAnimation();
            _animator.SetInteger(nameof(AnimationType), ((int)animationType));
        }

        private void ResetAnimation()
        {
            _animator.SetInteger(nameof(AnimationType), ((int)AnimationType.IDLE));
        }
    }
}