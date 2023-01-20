using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemies", menuName = "GameCharacters/Enemies", order = 1)]
public class EnemiesScriptable : ScriptableObject
{
    public string Name;
    public int maxLife;
    public int damage;
}