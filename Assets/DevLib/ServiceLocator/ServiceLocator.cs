using System.Collections.Generic;
using UnityEngine;
using System;

namespace DevLib.ServiceLocator
{
        public static class ServiceLocator
        {
                private static readonly Dictionary<Type, object> Services = new();

                [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
                public static void InitializeServiceLocator()
                {
                        Services.Clear();
                }

                public static void Register<T>(T service)
                {
                        Services[typeof(T)] = service;
                        Debug.Log($"[ServiceLocator] Register: {typeof(T).Name} Registered : {service.GetType().Name}");
                }

                public static void Unregister<T>()
                {
                        Services.Remove(typeof(T));
                        Debug.Log($"[ServiceLocator] Unregister: {typeof(T).Name}");
                }


                public static T Get<T>()
                {
                        if(Services.TryGetValue(typeof(T), out var service))
                                return (T)service;
                        
                        Debug.LogWarning($"[ServiceLocator] {typeof(T).Name} not registered");
                        return default(T);
                }
        }
}