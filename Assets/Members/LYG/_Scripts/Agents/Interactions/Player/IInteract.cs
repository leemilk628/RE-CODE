namespace Members.LYG._Scripts.Agents.Interactions.Player
{
        public interface IInteract
        {
                float OverlapCircleSize { get; }
                void DetectInteractable();
                bool Interact();
        }
}