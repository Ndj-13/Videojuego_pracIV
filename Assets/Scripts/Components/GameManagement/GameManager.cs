using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variables privadas: _
    //Variables publicas: mayuscula

    private static GameManager _gameManager;
    //static: accesible desde cualqier clase haciendo referencia a la clase en si

    //Variable publica para acceder al _gameManager:
    public static GameManager Instance
    {
        get { return _gameManager; }
        private set { } //se accede pero nadie puede editarla, solo leerla
    }

    //Diccionario de servicios:
    private Dictionary<string, AGameService> services = new Dictionary<string, AGameService>();

    //Instanciar la clase:

    private void Awake() //Awake: primeros en ejecutarse pero no controlamos el orden
    {
        if(_gameManager == null)
        {
            //Si es nulo le damos valor:
            _gameManager = this;

            //No se destruye sino que cambia de escena:
            DontDestroyOnLoad(gameObject);
        } else
        {
            //Destruimos cualquier _gameManager q pueda haber instanciado
            Destroy(gameObject);
        }
    }

    public void AddService(string name, AGameService service, bool isSingleton, bool dontDestroyOnLoad)
    {
        if(isSingleton && services.ContainsKey(name))
        {
            //Si sevicio ya esta registrado y es singleton: evitar q se cree nueva instancia

            Destroy(service.gameObject);
            return;
        }

        services.Add(name, service); //registramos el servicio
    }

    //Llamar a un servicio
    public AGameService GetService(string name)
    {
        return services[name];
    }
}
