using UnityEngine;

namespace Members.LYG._Scripts.CombatSystem
{
        public abstract class AbstractDamageCalculator
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