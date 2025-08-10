using BOforUnity.Examples;
using UnityEngine;

public class ProactiveSymbolTriggerDemo : ProactiveTrigger
{

    //Store reference to the GameObject to represent interior intervention symbol.
    public GameObject interiorSymbol;

    void Start()
    {
        exposer = GameObject.Find("BODesignParameterValues").GetComponent<ObjectVariableExposer>();
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
       
    }

    public override void OnInterventionEnd()
    {
        Renderer rend = interiorSymbol.GetComponent<Renderer>();

        if (rend != null && rend.material.HasProperty("_Color"))
        {
            Color currentColor = rend.material.color;
            currentColor.a = 0f;
            rend.material.color = currentColor;
        }

        interiorSymbol.SetActive(false);
    
    }
}