using System;

namespace Members.IInterface
{
        public interface IAnimateTrigger
        {
                event Action OnFootStepTrigger;
                event Action OnAnimationStart;
                event Action OnAnimationEnd;
        }
}