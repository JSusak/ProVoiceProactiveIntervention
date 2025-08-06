
//Helper file, containing immutable variables for global proactive intervention settings.
public static class ProactiveSettings
{
    //-1 for C1: Trained Loa. 0-4 for C2: Fixed LoA.
    public static int level = -1;

    //How long the proactive assistant should stay active and listen to user input upon intervention.
    public static float proactiveListenDuration = 20f;
}