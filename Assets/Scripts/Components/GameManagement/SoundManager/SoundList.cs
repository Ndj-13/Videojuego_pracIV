using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Components.GameManagement.SoundManager
{
    [CreateAssetMenu(fileName = "SoundList", menuName = "Sounds/SoundList")]
    public class SoundList : ScriptableObject
    {
        public Sound[] Sounds; //lista de sonidos
    }
}