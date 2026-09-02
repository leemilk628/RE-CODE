using DevLib.ModuleSystem;
using UnityEngine;

namespace Members.LYG._Scripts.Agents
{
        public class AbstractRenderer: MonoModule,  IRenderer, IAfterInitModule
        {
                public Animator Animator { get; private set; }
                public Vector2 FacingDirection { get; private set; }
                
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
        }
}