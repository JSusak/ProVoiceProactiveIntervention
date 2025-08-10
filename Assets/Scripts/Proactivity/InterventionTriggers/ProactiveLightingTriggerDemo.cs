using BOforUnity.Examples;
using UnityEngine;

public class ProactiveLightingTriggerDemo: ProactiveTrigger
{
    //Store reference to the GameObject to represent interior dashboard lighting.
    public GameObject interiorLightDash;
    private MeshRenderer rend;


    void Start()
    {
        if (interiorLightDash != null)
        {
            exposer = GameObject.Find("BODesignParameterValues").GetComponent<ObjectVariableExposer>();
            interiorLightDash.SetActive(true);
            rend = interiorLightDash.GetComponent<MeshRenderer>();
        }
    }

    public override void TriggerIntervention()
    {
            
        //On trigger, adjust the cyan glow of the dashboard lighting.
        if (rend != null && rend.material.HasProperty("_EmissionColor"))
        {
            float glow = Mathf.Clamp01(exposer.lightingGlow);
            Color emissionColor = Color.cyan * (glow * 7f);

            rend.material.SetColor("_EmissionColor", emissionColor);
            rend.material.EnableKeyword("_EMISSION");

        }
    }

    public override void OnInterventionEnd()
    {
        //Decativate the dashboard lighting after grace period finished.
         if (rend != null && rend.material.HasProperty("_EmissionColor"))
        {
            rend.material.SetColor("_EmissionColor", Color.black);
            rend.material.DisableKeyword("_EMISSION");
        }
    }
}