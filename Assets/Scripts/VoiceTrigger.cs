using UnityEngine;

public class VoiceTrigger : MonoBehaviour
{
    public AudioSource audioNote;

     public AudioClip voiceClip;
    private bool hasPlayed = false;


    void Start()
    {


    }

    void OnTriggerEnter(Collider other)
    {
        if (hasPlayed) return;

     Rigidbody rb = other.attachedRigidbody;

    if (rb != null && rb.gameObject.CompareTag("Player"))
    {
        Debug.Log("Player Rigidbody entered the trigger!");
      
            audioNote.volume = 1;
              audioNote.PlayOneShot(voiceClip);
            hasPlayed = true;
    }
    }
}