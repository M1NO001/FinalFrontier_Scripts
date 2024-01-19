using UnityEngine;
using UnityEngine.SceneManagement;


public enum Scenes
{
    Unknown,
    StartScene,
    CharacterSelectScene,
    GamePlayScene,
    LoadingScene,
    GameOverScene,
    EndingCredit,
    YCYDevScene
}


public class SceneLoadManager : Singleton<SceneLoadManager>
{
    private BaseSceneManager _curSceneManager;
    private Scenes _curSceneType = Scenes.StartScene; //���۾��� StartScene�̹Ƿ�
    private Scenes _nextSceneType = Scenes.Unknown; // ���� Scene�� LoadingScene�� ��� ������ ȣ�� �� Scene

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void Initialized(BaseSceneManager sceneManager)
    {
        _curSceneManager = sceneManager;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == _curSceneType.ToString())
        {
            LoadScenesManager();
        }
    }

    public void LoadScene(Scenes sceneType, Scenes nextSceneType = Scenes.Unknown)
    {
        if(_curSceneManager != null)
        {
            _curSceneManager.Clear();
        }

        _curSceneType = sceneType;
        if (_curSceneType == Scenes.LoadingScene) _nextSceneType = nextSceneType;
        SceneManager.LoadScene(_curSceneType.ToString());
    }

    public AsyncOperation LoadSceneAsync()
    {
        if (_curSceneManager != null)
        {
            _curSceneManager.Clear();
        }

        _curSceneType = _nextSceneType;
        return SceneManager.LoadSceneAsync(_nextSceneType.ToString());
    }

    public void LoadScenesManager()
    {
        BaseSceneManager go = FindObjectOfType<BaseSceneManager>();
        GameObject managerObj;
        if (go != null)
        {
            managerObj = go.gameObject;
        }
        else
        {
            managerObj = Instantiate(ResourceManager.Instance.LoadResource<GameObject>($"Prefabs/Managers/ScenesManager/{_curSceneType.ToString()}Manager"));
        }
        _curSceneManager = managerObj.GetComponent<BaseSceneManager>();
    }
}
