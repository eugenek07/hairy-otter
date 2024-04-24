using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerAttackTut : MonoBehaviour
{
    public float speed = 7;
    public float destroyDelay = 2.17f;
    public float erodeRate = 0.01f;
    public float erodeRefreshRate = 0.01f;
    public float erodeDelay = 0.5f;
    public SkinnedMeshRenderer erodeObject;
    public Transform fpsController;

    // Start is called before the first frame update
    void Start() {
        erodeObject.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)){
            fireTiger();
            Debug.Log("firing tiger");
        }
    }

    void fireTiger() {
        if (fpsController != null) {
            // Instantiate the object at the FPSController's position and rotation
            GameObject replica = Instantiate(gameObject, fpsController.position, fpsController.rotation);
            
            // Make it Visible
            GameObject childMesh = replica.transform.Find("tiger_run/Tiger(0)/tiger").gameObject;
            childMesh.GetComponent<SkinnedMeshRenderer>().enabled = true;

            // Add a Rigidbody if not already present
            Rigidbody rb = replica.GetComponent<Rigidbody>();
            if (rb == null) {
                rb = replica.AddComponent<Rigidbody>();
            }

            // Ensure the Rigidbody does not use gravity or rotation for simple forward movement
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeRotation;

            // Disable this script on the replica to prevent it from replicating itself
            TigerAttackTut replicatorScript = replica.GetComponent<TigerAttackTut>();
            if (replicatorScript != null) {
                replicatorScript.enabled = false;
            }

            // Apply a force to move the replica forward
            rb.velocity = fpsController.forward * speed;

            // Destroy the replica after a delay
            StartCoroutine(ErodeObject(childMesh.GetComponent<SkinnedMeshRenderer>()));
            Destroy(replica, destroyDelay);
        }
        else {
            Debug.LogError("FPSController is not assigned!");
        }
    }

    IEnumerator ErodeObject(SkinnedMeshRenderer obj) {
        yield return new WaitForSeconds(erodeDelay);

        float t = 0;
        while (t < 1) {
            t += erodeRate;
            obj.material.SetFloat("_Erode", t);
            yield return new WaitForSeconds(erodeRefreshRate);
        }
    }
}
