using System.Collections.Generic;
using Members.LYG._Scripts.Agents.StatModule;

namespace Members.IInterface
{
        public interface IStatModule
        {
                public List<Stat>  Stats { get; }
        }
}