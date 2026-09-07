using System.Collections.Generic;
using DevLib.ModuleSystem;
using UnityEngine;

namespace Members.LYG._Scripts.Agents.BuffModule
{
        public class AbstractBuffModule: MonoModule, IBuffModule
        {
                [field: SerializeField]public List<Buff> Buffs { get; private set; } = new();

                public List<(float, BuffType)> GetValue(string buffName)
                {
                        List<(float, BuffType)> buffs = new();
                        foreach (IBuff buff in Buffs)
                        {
                                if (buff.Name == buffName)
                                        buffs.Add((buff.Value, buff.Type));
                        }
                        return buffs;
                }
                
                public void AppendBuff(BuffType buffType,  string buffName, float value)
                {
                        Buff buff = new Buff(buffType, buffName, value);
                        Buffs.Add(buff);
                }
        }
}