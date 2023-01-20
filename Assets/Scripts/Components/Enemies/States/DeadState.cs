using System.Collections;
using System.Collections.Generic;
using Components.Enemies.Interfaces;
using UnityEngine;

namespace Components.Enemies.States
{
    public class DeadState : AEnemiesState
    {
        private float move = 0;

        public DeadState(IEnemies enemy) : base(enemy)
        {

        }
        //Redefinimos estados como abstractos:
        public override void Enter()
        {
            enemy.SetCurrentSpeed(move);
            Debug.Log("Muerto");
        }

        public override void Exit()
        {
            Debug.Log("Nunca deberia leer este mensaje");
            //Debug.Log($"Enemy {enemy.GetGameObject().name} ended patrolling.");
        }

        //2 funcionalidades:
        //Cada segundo buscar al personaje
        //Mover al personaje
        public override void Update() //localizar al personaje
        {
        }
        public override void FixedUpdate() //movimiento mientras no busca
        {
            if(enemy.PlayerAtSight() == null)
            {
                GameManager.Destroy(enemy.GetGameObject());
            }
        }

    }
}
