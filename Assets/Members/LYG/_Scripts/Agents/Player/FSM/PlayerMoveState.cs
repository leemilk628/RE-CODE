using DevLib.FsmSystem.Runtime;
using UnityEngine;

namespace Members.LYG._Scripts.Agents.Player.FSM
{
        public class PlayerMoveState : ActionablePlayerState
        {
                public PlayerMoveState(GameObject owner, StateSO stateData) : base(owner, stateData)
                {
                }

                protected override bool OnUpdate()
                {
                        Vector2 inputDirection = _player.PlayerInput.InputDirection;
                        if (inputDirection.sqrMagnitude < MoveThreshold)
                        {
                                _player.ChangeState(PlayerState.IDLE);
                                return false;
                        }
            
                        _player.Mover.SetMove(inputDirection);
                        
                        return true;
                }

                public override void Exit()
                {
                        _player.Mover.StopImmediately();
                        base.Exit();
                }
        }
}