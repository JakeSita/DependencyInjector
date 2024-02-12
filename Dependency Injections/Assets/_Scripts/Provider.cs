using UnityEngine;

namespace DependencyInjection
{
    public class Provider : MonoBehaviour, IDependencyProvider
    {
        [Provide]
        public ServiceA ProvideServiceA()
        {
            var serviceA = new ServiceA();
            serviceA.Initalize("From Provider");
            return serviceA;
        }
        
        [Provide]
        public ServiceB ProvideServiceB()
        {
            var serviceB = new ServiceB();
            serviceB.Initalize("From Provider");
            return serviceB;
        }
        [Provide]
        public FactoryA ProvideFactoryA()
        {
            return new FactoryA();
        }
    }

    public class ServiceA
    {
        //dummy implementation
        public void Initalize(string message = null)
        {
            Debug.Log($"ServiceA Initialized({message}");
        }
    }
    
    public class ServiceB
    {
        //dummy implementation
        public void Initalize(string message = null)
        {
            Debug.Log($"ServiceB Initialized({message}");
        }
    }
    
    public class FactoryA
    {
        private ServiceA _cacheServiceA;
        public ServiceA CreateServiceA()
        {
            if (_cacheServiceA == null)
            {
                _cacheServiceA = new ServiceA();
            }
            return _cacheServiceA;
        }
    }
}