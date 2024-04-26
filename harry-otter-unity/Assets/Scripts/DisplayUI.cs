using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUI : MonoBehaviour
{
    public GameObject icon;

    private Image iconSprite;

    private void Start()
    {
        iconSprite = icon.GetComponent<Image>(); 
    }

    public void Show() {
        icon.SetActive(true);

        if (iconSprite != null)
        {
            Color temp = iconSprite.color;
            temp.a = 1f;
            iconSprite.color = temp;
        }
    }

    public void Hide()
    {
        icon.SetActive(false);
    }

    public void HideAlpha() {
        if (iconSprite != null)
        {
            Color temp = iconSprite.color;
            temp.a = 0.2f;
            iconSprite.color = temp;
        }
    }
}
