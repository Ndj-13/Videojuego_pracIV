using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace Components.Interface.UINavigation
{
    public class pauseMenu : MonoBehaviour
    {
        //public MenuSoundPlayer msp;
        MenuSoundPlayer msp;

        [SerializeField] private Button menuButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button pauseButton;
        [SerializeField] GameObject pauseMen;

        [SerializeField] private Button musicButton;
        [SerializeField] private TextMeshProUGUI mText;
        bool musicActive = true;
        [SerializeField] private AudioSource music;
        //public Sprite musicButton1;
        //public Sprite musicButton2;

        private void Awake()
        {
            menuButton.onClick.AddListener(MenuButtonClickedEvent); //AddListener en vez de AddObserver
            resumeButton.onClick.AddListener(ResumeButtonClickedEvent);
            pauseButton.onClick.AddListener(PauseButtonClickedEvent);
            musicButton.onClick.AddListener(MusicButtonClickedEvent);
            Debug.Log("Pause Buttons Awaken");
            pauseMen.SetActive(false);

            //GameObject mspObj = GameObject.Find("MenuSoundPlayer");
            //msp = mspObj.GetComponent<MenuSoundPlayer>();
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
            Time.timeScale = 0f;
        }

        public void ResumeButtonClickedEvent()
        {
            pauseMen.SetActive(false);
            Time.timeScale = 1f;
        }

        public void MenuButtonClickedEvent()
        {
            SceneManager.LoadScene("Menu");
        }
        public void MusicButtonClickedEvent()
        {
            if (musicActive)
            {
                mText.text = "Music: OFF";
                music.mute = true;
                musicActive = false;
            }
            else if (!musicActive)
            {
                mText.text = "Music: ON";
                music.mute = false;
                musicActive = true;
            }
        }
    }
}