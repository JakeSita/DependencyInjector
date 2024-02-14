using System;
using UnityEngine;

namespace DependencyInjection
{
    public class ClassB : MonoBehaviour
    {
        [Inject] public ServiceB ServiceB;
        [Inject] public ServiceA ServiceA;
        [Inject] private WinterEnviroment winter;
        

        private void Start()
        {
            ServiceA.Initalize("ServiceA initialized from ClassB");
            ServiceB.Initalize("ServiceB initialized from ClassB");
        }
    }
}