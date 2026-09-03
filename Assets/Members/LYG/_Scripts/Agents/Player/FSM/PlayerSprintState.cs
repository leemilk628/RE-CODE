using DevLib.FsmSystem.Runtime;
using UnityEngine;

namespace Members.LYG._Scripts.Agents.Player.FSM
{
        public class PlayerSprintState: ActionablePlayerState
        {
                public PlayerSprintState(GameObject owner, StateSO stateData) : base(owner, stateData)
                {
                }

                public override void Enter()
                {
                        base.Enter();
                        Player.Mover.SetSpeed(SpeedType.Sprint);
                }

                protected override bool OnUpdate()
                {
                        Vector2 inputDirection = Player.PlayerInput.InputDirection;
                        if (!Player.PlayerInput.IsSprint && inputDirection.sqrMagnitude > MoveThreshold)
                        {
                                Player.ChangeState(PlayerState.MOVE);
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
                        Player.Mover.Stop();
                        base.Exit();
                }
        }
}