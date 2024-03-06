using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    // Start is called before the first frame update
    List<string> spellList = new List<string> { "lumos", "nox", "otterocity", "protego" };
    public GameObject wand;
    public GameObject projectilePrefab; // Assign in the inspector
    public GameObject shieldPrefab; // Assign in the inspecto
    public Transform projectileSpawnPoint; // Assign a point at tip of wand

    public float timeElapsed= 0.0f;
    public float spellDuration = 5.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Cast(string spell)
    {
        if (!spellList.Contains(spell))
        {
            Debug.Log($"{spell} is not a recognized spell.");
            return; // Use return instead of break, as break is used in loops/switch
        }

        switch (spell)
        {
            case "lumos":
                LumosSpell();
                break;
            case "nox":
                NoxSpell();
                break;
            case "otterocity":
                SummonProjectile();
                break;
            case "protego":
                SummonShield();
                break;
        }
    }

    void LumosSpell()
    {
        while (timeElapsed < spellDuration)
        {
            wandLightSource.intensity = Mathf.Lerp(0, 1, timeElapsed / spellDuration);
            timeElapsed += Time.deltaTime;
        }
        timeElapsed = 0; // Reset timeElapsed for next use
        return;
    }

    void NoxSpell()
    {
        while (timeElapsed < spellDuration)
        {
            wandLightSource.intensity = Mathf.Lerp(1, 0, timeElapsed / spellDuration);
            timeElapsed += Time.deltaTime;
        }
        timeElapsed = 0; // Reset timeElapsed for next use
        return;
    }

    void SummonProjectile() {
        Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        // move projectile
    }

    void summonShield() {
        Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        // decay and remove shield after predetermined time.
    }    
}
