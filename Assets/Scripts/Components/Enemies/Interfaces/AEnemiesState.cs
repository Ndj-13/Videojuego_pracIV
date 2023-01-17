using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Components.Enemies.Interfaces
{
    public abstract class AEnemiesState : IState
    {
        protected IEnemies enemy; //referencia al contexto
                                        //protected quiere decir q solo la pueden ver y modificar las clases hijas q extienden esta clase

        public AEnemiesState(IEnemies enemy)
        {
            this.enemy = enemy;
        }

        //Redefinimos estados como abstractos:
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
        public abstract void FixedUpdate();
    }
}
