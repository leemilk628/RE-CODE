using System.Collections.Generic;

namespace Members.LYG._Scripts.Agents.BuffModule
{
        public interface IBuffModule
        {
                public List<Buff>  Buffs { get; }
                public List<(float, BuffType)> GetValue(string buffName);
                public void AppendBuff(BuffType buffType, string buffName, float value);
        }
}