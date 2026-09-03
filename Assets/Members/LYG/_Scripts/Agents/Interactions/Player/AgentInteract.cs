using DevLib.ModuleSystem;
using UnityEngine;

namespace Members.LYG._Scripts.Agents.Interactions.Player
{
        public class AgentInteract: MonoModule, IInteract
        {
                [field:SerializeField] public float OverlapCircleSize { get;private set; }
                private Vector2 _playerPosition = new();
                
                public IInteractable InteractionObject { get; private set; }

                private void Update()
                {
                        DetectInteractable();
                }

                public void DetectInteractable()
                {
                        if(!Owner) return;
                        
                        _playerPosition = Owner.transform.position;
                        Collider2D[] results = Physics2D.OverlapCircleAll(_playerPosition, OverlapCircleSize);

                        if (results.Length <= 0) return;
                        
                        IInteractable closestInteractable = null;
                        float closestSqrDistance = float.PositiveInfinity;

                        foreach (Collider2D result in results)
                        {
                                if (!result.TryGetComponent(out IInteractable interactable))
                                        continue;

                                Vector2 closestPoint = result.ClosestPoint(_playerPosition);
                                float sqrDistance = (closestPoint - _playerPosition).sqrMagnitude;

                                if (sqrDistance >= closestSqrDistance)
                                        continue;

                                closestSqrDistance = sqrDistance;
                                closestInteractable = interactable;
                        }

                        InteractionObject = closestInteractable;
                }

                public bool Interact()
                {
                        if(InteractionObject == null) return false;
                        return InteractionObject?.Interaction() ??  false;
                }

#if  UNITY_EDITOR
                
                private void OnDrawGizmos()
                {
                        if (Owner == null) return;
                        _playerPosition = Owner.transform.position;
                        
                        Gizmos.color = Color.red;
                        Gizmos.DrawWireSphere(_playerPosition, OverlapCircleSize);
                }
#endif
        }
}