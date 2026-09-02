using DevLib.FsmSystem.Runtime;
using UnityEngine;

namespace Members.LYG._Scripts.Agents.Player.FSM
{
        public class PlayerDashState : ActionablePlayerState
        {
                private Vector2 direction;
                private bool endDash = false;
                
                
                public PlayerDashState(GameObject owner, StateSO stateData) : base(owner, stateData)
                {
                }
                public override void Enter()
                {
                        endDash = false;
                        
                        base.Enter();
                        
                        _player.Mover.Dash();
                        //_player.Mover.SetCanMove(false);
                }

                protected override bool OnUpdate()
                {
                        Vector2 inputDirection = _player.PlayerInput.InputDirection;
                        if(endDash)
                        {
                                if (inputDirection.sqrMagnitude < MoveThreshold)
                                {
                                        _player.Mover.StopImmediately();
                                        _player.ChangeState(PlayerState.IDLE);
                                        _player.Mover.SetCanMove(true);
                                        return false;
                                }
                                else if (inputDirection.sqrMagnitude > MoveThreshold)
                                {
                                        _player.Mover.StopImmediately();
                                        _player.ChangeState(PlayerState.DASH);
                                        _player.Mover.SetCanMove(true);
                                        return false;
                                }
                        }
                        return true;
                }

                private void SetEndDash()
                {
                        endDash = true;
                }
        }
}