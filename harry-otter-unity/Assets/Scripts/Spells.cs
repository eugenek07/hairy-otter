using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using echo17.EndlessBook;

public class Spells : MonoBehaviour
{
    // Start is called before the first frame update
    List<string> spellList = new List<string> { "lumos", "nox", "attack", "shield", "book" };
    public GameObject wand;
    public GameObject bookController;
    public GameObject projectilePrefab; 
    public GameObject shieldPrefab; 
    public Transform projectileSpawnPoint;
    public Transform player; 

    public float timeElapsed= 0.0f;
    public float spellDuration = 1.0f;
    // bool accio = false;

    public Light wandLight; 

    bool bookActive;

    void Start()
    {
        bookActive = false;
        //SummonProjectile();
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
            case "attack":
                SummonProjectile();
                break;
            case "shield":
                SummonShield();
                break;
            // case "accio":
            //     accio = true;
            //     break;
            case "book":
                // if (accio) {
                    SummonBook();
                // }
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
        //Vector3 projectileEulerAngle = transform.rotation.eulerAngles;
        //projectileEulerAngle = new Vector3(projectileEulerAngle.x, projectileEulerAngle.y + 180, projectileEulerAngle.z);
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(projectile.transform.up * 10f, ForceMode.Impulse);
        // move projectile
        StartCoroutine(DelayDestory(projectile, 10f)); 
    }

    void SummonShield() {
        Instantiate(shieldPrefab, transform.position, Quaternion.identity);
    }    

    void SummonBook(){
        //bookActive = !bookActive;
        //BookController controlScript = bookController.GetComponent<BookController>();
        //controlScript.toggleBook(bookActive);
    }

    IEnumerator DelayDestory(GameObject go, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Destroy(go); 
    }
}
