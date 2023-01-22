using System.Collections;
using System.Collections.Generic;
using Components.Enemies.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;
using Components.Enemies.States;
using IState = Components.Enemies.Interfaces.IState;

namespace Components.Enemies
{
    public class EnemyController : MonoBehaviour, IEnemies
    {
        /*------------------------------------------------
         * Propiedades dependientes del tipo de enemigo:
        --------------------------------------------------*/
        public EnemiesScriptable enemyType;
        private int life;

        /*------------------------------------------------
         * Otras propiedades del enemigo:
        -------------------------------------------------*/
        //Movimiento
        //[SerializeField] private float m_moveSpeed = 2;
        /*[SerializeField]*/ private float m_turnSpeed = 100;

        [SerializeField] private Animator m_animator = null;
        [SerializeField] private Rigidbody m_rigidBody = null;

        /*---------------------------------------------------
         * Estados del enemigo:
        -----------------------------------------------------*/
        public int viewingAngle; //cuanto ve --> angulo de vision
        public float WanderSpeed;
        public float ChaseSpeed;

        private IState currentState; 

        /*-----------------------------------------------------
         * Vision del enemigo:
        ------------------------------------------------------*/
        public Transform eyesTransform; 

        private GameObject playerAtSight; 
        private GameObject wallAtSight;
        private GameObject insideTrigger;

        private void Awake()
        {            
            if (!m_animator) { gameObject.GetComponent<Animator>(); }
            if (!m_rigidBody) { gameObject.GetComponent<Rigidbody>(); }
            //if (!health) { health = new Health(); }

            life = enemyType.maxLife;

            SetState(new PatrollingWalk(this)); 
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        #region Get & Set speeds
        public float GetRotateSpeed()
        {
            return m_turnSpeed;
        }

        public float GetWanderSpeed()
        {
            return WanderSpeed;
        }

        public float GetChaseSpeed()
        {
            return ChaseSpeed;
        }

        public void SetCurrentSpeed(float speed)
        {
            m_animator.SetFloat("MoveSpeed", speed);
        }

        public void Attack(float active)
        {
            m_animator.SetFloat("Attack", active);
        }

        public GameObject Radius()
        {
            return insideTrigger;
        }
        #endregion

        #region Set y Get State, state pattern
        public IState GetState()
        {
            return currentState; //estado actual
        }

        public void SetState(IState state)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }

            currentState = state; 
            currentState.Enter(); 
        }

        private void Update()
        {
            currentState.Update(); //llama a update de estado actual
            
        }

        private void FixedUpdate()
        {
            currentState.FixedUpdate(); //llama a fixedupdate de estado actual
        }
        #endregion

        #region Player at sight calculations
        public GameObject PlayerAtSight()
        {
            return playerAtSight;
        }
        public GameObject WallAtSight()
        {
            return wallAtSight;
        }

        private void OnTriggerEnter(Collider other) 
        {
            if (other.gameObject.CompareTag("Player"))
            {
                //Debug.Log("Estoy viendo al jugador");
                playerAtSight = PlayerIsOnSight(other.gameObject);
                insideTrigger = other.gameObject;

            }
            else if (other.gameObject.CompareTag("Wall"))
            {
                wallAtSight = WallIsOnSight(other.gameObject);
            }
            
            
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                playerAtSight = PlayerIsOnSight(other.gameObject);
                insideTrigger = other.gameObject;

            }
            else if (other.gameObject.CompareTag("Wall"))
            {
                wallAtSight = WallIsOnSight(other.gameObject);

            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerAtSight = null; //en el caso de q el jugador haya entrado en mi vision pero haya salido, tengo q devolver el valor a null
                insideTrigger = null;

            }
            else if (other.gameObject.CompareTag("Wall"))
            {
                wallAtSight = null;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Weapon"))
            {
                Debug.Log("Enemigo abatido");
                m_animator.SetTrigger("Hurt");
                life -= 5;
                Debug.Log($"Life: {life}");
                //health.TakeDamage(10.0f);

                if(life == 0)
                {
                    m_animator.SetBool("Dead", true);
                    currentState = new DeadState(this);
                }

            }
        
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.collider.CompareTag("Weapon"))
            {
                m_animator.ResetTrigger("Hurt");

            }

        }

        private GameObject WallIsOnSight(GameObject wall)
        {
            //Vector3 wallDirection = (wall.transform.position - transform.position).normalized;
            float range = 2.0f;
            
            //float angle = Vector3.Angle(transform.forward, wallDirection); //angulo tiene q ser 0 para girar
            //Debug.Log($"Pared a un angulo de {angle} grados");

            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, range)) //tiro una linea desde mi posicion hasta la suya para ver si hay algun objeto entre medias
            {
                if (hit.collider.CompareTag("Wall"))
                {
                    //Debug.Log("Muro a poca distancia");
                    return hit.collider.gameObject;
                }
                else return null;

            } else return null;

        }

        private GameObject PlayerIsOnSight(GameObject player)
        {
            Vector3 playerDirection = (player.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, playerDirection); //calculo direccion entre el jugador y enemigo

            if (angle < viewingAngle / 2) //si esta a la mitad de ese angulo
            {
                RaycastHit hit;

                Vector3 endPosition = player.transform.position;
                endPosition.y = eyesTransform.position.y;

                if (Physics.Linecast(eyesTransform.position, player.transform.position, out hit)) //tiro una linea desde mi posicion hasta la suya para ver si hay algun objeto entre medias
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        return hit.collider.gameObject; //devuelve al jugador si no hay nada entre medias y devolvera null si hay algo entre medias
                                                        //(no lo veo porq hay algo en medio)
                    }
                    else return null;
                } else return null;
            } else return null;
        }
        #endregion

        public void MoveTo(Transform target, float speed, float rotationSpeed) //calculos para mover personaje hasta el jugador
        {
            if(target == null)
            {
                //Debug.Log("MoveTo");
                transform.position += transform.forward * WanderSpeed * Time.deltaTime;
                //transform.Rotate(0, m_currentH * RotateSpeed * Time.deltaTime, 0);

                m_animator.SetFloat("MoveSpeed", speed);
            } else
            {
                Vector3 direction = (target.position - transform.position).normalized;
                direction.y = 0;    // No direction in vertical axis
                Quaternion toTargetRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toTargetRotation,
                    rotationSpeed * Time.deltaTime);

                transform.Translate(0, 0, speed * Time.fixedDeltaTime, Space.Self);
            }
            
        }
    }
}
