using Player.PlayerAnimation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAnimator : MonoBehaviour
{
    private AnimationType _currentAnimationType;

    public void PlayAnimation(AnimationType animationType, bool active)
    {
        if (!active)
        {
            if (_currentAnimationType == AnimationType.IDLE || animationType != _currentAnimationType)
            {
                return;
            }

            _currentAnimationType = AnimationType.IDLE;
            PlayAnimation(_currentAnimationType);
            return;
        }

        if (animationType <= _currentAnimationType)
        {
            return;
        }
        _currentAnimationType = animationType;
        PlayAnimation(_currentAnimationType);
    }

    protected abstract void PlayAnimation(AnimationType animationType);
  
}
