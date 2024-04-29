using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Corrupted;

[System.Serializable]
public class ComponentInterface<T> : MonoBehaviour
{

    [SerializeReference, SubclassSelector]
    public T[] components;

    public void SendInterface(System.Action<T> action)
    {
        foreach(T t in components)
        {
            action?.Invoke(t);
        }
    }


    public K[] GetInterfaces<K>() where K : T
    {
        return components.GetFromList((t) => (K)t);
    }

    public K GetInterface<K>() where K : T
    {
        return GetInterfaces<K>().FirstOrDefault();
    }


    public bool HasInterface<K>() where K : T
    {
        return GetInterface<K>() != null;
    }

    public bool TryGetInterface<K>(out K k) where K : T
    {
        k = GetInterface<K>();
        return k != null;
    }

}
