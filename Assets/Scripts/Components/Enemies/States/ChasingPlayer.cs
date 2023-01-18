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
            if (enemy.PlayerAtSight()) //mira si jugador continua a la vista o lo he perdido
            {
                enemy.MoveTo(playerTransform, chaseSpeed, rotationSpeed); //sigo moviendome hacia el jugador

                Vector3 toWaypoint = playerTransform.position - currentTransform.position;
                toWaypoint.y = 0;
                float distanceToWaypoint = toWaypoint.magnitude;

                // Debug.Log($"Distance to waypoint: {distanceToWaypoint}");
                if (distanceToWaypoint <= chaseSpeed)
                {
                    //Debug.Log($"Waypoint {currentWaypoint.name} reached");
                    //enemy.SetState(new SearchingForWaypoint(zombie));
                    enemy.NearToAttack(true);
                }
                else enemy.NearToAttack(false);
            }
            else
            {
                enemy.SetState(new PatrollingWalk(enemy)); //si lo he perdido vuelvo a patrullar
            }
        }
    }
}
