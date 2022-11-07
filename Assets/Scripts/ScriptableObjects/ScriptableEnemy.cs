using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class ScriptableEnemy : ScriptableObject
{
    public string NametoDisplay;
    public string FileNametoSpawn;
    [TextArea(1, 3)]
    public string Description;
    public int id;
    public int difficulty;
    public bool boss = false;
}
