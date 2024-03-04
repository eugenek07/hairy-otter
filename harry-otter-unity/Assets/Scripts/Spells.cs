using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    // Start is called before the first frame update
    List<string> spellList = new List<string> { "lumos", "nox", "otterocity", "protego" };
    public GameObject wand;

    public float timeElapsed = 0.0f;
    public float spellDuration = 1.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cast(string spell) 
    {
        if (!spellList.Contains(spell)) {
            Debug.Log($"{spell} is not a recognized spell.");
        }
        if (spell == "lumos") {
            if (timeElapsed < spellDuration) {
                wand.GetComponent<Light>().intensity = Mathf.Lerp(0, 1, timeElapsed/spellDuration);
                timeElapsed += Time.deltaTime;
            }
        } else if (spell == "nox") {
            if (timeElapsed < spellDuration) {
                wand.GetComponent<Light>().intensity = Mathf.Lerp(1, 0, timeElapsed/spellDuration);
                timeElapsed += Time.deltaTime;
            }            
        } else if (spell == "otterocity") {
            //summon a projectile
        } else if (spell == "protego") {
            //summon a shield
        }
        return; 
    }
}
