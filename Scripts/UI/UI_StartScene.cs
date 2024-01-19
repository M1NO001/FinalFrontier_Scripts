using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StartScene : UI_Scene
{
    [SerializeField] private GameObject SettingsUI; //UI매니저 이용하게끔 수정
    [SerializeField] private Button _StartButton;
    [SerializeField] private Button _EndingCreditButton;
    [SerializeField] private Button _ExitButton;
    [SerializeField] private Button _SettingButton;

    private StartSceneManager _startSceneManager;

    

    private void Awake()
    {
        _StartButton.onClick.AddListener(GameStart);
        _EndingCreditButton.onClick.AddListener(EndingCredit);
        _ExitButton.onClick.AddListener(ExitGame);
        _SettingButton.onClick.AddListener(OpenSettings);
    }
    private void Start()
    {
        _startSceneManager = StartSceneManager.Instance;
        SceneLoadManager.Instance.Initialized(_startSceneManager);
    }
    public void GameStart()
    {
        AudioManager.Instance.PlaySFX(SoundType.ButtonSound);
        SceneLoadManager.Instance.LoadScene(Scenes.LoadingScene, Scenes.CharacterSelectScene);
    }
    public void EndingCredit()
    {
        AudioManager.Instance.PlaySFX(SoundType.ButtonSound);
        SceneLoadManager.Instance.LoadScene(Scenes.EndingCredit);

    }
    public void ExitGame()
    {
        AudioManager.Instance.PlaySFX(SoundType.ButtonSound);
        Application.Quit();
    }
    public void OpenSettings()
    {
        AudioManager.Instance.PlaySFX(SoundType.ButtonSound);
        SettingsUI.SetActive(true);
    }
}
