using DevLib.FsmSystem.Runtime;
using Members.LYG._Scripts.Agents.Interactions.Player;
using Members.LYG._Scripts.Agents.Player.FSM;
using Members.LYG._Scripts.Input;
using UnityEngine;

namespace Members.LYG._Scripts.Agents.Player
{
        public class PlayerController : AbstractAgent
        {
                [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
                [SerializeField] private StateListSO playerStateList;
                
                private StateMachine _stateMachine;
                public IInteract Interact { get; private set; }

                protected override void InitializeModules()
                {
                        base.InitializeModules();
                        Interact = GetModule<IInteract>();
                        PlayerInput.SetEnable();
                        UnRegisterEvent();
                        RegisterEvent();
                        _stateMachine = new StateMachine(gameObject, playerStateList.states);
                }
                private void OnDestroy()
                {
                        UnRegisterEvent();
                        
                        PlayerInput.SetDisable();
                }
                
                private void Update()
                {
                        _stateMachine?.UpdateMachine();
                }

                private void FixedUpdate()
                {
                        Mover.Move();
                }
                
                private void Start()
                {
                        ChangeState(PlayerState.IDLE);
                }
                
                public void ChangeState(PlayerState newState)=> _stateMachine?.ChangeState((int)newState);
                

                private void RegisterEvent()
                {
                        PlayerInput.OnMoveChanged += Mover.SetMove;
                        PlayerInput.OnDashHandled += Mover.Dash;
                }

                private void UnRegisterEvent()
                {
                        PlayerInput.OnMoveChanged -= Mover.SetMove;
                        PlayerInput.OnDashHandled -= Mover.Dash;
                }
        }
}