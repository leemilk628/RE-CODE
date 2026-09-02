using DevLib.ModuleSystem;

namespace Members.LYG._Scripts.Agents
{
        public class AbstractAgent: ModuleOwner
        {
                public IMover Mover { get; private set; }

                protected override void InitializeModules()
                {
                        base.InitializeModules();
                        Mover = GetModule<IMover>();
                }
        }
}