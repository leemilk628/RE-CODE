using System;
using System.Collections.Generic;
using System.Linq;
using DevLib.ModuleSystem;
using Members.LYG._Scripts.CombatSystem.SkillSystem;
using UnityEngine;

namespace Members.LYG._Scripts.Agents.Player.FSM
{
    public class PlayerSkillModule : AbstractSkillModule, IAfterInitModule
    {
        [Serializable]
        public struct LoadoutEntry
        {
            public int slot;
            public SkillDataSO skillData;
        }

        [Tooltip("시작 시 각 슬롯에 장착될 기본 스킬")]
        [SerializeField] private List<LoadoutEntry> defaultLoadout = new();

        private readonly Dictionary<int, int> _slotToSkillId = new();

        private int _basicAttackId;
        private bool _hasBasicAttack;

        public int RequestedSkillId { get; private set; }
        public int? RequestedInputSlot { get; private set; }

        public event Action<int, SkillDataSO> OnSlotChanged; //UI가 구독해서 처리한다.


        // 스킬 딕셔너리가 다 채워지고 (base.Init에서) 그 뒤에 호출해서 정리작업을 수행한다.
        public void AfterInit()
        {
            CacheBasicAttack();
            BuildDefaultLoadout();
        }

        private void CacheBasicAttack()
        {
            ISkill basicSkill = SkillDict.Values
                .FirstOrDefault(s => s.SkillData.skillCategory == SkillCategory.BasicAttack);

            if (basicSkill == null)
            {
                Debug.LogWarning($"[PlayerSkillModule] 기본 공격이 누락되었습니다. : {gameObject}");
                return;
            }

            _basicAttackId = basicSkill.SkillData.skillIdHash;
            _hasBasicAttack = true; //기본 공격 소유중
        }

        private void BuildDefaultLoadout()
        {
            foreach (LoadoutEntry entry in defaultLoadout)
            {
                if (entry.skillData == null) continue;
                EquipSkill(entry.slot, entry.skillData.skillIdHash);
            }
        }

        #region 키 입력 해석 및 시전요청 (FSM에서 요청)

        public bool TryResolveBasicAttack(out int skillId)
        {
            skillId = _basicAttackId;
            return _hasBasicAttack;
        }

        public bool TryResolveSlot(int slot, out int skillId)
        {
            return _slotToSkillId.TryGetValue(slot, out skillId);
        }

        public bool TryRequestSkill(int skillId, int? inputSlot)
        {
            if (CurrentSkill is { IsUsing: true, CanInterrupt: true}) return false;

            if (!CanUseSkill(skillId)) return false;
            RequestedSkillId = skillId;
            RequestedInputSlot = inputSlot;
            return true;
        }

        #endregion


        #region UI 연동용 API

        public void EquipSkill(int entrySlot, int skillId)
        {
            if (!SkillDict.TryGetValue(skillId, out ISkill skill))
            {
                Debug.LogWarning($"[PlayerSkillModule] 장착하려는 스킬이 없습니다. : {skillId}");
                return;
            }

            _slotToSkillId[entrySlot] = skillId;
            OnSlotChanged?.Invoke(entrySlot, skill.SkillData);
        }

        public void UnEquipSkill(int entrySlot)
        {
            if (_slotToSkillId.Remove(entrySlot))
                OnSlotChanged?.Invoke(entrySlot, null);
        }

        public SkillDataSO GetSlotData(int slot)
        {
            return _slotToSkillId.TryGetValue(slot, out int skillId)
                   && SkillDict.TryGetValue(skillId, out ISkill skill)
                ? skill.SkillData
                : null;
        }

        public override float GetBaseDamage(SkillDataSO skillData)
        {
            return skillData.damageData.BaseDamage;
        }

        #endregion

        public SkillDataSO GetSkillData(int skillId) =>
            SkillDict.TryGetValue(skillId, out ISkill skill) ? skill.SkillData : null;
    }
}