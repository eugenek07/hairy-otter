using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using echo17.EndlessBook;

public class Spells : MonoBehaviour
{
    // Start is called before the first frame update
    List<string> spellList = new List<string> { "lumos", "nox", "attack", "shield", "book" };
    public GameObject wand;
    public BookController bookController;
    public GameObject projectilePrefab; 
    public GameObject shieldPrefab; 
    public Transform projectileSpawnPoint;
    public Transform xrRig;
    public Transform playerCamera;
    public PlayerHP playerHP;

    private AudioSource audioSource;
    public AudioClip fireBallClip;


    private CharacterController m_charController; 

    public float spellDuration = 1.0f;

    // bool accio = false;

    public Light wandLight; 

    bool bookActive;

    public bool webVersion = false;

    private VoiceController m_voiceController; 

    void Start()
    {
        bookActive = false;
        //SummonProjectile();
        audioSource = GetComponent<AudioSource>();

        if (webVersion)
        {
            m_voiceController = FindObjectOfType<VoiceController>(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_voiceController.ActivateVoiceService(); 
        }
        else if (Input.GetMouseButtonUp(0)) {
            m_voiceController.DeactivateVoiceService();
        }
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
                bookController.spawnBook(); 
                // if (accio) {
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
        StartCoroutine(DelayDestroy(projectile, 10f));

        audioSource.PlayOneShot(fireBallClip); 
    }

    void SummonShield() {
        Vector3 groundPos = new Vector3(0, 0, 0); 
        if (!webVersion)
        {
            m_charController = xrRig.GetComponent<CharacterController>();
            float groundHeight = m_charController.center.y - m_charController.height / 2f;
            groundPos = new Vector3(playerCamera.position.x, groundHeight, playerCamera.position.z);
        }
        else
        {
            groundPos = xrRig.position; 
        }

        GameObject shield = Instantiate(shieldPrefab, groundPos, Quaternion.identity);
        StartCoroutine(ToggleShieldStatus()); 
        StartCoroutine(DelayDestroy(shield, 10f)); 
    }

    IEnumerator ToggleShieldStatus()
    {
        yield return new WaitForSeconds(1f);

        playerHP.EnableShield();

        yield return new WaitForSeconds(8.5f);

        playerHP.DisableShield();
    }

    void SummonBook(){
        //bookActive = !bookActive;
        //BookController controlScript = bookController.GetComponent<BookController>();
        //controlScript.toggleBook(bookActive);
    }

    public static IEnumerator DelayDestroy(GameObject go, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Destroy(go); 
    }
}
