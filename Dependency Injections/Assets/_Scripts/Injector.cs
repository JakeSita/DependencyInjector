using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = System.Object;


namespace DependencyInjection
{
    // This attribute is used to mark a field or method as a dependency
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
    public sealed class InjectAttribute : Attribute {}
    
    // This attribute is used to mark a method as a provider
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ProvideAttribute : Attribute {}

    //Marker Interface no implementation required
    public interface IDependencyProvider
    {
        
    }
    [DefaultExecutionOrder(-10000)]
    //This class is used to inject dependencies into a MonoBehaviour
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
            //Find injectable objects and inject their dependencies
            var injectables = FindMonoBehaviours().Where(IsInjectable);
            foreach (var injectable in injectables)
            {
                Inject(injectable);
            }
            


        }

        void Inject(Object instance)
        {
            var type = instance.GetType();
            var fields = type.GetFields(k_bindingFlags)
                .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));
            
            foreach(var injectableFields in fields)
            {
                var fieldType = injectableFields.FieldType;
                var value = Resolve(fieldType);
                if (value == null)
                {
                    throw new Exception($"No provider found for {fieldType.Name} into {type.Name}");
                }
                injectableFields.SetValue(instance, value);
                Debug.Log($" Field injected {fieldType.Name} into {type.Name}");
                
            }
            
            var injectableMethods = type.GetMethods(k_bindingFlags)
                .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));
            
            foreach (var injectableMethod in injectableMethods)
            {
                var parameters = injectableMethod.GetParameters()
                    .Select(paramenter => paramenter.ParameterType)
                    .ToArray();
                
                var resolvedInstances = parameters.Select(Resolve).ToArray();

                if (resolvedInstances.Any(resolvedInstance => resolvedInstance == null))
                {
                    throw new Exception($"Falied to resolve parameters for {injectableMethod.Name} into {type.Name}");
                }
                injectableMethod.Invoke(instance, resolvedInstances);
                Debug.Log($"Method Injected {injectableMethod.Name} into {type.Name}");

            }
        }
        
        Object Resolve(Type type)
        {
            if (registry.TryGetValue(type, out var result))
            {
                return result;
            }
            throw new Exception($"No provider found for {type.Name}");
        }

        static bool IsInjectable(MonoBehaviour obj)
        {
            var members = obj.GetType().GetMembers(k_bindingFlags);
            return members.Any(member => Attribute.IsDefined(member, typeof(InjectAttribute)));
        }
        //This method is used to inject dependencies into a MonoBehaviour
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
        //This method is used to inject dependencies into a MonoBehaviour
        static MonoBehaviour[] FindMonoBehaviours()
        {
            return FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID);
        }
        
    }
    
    
    
}
