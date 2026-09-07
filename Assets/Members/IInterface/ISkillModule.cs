using System;
using DevLib.ModuleSystem;
using UnityEngine;

namespace Members.IInterface
{
        public interface ISkillModule
        {
                event Action<int> OnSkillEnd;
                ModuleOwner Owner { get; }
                ISkill CurrentSkill { get; }
                bool CanUseSkill(int skillId, GameObject target = null);
                void UseSkill(int skillIde, GameObject target = null);
        }
}