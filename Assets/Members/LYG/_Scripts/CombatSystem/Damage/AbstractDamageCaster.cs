using DevLib.ModuleSystem;
using UnityEngine;

namespace Members.LYG._Scripts.CombatSystem.Damage
{
        public abstract class AbstractDamageCaster: MonoModule
        {
                [field:SerializeField] private ContactFilter2D attackFilter;
                
                protected virtual void DamageCaster()
                {
                }
        }
}