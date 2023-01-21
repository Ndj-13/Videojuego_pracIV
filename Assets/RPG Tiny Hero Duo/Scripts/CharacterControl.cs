using System.Collections.Generic;
using Patterns.Observer;
using Components.GameManagement.Scores;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private enum ControlMode
    {
        /// <summary>
        /// Up moves the character forward, left and right turn the character gradually and down moves the character backwards
        /// </summary>
        Tank
        /// <summary>
        /// Character freely moves in the chosen direction from the perspective of the camera
        /// </summary>
        //Direct
    }

    //[SerializeField] private float moveSpeed = 2;
    //[SerializeField] private float turnSpeed = 100;
    //[SerializeField] private float jumpForce = 4;
    [SerializeField] private float moveSpeed = 2;
    //[SerializeField] private Health health;
    private float turnSpeed = 100;
    private float jumpForce = 4;
    //[SerializeField] private BoxCollider weapon;
    private Health health;

    [SerializeField] private Animator animator = null;
    [SerializeField] private Rigidbody rigidBody = null;

    [SerializeField] private ControlMode controlMode = ControlMode.Tank;


    private float currentV = 0;
    private float currentH = 0;

    private readonly float interpolation = 10;
    private readonly float walkScale = 0.33f;
    private readonly float backwardsWalkScale = 0.16f;
    private readonly float backwardRunScale = 0.66f;

    private bool wasGrounded; //Midair
   private Vector3 m_currentDirection = Vector3.zero;

    private float jumpTimeStamp = 0;
    private float minJumpInterval = 0.9f;
    private bool jumpInput = false; //boton de saltar pulsado
    private int attackInput = 0;

    private bool _isGrounded; //saber si esta en el suelo

    private List<Collider> _collisions = new List<Collider>();

    SubjectPuppetsPicked publicarPuppets;
    [SerializeField] PuppetCounter observandoPuppets;

    [SerializeField] public Transform campoLimite;

    private GameObject puppetAtSight;
    private int notificar;
    private int numPuppets;

    private void Awake()
    {
        //if (!weapon) { gameObject.GetComponent<BoxCollider>(); }
        if (!animator) { gameObject.GetComponent<Animator>(); }
        if (!rigidBody) { gameObject.GetComponent<Animator>(); }
        if (!health) { health = FindObjectOfType<Health>(); }
        if (!campoLimite) { gameObject.GetComponent<Transform>(); }

        if (!publicarPuppets) { publicarPuppets = new SubjectPuppetsPicked(); }
        if (!observandoPuppets) { gameObject.GetComponent<PuppetCounter>(); }

        publicarPuppets.AddObserver(observandoPuppets);

        notificar = 0;
        numPuppets = 0;
     }

    public GameObject PuppetAtSight()
    {
        return puppetAtSight;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Puppet"))
        { 
            numPuppets++;
            //puppetAtSight = PuppetIsOnSight(other.gameObject);
            //if (puppetAtSight != null)
            //{
            //    Debug.Log($"Nuevo puppet, numero de puppets: {numPuppets}");
            //    notificar++;
            //    publicarPuppets.NotifyObservers(1);
            //}
            //else Debug.Log("Hay algo cerca pero no lo veo");
        }


        ////Debug.Log("Veo algo");
        //if (PuppetAtSight() != null)
        //{
        //    Debug.Log("Nuevo Puppet");
        //    notificar = 1;
        //    publicarPuppets.NotifyObservers(notificar);
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Puppet"))
        {
            Vector3 toWaypoint = other.gameObject.transform.position - transform.position;
            toWaypoint.y = 0;
            float distanceToWaypoint = toWaypoint.magnitude;

            if (distanceToWaypoint < 1.5f)
            {
                if(notificar < numPuppets)
                {
                    notificar++;
                    publicarPuppets.NotifyObservers(1);
                }
                
            }

        }
        //{
        //    //Debug.Log("Sigue habiendo algo cerca");
        //    //Debug.Log($"Notficar: {notificar} < numPuppets: {numPuppets}");
        //    if (notificar < numPuppets && puppetAtSight != null)
        //    {
        //        Debug.Log("Espera, ¿es un puppet!");
        //        notificar++;
        //        publicarPuppets.NotifyObservers(1);
        //    }
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Puppet"))
        {
            numPuppets--;
            if(numPuppets < notificar)
            {
                notificar--;
                publicarPuppets.NotifyObservers(-1);
            }
            //numPuppets--;
            //puppetAtSight = null;
            //if (notificar == numPuppets && notificar > 0)
            //{
            //    notificar--;
            //    publicarPuppets.NotifyObservers(-1);
            //}
        }

        //puppetAtSight = PuppetIsOnSight(other.gameObject);

        //if (PuppetAtSight() == null && notificar == 1)
        //{
        //    notificar = -1;
        //    Debug.Log("Hemos perdido al Puppet");
        //    publicarPuppets.NotifyObservers(-1);
        //}
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if(Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.05f)
            {
                if(!_collisions.Contains(collision.collider))
                {
                    _collisions.Add(collision.collider);
                }
                _isGrounded = true;
            }
        }

        if (collision.collider.CompareTag("EnemyHand"))
        {
            Debug.Log("Auch");
            //animator.SetTrigger("Hurt");
            health.TakeDamage(0.5f);
        }


    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            _isGrounded = true;
            if (!_collisions.Contains(collision.collider))
            {
                _collisions.Add(collision.collider);
            }
        }
        else
        {
            if (_collisions.Contains(collision.collider))
            {
                _collisions.Remove(collision.collider);
            }
            if (_collisions.Count == 0) { _isGrounded = false; }
        }

        if (collision.collider.CompareTag("EnemyHand"))
        {
            //collision.collider.enabled = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (_collisions.Contains(collision.collider))
        {
            _collisions.Remove(collision.collider);
        }
        if (_collisions.Count == 0) { _isGrounded = false; }

        if (collision.collider.CompareTag("EnemyHand"))
        {
            Debug.Log("Fuera puño");
            animator.ResetTrigger("Hurt");
            //collision.collider.enabled = true;
        }
    }



    private void Update()
    {

        if (!jumpInput && Input.GetKey(KeyCode.Space))
        {
            jumpInput = true;
            Debug.Log("Saltar");
        } 
        if(Input.GetKey(KeyCode.K))
        {
            attackInput = 1;
            //Debug.Log(attackInput+"Atacar"); 
        } else if (Input.GetKey(KeyCode.L))
        {
            attackInput = 2;
            //Debug.Log(attackInput+"Super ataque");
        }
        
    }

    private void FixedUpdate()
    {
        animator.SetBool("Grounded", _isGrounded);

        switch(controlMode)
        {
            case ControlMode.Tank:
                TankUpdate();
                break;
            default:
                Debug.LogError("Unsopported state");
                break;
        }

        wasGrounded = _isGrounded;
        jumpInput = false;
        attackInput = 0;
    }

    private void TankUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        bool walk = Input.GetKey(KeyCode.LeftShift);
        
        if (v < 0)
        {
            if (walk) { v *= backwardsWalkScale; }
            else { v *= backwardRunScale; }
        }
        else if (walk)
        {
            v *= walkScale;
        }

        currentV = Mathf.Lerp(currentV, v, Time.deltaTime * interpolation);
        currentH = Mathf.Lerp(currentH, h, Time.deltaTime * interpolation);

        //transform.Translate(0, currentH * Time.deltaTime * moveSpeed, 0);
        //transform.Translate(currentV * Time.deltaTime * moveSpeed, 0, 0);
        transform.position += transform.forward * currentV * moveSpeed * Time.deltaTime;
        //transform.position += transform.right * currentH * moveSpeed * Time.deltaTime;
        transform.Rotate(0, currentH * turnSpeed *Time.deltaTime, 0);

        animator.SetFloat("MoveSpeed_H", currentH);
        animator.SetFloat("MoveSpeed_V", currentV);

        Attack();

        JumpAndLanding();
    }

    private void Attack()
    {
        //Debug.Log(attackInput);
        if(attackInput == 1)
        {
            animator.SetInteger("Attack", 1);
        }
        else if (attackInput == 2)
        {
            animator.SetInteger("Attack", 2);
        }
        else
        {
            animator.SetInteger("Attack", 0);
        }
    }

    private void JumpAndLanding()
    {
        bool jumpCooldownOver = (Time.time - jumpTimeStamp) >= minJumpInterval;

        if(jumpCooldownOver && _isGrounded && jumpInput) //si ha terminado de saltar, esta en el suelo y le dan a saltar
        {
            jumpTimeStamp = Time.time;
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if(!wasGrounded && _isGrounded) //si no estaba en el suelo pero ahora lo esta le decimos q esta aterrizando
        {
            animator.SetTrigger("Land");
        }
        if(!_isGrounded && wasGrounded) //si ahora no esta en el suelo pero lo estaba le decimos q esta saltando
        {
            animator.SetTrigger("Jump");
        }

    }

    private GameObject PuppetIsOnSight(GameObject pup)
    {
        Vector3 pupDirection = (pup.transform.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, pupDirection); //calculo direccion entre el jugador y enemigo

        if (angle < 360 / 2)
        {
            RaycastHit hit;

            Vector3 endPosition = pup.transform.position;
            endPosition.y = campoLimite.position.y;

            if (Physics.Linecast(campoLimite.position, pup.transform.position, out hit)) //tiro una linea desde mi posicion hasta la suya para ver si hay algun objeto entre medias
            {
                if (hit.collider.CompareTag("Puppet"))
                {
                    return hit.collider.gameObject;
                }
                else return null;
            }
            else return null;
        }
        else return null;
    }
    /*

    private void FixedUpdate()
    {
        m_animator.SetBool("Grounded", m_isGrounded);

        switch (m_controlMode)
        {
            case ControlMode.Direct:
                DirectUpdate();
                break;

            case ControlMode.Tank:
                TankUpdate();
                break;

            default:
                Debug.LogError("Unsupported state");
                break;
        }

        m_wasGrounded = m_isGrounded;
        m_jumpInput = false;
    }

    private void DirectUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Transform camera = Camera.main.transform;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            v *= m_walkScale;
            h *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (direction != Vector3.zero)
        {
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

            transform.rotation = Quaternion.LookRotation(m_currentDirection);
            transform.position += m_currentDirection * m_moveSpeed * Time.deltaTime;

            m_animator.SetFloat("MoveSpeed", direction.magnitude);
        }

        JumpingAndLanding();
    }

    }*/
}
