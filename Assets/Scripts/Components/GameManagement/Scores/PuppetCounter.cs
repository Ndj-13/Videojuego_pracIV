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
        //[SerializeField] GameObject player;

        //SubjectPuppetsPicked puppet = new SubjectPuppetsPicked();
        //ObserverPuppetsPicked observer = new ObserverPuppetsPicked();


        public int numCollected;

        // Start is called before the first frame update
        void Start()
        {
            //if (!player) { player.GetComponent<GameObject>(); }
            numPuppetsText = GetComponent<TextMeshProUGUI>();
            //numCollectedText.text = numCollected.ToString();

            //player.

        }

        // Update is called once per frame
        void Update()
        {
            numPuppetsText.text = numCollected.ToString("");
            //player.gameObject.
            //player.
        }

        public void AddCollected(int collectedPuppets)
        {
            Debug.Log("Update num puppets");
            numCollected = collectedPuppets; //no se si pasar como parametro directamente el numPuppets o sumar 1 cada vez
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