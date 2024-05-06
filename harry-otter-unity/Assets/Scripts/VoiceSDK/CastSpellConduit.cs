using System.Collections;
using System.Collections.Generic;
using Meta.WitAi;
using UnityEngine;
using TMPro; 

public class CastSpellConduit : MonoBehaviour
{
    private const string CAST_SPELL_INTENT = "cast_spell";

    Spells spellCaster;

    [SerializeField] private TMP_Text speechText; 

    void Start()
    {
        spellCaster = FindObjectOfType<Spells>();
    }

    [MatchIntent(CAST_SPELL_INTENT)]
    public void CastSpell(string[] values)
    {
        Debug.Log("CastSpellConduit -> CastSpell()");

        string resolved_value = values[0];

        Debug.Log("Said word: " + resolved_value); 

        if (!string.IsNullOrEmpty(resolved_value))
        {
            speechText.text = resolved_value[0].ToString().ToUpper() + resolved_value.Substring(1) + "."; 

            if (resolved_value == "attack")
            {
                Debug.Log("Attack!");
                spellCaster.Cast("attack");
            }
            if (resolved_value == "shield")
            {
                Debug.Log("Shield!");
                spellCaster.Cast("shield");
            }
            if (resolved_value == "book")
            {
                Debug.Log("Book!");
                spellCaster.Cast("book");
            }
            if (resolved_value == "lumos")
            {
                Debug.Log("Lumos!");
                spellCaster.Cast("lumos");
            }
            if (resolved_value == "nox")
            {
                Debug.Log("Nox!");
                spellCaster.Cast("nox");
            }
            // Debug.Log("ChangeLightConduit -> ChangeLight(): match");
        }
    }
}
