using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : BaseSceneManager
{
    int backGroundImageNum;
    [SerializeField]
    Image progressBar;

    protected override bool Init()
    {
        if (!base.Init()) return false;

        SceneType = Scenes.LoadingScene;
        //UIManager.Instance.GetUIComponent<UI_LoadingScene>();
        backGroundImageNum = Random.Range(0, 4);
        Transform firstChild = transform.GetChild(0);
        Transform secondChild = firstChild.GetChild(backGroundImageNum);
        secondChild.gameObject.SetActive(true);
        StartCoroutine(LoadSceneProcess());
        

        return true;
    }

    public override void Clear()
    {
        //UIManager.Instance.RemoveUIComponent<UI_LoadingScene>();
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("LoadingScene");
    } // 이거 없애도 됨
   
    IEnumerator LoadSceneProcess()
    {
        yield return null;
        AsyncOperation op = SceneLoadManager.Instance.LoadSceneAsync();
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    break;
                }
            }
        }
    }
}
