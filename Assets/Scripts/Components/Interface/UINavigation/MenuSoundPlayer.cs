using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSoundPlayer : MonoBehaviour
{
    public SoundManager _soundManager;
    private string escenaActual = null;
    private string escenaAnterior = null;

    public void Start()
    {
        _soundManager = gameObject.GetComponent<SoundManager>();
       
    }
    private void Update()
    {
        escenaActual = SceneManager.GetActiveScene().name;
        //Debug.Log("Last: " + escenaAnterior + "    Awake: " + escenaActual);
        if (_soundManager == null)
        {
            if (escenaActual == "Game")
            {
                _soundManager = (SoundManager)GameManager.Instance.GetService("SoundManager");
                _soundManager.Play("gameMusic");
            }
            else if (escenaActual == "Menu")
            {
                _soundManager = (SoundManager)GameManager.Instance.GetService("SoundManager");
                _soundManager.Play("menuMusic");
            }
        }
        else if (escenaActual != "Credits" || escenaActual != "Records")
        {
            if (_soundManager != null && escenaActual != escenaAnterior)
            {
                _soundManager = null;
            }
        }

        escenaAnterior = escenaActual;
    }
    public bool IsActive()
    {
        return _soundManager.IsActive();
    }

    public void ChangeMute(bool active)
    {
        _soundManager.ChangeMute(active);
    }
}
