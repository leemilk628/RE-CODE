using DevLib.FsmSystem.Runtime;
using UnityEngine;

namespace Members.LYG._Scripts.Agents.Player.FSM
{
        public class PlayerIdleState : ActionablePlayerState
        {
                public PlayerIdleState(GameObject owner, StateSO stateData) : base(owner, stateData)
                {
                }

                protected override bool OnUpdate()
                {
                        Vector2 inputDirection = _player.PlayerInput.InputDirection;
                        if (inputDirection.sqrMagnitude > MoveThreshold)
                        {
                                _player.ChangeState(PlayerState.MOVE);
                                return false;
                        }
                        return true;
                }
        }
}