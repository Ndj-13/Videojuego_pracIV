using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;

    Vector3 dist; 

    private void Start()
    {
        //if(!Player) { gameObject.GetComponent<GameObject>(); }
        dist = transform.position - Player.transform.position; //camara-jugador
        //transform.Rotate(0, dist * Time.deltaTime, 0);
    }

    private void Update()
    {
        transform.position = Player.transform.position + dist;
        //if (Player.GetComponent<BoxCollider>().enabled = true) Player = null;
    }
}
