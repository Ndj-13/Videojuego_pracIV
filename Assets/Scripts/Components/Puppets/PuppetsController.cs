using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetsController : MonoBehaviour, IPuppet
{

    public PuppetsScriptable puppetType;
    public int viewingAngle; //cuanto ve --> angulo de vision
    public float WalkSpeed;
    public float RotateSpeed;
    Vector3 dir;

    private IPuppetState currentState; //mantiene estado actual --> contexto sabe cual es el estado actual

    private Animator animator; //para cambiar de animacion

    public Transform eyesTransform; //cuando ve al personaje salta
    public Transform contactFollowTransform; //cuado se acerca mas comienza a seguirle

    private GameObject playerAtSight; //guarda si esta viendo al jugador
    private GameObject startFollowPlayer;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();

        SetState(new WaitingForPlayer(this)); //contexto esta poniendo como estado inicial el search y a partir de aqui ya los otros estados van saltando de un estado a otro
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    #region Get & Set speeds
    public float GetRotateSpeed()
    {
        return RotateSpeed;
    }

    public float GetWalkSpeed()
    {
        return WalkSpeed;
    }

    public void SetCurrentSpeed(float speed)
    {
        animator.SetFloat("MoveSpeed", speed);
    }
    #endregion

    #region Set y Get State, state pattern
    public IPuppetState GetState()
    {
        return currentState; //estado actual
    }

    public void SetState(IPuppetState state)
    {
        //cuando algn le llama para cambiar de estado, comprueba estado actual para ejecutar metodo exit, para salir del estado
        // Exit old state
        if (currentState != null)
        {
            currentState.Exit();
        }

        // Set current state and enter
        currentState = state; //dice cual es el nuevo estado actual
        currentState.Enter(); // le dice q ejecute nuevo estado
    }

    private void Update()
    {
        currentState.Update(); //llama a update de estado actual
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate(); //llama a fixedupdate de estado actual
    }
    #endregion

    #region Player at sight calculations
    public GameObject PlayerAtSight()
    {
        return playerAtSight;
    }
    public GameObject PlayerNearToFollow()
    {
        return startFollowPlayer;
    }
    public void PlayerAtSightNull()
    {
        playerAtSight = null;
    }

    private void OnTriggerEnter(Collider other) //si me entra una colision miro si es el jugador
    {
        if (other.CompareTag("Player"))
        {
            playerAtSight = PlayerIsOnSight(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerAtSight = PlayerIsOnSight(other.gameObject);
            //if (playerAtSight == null) startFollowPlayer = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerAtSight = null; //en el caso de q el jugador haya entrado en mi vision pero haya salido, tengo q devolver el valor a null
            //startFollowPlayer = null;
            //if(playerAtSight == null) startFollowPlayer = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.CompareTag("Player"))
        //{
        //    startFollowPlayer = PlayerIsNear(collision.gameObject);
        //}
    }

    //Comprobar q esta a la vista
    private GameObject PlayerIsOnSight(GameObject player)
    {
        animator.SetBool("Jump", true);

        //Mirar al jugador
        Vector3 direction = (player.gameObject.transform.position - transform.position).normalized;
        direction.y = 0;

        Quaternion toTargetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toTargetRotation, RotateSpeed * Time.deltaTime);
        
        return player.gameObject; //devuelve al jugador si no hay nada entre medias y devolvera null si hay algo entre medias

    }

    //Comprobar q esta cerca
    private GameObject PlayerIsNear(GameObject player)
    {
        //Debug.Log("Lo comienzo a seguir");
        animator.SetFloat("MoveSpeed", WalkSpeed);
        //dir = transform.position - player.transform.position;
        return player;

    }
    #endregion

    public void MoveTo(Transform target, float speed, float rotationSpeed) //calculos para mover personaje
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0;    // No direction in vertical axis
        Quaternion toTargetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toTargetRotation,
            rotationSpeed * Time.deltaTime);

        transform.Translate(0, 0, speed * Time.fixedDeltaTime, Space.Self);
        
    }
}

    

