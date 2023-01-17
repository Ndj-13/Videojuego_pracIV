using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPuppet
{
    public GameObject GetGameObject(); //obtener game object
    public void SetState(IPuppetState state); //obtener metodo actual
    public IPuppetState GetState(); //establecer metodo actual --> cambiar de estado

    public GameObject PlayerAtSight(); //dice si el jugador esta a la vista
    public GameObject PlayerNearToFollow(); //dice si el jugador esta suficientemente cerca para seguirle
    public void PlayerAtSightNull();
    //public GameObject FollowingPlayer(); //sige al jugador

    // Movement management
    public float GetRotateSpeed(); //velocidad de rotaion
    public float GetWalkSpeed(); //velocidad de caminar
    public void SetCurrentSpeed(float speed);

    public void MoveTo(Transform target, float speed, float rotateSpeed);
}
