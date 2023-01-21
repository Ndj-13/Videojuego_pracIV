using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components.Enemies.Interfaces;

namespace Components.Enemies.States
{
    public class SearchingForPlayer : AEnemiesState
    {
        //Llama a esta clase 1 vez por segundo: el zombie va buscando por donde mirar y se va moviendo por el escenario
        //Mientras hace esto, cada segundo va mirando si ve al personaje, si el metodo no le devuelve null (es decir lo esta viendo) y cambia de estado a perseguirle
        public SearchingForPlayer(IEnemies enemy) : base(enemy)
        {
        }

        public override void Enter()
        {
            //Debug.Log("Buscando al jugador");
            //Debug.Log($"Enemy {enemy.GetGameObject().name} started searching for player");
        }

        public override void Exit()
        {
            //Debug.Log($"Enemy {enemy.GetGameObject().name} ended searching for player");
        }

        public override void Update()
        {
            if (enemy.PlayerAtSight() != null) //si no devuelve null quiere decir q ha encontrado al jugador y lo persigue
            {
                //Debug.Log("Jugador encontrado");
                enemy.SetState(new ChasingPlayer(enemy));
            }
            else
            {
                enemy.SetState(new PatrollingWalk(enemy)); //si no, continua patrullando
            }
        }

        public override void FixedUpdate()
        {
        }
    }
}