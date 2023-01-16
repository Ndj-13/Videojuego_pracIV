using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Interface
{
    public class GameScene : MonoBehaviour
    {
        [SerializeField] private Button pauseButton;

        public EventHandler OnPauseGame;
        //Notifica si el jugador pulsa algun boton de la interfaz de la pantalla de juego

        private void Awake()
        {
            pauseButton.onClick.AddListener(PauseButtonClickedEvent);
        }

        private void PauseButtonClickedEvent()
        {
            OnPauseGame.Invoke(this, null);
        }
    }
}