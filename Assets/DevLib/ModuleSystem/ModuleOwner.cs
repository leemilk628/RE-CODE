using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DevLib.ModuleSystem
{
    public abstract class ModuleOwner : MonoBehaviour
    {
        protected Dictionary<Type, IModule> ModuleDict;
        
        protected virtual void Awake()
        {
            ModuleDict = GetComponentsInChildren<IModule>().ToDictionary(m => m.GetType());
            
            InitializeModules();
            AfterInitializeModules();
        }

        protected virtual void InitializeModules()
        {
            foreach(IModule module in ModuleDict.Values)
                module.Initialize(this);
        }

        protected virtual void AfterInitializeModules()
        {
            foreach(IAfterInitModule module in ModuleDict.Values.OfType<IAfterInitModule>())
                module.AfterInit();
        }

        public T GetModule<T>()
        {
            if(ModuleDict.TryGetValue(typeof(T), out IModule module))
                return (T)module;
            
            IModule findModule = ModuleDict.Values.FirstOrDefault(m => m is T);

            if (findModule is T casted)
                return casted;
            
            return default;
        }
    }
}