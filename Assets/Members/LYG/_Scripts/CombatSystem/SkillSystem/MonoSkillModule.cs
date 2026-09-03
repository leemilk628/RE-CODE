using UnityEngine;

namespace Members.LYG._Scripts.CombatSystem.SkillSystem
{
        public class MonoSkillModule: MonoBehaviour, IInitializeSkill
        {
                public ISkillModule SkillModule { get; private set; }
                
                public virtual void InitializeSkill(ISkillModule skillModule)
                {
                        SkillModule = skillModule;
                }
        }
}