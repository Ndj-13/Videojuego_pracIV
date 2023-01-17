using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components.Enemies.Interfaces;

namespace Components.Enemies.States
{
    public class PatrollingWalk : AEnemiesState
    {
        private Transform currentTransform;
        private float speed;
        private float rotationSpeed;

        private float secondsToSeek = 1f;
        private float lastSeek = 0f;

        public PatrollingWalk(IEnemies enemy) : base(enemy)
        {

        }
        //Redefinimos estados como abstractos:
        public override void Enter()
        {
            //Al entrar en el estado andar andamos hacia delante
            currentTransform = enemy.GetGameObject().transform;
            speed = enemy.GetWanderSpeed();
            rotationSpeed = enemy.GetRotateSpeed(); //para cuando gire porq topa con un muro

            enemy.SetCurrentSpeed(speed);
            //Debug.Log($"Enemy {enemy.GetGameObject().name} started patrolling.");
        }

        public override void Exit()
        {
            //Debug.Log($"Enemy {enemy.GetGameObject().name} ended patrolling.");
        }

        //2 funcionalidades:
        //Cada segundo buscar al personaje
        //Mover al personaje
        public override void Update() //localizar al personaje
        {
            lastSeek += Time.deltaTime; 

            if(lastSeek >= secondsToSeek)
            {
                enemy.SetState(new SearchingForPlayer(enemy));
                lastSeek = 0f;
                //Debug.Log("Seeking for enemy");
            }
        }
        public override void FixedUpdate() //movimiento mientras no busca
        {
            enemy.MoveTo(null, speed, rotationSpeed);
        }

    }
}