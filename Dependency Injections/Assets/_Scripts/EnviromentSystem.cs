using UnityEngine;
using System;

namespace DependencyInjection
{
    public interface IEnviromentSystem
    {
        void Initialize();
        String ProvideString();
    }

    public class EnviromentSystem : MonoBehaviour, IDependencyProvider, IEnviromentSystem
    {
        [Provide]
        public IEnviromentSystem ProvideEnviromentSystem()
        {
            return this;
        }
        public String ProvideString()
        {
            return "Hello World";
        }
        public void Initialize()
        {
            Debug.Log("Enviroment System Initialized");
        }
    }
}