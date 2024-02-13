using UnityEngine;

namespace DependencyInjection
{
    public class ClassA : MonoBehaviour
    {
        ServiceA ServiceA;

        [Inject] private IEnviromentSystem enviroment;
        [Inject]
        public void InjectServiceA(ServiceA serviceA)
        {
            this.ServiceA = serviceA;
            enviroment.Initialize();
        }
    }
}