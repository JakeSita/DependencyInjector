using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace factory
{
    public interface IFactory
    {
        public String Name { get; set; }
        public void Initalize();
    }


    public abstract class Factory : MonoBehaviour
    {
        public abstract IFactory GetProduct(Vector2 position = default);
    }


    public class FactoryA: Factory
    {
       [SerializeField] ProductA _APrefab;
        
        public override IFactory GetProduct(Vector2 position = default)

        {
            var gameobject = Instantiate(_APrefab, position, Quaternion.identity);
            ProductA newProduct = gameobject.GetComponent<ProductA>();
            newProduct.Initalize();
            return newProduct;
        }
        
    }

    public static class ProductFactoryManager
    {
        public static Dictionary<String,ProductA> ProductDic = new Dictionary<String,ProductA>();
        public static void AddProduct(String name, ProductA product)
        {
            ProductDic.Add(name, product);
        }
        
        public static ProductA GetProduct(String name)
        {
            return ProductDic[name];
        }


    }
    
   
    
    
    
    
    
}
    
    
    
