using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


namespace DependencyInjection
{
    // This attribute is used to mark a field or method as a dependency
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
    public sealed class InjectAttribute : Attribute
    {
        public InjectAttribute()
        {
            
        }
    }
    
    // This attribute is used to mark a method as a provider
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ProvideAttribute : Attribute
    {
        public ProvideAttribute()
        {
            
        }
    }

    //Marker Interface no implementation required
    public interface IDependencyProvider
    {
        
    }

    public class Injector : Singleton<Injector>
    {
        const BindingFlags k_bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        readonly Dictionary<Type, object> registry = new Dictionary<Type, object>();

        protected override void Awake()
        {
            base.Awake();
            //Find all modules implementing IDependencyProvider 
            var providers = FindMonoBehaviours().OfType<IDependencyProvider>();
            foreach (var provider in providers)
            {
                RegisterProvider(provider);
            }

        }
        
        void RegisterProvider(IDependencyProvider provider)
        {
            var methods = provider.GetType().GetMethods(k_bindingFlags);
            foreach (var method in methods)
            {
                if (method.GetCustomAttribute<ProvideAttribute>() != null)
                {
                    var returnType = method.ReturnType;

                    var result = method.Invoke(provider, null);
                    if (result != null)
                    {
                        registry.Add(returnType, result);
                        Debug.Log($"Provider {provider.GetType().Name} registered {returnType.Name}");
                    }
                    else
                    {
                        throw new Exception($"Provider {provider.GetType().Name} returned null for {returnType.Name}");
                    }
                    
                }
            }
        }
        
        static MonoBehaviour[] FindMonoBehaviours()
        {
            return FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID);
        }
        
    }
    
    
    
}
