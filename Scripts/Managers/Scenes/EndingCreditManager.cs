using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingCreditManager : BaseSceneManager
{
    protected override bool Init()
    {
        AudioManager.Instance.PlayMusic(SoundType.CreditSceneTheme);
        if (!base.Init()) return false;
       
        SceneType = Scenes.EndingCredit;
        StartCoroutine(ReturnToMain());
        return true;
    }
    IEnumerator ReturnToMain()
    {
        yield return new WaitForSeconds(10f);
        SceneLoadManager.Instance.LoadScene(Scenes.StartScene);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneLoadManager.Instance.LoadScene(Scenes.StartScene);
        }
    }
}
