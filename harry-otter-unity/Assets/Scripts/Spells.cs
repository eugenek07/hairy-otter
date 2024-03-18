using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using echo17.EndlessBook;

public class Spells : MonoBehaviour
{
    // Start is called before the first frame update
    List<string> spellList = new List<string> { "lumos", "nox", "otterocity", "protego" };
    public GameObject wand;
    public GameObject bookController;
    public GameObject projectilePrefab; // Assign in the inspector
    public GameObject shieldPrefab; // Assign in the inspecto
    public Transform projectileSpawnPoint; // Assign a point at tip of wand

    public float timeElapsed= 0.0f;
    public float spellDuration = 1.0f;

    public Light wandLight; 

    bool bookActive;

    void Start()
    {
        bookActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cast(string spell)
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
            case "book":
                SummonBook();
                break;
        }
    }

    void LumosSpell()
    {
        StartCoroutine(LightFade(wandLight.intensity, 1f));
    }

    void NoxSpell()
    {
        StartCoroutine(LightFade(wandLight.intensity, 0f)); 
    }

    IEnumerator LightFade(float startIntensity, float endIntensity)
    {
        float timeElapsed = 0f;
        float interval = 0.02f; 
        while (timeElapsed < spellDuration)
        {
            wandLight.intensity = Mathf.Lerp(startIntensity, endIntensity, timeElapsed / spellDuration);
            timeElapsed += interval;
            yield return new WaitForSeconds(interval);
        }
    }

    void SummonProjectile() {
        Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        // move projectile
    }

    void SummonShield() {
        Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        // decay and remove shield after predetermined time.
    }    

    void SummonBook(){
        bookActive = !bookActive;
        BookController controlScript = bookController.GetComponent<BookController>();
        controlScript.toggleBook(bookActive);
    }
}
