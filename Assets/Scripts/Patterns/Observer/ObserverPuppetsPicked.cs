using System.Collections;
using System.Collections.Generic;
using TMPro;
using Components.GameManagement.Scores;
using UnityEngine;

namespace Patterns.Observer
{ 
    public class ObserverPuppetsPicked : MonoBehaviour
    {
        private void Awake()
        {
            
        }

        public void UpdateObserver(int data)
        {
            Debug.Log("Notificando al observador");
            //TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
            //text.text = $"Puppets: {data}";

            //PuppetCounter counter;
        }
    }
}
