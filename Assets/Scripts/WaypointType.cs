using UnityEngine;
using System.Collections.Generic;

//Waypoint type used to record player location within the environment.
public enum WaypointType
{
    Normal,
    Intersection,
    Turn,
    Straight
}

public class WaypointRoad : MonoBehaviour {
    public WaypointType type = WaypointType.Normal;
}