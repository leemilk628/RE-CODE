using DevLib.FsmSystem.Runtime;
using UnityEngine;

namespace Members.LYG._Scripts.Agents.Player.FSM
{
        public class PlayerMoveState : ActionablePlayerState
        {
                public PlayerMoveState(GameObject owner, StateSO stateData) : base(owner, stateData)
                {
                }

                public override void Enter()
                {
                        base.Enter();
                        Player.Mover.SetSpeed(SpeedType.Move);
                }

                protected override bool OnUpdate()
                {
                        Vector2 inputDirection = Player.PlayerInput.InputDirection;
                        if (Player.PlayerInput.IsSprint && inputDirection.sqrMagnitude > MoveThreshold)
                        {
                                Player.ChangeState(PlayerState.SPRINT);
                                return false;
                        }
                        
                        if (inputDirection.sqrMagnitude < MoveThreshold)
                        {
                                Player.ChangeState(PlayerState.IDLE);
                                return false;
                        }
            
                        Player.Mover.SetMove(inputDirection);
                        
                        return true;
                }

                public override void Exit()
                {
                        if (!Player.PlayerInput.IsSprint)
                                Player.Mover.Stop();
                        base.Exit();
                }
        }
}