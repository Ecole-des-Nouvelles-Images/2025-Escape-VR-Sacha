using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _settingPanel;
    
    private InputAction _menuButton;
    private InputAction _menuButtonInteraction;

    private void Awake()
    {
        _menuButton = InputSystem.actions.FindAction("XRI Left/MenuButton", true);
    }
    private void Start()
    {
        if (_pauseMenu.activeSelf)
        {
            _pauseMenu.SetActive(false);
        }
    }
    private void Update()
    {
        if (_menuButton.triggered)
        {
            SwitchMenuActivation();
        }
    }
    public void SwitchMenuActivation()
    {
        _pauseMenu.SetActive(!_pauseMenu.activeSelf);
    }

    public void SwitchSettingsActivation()
    {
        _mainPanel.SetActive(!_mainPanel.activeSelf);
        _settingPanel.SetActive(!_settingPanel.activeSelf);
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
