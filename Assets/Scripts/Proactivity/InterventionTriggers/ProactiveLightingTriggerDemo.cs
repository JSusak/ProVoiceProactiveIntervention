using BOforUnity.Examples;
using UnityEngine;

public class ProactiveLightingTriggerDemo: ProactiveTrigger
{
    public GameObject interiorLightDash;

    private MeshRenderer rend;


    void Start()
    {
        if (interiorLightDash != null)
        {
                 exposer = GameObject
            .Find("BODesignParameterValues").GetComponent<ObjectVariableExposer>();
            interiorLightDash.SetActive(true);
            rend = interiorLightDash.GetComponent<MeshRenderer>();
        }
    }

    public override void TriggerIntervention()
    {
            

        if (rend != null && rend.material.HasProperty("_EmissionColor"))
        {
            float glow = Mathf.Clamp01(exposer.lightingGlow);
            Color emissionColor = Color.cyan * (glow * 7f);

           rend.material.SetColor("_EmissionColor", emissionColor);
            rend.material.EnableKeyword("_EMISSION");

            Debug.Log($"Applied emission glow intensity: {glow * 5f}");

        }
    }

    public override void OnInterventionEnd()
    {
         if (rend != null && rend.material.HasProperty("_EmissionColor"))
        {
       rend.material.SetColor("_EmissionColor", Color.black);
        rend.material.DisableKeyword("_EMISSION");

        }
    }
}