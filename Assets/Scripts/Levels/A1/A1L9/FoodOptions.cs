using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace B25.Boludin.A1.L9
{
    public enum FoodType { Healthy, Bad }

    [CreateAssetMenu(fileName = "Food Options", menuName = "ScriptableObjects/Options/A1L9/Food Options", order = 1)]
    public class FoodOptions : Options
    {
        [SerializeField]
        private FoodType foodType;

        public FoodType FoodType
        {
            get { return foodType; }
            set { foodType = value; }
        }
    } 
}
