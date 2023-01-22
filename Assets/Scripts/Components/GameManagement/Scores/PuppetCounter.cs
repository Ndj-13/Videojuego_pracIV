using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Patterns.Observer;
using TMPro;

namespace Components.GameManagement.Scores
{
    public class PuppetCounter : MonoBehaviour, IObserver<int>
    {
        public TextMeshProUGUI numPuppetsText; //puntos por recoger patitos
        public TextMeshProUGUI puertaMagica;
        //[SerializeField] GameObject player;

        //SubjectPuppetsPicked puppet = new SubjectPuppetsPicked();
        //ObserverPuppetsPicked observer = new ObserverPuppetsPicked();


        public int numCollected;

        // Start is called before the first frame update
        void Start()
        {
            //if (!player) { player.GetComponent<GameObject>(); }
            numPuppetsText = GetComponent<TextMeshProUGUI>();
            puertaMagica = GetComponent<TextMeshProUGUI>();
            //numCollectedText.text = numCollected.ToString();

            //player.

        }

        // Update is called once per frame
        void Update()
        {
            numPuppetsText.text = numCollected.ToString("") + "/4";
            //player.gameObject.
            //player.
            if(numCollected == 4)
            {
                Debug.Log("Ha aparecido una puerta mágica");
                puertaMagica.text = "Encuentra la puerta mágica";

            }
        }

        public void AddCollected(int collectedPuppets)
        {
            Debug.Log("Update num puppets");
            numCollected = collectedPuppets; 
        }

        public void UpdateObserver(int newPuppet)
        {
            numCollected += newPuppet;
            //TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
            //text.text = $"Puppets: {data}";

            //PuppetCounter counter;
        }
    }
}