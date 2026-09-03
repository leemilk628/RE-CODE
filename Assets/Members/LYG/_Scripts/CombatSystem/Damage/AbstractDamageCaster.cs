using DevLib.ModuleSystem;
using UnityEngine;

namespace Members.LYG._Scripts.CombatSystem.Damage
{
        public abstract class AbstractDamageCaster: MonoModule
        {
                [field:SerializeField] private ContactFilter2D attackFilter;
                private AbstractDamageCalculator _damageCalculator;

                public override void Initialize(ModuleOwner owner)
                {
                        base.Initialize(owner);
                        _damageCalculator = owner.GetModule<AbstractDamageCalculator>();
                }

                protected virtual void DamageCaster()
                {
                }
        }
}