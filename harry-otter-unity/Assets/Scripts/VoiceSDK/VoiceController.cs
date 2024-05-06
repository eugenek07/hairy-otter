using System.Collections;
using System.Collections.Generic;
using Meta.WitAi; 
using Meta.WitAi.Requests;
using UnityEngine;
using TMPro; 

public class VoiceController : MonoBehaviour
{
    [SerializeField] private VoiceService voiceService;

    private VoiceServiceRequest voiceServiceRequest;
    private VoiceServiceRequestEvents voiceServiceRequestEvents;

    bool voiceRecActive = false;

    float timeUntilAllowVoice = 0f;
    float voiceCooldown = 0.5f;

    [SerializeField] DisplayUI micIcon;
    [SerializeField] DisplayUI speechTextUI;
    [SerializeField] TMP_Text speechText;

    void Start()
    {
        
    }

    void Update()
    {
        // Debug.Log("testing");
        /*
        if (!voiceRecActive && Input.GetKey("space"))
        {
            Debug.Log("space pressed");
            ActivateVoiceService(); 
        }
        */
    }

    public void ToggleVoiceService()
    {
        if (Time.time > timeUntilAllowVoice)
        {
            if (!voiceRecActive) ActivateVoiceService(); 
            else DeactivateVoiceService();

            timeUntilAllowVoice = Time.time + voiceCooldown;
        }
    }

    public void ActivateVoiceService()
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

        micIcon.ShowAlpha();
        speechText.text = "";
        speechTextUI.Hide();
    }

    public void DeactivateVoiceService()
    {
        if (Time.time > timeUntilAllowVoice)
        {
            Debug.Log("VoiceController -> DeactivateVoiceService()");

            if (voiceServiceRequest != null) voiceServiceRequest.DeactivateAudio();
            voiceRecActive = false;

            micIcon.HideAlpha();
            speechTextUI.Show(); 
        }
    }

    private void OnInit(VoiceServiceRequest request)
    {
        // uI.SetActive(true);
    }

    private void OnComplete(VoiceServiceRequest request)
    {
        // uI.SetActive(false);
        if (voiceServiceRequest != null) DeactivateVoiceService();
    }
}
