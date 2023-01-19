using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingPlayer : APuppetState
{
    private Transform playerTransform;
    private Transform currentTransform;

    private float rotationSpeed;
    private float walkSpeed;

    private float secondsToSeek = 1f;
    private float lastSeek = 0f;

    public FollowingPlayer(IPuppet pup) : base(pup)
    {
    }

    public override void Enter()
    {
        Debug.Log("Seguir");

        currentTransform = pup.GetGameObject().transform;
        playerTransform = pup.PlayerAtSight().transform;
        rotationSpeed = pup.GetRotateSpeed();
        walkSpeed = pup.GetWalkSpeed();

        //Debug.Log($"Zombie {pup.GetGameObject().name} started chasing player");
    }

    public override void Exit()
    {
        //Debug.Log($"Zombie {pup.GetGameObject().name} ended chasing player");
    }

    public override void Update()
    {
        //lastSeek += Time.deltaTime; //incrementa variable x tiempo
        //                            //lo hacemos cada segundo porq no es necesario q lo haga cada vuelta del bucle
        //
        //if (lastSeek >= secondsToSeek) //cuando llego a 1 segundo, cambio al estado buscar al personaje (queremos q lo busque cada 1seg)
        //{
        //    pup.SetState(new WaitingForPlayer(pup)); //se cambia al estado buscar
        //    lastSeek = 0f; //se vuelve a inicializar a 0
        //    Debug.Log("Seeking for enemy");
        //}
    }

    public override void FixedUpdate()
    {
        
        if (pup.PlayerAtSight() != null) //mira si jugador continua a la vista o lo he perdido
        {
            //Debug.Log("Following: jugador a la vista");
            //currentTransform.SetParent(playerTransform);
            Vector3 toWaypoint = playerTransform.position - currentTransform.position;
            toWaypoint.y = 0;
            float distanceToWaypoint = toWaypoint.magnitude;

            if (distanceToWaypoint > 1.0f) pup.MoveTo(playerTransform, walkSpeed, rotationSpeed); //sigo moviendome hacia el jugador
        }
        else
        {
            pup.SetState(new WaitingForPlayer(pup));
        }
        //pup.MoveTo(playerTransform, walkSpeed, rotationSpeed);

        //Vector3 toWaypoint = playerTransform.position - currentTransform.position;
        //toWaypoint.y = 0;
        //float distanceToWaypoint = toWaypoint.magnitude;
        //
        //// Debug.Log($"Distance to waypoint: {distanceToWaypoint}");
        //if (distanceToWaypoint <= walkSpeed)
        //{
        //    Debug.Log("Objetivo alcanzado");
        //    pup.SetState(new WaitingForPlayer(pup));
        //}
    }
}
