using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace B25.Boludin.A2.L9
{
    [CreateAssetMenu(fileName = "Podium Option", menuName = "ScriptableObjects/A2L9/Podium Option", order = 1)]
    public class PodiumOption : Options
    {
        [SerializeField]
        private string gender;

        public string Gender { get => gender; }
    } 
}
