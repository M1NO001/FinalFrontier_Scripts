using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : BaseSceneManager
{
    private static StartSceneManager _instance;

    public static StartSceneManager Instance
    {
        get
        {
            if (_instance != null) { return _instance; }

            _instance = FindObjectOfType<StartSceneManager>();
            if (_instance != null) { return _instance; }

            _instance = new GameObject(nameof(StartSceneManager)).AddComponent<StartSceneManager>();
            return _instance;
        }
    }

    protected override bool Init()
    {
        if (!base.Init()) return false;

        SceneType = Scenes.StartScene;
        AudioManager.Instance.PlayMusic(SoundType.MainSceneTheme);

        return true;
    }

    public override void Clear()
    {
        AudioManager.Instance.StopMusic();
        UIManager.Instance.CloseSceneUI();
    }
}
