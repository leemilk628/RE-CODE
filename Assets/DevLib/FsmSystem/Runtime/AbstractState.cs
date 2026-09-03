using UnityEngine;

namespace DevLib.FsmSystem.Runtime
{
    public class AbstractState
    {
        public GameObject Owner { get;private  set; }
        public StateSO StateSO { get;private  set; }

        protected AbstractState(GameObject owner, StateSO stateData)
        {
            Owner = owner;
            StateSO = stateData;
        }
        
        public virtual void Enter() {}

        public void Update() => OnUpdate();

        protected virtual bool OnUpdate() => false;
        
        public virtual void Exit() {}
    }
}