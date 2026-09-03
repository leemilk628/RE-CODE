using System;
using UnityEngine;

namespace Members.LYG._Scripts.CombatSystem.SkillSystem
{
        public class AbstractSkill: MonoSkillModule, ISkill
        {
                public event Action<ISkill> OnSkillEnd;
                
                [field:SerializeField]public SkillDataSO SkillData { get; private set; }

                public float NormalizedCoolTime
                {
                        get
                        {
                                if (SkillData == null || SkillData.coolTime <= 0) return 0f;
                                return Mathf.Clamp01(1f - (Time.time - _lastUseTime)/SkillData.coolTime);
                        }
                }
                public bool IsUsing { get; private set; }
                public bool CanInterrupt { get; private set; }
                
                private float _lastUseTime = float.NegativeInfinity;

                public virtual bool CanUseSkill(GameObject target = null)
                { 
                        return false;
                }

                public virtual void UseSkill(GameObject target = null)
                {
                        IsUsing = true;
                }

                public virtual void OnUpdateSkill() { }

                public virtual void OnReleaseInput() { }
                
                public void StopSkill()
                {
                        CleanUpSkillData();
                }

                public virtual void CleanUpSkillData()
                {
                        _lastUseTime = Time.time;
                        IsUsing = false;
                        OnSkillEnd?.Invoke(this);
                }
        }
}