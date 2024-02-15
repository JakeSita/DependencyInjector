using System;
using System.Collections.Generic;
using DependencyInjection;
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


    public class FactoryA: Factory,IDependencyProvider
    {
        [SerializeField] private ProductA aPrefab;

        
        public ProductA APrefab { get => aPrefab; set => aPrefab = value; }

       [Provide]
       public FactoryA ProvideFactoryA()
       {
           return this;
       }
       
        public override IFactory GetProduct(Vector2 position = default)

        {
            ProductA newProduct = new ProductA.ProductABuilder().SetName("ProductA").SetPrefab(APrefab.gameObject).SetPosition(position).Build(); 
            ProductFactoryManager.AddProduct(newProduct.name, newProduct);
            return newProduct;
        }
        
        
        
    }

    public static class ProductFactoryManager
    {
        public static Dictionary<String,IFactory> ProductDic = new Dictionary<String,IFactory>();
        public static void AddProduct(String name, IFactory product)
        {
            ProductDic.Add(name, product);
        }
        
        public static IFactory GetProduct(String name)
        {
            return ProductDic[name];
        }


    }
    
   
    
    
    
    
    
}
    
    
    
