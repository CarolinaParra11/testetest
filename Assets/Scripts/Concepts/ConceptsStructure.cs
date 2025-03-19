using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections.ObjectModel;

[CreateAssetMenu(fileName = "ConceptsStructure", menuName = "ScriptableObjects/Concepts/Concepts structure", order = 1)]
public class ConceptsStructure : ScriptableObject
{
    [SerializeField]
    private string[] keys;

    public IReadOnlyCollection<string> Keys { get => new ReadOnlyCollection<string>(keys); }
}
