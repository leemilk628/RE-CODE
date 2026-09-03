using DevLib.ModuleSystem;

namespace Members.LYG._Scripts.Agents
{
        public class AbstractAgent: ModuleOwner
        {
                public IMover Mover { get; private set; }
                public IRenderer Renderer { get; private set; }
                public IAnimateTrigger Trigger { get; private set; }

                protected override void InitializeModules()
                {
                        base.InitializeModules();
                        Mover = GetModule<IMover>();
                        Renderer =  GetModule<IRenderer>();
                        Trigger =  GetModule<IAnimateTrigger>();
                }
        }
}