using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    // Start is called before the first frame update
    List<string> spellList = new List<string> { "lumos", "nox", "otterocity", "protego" };
    public GameObject wand;

    puiblic float timeElapsed= 0.0f;
    public float spellDuration = 1.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Cast(string spell) 
    {
        if (!spellList.Contains(spell)) {
            Console.WriteLine($"{spell} is not a recognized spell.");
            break;
        }
        if (spell == "lumos") {
            if (timeElapsed < spellDuration) {
                wand.LightSource.intensity = Mathf.Lerp(0, 1, timeElapsed/spellDuration);
                timeElapsed += Time.deltaTime;
            }
        } elif (spell == "nox") {
            if (timeElapsed < spellDuration) {
                wand.LightSource.intensity = Mathf.Lerp(1, 0, timeElapsed/spellDuration);
                timeElapsed += Time.deltaTime;
            }            
        } elif (spell == "otterocity") {
            //summon a projectile
        } elif (spell == "protego") {
            //summon a shield
        }
    }
}
