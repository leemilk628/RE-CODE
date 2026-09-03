using DevLib.FsmSystem.Runtime;
using UnityEngine;

namespace Members.LYG._Scripts.Agents.Player.FSM
{

        public abstract class ActionablePlayerState: AbstractPlayerState
        {
                protected ActionablePlayerState(GameObject owner, StateSO stateData) : base(owner, stateData)
                {
                }

                public override void Enter()
                {
                        base.Enter();
                        Player.PlayerInput.OnDashHandled += HandleDashKey;
                        Player.PlayerInput.OnInteractHandled += HandleInteractKey;
                        Player.PlayerInput.OnAttackHandled += HandleAttackKey;
                }

                public override void Exit()
                {
                        Player.PlayerInput.OnDashHandled -= HandleDashKey;
                        Player.PlayerInput.OnInteractHandled -= HandleInteractKey;
                        Player.PlayerInput.OnAttackHandled -= HandleAttackKey;
                }

                private void HandleDashKey()
                {
                         Player.ChangeState(PlayerState.DASH);
                }

                private void HandleInteractKey()
                {
                        Player.Interact.Interact();
                }

                private void HandleAttackKey(bool obj)
                {
                }
        }
}