using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditSceneManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneLoadManager.Instance.LoadScene(Scenes.StartScene);
            AudioManager.Instance.PlayMusic(SoundType.MainSceneTheme);
        }
    }
}
