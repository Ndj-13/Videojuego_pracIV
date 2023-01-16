using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase para los servicios del juego
public class AGameService : MonoBehaviour
{
    public void RegisterService(string name, bool isSingleton, bool dontDestroyOnLoad)
    {
        Debug.Log($"Registering{name} service");

        //Accedemos a GameManager:
        GameManager.Instance.AddService(name, this, isSingleton, dontDestroyOnLoad); //añadimos servicio
    }
}
