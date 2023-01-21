using System.Collections.Generic;
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

    private void Awake()
    {
        //if (!weapon) { gameObject.GetComponent<BoxCollider>(); }
        if (!animator) { gameObject.GetComponent<Animator>(); }
        if (!rigidBody) { gameObject.GetComponent<Animator>(); }
        if (!health) { health = FindObjectOfType<Health>(); }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
       
    }

    private void OnTriggerExit(Collider other)
    {
        
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
