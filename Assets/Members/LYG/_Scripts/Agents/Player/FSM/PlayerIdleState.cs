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
                        Vector2 inputDirection = Player.PlayerInput.InputDirection;
                        if (inputDirection.sqrMagnitude > MoveThreshold)
                        {
                                Player.ChangeState(PlayerState.MOVE);
                                return false;
                        }
                        return true;
                }
        }
}