using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class ConceptManager : MonoBehaviour
{
    private Dictionary<string, double> concepts;

    public void AddConcepts(IEnumerable<Concept> concepts)
    {
        if (this.concepts is null)
            this.concepts = new Dictionary<string, double>();

        foreach (var concept in concepts)
        {
            if (this.concepts.ContainsKey(concept.Key))
            {
                this.concepts[concept.Key] += concept.Reward;
            }
            else
            {
                this.concepts.Add(concept.Key, concept.Reward);
            }
        }
    }

    public void AddConcepts(IEnumerable<Concepts> concepts)
    {
        foreach (var item in concepts)
            AddConcepts(item.ListConcepts);
    }

    public Dictionary<string, double> GetResult() => concepts;
}
