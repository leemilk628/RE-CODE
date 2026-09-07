using System;

namespace Members.LYG._Scripts.Agents.BuffModule
{
        [Serializable]
        public class Buff: IBuff
        {
                public BuffType Type { get; private set; }
                public string Name { get; private set; }
                public float Value { get; private set; }

                public Buff(BuffType type, string name, float value)
                {
                        Type = type;
                        Name = name;
                        Value = value;
                }
        }
}