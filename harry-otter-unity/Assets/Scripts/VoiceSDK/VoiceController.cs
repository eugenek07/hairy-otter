using System.Collections;
using System.Collections.Generic;
using Meta.WitAi; 
using Meta.WitAi.Requests;
using UnityEngine;

public class VoiceController : MonoBehaviour
{
    [SerializeField] private VoiceService voiceService;

    private VoiceServiceRequest voiceServiceRequest;
    private VoiceServiceRequestEvents voiceServiceRequestEvents;

    bool voiceRecActive = false; 

    void Start()
    {
        // Debug.Log("... start ");
    }

    void Update()
    {
        // Debug.Log("testing");
        if (!voiceRecActive)
        {
            ActivateVoiceService();
        }
        /*
        if (!voiceRecActive && Input.GetKey("space"))
        {
            Debug.Log("space pressed");
            ActivateVoiceService(); 
        }
        */
    }

    private void ActivateVoiceService()
    {
        Debug.Log("VoiceController -> ActivateVoiceService()");

        if (voiceServiceRequestEvents == null)
        {
            voiceServiceRequestEvents = new VoiceServiceRequestEvents();

            voiceServiceRequestEvents.OnInit.AddListener(OnInit);
            voiceServiceRequestEvents.OnComplete.AddListener(OnComplete);
        }

        voiceServiceRequest = voiceService.Activate(voiceServiceRequestEvents);
        voiceRecActive = true; 
    }

    private void DeactivateVoiceService()
    {
        Debug.Log("VoiceController -> DeactivateVoiceService()");

        voiceServiceRequest.DeactivateAudio();
        voiceRecActive = false; 
    }

    private void OnInit(VoiceServiceRequest request)
    {
        // uI.SetActive(true);
    }

    private void OnComplete(VoiceServiceRequest request)
    {
        // uI.SetActive(false);
        DeactivateVoiceService();
    }
}
