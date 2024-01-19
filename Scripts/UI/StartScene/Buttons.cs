using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] private GameObject SettingsUI;
    
    public void GameStart()
    {
        AudioManager.Instance.PlaySFX(SoundType.ButtonSound);
        //LoadingSceneManager.LoadScene("GamePlayScene");
        SceneLoadManager.Instance.LoadScene(Scenes.LoadingScene, Scenes.GamePlayScene);
    }
    public void EndingCredit()
    {
        AudioManager.Instance.PlaySFX(SoundType.ButtonSound);
        //SceneManager.LoadScene("EndingCredit");
        SceneLoadManager.Instance.LoadScene(Scenes.EndingCredit);

    }

    public void ExitGame()
    {
        AudioManager.Instance.PlaySFX(SoundType.EndBtnSound);
        Application.Quit();
    }
    public void OpenSettings()
    {
        AudioManager.Instance.PlaySFX(SoundType.ButtonSound);
        SettingsUI.SetActive(true);
    }

}
