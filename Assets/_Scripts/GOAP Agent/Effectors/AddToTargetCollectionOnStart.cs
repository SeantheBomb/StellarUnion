using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToTargetCollectionOnStart : MonoBehaviour
{

    public CollectionTarget collection;

    // Start is called before the first frame update
    void Start()
    {
        collection.Add(transform);
    }

}
