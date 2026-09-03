using DevLib.ModuleSystem;
using Members.LYG._Scripts.CombatSystem.Damage;

namespace Members.LYG._Scripts.Agents
{
        public class AbstractAgent: ModuleOwner, IDamageable
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

                public void ApplyDamage(DamageData damageData)
                {
                        
                }
        }
}