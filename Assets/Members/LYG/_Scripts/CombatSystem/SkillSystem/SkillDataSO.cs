using Members.LYG._Scripts.CombatSystem.Damage;
using UnityEngine;

namespace Members.LYG._Scripts.CombatSystem.SkillSystem
{
        [CreateAssetMenu(fileName = "Skill Data", menuName = "Eric/Skill Data SO", order = 0)]
        public class SkillDataSO : ScriptableObject
        {
                public int skillIdHash;
                public string skillName;
                public string skillDescription;
                public Sprite skillIcon;
                public int maxSkillLevel;
                public int skillLevel;
                public SkillType skillType = SkillType.NoneDamage;
                public DirectionType directionType = DirectionType.Pointer;
                public SkillCategory skillCategory = SkillCategory.BasicAttack;
                public float coolTime = 1f;
                [SerializeField] public DamageData damageData;
                
        }
}