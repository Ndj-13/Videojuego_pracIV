using System.Collections;
using System.Collections.Generic;
using Components.Enemies.Interfaces;
using UnityEngine;

namespace Components.Enemies.States
{
    public class AttackPlayer : AEnemiesState
    {
        private Transform playerTransform;
        private Transform currentTransform;

        private float secondsToSeek = 3f;
        private float lastSeek = 0f;

        private int canAttack;

        public AttackPlayer(IEnemies enemy) : base(enemy)
        {
        }

        public override void Enter()
        {
            currentTransform = enemy.GetGameObject().transform;
            playerTransform = enemy.PlayerAtSight().transform;

            canAttack = 0;

            Debug.Log("Estado atacar");
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            lastSeek += Time.deltaTime;
        }

        public override void FixedUpdate()
        {
            Vector3 toWaypoint = playerTransform.position - currentTransform.position;
            toWaypoint.y = 0;
            float distanceToWaypoint = toWaypoint.magnitude;

            if (canAttack == 0)
            {
                Debug.Log("Puede atacar");
                enemy.Attack(distanceToWaypoint);
                canAttack++;
            } else
            {
                enemy.Attack(2.0f);
            }
            if (lastSeek >= secondsToSeek || distanceToWaypoint > 1.5f)
            {
                enemy.SetState(new SearchingForPlayer(enemy));
                lastSeek = 0f;
                Debug.Log("Seeking for enemy");
            }
        }
    }
}