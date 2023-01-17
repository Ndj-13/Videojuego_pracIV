using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsComponent : MonoBehaviour
{
    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other) //ser hijo
    {
        Debug.Log("Algo ha chocado con el muro");
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemigo ha chocado contra el muro");
        }
    }

    private void OnTriggerStay(Collider other)
    {


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

        }
    }
}
