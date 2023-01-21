using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public GameObject mirarA;

    private float moveSpeed = 2;
    private float turnSpeed = 100;
    public float damping = 1;
    private float currentH = 0;
    private readonly float interpolation = 10;

    Vector3 dist; 

    private void Start()
    {
        
        if(!Player) { gameObject.GetComponent<GameObject>(); }
        if (!mirarA) { gameObject.GetComponent<GameObject>(); }
        dist = transform.position - Player.transform.position; //camara-jugador
        //dist.y = 0;
        //transform.Rotate(0, dist * Time.deltaTime, 0);
        this.transform.parent = Player.transform;
        
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Quaternion toTargetRotation = Quaternion.LookRotation(Player.transform.forward, Vector3.up);

        if (toTargetRotation.y != 0.0f)
        {
            Debug.Log("Personaje girando");
            //Quaternion toTargetRotation = Quaternion.LookRotation(dist.normalized, Vector3.up);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, toTargetRotation,
            //    turnSpeed * Time.deltaTime);
            this.transform.parent = Player.transform;
        }

            //currentH = Mathf.Lerp(currentH, h, Time.deltaTime * interpolation);

            //Quaternion rotation = Quaternion.LookRotation(transform.position, Vector3.up);
            //transform.rotation = Quaternion.RotateTowards(Player.transform.rotation, rotation,  turnSpeed* Time.deltaTime);

            //transform.Translate(0, 0, moveSpeed * Time.fixedDeltaTime, Space.Self);
            //transform.position = Player.transform.position + dist;
        //if (Player.GetComponent<BoxCollider>().enabled = true) Player = null;
        //transform.rotation = Quaternion.LookRotation(Player.transform.position, Vector3.up);

        transform.LookAt(mirarA.transform);
    }
}
