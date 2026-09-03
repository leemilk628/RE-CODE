using DevLib.FsmSystem.Runtime;
using UnityEngine;

namespace Members.LYG._Scripts.Agents.Player.FSM
{
        public class PlayerDashState : ActionablePlayerState
        {
                private Vector2 _direction;
                private bool _endDash = false;
                
                
                public PlayerDashState(GameObject owner, StateSO stateData) : base(owner, stateData)
                {
                }
                public override void Enter()
                {
                        _endDash = false;
                        
                        base.Enter();
                        
                        Player.Mover.Dash(); 
                        Player.Mover.SetCanMove(false);
                }

                protected override bool OnUpdate()
                {
                        Player.Mover.OnDashEnd += SetEndDash;
                        Vector2 inputDirection = Player.PlayerInput.InputDirection;
                        if(_endDash)
                        {
                                if (inputDirection.sqrMagnitude < MoveThreshold)
                                {
                                        Player.Mover.StopImmediately();
                                        Player.ChangeState(PlayerState.IDLE);
                                        Player.Mover.SetCanMove(true);
                                        return false;
                                }
                                else if (inputDirection.sqrMagnitude > MoveThreshold)
                                {
                                        Player.Mover.StopImmediately();
                                        Player.ChangeState(PlayerState.MOVE);
                                        Player.Mover.SetCanMove(true);
                                        return false;
                                }
                        }
                        return true;
                }

                private void SetEndDash()
                {
                        _endDash = true;
                }
        }
}