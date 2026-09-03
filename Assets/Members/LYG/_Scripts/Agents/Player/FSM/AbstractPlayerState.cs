using DevLib.FsmSystem.Runtime;
using UnityEngine;

namespace Members.LYG._Scripts.Agents.Player.FSM
{
        public abstract class AbstractPlayerState : AbstractState
        {
                protected readonly PlayerController Player;
        
                protected const float MoveThreshold = 0.0001f;

                protected AbstractPlayerState(GameObject owner, StateSO stateData) : base(owner, stateData)
                {
                        Player = owner.GetComponent<PlayerController>();
                        Debug.Assert(Player != null, "PlayerController is null. PlayerState should bew child of PlayerController");
                }
        }
}