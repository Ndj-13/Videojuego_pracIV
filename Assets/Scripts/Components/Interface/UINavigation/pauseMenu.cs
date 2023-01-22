using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class pauseMenu : MonoBehaviour
{
    //public MenuSoundPlayer msp;
    MenuSoundPlayer msp;

    [SerializeField] private Button menuButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] GameObject pauseMen;

    [SerializeField] private Button musicButton;
    public Sprite musicButton1;
    public Sprite musicButton2;

    private void Awake()
    {
        menuButton.onClick.AddListener(MenuButtonClickedEvent); //AddListener en vez de AddObserver
        resumeButton.onClick.AddListener(ResumeButtonClickedEvent);
        pauseButton.onClick.AddListener(PauseButtonClickedEvent);
        musicButton.onClick.AddListener(MusicButtonClickedEvent);
        Debug.Log("Pause Buttons Awaken");
        pauseMen.SetActive(false);

        GameObject mspObj = GameObject.Find("MenuSoundPlayer");
        msp = mspObj.GetComponent<MenuSoundPlayer>();
    }

    private void OnDestroy()
    {
        /* menuButton.onClick.RemoveListener(MenuButtonClickedEvent);
         resumeButton.onClick.RemoveListener(ResumeButtonClickedEvent);
         pauseButton.onClick.RemoveListener(PauseButtonClickedEvent);
         Debug.Log("Pause Buttons Destroyed");*/
    }

    public void PauseButtonClickedEvent()
    {
        pauseMen.SetActive(true);
        //Time.timeScale = 0f;
    }

    public void ResumeButtonClickedEvent()
    {
        pauseMen.SetActive(false);
        //Time.timeScale = 1f;
    }

    public void MenuButtonClickedEvent()
    {
        SceneManager.LoadScene("Menu");
    }
    public void MusicButtonClickedEvent()
    {
        bool musicActive = msp.IsActive();
        Debug.Log("Music was playing: " + musicActive);
        if (musicActive)
        {
            musicButton.image.sprite = musicButton1;
            musicActive = false;
        }
        else if (!musicActive)
        {
            musicButton.image.sprite = musicButton2;
            musicActive = true;
        }
        msp.ChangeMute(musicActive);
    }
}