using System;
using UnityEngine;

namespace factory
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class productB : MonoBehaviour, IFactory
    {
        [SerializeField] private String productName;
        public String Name { get => productName; set => productName = value; }
        
        private SpriteRenderer SR;
        public void Initalize()
        {
            SR = GetComponent<SpriteRenderer>();
            SR.color = Color.blue;
        }
    }
}