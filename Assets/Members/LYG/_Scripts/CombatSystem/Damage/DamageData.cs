using System;
using DevLib.ModuleSystem;

namespace Members.LYG._Scripts.CombatSystem.Damage
{
        [Serializable]
        public struct DamageData
        {
                public ModuleOwner Dealer {get; set;}
                
                public float BaseDamage{get; set;}
                
                public float CriticalMultiplier{get; set;}
                public float CriticalPercent{get; set;}
                
                public float KnockBackMinForce{get; set;}
                public float KnockBackMaxForce{get; set;}
        }
}