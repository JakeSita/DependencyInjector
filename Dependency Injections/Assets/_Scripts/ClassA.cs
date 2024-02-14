using UnityEngine;
using factory;

namespace DependencyInjection
{
    public class ClassA : MonoBehaviour
    {
        [SerializeField] private GameObject ProductAPrefab;
        ServiceA ServiceA;
        FactoryA factoryA;
        [Inject]
        public void InjectServiceA(ServiceA serviceA)
        {
            this.ServiceA = serviceA;
        }
        
        [Inject]
        public void InjectFactory(FactoryA factory)
        {
            this.factoryA = factory.ProvideFactoryA();
        }
        
        
        private void Start()
        {
            ServiceA.Initalize("ServiceA initialized from ClassA");
            factoryA.APrefab = ProductAPrefab.GetComponent<ProductA>();
            factoryA.GetProduct();
        }
        
        
    }
}