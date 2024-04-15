using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Hand : MonoBehaviour
{
    //This is where controller actions will occour (casting spells, flying broom) for each left and right hand controller

    #region Variables
    public InputActionReference controllerActionTrigger;

    private XRDirectInteractor interactor;

    [SerializeField]
    private ActionBasedContinuousMoveProvider continuousMoveProvider;

    [SerializeField]
    private Player player; 
    

    #endregion 
    // Start is called before the first frame update
    void Start()
    {
        interactor = GetComponent<XRDirectInteractor>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    #region Methods
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("broom"))
        {
            if (interactor.hasSelection)
            {
                float trigger = controllerActionTrigger.action.ReadValue<float>();

                if (trigger != 0)
                {
                    continuousMoveProvider.enableFly = true;
                    player.isFalling = false; 
                } 
                else
                {
                    continuousMoveProvider.enableFly = false;
                    player.isFalling = true; 
                    
                }
            }
        }
    }
    #endregion 
}
