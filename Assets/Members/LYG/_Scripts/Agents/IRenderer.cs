using UnityEngine;

namespace Members.LYG._Scripts.Agents
{
        public interface IRenderer
        {
                Animator Animator { get; }
                Vector2 FacingDirection { get; }
                void SetDirection(Vector2 direction);
                void RenderClip(int clipHash);
                void RenderClipIfNotPlaying(int clipHash);
        }
}