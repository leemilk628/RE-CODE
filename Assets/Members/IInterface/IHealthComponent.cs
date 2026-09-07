using System;

namespace Members.IInterface
{
        public interface IHealthComponent
        {
                public event Action HandleOnDeath ;
                public event Action<float> HandleOnHeal ;
                public event Action<float> HandleOnDamage ;
                public event Action HealthChanged;
                
                public float MaxHp { get; }
                public float Hp { get; }
                
                public float NormalizedHp { get; }
        }
}