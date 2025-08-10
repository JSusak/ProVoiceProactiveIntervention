using BOforUnity.Examples;
using UnityEngine;

public class ProactiveAlertTriggerDemo : ProactiveTrigger
{
    public AudioClip voiceNote;
    public AudioSource audioSource;

    void Start()
    {
        exposer = GameObject.Find("BODesignParameterValues").GetComponent<ObjectVariableExposer>();
    }

    public override void TriggerIntervention()
    {
        //Adjust alert volume according to alertVolume parameter value in ObjectVariableExposer.
        audioSource.volume = Mathf.Clamp01(exposer.alertVolume);
        audioSource.PlayOneShot(voiceNote);
        hasPlayed = true;
    }

    public override void OnInterventionEnd()
    {
        
    }
}