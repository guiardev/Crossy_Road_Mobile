using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Basic", menuName = "Scriptable/Spanws", order = 1)]

public class spanws : ScriptableObject{

    public GameObject[] prefabs;
    public int minSpeed, maxSpeed;
    public bool IsSpawn, isReverse, isTrem, isDupla;

}