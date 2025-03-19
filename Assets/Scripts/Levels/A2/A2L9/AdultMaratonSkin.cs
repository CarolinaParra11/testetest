using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

namespace B25.Boludin.A2.L9
{
    [CreateAssetMenu(fileName = "Adult Maraton Skin", menuName = "ScriptableObjects/A2L9/Adult Maraton Skin", order = 1)]
    public class AdultMaratonSkin : ScriptableObject
    {
        [SerializeField]
        private SpriteLibraryAsset library;
        
        [SerializeField]
        private string gender;

        public SpriteLibraryAsset Library { get => library; set => library = value; }
        public string Gender { get => gender; set => gender = value; }

    } 
}
