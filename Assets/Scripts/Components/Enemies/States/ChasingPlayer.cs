using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components.Enemies.Interfaces;

namespace Components.Enemies.States
{
    public class ChasingPlayer : AEnemiesState
    {
        private Transform playerTransform;
        private Transform currentTransform;

        private float rotationSpeed;
        private float chaseSpeed;

        public ChasingPlayer(IEnemies enemy) : base(enemy)
        {
        }

        public override void Enter()
        {
            currentTransform = enemy.GetGameObject().transform;
            playerTransform = enemy.PlayerAtSight().transform;
            rotationSpeed = enemy.GetRotateSpeed();
            chaseSpeed = enemy.GetChaseSpeed();
            //Debug.Log($"Enemy {enemy.GetGameObject().name} started chasing player");
        }

        public override void Exit()
        {
            Debug.Log("Persiguiendo al jugador");
            //Debug.Log($"Enemy {enemy.GetGameObject().name} ended chasing player");
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
            Vector3 toWaypoint = playerTransform.position - currentTransform.position;
            toWaypoint.y = 0;
            float distanceToWaypoint = toWaypoint.magnitude;

            if (enemy.PlayerAtSight()) //mira si jugador continua a la vista o lo he perdido
            {
                if (distanceToWaypoint > 1.5f)
                {
                    enemy.Attack(false);
                    enemy.MoveTo(playerTransform, chaseSpeed, rotationSpeed); //sigo moviendome hacia el jugador
                }
                else enemy.Attack(true);
            }   
            else
            {
                enemy.SetState(new SearchingForPlayer(enemy)); //si lo he perdido vuelvo a patrullar
            }
        }
    }
}
