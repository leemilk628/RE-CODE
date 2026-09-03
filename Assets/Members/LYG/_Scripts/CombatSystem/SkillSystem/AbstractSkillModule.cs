using System;
using System.Collections.Generic;
using System.Linq;
using DevLib.ModuleSystem;
using UnityEngine;

namespace Members.LYG._Scripts.CombatSystem.SkillSystem
{
        public abstract class AbstractSkillModule : MonoModule, ISkillModule
        {
                public event Action<int> OnSkillEnd;

                protected Dictionary<int, ISkill> SkillDict;
                private List<IInitializeSkill> InitializeSkills;
        
                public ISkill CurrentSkill { get; private set; }

                public override void Initialize(ModuleOwner owner)
                {
                        base.Initialize(owner);
                        SkillDict = GetComponentsInChildren<ISkill>()
                                .ToDictionary(s => s.SkillData.skillIdHash);
                        
                        InitializeSkills = GetComponentsInChildren<IInitializeSkill>().ToList();
            
                        foreach(IInitializeSkill initSkill in InitializeSkills)
                                initSkill.InitializeSkill(this);
                }


                public bool CanUseSkill(int skillId, GameObject target = null)
                {
                        if (SkillDict.TryGetValue(skillId, out ISkill skill))
                        {
                                return skill.CanUseSkill(target);
                        }

                        return false;
                }

       
                public void UseSkill(int skillId, GameObject target = null)
                {
                        if (SkillDict.TryGetValue(skillId, out ISkill skill))
                        {
                                if (CurrentSkill is { IsUsing: true })
                                {
                                        ISkill oldSkill = CurrentSkill;
                                        CurrentSkill = null;
                                        oldSkill.OnSkillEnd -= HandleSkillEnd; 
                                        oldSkill.StopSkill();
                                }
                
                                CurrentSkill = skill;
                                CurrentSkill.OnSkillEnd += HandleSkillEnd;
                                CurrentSkill.UseSkill(target);
                        }
                }
        
                private void HandleSkillEnd(ISkill endSkill)
                {
                        endSkill.OnSkillEnd -= HandleSkillEnd; 
                        int skillId = endSkill.SkillData.skillIdHash;
                        if(endSkill == CurrentSkill)
                                CurrentSkill = null;
                        OnSkillEnd?.Invoke(skillId);
                }
                
                public abstract float GetBaseDamage(SkillDataSO skillData);
        }
}