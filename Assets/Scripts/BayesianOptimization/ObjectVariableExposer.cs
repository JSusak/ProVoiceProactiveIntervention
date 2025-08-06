using QuestionnaireToolkit.Scripts;
using UnityEngine;

namespace BOforUnity.Examples
{
    //ObjectVariableExposer contains references to all design parameter values modified
    //during optimization.
    public class ObjectVariableExposer : MonoBehaviour
    {

        // Proactive parameter variables

        public float colorA;
        public float lightingGlow;
        public float alertVolume;
        public float levelOfAutonomy;

        void Start()
        {
            GameObject.FindWithTag("BOforUnityManager").GetComponent<BoForUnityManager>().optimizer.UpdateDesignParameters();
        }

        // This Update is only needed to listen on any changes to these parameters made by the optimizer
        void Update()
        { }

        public void StartQuestionnaire()
        {
            GameObject.FindWithTag("QTQuestionnaireManager").GetComponent<QTQuestionnaireManager>()
                .StartQuestionnaire();
        }
    }
}