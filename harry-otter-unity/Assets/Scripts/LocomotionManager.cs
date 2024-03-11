using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

/* IMPORTANT PLEASE NOTE:
 When using this script attatch the function SwitchLocomotion to some button in the UI settings menu!
You can only use one type of movement at a time, either continuous or teleport, and it seems intuitive
to not give the player both options at the same time, but rather set it on settings (which the UI will be done later)*/
public class LocomotionManager : MonoBehaviour
{
    public GameObject leftRayTeleport;
    public GameObject rightRayTeleport;


    private TeleportationProvider teleportationProvider;
    private ActionBasedContinuousMoveProvider continuousMoveProvider;

    private InputActionReference continuousMoveInputReference;
    private InputActionAsset teleportationInputReference;

    // Start is called before the first frame update
    void Start()
    {
        teleportationProvider = GetComponent<TeleportationProvider>();
        continuousMoveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
        SetContinuousMoveInputReference();
        SetTeleportationInputReference();
    }

    public void SwitchLocomotion(int locomotionValue)
    {
        if (locomotionValue == 0)
        {
            DisableTeleport();
            EnableContinuous();
        }
        else if (locomotionValue == 1)
        {
            DisableContinuous();
            EnableTeleport();
        }
    }

    //This function assumes that the user will only have the left hand needed for input with their Continuous Move Provider
    private void SetContinuousMoveInputReference()
    {
        if (continuousMoveProvider.leftHandMoveAction.reference != null)
        {
            continuousMoveInputReference = continuousMoveProvider.leftHandMoveAction.reference;
        }
        else
        {
            Debug.Log("No Continuous Move Provider Input Action was found on the Left Hand. Please set it on your  Left hand Move Action found on the Continuous Move Provider use the Locomotion Manager");
        }
    }

    private void SetTeleportationInputReference()
    {
        //Just accessing general default controller so hand being referenced does not matter
        teleportationInputReference = leftRayTeleport.GetComponent<TeleportationController>().inputAction;
        if (teleportationInputReference == null)
        {
            Debug.Log("No Input Action Asset reference was found in the Teleportation Controller Fixed script. Please assign to use Locomotion Manager");
        }
    }
    private void DisableTeleport()
    {
        leftRayTeleport.SetActive(false);
        rightRayTeleport.SetActive(false);
        teleportationProvider.enabled = false;
    }

    private void EnableTeleport()
    {
        leftRayTeleport.GetComponent<TeleportationController>().inputAction = teleportationInputReference;
        rightRayTeleport.GetComponent<TeleportationController>().inputAction = teleportationInputReference;
        leftRayTeleport.SetActive(true);
        rightRayTeleport.SetActive(true);
        teleportationProvider.enabled = true;
    }

    private void DisableContinuous()
    {
        continuousMoveProvider.enabled = false;
    }

    private void EnableContinuous()
    {
        continuousMoveProvider.leftHandMoveAction = new InputActionProperty(continuousMoveInputReference);
        continuousMoveProvider.enabled = true;
    }
}
