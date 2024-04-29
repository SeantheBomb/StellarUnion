using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EndTurnButton : MonoBehaviour
{

    public static EndTurnButton instance;

    public static System.Action OnEndTurn;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        GetComponent<Button>().onClick.AddListener(OnClick);    
    }

    void OnClick()
    {
        OnEndTurn?.Invoke();
    }

    public void SetInteract(bool value)
    {
        GetComponent<Button>().interactable = value;
    }

    
}
