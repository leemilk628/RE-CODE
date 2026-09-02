using System.Collections.Generic;
using UnityEngine;
using System;

namespace DevLib.ServiceLocator
{
        public static class ServiceLocator
        {
                private static readonly Dictionary<Type, object> _services = new();

                [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
                public static void InitializeServiceLocator()
                {
                        _services.Clear();
                }

                public static void Register<T>(T service)
                {
                        _services[typeof(T)] = service;
                        Debug.Log($"[ServiceLocator] Register: {typeof(T).Name} Registered : {service.GetType().Name}");
                }

                public static void Unregister<T>()
                {
                        _services.Remove(typeof(T));
                        Debug.Log($"[ServiceLocator] Unregister: {typeof(T).Name}");
                }


                public static T Get<T>()
                {
                        if(_services.TryGetValue(typeof(T), out var service))
                                return (T)service;
                        
                        Debug.LogWarning($"[ServiceLocator] {typeof(T).Name} not registered");
                        return default(T);
                }
        }
}