using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaCombate : MonoBehaviour //ENEMIGO
{
    public int hp;
    public int damageSword;
    public Animator m_animator;

    private void Start()
    {

    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Weapon"))
        {
            m_animator.SetTrigger("Damaged");
        }

        hp -= damageSword;

        if(hp <= 0)
        {
            m_animator.SetTrigger("Dead");
        }
    }

}
