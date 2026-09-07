namespace Members.IInterface
{
        public interface IInteract
        {
                float OverlapCircleSize { get; }
                void DetectInteractable();
                bool Interact();
        }
}