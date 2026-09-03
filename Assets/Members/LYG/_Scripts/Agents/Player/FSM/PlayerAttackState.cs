using DevLib.FsmSystem.Runtime;
using UnityEngine;

namespace Members.LYG._Scripts.Agents.Player.FSM
{
        public class PlayerAttackState: AbstractPlayerState
        {
                public PlayerAttackState(GameObject owner, StateSO stateData) : base(owner, stateData)
                {
                }
        }
}