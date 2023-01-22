using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Story : MonoBehaviour
{
    [SerializeField] GameObject vineta1;
    [SerializeField] GameObject vineta2;
    [SerializeField] GameObject vineta3;
    public Animator animator;

    void Start()
    {
        vineta2.SetActive(false);
        vineta3.SetActive(false);
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(4);
        vineta2.SetActive(true);
        yield return new WaitForSeconds(4);
        vineta3.SetActive(true);
        yield return new WaitForSeconds(4);
        animator.SetTrigger("FadeOutTrg");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene("Game");
    }
}