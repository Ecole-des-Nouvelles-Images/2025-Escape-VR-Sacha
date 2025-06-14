using System;
using Manager;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using Utils;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _settingPanel;
    [SerializeField] private NearFarInteractor _leftControllerNearFarInteractor;
    [SerializeField] private NearFarInteractor _rightControllerNearFarInteractor;
    
    private InputAction _menuButton;
    private InputAction _menuButtonInteraction;

    private void OnEnable()
    {
        SceneLoader.OnLoad += DisablePause;
    }

    private void OnDisable()
    {
        SceneLoader.OnLoad -= DisablePause;
    }

    private void OnDestroy()
    {
        SceneLoader.OnLoad -= DisablePause;
    }

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
        if (SceneManager.GetActiveScene().name != "MainMenu" && _menuButton.triggered)
        {
            SwitchMenuActivation();
            _leftControllerNearFarInteractor.enableFarCasting = !_leftControllerNearFarInteractor.enableFarCasting;
            _rightControllerNearFarInteractor.enableFarCasting = !_rightControllerNearFarInteractor.enableFarCasting;
        }
    }

    private void DisablePause(string bait)
    {
        _pauseMenu.SetActive(false);
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
