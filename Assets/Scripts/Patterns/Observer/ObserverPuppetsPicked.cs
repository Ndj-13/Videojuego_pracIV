using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Components.UI
{
    public class ObserverPuppetsPicked : MonoBehaviour, IObserver<int>
    {
        private void Awake()
        {
            
        }

        public void UpdateObserver(int data)
        {
            TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
            text.text = $"Puppets: {data}";
        }
    }
}