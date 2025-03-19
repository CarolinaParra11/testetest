using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Concepts", menuName = "ScriptableObjects/Concepts/Concepts", order = 1)]
public class Concepts : ScriptableObject
{
    [SerializeField]
    private ConceptsStructure structure;

    [SerializeField]
    private List<Concept> concepts;

    public List<Concept> ListConcepts
    {
        get { return concepts; }
    }

#if UNITY_EDITOR
    public void CheckStructure()
    {
        if (structure is null)
        {
            Debug.LogError("The structure cannot be null");
            return;
        }

        foreach (var item in structure.Keys)
        {
            var concept = concepts.FirstOrDefault(c => c.Key.Equals(item));

            if (concepts is null || concept.Key != item)
            {
                concept = new Concept();
                concept.Key = item;
                concepts.Add(concept);
            }
        }
    }
#endif
}
