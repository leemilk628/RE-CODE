using UnityEngine;

namespace Members.LYG._Scripts.Agents.Interactions.Test
{
        public class TestInteract:MonoBehaviour,  IInteractable
        {
                [SerializeField] private string interactionMessage;
                public bool Interaction()
                {
                        Debug.Log(interactionMessage == "" ? "Interact" :  interactionMessage);
                        return true;
                }
        }
}