using BOforUnity.Examples;
using UnityEngine;

public class ProactiveSymbolTriggerDemo : ProactiveTrigger
{
    public GameObject interiorSymbol;

    void Start()
    {
             exposer = GameObject
            .Find("BODesignParameterValues").GetComponent<ObjectVariableExposer>();
        interiorSymbol.SetActive(false);
    }

    public override void TriggerIntervention()
    {
        interiorSymbol.SetActive(true);
        Renderer rend = interiorSymbol.GetComponent<Renderer>();
        Color currentColor = rend.material.color;
        currentColor.a = exposer.colorA;
        rend.material.color = new Color(currentColor.a, currentColor.g, currentColor.b, exposer.colorA);
    }

    public override void OnPlayerIntervention()
    {
        Debug.Log("Player has intervened with the proactive assistant for level " + ProactiveSettings.level);
    }

    public override void OnInterventionEnd()
    {
         Renderer rend = interiorSymbol.GetComponent<Renderer>();

    if (rend != null && rend.material.HasProperty("_Color"))
    {
        Color currentColor = rend.material.color;
        currentColor.a = 0f;  // fully transparent
        rend.material.color = currentColor;
    }

    // Optionally deactivate GameObject as well if needed
    interiorSymbol.SetActive(false);
    }
}