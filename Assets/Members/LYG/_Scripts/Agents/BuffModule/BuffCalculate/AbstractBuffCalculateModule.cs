using System.Collections.Generic;
using DevLib.ModuleSystem;

namespace Members.LYG._Scripts.Agents.BuffModule.BuffCalculate
{
        public class AbstractBuffCalculateModule: MonoModule, IBuffCalculateModule
        {
                public float BuffCalculate(string buffName, float baseStat)
                {
                        IBuffModule buffModule = Owner.GetModule<IBuffModule>();
                        float resultValue = baseStat;
                        List<(float, BuffType)> buffs = new();
                        buffs = buffModule.GetValue(buffName);
                        
                        AddBuff(buffs, resultValue);
                        PercentageBuff(buffs, resultValue);
                        MultiplierBuff(buffs, resultValue);
                        
                        return resultValue;
                }

                private float AddBuff(List<(float, BuffType)> buffs, float value)
                {
                        foreach ((float, BuffType) buff in buffs)
                        {
                                float buffValue;
                                BuffType buffType;
                                (buffValue, buffType) = buff;
                                if (buffType == BuffType.Add)
                                        value += buffValue;
                        }
                        return value;
                }
                private float PercentageBuff(List<(float, BuffType)> buffs, float value)
                {
                        foreach ((float, BuffType) buff in buffs)
                        {
                                float buffValue;
                                BuffType buffType;
                                (buffValue, buffType) = buff;
                                if (buffType == BuffType.Percentage)
                                        value += value * (buffValue / 100);
                        }
                        return value;
                }
                private float MultiplierBuff(List<(float, BuffType)> buffs, float value)
                {
                        foreach ((float, BuffType) buff in buffs)
                        {
                                float buffValue;
                                BuffType buffType;
                                (buffValue, buffType) = buff;
                                if (buffType == BuffType.Multiplier)
                                        value *= buffValue;
                        }
                        return value;
                }
        }
}