using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationController : MonoBehaviour
{
    //Used to determine current active state of the Teleportation System
    static private bool teleportIsActive = false;

    //Creates an enum that will determine if we're using the right or left controller
    public enum ControllerType
    {
        RightHand,
        LeftHand
    }

    //Stores the target controller from the editor
    public ControllerType targetController;

    //References our Input Actions that we are using
    public InputActionAsset inputAction;

    //References the rayInteractor to be enabled/disabled later
    public XRRayInteractor rayInteractor;

    //References the Teleportation Provider so we can use it to teleport the Player in the event of a succesful teleport call
    public TeleportationProvider teleportationProvider;


    //Will reference the Thumbstick Input Action when the scene starts up
    private InputAction thumbstickInputAction;

    //Stores Action for Teleport Mode Activate
    private InputAction teleportActivate;

    //Stores Action for Teleport Mode Cancel
    private InputAction teleportCancel;

    void Start()
    {
        //We don't want the rayInteractor to on unless we're using the forward press on the thumbstick so we deactivate it here
        rayInteractor.enabled = false;

        //This will find the Action Map of our target controller for Teleport Mode Activate.
        //It will enable it and then subscribe itself to our OnTeleportActivate function
        Debug.Log("XRI " + targetController.ToString());
        teleportActivate = inputAction.FindActionMap("XRI " + targetController.ToString()).FindAction("Teleport Mode Activate");
        teleportActivate.Enable();
        teleportActivate.performed += OnTeleportActivate;

        //This will find the Action Map of our target controller for Teleport Mode Cancel.
        //It will enable it and then subscribe itself to our OnTeleportCancel function
        teleportCancel = inputAction.FindActionMap("XRI " + targetController.ToString()).FindAction("Teleport Mode Cancel");
        teleportCancel.Enable();
        teleportCancel.performed += OnTeleportCancel;


        //We grab this reference so we can use it to tell if the thumbstick is still being pressed
        thumbstickInputAction = inputAction.FindActionMap("XRI " + targetController.ToString()).FindAction("Move");
        thumbstickInputAction.Enable();
    }

    private void OnDestroy()
    {
        teleportActivate.performed -= OnTeleportActivate;
        teleportCancel.performed -= OnTeleportCancel;
    }
    //We use the Update function to check for when a teleportation event has occured. 
    //The checks needed to ensure a succesful teleport event are
    //-Teleporting is currently active
    //-The ray currently the active one
    //-The Thumbstick isn't being pressed
    //-The rayInteractor is hitting a valid target
    //If those pass, we make a teleportation request to the Teleport Provider
    void Update()
    {
        if (!teleportIsActive)
        {
            return;
        }
        if (!rayInteractor.enabled)
        {
            return;
        }
        if (thumbstickInputAction.triggered)
        {
            return;
        }
        //if raycast actually hits something
        if (!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit raycastHit))
        {
            rayInteractor.enabled = false;
            teleportIsActive = false;
            return;
        }
        //if raycast hits valid area with layer "Teleport" 
        if (raycastHit.collider.gameObject.tag != "Teleport")
        {
            rayInteractor.enabled = false;
            teleportIsActive = false;
            return; 
        }

        TeleportRequest teleportRequest = new TeleportRequest()
        {
            destinationPosition = raycastHit.point,
        };

        teleportationProvider.QueueTeleportRequest(teleportRequest);

        rayInteractor.enabled = false;
        teleportIsActive = false;
    }

    //This is called when our Teleport Mode Activated action map is triggered
    private void OnTeleportActivate(InputAction.CallbackContext context)
    {
        if (!teleportIsActive)
        {
            rayInteractor.enabled = true;
            teleportIsActive = true;
        }

    }

    //This is called when our Teleport Mode Cancel action map is triggered
    private void OnTeleportCancel(InputAction.CallbackContext context)
    {
        if (teleportIsActive && rayInteractor.enabled == true)
        {
            rayInteractor.enabled = false;
            teleportIsActive = false;
        }

    }
}
