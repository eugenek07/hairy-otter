using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayUI : MonoBehaviour
{
    public GameObject icon; 

    public void Show() {
        icon.SetActive(true); 
    }

    public void Hide()
    {
        icon.SetActive(false);
    }
}
