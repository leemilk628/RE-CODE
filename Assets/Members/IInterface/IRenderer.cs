using UnityEngine;

namespace Members.IInterface
{
        public interface IRenderer
        {
                Animator Animator { get; }
                Vector2 FacingDirection { get; }
                void SetDirection(Vector2 direction);
                void RenderClip(int clipHash);
                void RenderClipIfNotPlaying(int clipHash);
                public void FlipX(Vector2 mouseDirection);
        }
}