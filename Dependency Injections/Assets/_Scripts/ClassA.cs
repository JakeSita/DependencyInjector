using UnityEngine;

namespace DependencyInjection
{
    public class ClassA : MonoBehaviour
    {
        ServiceA ServiceA;
        [Inject]
        public void InjectServiceA(ServiceA serviceA)
        {
            this.ServiceA = serviceA;
        }
        
        
    }
}