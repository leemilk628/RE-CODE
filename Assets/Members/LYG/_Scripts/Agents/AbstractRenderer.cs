using System;
using DevLib.ModuleSystem;
using UnityEngine;

namespace Members.LYG._Scripts.Agents
{
        public class AbstractRenderer: MonoModule,  IRenderer,IAnimateTrigger, IAfterInitModule
        {
                public Animator Animator { get; private set; }
                public Vector2 FacingDirection { get; private set; }
                
                public override void Initialize(ModuleOwner owner)
                {
                        base.Initialize(owner);
                        Animator = GetComponent<Animator>();
                }
                
                public void SetDirection(Vector2 direction)
                {
                        FacingDirection =  direction;
                }

                public void RenderClip(int clipHash)
                {
                        Animator.Play(clipHash,0,0);
                }

                public void RenderClipIfNotPlaying(int clipHash)
                {
                        if (Animator.GetCurrentAnimatorStateInfo(0).shortNameHash != clipHash)
                                RenderClip(clipHash);
                }
                
                public void AfterInit()
                {
                        if(Owner.GetModule<IMover>() is { } mover)
                        {
                                mover.OnMoveChanged += SetDirection;
                        }
                }

                private void OnDestroy()
                {
                        if(Owner.GetModule<IMover>() is { } mover)
                        {
                                mover.OnMoveChanged -= SetDirection;
                        }
                }

                public event Action OnFootStepTrigger;
                public event Action OnAnimationStart;
                public event Action OnAnimationEnd;
                
                public void FootStepTrigger() => OnFootStepTrigger?.Invoke();
                public void AnimationEnd() => OnAnimationEnd?.Invoke();
                public void AnimationStart() => OnAnimationStart?.Invoke();
        }
}