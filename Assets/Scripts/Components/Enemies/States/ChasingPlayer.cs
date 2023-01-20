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
        private float move;

        public ChasingPlayer(IEnemies enemy) : base(enemy)
        {
        }

        public override void Enter()
        {
            currentTransform = enemy.GetGameObject().transform;
            playerTransform = enemy.PlayerAtSight().transform;
            rotationSpeed = enemy.GetRotateSpeed();
            chaseSpeed = enemy.GetChaseSpeed();

            Debug.Log($"Enemy {enemy.GetGameObject().name} started chasing player");
        }

        public override void Exit()
        {
            //Debug.Log("Persiguiendo al jugador");
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
                //Debug.Log($"Distancia del jugador{distanceToWaypoint}");
                //enemy.Attack(distanceToWaypoint);

                if (distanceToWaypoint > 1.5f)
                {
                    move = chaseSpeed;
                    //enemy.Attack(distanceToWA);
                    enemy.MoveTo(playerTransform, move, rotationSpeed); //sigo moviendome hacia el jugador
                }
                else
                {
                    move = 0;
                    enemy.SetState(new AttackPlayer(enemy));
                }
                //else enemy.Attack(distanceToWaypoint);
            }   
            else
            {
                enemy.SetState(new SearchingForPlayer(enemy)); //si lo he perdido vuelvo a patrullar
            }
        }
    }
}
