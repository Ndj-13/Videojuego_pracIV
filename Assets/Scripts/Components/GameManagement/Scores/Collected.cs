using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Collected : MonoBehaviour
{
    private Text numCollectedText; //puntos por recoger patitos
    public int numCollected;
    
    // Start is called before the first frame update
    void Start()
    {
        numCollectedText = GetComponent<Text>();
        numCollectedText.text = numCollected.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        numCollectedText.text = numCollected.ToString();
    }

    public void AddCollected(int collectedPuppets)
    {
        numCollected += collectedPuppets; //no se si pasar como parametro directamente el numPuppets o sumar 1 cada vez
    }
}
