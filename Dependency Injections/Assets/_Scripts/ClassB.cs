using System;
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

        private void Start()
        {
            ServiceA.Initalize("ServiceA initialized from ClassB");
            ServiceB.Initalize("ServiceB initialized from ClassB");
            FactoryA.CreateServiceA().Initalize("FactoryA initialized from ClassB");
        }
    }
}