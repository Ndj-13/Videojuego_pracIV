using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Components.Interface.UINavigation
{
    public class MainMenu : MonoBehaviour
    {
        //public MenuSoundPlayer msp;
        MenuSoundPlayer msp;

        [SerializeField] private Button startButton;
        [SerializeField] private Button creditsButton;
        [SerializeField] private Button recordsButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button musicButton;
        public Sprite musicButton1;
        public Sprite musicButton2;

        private void Awake()
        {
            startButton.onClick.AddListener(StartButtonClickedEvent); //AddListener en vez de AddObserver
            creditsButton.onClick.AddListener(CreditsButtonClickedEvent);
            //recordsButton.onClick.AddListener(RecordsButtonClickedEvent);
            quitButton.onClick.AddListener(QuitGame);
            //musicButton.onClick.AddListener(MusicButtonClickedEvent);
            Debug.Log("Menu Buttons Awaken");

            //GameObject mspObj = GameObject.Find("MenuSoundPlayer");
            //msp = mspObj.GetComponent<MenuSoundPlayer>();
        }

        private void OnDestroy()
        {
            /* startButton.onClick.RemoveListener(StartButtonClickedEvent);
             creditsButton.onClick.RemoveListener(CreditsButtonClickedEvent);
             recordsButton.onClick.RemoveListener(RecordsButtonClickedEvent);
             quitButton.onClick.RemoveListener(QuitGame);
             Debug.Log("Menu Buttons Destroyed");*/
        }

        public void StartButtonClickedEvent()
        {
            SceneManager.LoadScene("Story");
        }

        public void CreditsButtonClickedEvent()
        {
            SceneManager.LoadScene("Credits");
        }

        public void RecordsButtonClickedEvent()
        {
            SceneManager.LoadScene("Records");
        }

        private void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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
}
