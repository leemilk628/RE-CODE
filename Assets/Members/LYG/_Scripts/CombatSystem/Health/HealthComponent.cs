using System;
using DevLib.ModuleSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Members.LYG._Scripts.CombatSystem.Health
{
        public class HealthComponent: MonoModule, IHealthComponent
        {
                private float _maxHp;
                private float _hp;
                private float _normalizedHp;
                
                public event Action HandleOnDeath;
                public event Action<float> HandleOnHeal;
                public event Action<float> HandleOnDamage;
                public event Action HealthChanged;
                
                [field:SerializeField]public float MaxHp { get; private set; }

                public float Hp
                {
                        get => _hp;
                        set
                        {
                                Debug.Log("value: " + value);
                                HealthChanged?.Invoke();
                                if(value < _hp)
                                {
                                        if (value <= 0)
                                        {
                                                _hp = 0;
                                                HandleOnDeath?.Invoke();
                                        }
                                        else
                                        {
                                                _hp = value;
                                        }

                                        HandleOnDamage?.Invoke(value);
                                }
                                else if (value > 0)
                                {
                                        _hp = value  > MaxHp ? MaxHp : value;

                                        HandleOnHeal?.Invoke(value);
                                }
                                Debug.Log("HP: " + _hp);
                        }
                        
                }

                public float NormalizedHp
                {
                        get => _normalizedHp;
                        private set => _normalizedHp = value;
                }


                private void OnEnable()
                {
                        Hp = MaxHp;
                        HealthChanged += NormalizingHp;
                        HealthChanged?.Invoke();
                }


                private void NormalizingHp()
                {
                        NormalizedHp =  Mathf.InverseLerp(0, MaxHp, _hp);
                }


                private void Update()
                {
                        Debug.Log("HP: " + _hp);
                        Debug.Log("NormalizedHp: " + _normalizedHp);

                        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
                                Hp -=  10;
                }
        }
}