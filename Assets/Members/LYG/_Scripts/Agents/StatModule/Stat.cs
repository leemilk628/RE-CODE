using System;
using Members.IInterface;
using UnityEngine;

namespace Members.LYG._Scripts.Agents.StatModule
{
        [Serializable]
        public class Stat: IStat
        {
                public string Name { get; private set; }
                public float Value { get; private set; }
                public float MinValue { get;private set;  }
                public float MaxValue { get;private set;  }

                public void SetValue()
                {
                        Value = Mathf.Clamp(Value, MinValue, MaxValue);
                }
        }
}