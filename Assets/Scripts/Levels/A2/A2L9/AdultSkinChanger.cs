using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

namespace B25.Boludin.A2.L9
{
    public class AdultSkinChanger : MonoBehaviour
    {
        [SerializeField]
        private AdultMaratonSkin skin;

        [SerializeField]
        private SpriteLibrary library;

        [SerializeField]
        private SpriteResolver[] sprites;

        [SerializeField]
        private int position;

        void Start()
        {
            if (skin == null)
            {
                Debug.LogError("The skin is null");
                return;
            }

            if (library == null)
                library = GetComponent<SpriteLibrary>();

            library.spriteLibraryAsset = skin.Library;

            foreach (var item in sprites)
                item.SetCategoryAndLabel(item.GetCategory(), skin.Gender + position.ToString());
        }
    } 
}
