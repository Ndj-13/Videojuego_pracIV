using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components.Enemies.Interfaces;

namespace Components.Enemies.States
{
    public class RotatingToContinue : AEnemiesState
    {
        private Transform currentTransform; //transformada del zombie
        private float angle = 1.2f;
        private float h = 0.0f;

        private float m_currentV = 0;
        private float m_currentH = 0;

        private readonly float m_interpolation = 10;

        //private Vector3 newDirectionDir;
        //private Quaternion newDirectionRotation;
        private float rotateSpeed; //velocidad de rotacion del personaje

        public RotatingToContinue(IEnemies enemy) : base(enemy) //constryctor: llama a constructor de AzombieState pasandole el conexto pa q se lo guarde
        {
        }

        public override void Enter()
        {
            rotateSpeed = enemy.GetRotateSpeed();
            //newDirection = currentTransform.right.transform;
            currentTransform = enemy.GetGameObject().transform;
            Debug.Log($"Direccion actual{currentTransform.position}");

        }
        public override void Exit()
        {

        }
        public override void Update()
        {

        }

        public override void FixedUpdate()
        {
            //float v = Input.GetAxis("Vertical");
            h += Time.deltaTime;
            Debug.Log($"h = {h}");

            if(h < angle)
            {
                //m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
                m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);
                Debug.Log($"m_currentH: {m_currentH}");

                //currentTransform.position += currentTransform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
                currentTransform.Rotate(0, m_currentH * rotateSpeed * Time.deltaTime, 0);

                //currentRotation = m_currentH;
            }
            else
            {
                //animator.SetFloat("MoveSpeed")
                enemy.SetState(new PatrollingWalk(enemy));
            }
            //currentTransform.transform.rotation = Quaternion.RotateTowards(currentTransform.rotation, )
            //currentTransform.transform.Rotate(0, angle * rotateSpeed * Time.deltaTime, 0);
            //currentTransform.transform.rotation = Quaternion.RotateTowards(currentTransform.rotation, angle * rotateSpeed * Time.fixedDeltaTime, 0);
            
        }
    }
}
