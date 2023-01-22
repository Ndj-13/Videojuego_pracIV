using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private Button menuButton;

    private void Awake()
    {
        menuButton.onClick.AddListener(MenuButtonClickedEvent);
    }

    private void OnDestroy()
    {
        menuButton.onClick.RemoveListener(MenuButtonClickedEvent);
    }

    public void MenuButtonClickedEvent()
    {
        SceneManager.LoadScene("Menu");
    }
}
