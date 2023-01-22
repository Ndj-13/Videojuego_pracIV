using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "Sounds/Sound")]

public class Sound : ScriptableObject
{
    public AudioClip soundClip;
    [Range(0.0f, 1.0f)] public float volume = 0.5f;
    [RangeAttribute(0.1f, 3f)] public float pitch = 1f;
}
