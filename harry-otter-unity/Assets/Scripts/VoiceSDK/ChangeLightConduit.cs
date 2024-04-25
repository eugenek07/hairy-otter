using System.Collections;
using System.Collections.Generic;
using Meta.WitAi;
using UnityEngine;

public class ChangeLightConduit : MonoBehaviour
{
    private const string CHANGE_LIGHT_INTENT = "change_light";

    Spells spellCaster;

    void Start()
    {
        spellCaster = FindObjectOfType<Spells>();
    }

    [MatchIntent(CHANGE_LIGHT_INTENT)]
    public void ChangeLight(string[] values)
    {
        Debug.Log("ChangeLightConduit -> ChangeLight()");

        string resolved_value = values[0];

        Debug.Log("Said word: " + resolved_value); 

        if (!string.IsNullOrEmpty(resolved_value))
        {
            if (resolved_value == "on")
            {
                Debug.Log("Lumos!");
                spellCaster.Cast("lumos");
            }
            if (resolved_value == "off")
            {
                Debug.Log("Nox!");
                spellCaster.Cast("nox");
            }
            // Debug.Log("ChangeLightConduit -> ChangeLight(): match");
        }
    }
}
