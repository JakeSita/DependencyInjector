using UnityEngine;

namespace DependencyInjection
{
    public interface IEnviromentSystem
    {
        IEnviromentSystem ProvideEnviromentSystem();
        void Initialize();
    }

    public class EnviromentSystem : MonoBehaviour, IDependencyProvider, IEnviromentSystem
    {
        [Provide]
        public IEnviromentSystem ProvideEnviromentSystem()
        {
            return this;
        }

        public void Initialize()
        {
            Debug.Log("Enviroment System Initialized");
        }
    }
}