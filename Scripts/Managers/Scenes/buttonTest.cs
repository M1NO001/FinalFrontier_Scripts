using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonTest : MonoBehaviour
{
    public void LoadGamePlayScene(int level)
    {
        SceneLoadManager.Instance.LoadScene(Scenes.LoadingScene, Scenes.GamePlayScene);
    }
    public void LoadYCYDevScene(int level)
    {
        SceneLoadManager.Instance.LoadScene(Scenes.YCYDevScene);
    }
}
