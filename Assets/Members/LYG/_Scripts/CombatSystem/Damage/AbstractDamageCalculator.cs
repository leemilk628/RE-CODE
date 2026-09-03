using DevLib.ModuleSystem;
using UnityEngine;

namespace Members.LYG._Scripts.CombatSystem.Damage
{
        public abstract class AbstractDamageCalculator: MonoModule
        {
                private float _lastDamage;
                protected virtual float CalculateCriticalDamage(float damage, float critical, float criticalPercent)
                {
                        _lastDamage = damage;
                        bool isCritical = Random.Range(0, 100) < criticalPercent;
                        if(isCritical) _lastDamage += _lastDamage * critical;
                        return _lastDamage;
                }
        }
}