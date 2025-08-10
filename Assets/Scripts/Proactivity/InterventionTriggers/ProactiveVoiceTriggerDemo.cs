using UnityEngine;
using BOforUnity.Examples;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System.Linq;


//Voice detection from https://www.youtube.com/watch?v=HwT6QyOA80E
public class ProactiveVoiceTriggerDemo: ProactiveTrigger
{

    //Store reference to all assistant voice clips per LoA
    public AudioSource audioNote;
    private AudioClip levelZeroClip;
    private AudioClip levelOneClip;
    private AudioClip levelTwoClip;
    private AudioClip levelThreeClip;
    private AudioClip levelFourClip;

    private bool voiceIntervened = false;

    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    private int levelOfAutonomy;


    void Awake()
    {
        levelZeroClip = Resources.Load<AudioClip>("VoiceNotes/ProactiveIntervention/proactiveLevel0");
        levelOneClip = Resources.Load<AudioClip>("VoiceNotes/ProactiveIntervention/proactiveLevel1");
        levelTwoClip = Resources.Load<AudioClip>("VoiceNotes/ProactiveIntervention/proactiveLevel2");
        levelThreeClip = Resources.Load<AudioClip>("VoiceNotes/ProactiveIntervention/proactiveLevel3");
        levelFourClip = Resources.Load<AudioClip>("VoiceNotes/ProactiveIntervention/proactiveLevel4");
    }

    void Start()
    {

        exposer = GameObject.Find("BODesignParameterValues").GetComponent<ObjectVariableExposer>();

        keywords.Add("yes", () => { OnKeywordRecognized("yes"); });
        keywords.Add("yeah", () => { OnKeywordRecognized("yes"); });
        keywords.Add("ok", () => { OnKeywordRecognized("yes"); });
        keywords.Add("okay", () => { OnKeywordRecognized("yes"); });
        keywords.Add("sure", () => { OnKeywordRecognized("yes"); });
        keywords.Add("no", () => { OnKeywordRecognized("no"); });
        keywords.Add("cancel", () => { OnKeywordRecognized("cancel"); });
        keywords.Add("search", () => { OnKeywordRecognized("search"); });
        keywords.Add("find", () => { OnKeywordRecognized("search"); });
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizerOnPhraseRecognized;

    }

    void KeywordRecognizerOnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;

        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

    public override void TriggerIntervention()
    {

        keywordRecognizer.Start();

        //Example of changing a design parameter with nuanced adjustment for each LoA.

        //Condition 1 (Trained): LoA is NOT defined in ProactiveSettings.cs, uses levelOfAutonomy parameter defined
        //in BOForUnityManager.
        //Condition 2 (Fixed): LoA is defined in ProactiveSettings.cs - Use ProactiveSettings.level across each
        //iteration.
        levelOfAutonomy = ProactiveSettings.level;

        //Check if in study condition 1 or condition 2.
        if (levelOfAutonomy == -1)
        {
            //Normalise LoA parameter value into integer between 0-4.
            float boLevelOfAutonomy = exposer.levelOfAutonomy;
            levelOfAutonomy = Mathf.FloorToInt(boLevelOfAutonomy * 5f);
            levelOfAutonomy = Mathf.Clamp(levelOfAutonomy, 0, 4);
        }

        switch (levelOfAutonomy)
        {
            case 0:
                audioNote.PlayOneShot(levelZeroClip);
                break;
            case 1:
                audioNote.PlayOneShot(levelOneClip);
                break;
            case 2:
                audioNote.PlayOneShot(levelTwoClip);
                break;
            case 3:
                audioNote.PlayOneShot(levelThreeClip);
                break;
            case 4:
                audioNote.PlayOneShot(levelFourClip);
                break;
            default:
                Debug.LogWarning($"No audio defined for LoA {ProactiveSettings.level}");
                break;
        }
    }

    public void OnKeywordRecognized(string keyword)
    {
        if (voiceIntervened) return;
        voiceIntervened = true;
        Debug.Log($"User said: {keyword}");
        MarkAsIntervened();

        switch (keyword.ToLower())
        {
            case "yes":
                OnPlayerConfirmed();
                break;
            case "no":
                OnPlayerDeclined();
                break;
            case "cancel":
                OnPlayerDeclined();
                break;
            case "search":
                OnPlayerSearched();
                break;
        }
    }
    
    private void OnPlayerConfirmed()
    {
        if (levelOfAutonomy == 2)
        {
            audioNote.PlayOneShot(Resources.Load<AudioClip>("VoiceNotes/ProactiveIntervention/proactiveLevel2Yes"));

        }
        OnPlayerIntervention();
    }

    private void OnPlayerDeclined()
    {
        audioNote.PlayOneShot(Resources.Load<AudioClip>("VoiceNotes/ProactiveIntervention/proactiveAlertCancel"));
        OnPlayerIntervention(); 
    }

    private void OnPlayerSearched()
    {
        if (levelOfAutonomy == 1)
        {
            audioNote.PlayOneShot(Resources.Load<AudioClip>("VoiceNotes/ProactiveIntervention/proactiveLevel1Search"));
        }
        OnPlayerIntervention();
    }


    public override void OnInterventionEnd()
    {
        keywordRecognizer.Stop();
    }
}