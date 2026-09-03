using DevLib.FsmSystem.Runtime;
using Members.LYG._Scripts.CombatSystem.SkillSystem;
using UnityEngine;

namespace Members.LYG._Scripts.Agents.Player.FSM
{
        public class PlayerSkillState: AbstractPlayerState
        {   
            private PlayerSkillModule _skillModule;
        private ISkill _currentSkill;
        private bool _isSkillEnd;
        private int? _castInputSlot;
        
        public PlayerSkillState(GameObject owner, StateSO stateData) : base(owner, stateData)
        {
            _skillModule = Player.GetModule<PlayerSkillModule>();
        }

        public override void Enter()
        {
            _isSkillEnd = false;
            _skillModule.OnSkillEnd += HandleSkillEnd;

            _castInputSlot = _skillModule.RequestedInputSlot;
            
            ApplyAimFacing(_skillModule.RequestedSkillId);
            
            _skillModule.UseSkill(_skillModule.RequestedSkillId);
            _currentSkill = _skillModule.CurrentSkill; 
            
            Player.PlayerInput.OnAttackHandled += HandleAttackDuringSkill;
            Player.PlayerInput.OnSkillHandled += HandleSkillDuringSkill;
        }

        protected override bool OnUpdate()
        {
            _currentSkill?.OnUpdateSkill();
            if (_isSkillEnd)
            {
                Player.ChangeState(PlayerState.IDLE);
                return false;
            }
            
            return true;
        }

        public override void Exit()
        {
            Player.PlayerInput.OnAttackHandled -= HandleAttackDuringSkill;
            Player.PlayerInput.OnSkillHandled -= HandleSkillDuringSkill;
            
            _skillModule.OnSkillEnd -= HandleSkillEnd;
            
            if(_currentSkill is {IsUsing: true})
                _currentSkill.StopSkill();
            base.Exit();
        }

        private void HandleSkillEnd(int skillId) => _isSkillEnd = true;
        
        private void ApplyAimFacing(int skillId)
        {
            SkillDataSO skillData = _skillModule.GetSkillData(skillId);
            if (skillData == null || skillData.directionType != DirectionType.Pointer) return;

            Vector3 mouseWorldPosition = Player.PlayerInput.MousePosition;
            Vector2 direction = ((Vector2)(mouseWorldPosition - Player.transform.position)).normalized;
            Player.Renderer.SetDirection(direction);
        }

        #region 시전 중 입력 처리 핸들러

        private void HandleAttackDuringSkill(bool isPressed)
        {
            if (!isPressed)
            {
                if (_castInputSlot == null)
                    _currentSkill?.OnReleaseInput();
                return;
            }

            if (_skillModule.TryResolveBasicAttack(out int id))
                TryCancelInto(id, null);
        }

        private void HandleSkillDuringSkill(int slot, bool isPressed)
        {
            if (!isPressed) 
            {
                if (_castInputSlot == slot)
                    _currentSkill?.OnReleaseInput();
                return;
            }
            if(_skillModule.TryResolveSlot(slot, out int id))
                TryCancelInto(id, slot);
        }
        
        private void TryCancelInto(int skillId,  int? inputSlot)
        {
            if (_currentSkill is { CanInterrupt: true } && _skillModule.TryRequestSkill(skillId, inputSlot))
                Player.ChangeState(PlayerState.SKILL);
        }
        
        #endregion
        }
}