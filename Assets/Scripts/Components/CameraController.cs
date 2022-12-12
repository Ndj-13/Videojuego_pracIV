using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;

    Vector3 dist; 

    private void Start()
    {
        dist = transform.position - Player.transform.position; //camara-jugador
    }

    private void Update()
    {
        transform.position = Player.transform.position + dist;
    }
}
