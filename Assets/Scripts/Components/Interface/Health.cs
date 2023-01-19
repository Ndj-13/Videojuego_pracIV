using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //[SerializeField] private float startingHealth;
    private float startingHealth;
    public float currentHealth;
    public int numHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    private Animator anim;
    private bool dead;
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");

        }
        else
        {
            if(!dead)
            anim.SetTrigger("Die");
            anim.SetInteger("MoveSpeed", 0); 
            dead = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
       
        
        for (int i = 0; i < hearts.Length; i++)
        {
           
            if (i < currentHealth)
            {
                if (((float)(i + 0.5f)) == currentHealth || 0.5 == currentHealth)
                {
                hearts[i].sprite = halfHeart;
                }else
                {
                    hearts[i].sprite = fullHeart;
                }
            }
            else
            {
                hearts[i].sprite = emptyHeart;
       
            }
            if(i < numHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
            TakeDamage(0.5f);
    }
}
