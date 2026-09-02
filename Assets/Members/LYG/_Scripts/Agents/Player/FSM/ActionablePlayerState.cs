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
                        _player.PlayerInput.OnDashHandled += HandleDashKey;
                }

                public override void Exit()
                {
                        _player.PlayerInput.OnDashHandled -= HandleDashKey;
                }

                private void HandleDashKey()
                {
                         _player.ChangeState(PlayerState.DASH);
                }
        }
}