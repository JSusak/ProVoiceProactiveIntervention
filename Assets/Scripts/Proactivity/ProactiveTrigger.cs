using UnityEngine;
using BOforUnity.Examples;
using System.Collections;

/*
    Abstract class containing core logic for proactive intervention and behaviour for:
    - triggering proactive intervention on collider enter.
    - Listening to user response and checking if user has intervened.
    - Ending intervention after period of time.
*/
public abstract class ProactiveTrigger : MonoBehaviour
{
    public ObjectVariableExposer exposer;
    public bool hasPlayed = false;


    private IEnumerator listenCoroutine;
    private bool intervened = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasPlayed) return;

        Rigidbody rb = other.attachedRigidbody;

        //Once player is registered in the collider, trigger proactive intervention and begin input listening period.
        if (rb != null && rb.gameObject.CompareTag("Player"))
        {
            TriggerIntervention();
            hasPlayed = true;

            //Listen for a grace period, timing defined in ProactiveSettings.cs
            listenCoroutine = WaitForIntervention(ProactiveSettings.proactiveListenDuration);
            StartCoroutine(listenCoroutine);
        }
    }


    //Wait for a period of time (ProactiveSettings.cs) to check if user has interveneted. If not, end the intervention.
    private IEnumerator WaitForIntervention(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (!intervened)
        {
            OnInterventionEnd();
        }
    }

    //Call this in the inheriting class during update depending on the type of input recieved.
    //set intervented to true and call onPlayerIntervention.
    public void MarkAsIntervened()
    {
        if (intervened) return;
        intervened = true;
        if (listenCoroutine != null)
        {
            StopCoroutine(listenCoroutine);
            //call any deactivate scripts
            //OnInterventionEnd();
        }
        OnPlayerIntervention();
    }


    //What should happen for this trigger once the player enters the trigger collider.
    public abstract void TriggerIntervention();

    //What to do here when the player chooses to intervene/engage with the proactive assistant for each level.
    //In the inheriting class, call for each level if needed. virtual = Optional.
    public virtual void OnPlayerIntervention() { }

    //What to do with the trigger once the user intervention period is finished.
    public abstract void OnInterventionEnd();
}
