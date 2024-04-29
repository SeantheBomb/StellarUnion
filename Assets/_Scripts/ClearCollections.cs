using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCollections : MonoBehaviour
{

    public CollectionTarget[] collections;

    private void Awake()
    {
        foreach(var c in collections)
        {
            c.Clear();
        }
    }
}
