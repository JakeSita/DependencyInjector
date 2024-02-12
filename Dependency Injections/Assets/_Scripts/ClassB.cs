using UnityEngine;

namespace DependencyInjection
{
    public class ClassB : MonoBehaviour
    {
        [Inject] public ServiceB ServiceB;
        FactoryA FactoryA;
        [Inject] public ServiceA ServiceA;
        [Inject] 
        public void Init(FactoryA factoryA)
        {
            this.FactoryA = factoryA;
        }
        
    }
}