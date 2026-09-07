using System;
using Members.LYG._Scripts.CombatSystem.SkillSystem;
using UnityEngine;

namespace Members.IInterface
{
        public interface ISkill
        {
                public event Action<ISkill> OnSkillEnd;
                
                SkillDataSO SkillData { get; }
                
                float NormalizedCoolTime {get; }
                bool IsUsing { get; }
                bool CanInterrupt { get; }
                bool CanUseSkill(GameObject target = null);
                void UseSkill(GameObject target = null);

                void OnUpdateSkill();
                void OnReleaseInput();
                void StopSkill();
                void CleanUpSkillData();
        }
}