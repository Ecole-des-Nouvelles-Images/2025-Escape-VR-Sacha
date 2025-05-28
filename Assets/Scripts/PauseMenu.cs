using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _settingPanel;
    [SerializeField] private NearFarInteractor _leftControllerNearFarInteractor;
    [SerializeField] private NearFarInteractor _rightControllerNearFarInteractor;
    
    private InputAction _menuButton;
    private InputAction _menuButtonInteraction;

    private void Awake()
    {
        _leftControllerNearFarInteractor.enableFarCasting = false;
        _rightControllerNearFarInteractor.enableFarCasting = false;
        _menuButton = InputSystem.actions.FindAction("XRI Left/MenuButton", true);
        _pauseMenu.SetActive(false);
    }
    private void Start()
    {
        _pauseMenu.SetActive(false);
    }
    private void Update()
    {
        if (_menuButton.triggered)
        {
            SwitchMenuActivation();
            _leftControllerNearFarInteractor.enableFarCasting = !_leftControllerNearFarInteractor.enableFarCasting;
            _rightControllerNearFarInteractor.enableFarCasting = !_rightControllerNearFarInteractor.enableFarCasting;
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
