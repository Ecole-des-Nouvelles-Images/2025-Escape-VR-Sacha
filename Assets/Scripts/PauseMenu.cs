using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    
    //private InputAction _menuButton;
    private InputAction _menuButtonInteraction;

    private void Awake()
    {
        //_menuButton = InputSystem.actions.FindAction("XRI Left/MenuButton", true);
        _menuButtonInteraction = InputSystem.actions.FindAction("XRI Left Interaction/MenuButton", true);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_menuButtonInteraction.IsPressed())
        {
            SwitchMenuActivation();
        }
    }

    private void SwitchMenuActivation()
    {
        if (_pauseMenu.activeSelf)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
        _pauseMenu.SetActive(!_pauseMenu.activeSelf);
    }
}
