using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Puppets", menuName = "GameCharacters/Puppets", order = 0)]
public class PuppetsScriptable : ScriptableObject
{
    public string Name;
    public int points;
}
