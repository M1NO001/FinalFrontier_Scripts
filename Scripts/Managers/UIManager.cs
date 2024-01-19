using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    UI_Scene _sceneUI = null; // 현재의 고정 캔버스 UI

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        if(_sceneUI != null && typeof(T) == _sceneUI.GetType())
        {
            _sceneUI.gameObject.SetActive(true);
            return _sceneUI.gameObject.GetComponent<T>();
        }

        GameObject go = Instantiate(ResourceManager.Instance.LoadResource<GameObject>($"Prefabs/UI/Scene/{name}"));
        T sceneUI = go.GetComponent<T>();
        _sceneUI = sceneUI;

        return sceneUI;
    }

    public void CloseUI()
    {
        if(_sceneUI != null)
        {
            _sceneUI.gameObject?.SetActive(false);
        }
    }

    public void CloseSceneUI()
    {
        if (_sceneUI != null)
        {
            _sceneUI = null;
        }
    }
}