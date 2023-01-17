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
        public int viewingAngle; //cuanto ve --> angulo de vision
        public float WanderSpeed;
        public float ChaseSpeed;
        public float RotateSpeed;

        private float m_currentV = 0;
        private float m_currentH = 0;

        private readonly float m_interpolation = 10;

        [SerializeField] private Animator m_animator = null;

        private IState currentState; //mantiene estado actual --> contexto sabe cual es el estado actual

        //public Transform currentWaypoint; //way point actual
        //public Transform[] waypoints; //listado de way points

        private Animator animator; //para cambiar de animacion
        public Transform eyesTransform; //hemos creado una esfera que es el radio de vision y desde unity a esta variable le asignamos ese objeto

        private GameObject playerAtSight; //guarda si esta viendo al jugador

        private void Awake()
        {
            //assert: si no se cumple corta codigo para q pare --> comprueba q al menos haya un waypoint definido
            //Assert.IsTrue(waypoints.Length > 0, "Waypoints must be greater than 1");
            //if (currentWaypoint == null)
            //{
            //    currentWaypoint = waypoints[0]; //si no sabemos cual es el actual se pone q vaya al 0
            //}

            animator = gameObject.GetComponent<Animator>();

            SetState(new PatrollingWalk(this)); //contexto esta poniendo como estado inicial el search y a partir de aqui ya los otros estados van saltando de un estado a otro
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        #region Get & Set speeds
        public float GetRotateSpeed()
        {
            return RotateSpeed;
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
            animator.SetFloat("MoveSpeed", speed);
        }
        #endregion

        #region Set y Get State, state pattern
        public IState GetState()
        {
            return currentState; //estado actual
        }

        public void SetState(IState state)
        {
            //cuando algn le llama para cambiar de estado, comprueba estado actual para ejecutar metodo exit, para salir del estado
            // Exit old state
            if (currentState != null)
            {
                currentState.Exit();
            }

            // Set current state and enter
            currentState = state; //dice cual es el nuevo estado actual
            currentState.Enter(); // le dice q ejecute nuevo estado
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

        private void OnTriggerEnter(Collider other) //si me entra una colision miro si es el jugador
        {
            if (other.CompareTag("Player"))
            {
                playerAtSight = PlayerIsOnSight(other.gameObject);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerAtSight = PlayerIsOnSight(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerAtSight = null; //en el caso de q el jugador haya entrado en mi vision pero haya salido, tengo q devolver el valor a null
            }
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
                }
            }

            return null;
        }
        #endregion

        public void MoveTo(Transform target, float speed, float rotationSpeed) //calculos para mover personaje hasta el jugador
        {
            if(target == null)
            {
                Debug.Log("MoveTo");
                transform.position += transform.forward * WanderSpeed * Time.deltaTime;
                //transform.Rotate(0, m_currentH * RotateSpeed * Time.deltaTime, 0);

                animator.SetFloat("MoveSpeed", speed);
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
