using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CreatureStats
{
    [Range(1, 2)] public float size = 1;
    [Range(0, 7)] public float speed = 3.5f;
    [Range(1,10)] public float sightRange = 5;
}
