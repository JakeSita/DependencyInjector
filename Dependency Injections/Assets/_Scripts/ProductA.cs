using System;
using DependencyInjection;
using UnityEngine;

namespace factory
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ProductA : MonoBehaviour, IFactory
    {
       public String ProductName { get; private set; }
       [SerializeField] private GameObject _prefab { get; set; }
       
        
        public GameObject Prefab { get => _prefab; set => _prefab = value;}
        public String Name { get => ProductName; set => ProductName = value; }
        private SpriteRenderer SR;
        public static int InstanceNum = 0;
        
        
        public void Initalize()
        {
            InstanceNum++;
            gameObject.name = $"{ProductName}{InstanceNum}";
            SR = GetComponent<SpriteRenderer>();
            SR.color = Color.red;
        }
        
        
        public class ProductABuilder
        {
            private string name = "ProductA";
            private GameObject prefab = null;
            private Vector2 Position = default;
            public ProductABuilder SetName(string name)
            {
                this.name = name;
                return this;
            }
            
            public ProductABuilder SetPrefab(GameObject prefab)
            {
                this.prefab = prefab;
                return this;
            }
            
            public ProductABuilder SetPosition(Vector2 position)
            {
                this.Position = position;
                return this;
            }
            
            public ProductA Build()
            {
                var gameobject = Instantiate(prefab, Position, Quaternion.identity);
                ProductA newProduct = gameobject.GetComponent<ProductA>();
                newProduct.ProductName = name;
                newProduct.Prefab = prefab;
                newProduct.Initalize();
                return newProduct;
            }
            
            
        }
    }
}