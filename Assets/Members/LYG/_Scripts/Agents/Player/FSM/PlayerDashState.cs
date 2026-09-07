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
                        Player.Mover.OnDashEnd += SetEndDash;
                        Player.Mover.SetCanMove(false);
                }

                protected override bool OnUpdate()
                {
                        Vector2 inputDirection = Player.PlayerInput.InputDirection;
                        if(_endDash)
                        {
                                if (inputDirection.sqrMagnitude < MoveThreshold)
                                {
                                        Player.Mover.SetCanMove(true);
                                        Player.ChangeState(PlayerState.IDLE);
                                        return false;
                                }
                                else if (inputDirection.sqrMagnitude > MoveThreshold)
                                {
                                        Player.Mover.SetCanMove(true);
                                        Player.ChangeState(PlayerState.MOVE);
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