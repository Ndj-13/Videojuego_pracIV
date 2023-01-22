using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Components.Interface.UINavigation.SoundManger
{
    [CreateAssetMenu(fileName = "SoundList", menuName = "Sounds/SoundList")]
    public class SoundList : ScriptableObject
    {
        public Sound[] Sounds; //lista de sonidos
    }
}