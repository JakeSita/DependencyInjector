using System;
using UnityEngine;

namespace factory
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ProductA : MonoBehaviour, IFactory
    {
        [SerializeField] private String productName;
        [SerializeField] private GameObject _prefab;
        public GameObject Prefab { get => _prefab; set => _prefab = value;}
        public String Name { get => productName; set => productName = value; }
        private SpriteRenderer SR;
        public static int Instance = 0;
        
        
        public void Initalize()
        {
            Instance++;
            gameObject.name = $"{productName}{Instance}";
            SR = GetComponent<SpriteRenderer>();
            SR.color = Color.red;
            ProductFactoryManager.AddProduct(productName, this);
        }
    }
}