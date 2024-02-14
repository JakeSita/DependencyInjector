using UnityEngine;

namespace DependencyInjection
{
    public class Provider : MonoBehaviour, IDependencyProvider
    {
        [SerializeField] private GameObject ProductAPrefab;
        
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
}