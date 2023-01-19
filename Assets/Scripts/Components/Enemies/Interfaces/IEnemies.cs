using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components.Enemies.Interfaces;

public interface IEnemies
{
    public GameObject GetGameObject(); //obtener game object
    public void SetState(IState state); //obtener metodo actual
    public IState GetState(); //establecer metodo actual --> cambiar de estado

    public GameObject WallAtSight();
    public GameObject PlayerAtSight(); //dice si el jugador esta a la vista

    // Movement management
    public float GetRotateSpeed(); //velocidad de rotaion
    public float GetWanderSpeed(); //velocidad de caminar
    public float GetChaseSpeed(); //velocidad de perseguir
    public void SetCurrentSpeed(float speed); //
    public void Attack(bool active);
    //public void NearToAttack(bool dist);
    public void MoveTo(Transform target, float speed, float rotateSpeed);

    // Waypoints management
    //public Transform[] GetWayPoints(); //devuelve lista de waypoints
    //public Transform GetCurrentWayPoint(); //waypint actual
    //public void SetCurrentWayPoint(Transform waypoint);
}
