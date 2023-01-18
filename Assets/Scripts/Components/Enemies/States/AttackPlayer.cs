using System.Collections;
using System.Collections.Generic;
using Components.Enemies.Interfaces;
using UnityEngine;

public class AttackPlayer : AEnemiesState
{
    private Transform playerTransform;
    private Transform currentTransform;

    private float rotationSpeed;
    private float chaseSpeed;

    public AttackPlayer(IEnemies enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        currentTransform = enemy.GetGameObject().transform;
        playerTransform = enemy.PlayerAtSight().transform;
        //rotationSpeed = enemy.GetRotateSpeed();
        chaseSpeed = enemy.GetChaseSpeed();
        //Debug.Log($"Enemy {enemy.GetGameObject().name} started chasing player");
    }

    public override void Exit()
    {
        //Debug.Log($"Enemy {enemy.GetGameObject().name} ended chasing player");
    }

    public override void Update()
    {
    }

    public override void FixedUpdate()
    {
        //if(enemy.PlayerAtSight)
    }
}
