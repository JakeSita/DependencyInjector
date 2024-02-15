using UnityEngine;
using factory;
using Unity.VisualScripting;

namespace DependencyInjection
{
    public class ClassA : MonoBehaviour
    {
        ServiceA ServiceA;
        FactoryA factoryA;
        
        [SerializeField] private Vector2 pos;
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
        }
        
        //on right click call factoryA.get product at a mouse position
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                factoryA.GetProduct(pos);
            }
        }
        
        
    }
}