using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class BroomMovement : MonoBehaviour
{
    #region Variables
    public bool isRidingBroom;

    [SerializeField]
    private GameObject leftHandController;
    [SerializeField]
    private GameObject rightHandController;
    [SerializeField]
    private GameObject headVR;
    [SerializeField]
    private GameObject locomotionManagerObject; 

    private TeleportationProvider teleportationProvider;
    private ActionBasedContinuousMoveProvider continuousProvider;
    private LocomotionManager locomotionMangager; 

    #endregion 
    // Start is called before the first frame update
    void Start()
    {
        teleportationProvider = locomotionManagerObject.GetComponent<TeleportationProvider>();
        continuousProvider = locomotionManagerObject.GetComponent<ActionBasedContinuousMoveProvider>();
        locomotionMangager = locomotionManagerObject.GetComponent<LocomotionManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (isRidingBroom)
        {
            teleportationProvider.enabled = false;
            continuousProvider.enabled = true; 
        }
    }
}
