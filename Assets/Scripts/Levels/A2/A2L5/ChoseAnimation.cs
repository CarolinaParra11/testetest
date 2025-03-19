using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace B25.Boludin.V2.L28
{
    [CreateAssetMenu(fileName = "Chose Animation", menuName = "ScriptableObjects/V2L29/Chose Animation", order = 1)]
    public class ChoseAnimation : ScriptableObject
    {
        [SerializeField]
        private Sprite background;

        [SerializeField]
        private GameObject animation;

        public GameObject Animation
        {
            get { return animation; }
            set { animation = value; }
        }

        public Sprite Background { get => background; set => background = value; }
    }
}
