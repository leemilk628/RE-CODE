using DevLib.FsmSystem.Runtime;
using UnityEngine;

namespace Members.LYG._Scripts.Agents.Player.FSM
{
        public abstract class AbstractPlayerState : AbstractState
        {
                protected PlayerController _player;
        
                protected const float MoveThreshold = 0.01f;

                protected AbstractPlayerState(GameObject owner, StateSO stateData) : base(owner, stateData)
                {
                        _player = owner.GetComponent<PlayerController>();
                        Debug.Assert(_player != null, "PlayerController is null. PlayerState should bew child of PlayerController");
            
                }
        }
}