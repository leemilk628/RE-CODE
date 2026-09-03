using System;

namespace Members.LYG._Scripts.Agents
{
        public interface IAnimateTrigger
        {
                event Action OnFootStepTrigger;
                event Action OnAnimationStart;
                event Action OnAnimationEnd;
        }
}