using QuestionnaireToolkit.Scripts;
using UnityEngine;
using System.Collections;

public class LoadQuestionnaire : MonoBehaviour
{

    private bool hasStarted = false;
    private GameObject xrRig;
    void OnTriggerEnter(Collider other)
    {

      
     Rigidbody rb = other.attachedRigidbody;

    if (rb != null && rb.gameObject.CompareTag("Player"))
    {
    StartCoroutine(QuestionStart());
    }
    }
    
        public void StartQuestionnaireFromButton()
    {
        if (!hasStarted)
        {
            StartCoroutine(QuestionStart());
        }
    }

    public IEnumerator QuestionStart()
    {
        hasStarted = true;
        yield return new WaitForSeconds(0.5f);



        GameObject.FindWithTag("QTQuestionnaireManager").GetComponent<QTQuestionnaireManager>()
                   .StartQuestionnaire();

        
       
        Time.timeScale = 0f;

  
    }
}