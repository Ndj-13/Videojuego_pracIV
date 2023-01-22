using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_FP : MonoBehaviour
{
    public GameObject player;

    public float damping = 1;
    Vector3 dist;


    void Start()
    {
        if (!player) { gameObject.GetComponent<GameObject>(); }
        dist = transform.position - player.transform.position;
    }

    void Update()
    {
        transform.position = player.transform.position + dist;
    }
}
