using System;
using UnityEngine;

namespace DependencyInjection
{
    public class WinterEnviroment: MonoBehaviour, IEnviromentSystem, IDependencyProvider
    {
        [Provide]
        public WinterEnviroment ProvideEnviromentSystem()
        {
            return this;
        }
        
        public String ProvideString()
        {
            return "Hello Winter";
        }

        public void Initialize()
        {
            Debug.Log("Winter Enviroment System Initialized");
        }
    }
}