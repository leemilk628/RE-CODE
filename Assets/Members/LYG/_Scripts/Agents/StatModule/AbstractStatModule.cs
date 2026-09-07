using System.Collections.Generic;
using DevLib.ModuleSystem;
using Members.IInterface;
using UnityEngine;

namespace Members.LYG._Scripts.Agents.StatModule
{
        public class AbstractStatModule: MonoModule, IStatModule
        {
                [field:SerializeField]public List<Stat> Stats { get; private set; }  = new();

                private readonly Dictionary<string, float> _statDict = new();

                private void OnValidate()
                {
                        _statDict.Clear();
                        foreach (Stat stat in Stats)
                        {
                                stat.SetValue();
                                _statDict.Add(stat.Name, stat.Value);
                        }
                }

                public virtual float GetValue(string statName)
                {
                        return _statDict.TryGetValue(statName, out float value) ? value : throw new KeyNotFoundException($"Stat {statName} does not exist");
                }
        }
}