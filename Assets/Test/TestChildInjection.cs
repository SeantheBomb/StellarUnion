using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChildInjection : MonoBehaviour
{
    [SerializeReference, SubclassSelector]
    public List<BaseClass> test;


}


[System.Serializable]
public abstract class BaseClass
{
    public string name;
}

[System.Serializable]

public class TestClass : BaseClass
{
    public string description;
}

[System.Serializable]

public class TestClassB : BaseClass
{
    public string tag;
}