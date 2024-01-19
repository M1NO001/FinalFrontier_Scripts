using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectSceneManager : BaseSceneManager
{
    private static CharacterSelectSceneManager _instance;

    public static CharacterSelectSceneManager Instance
    {
        get
        {
            if (_instance != null) { return _instance; }

            _instance = FindObjectOfType<CharacterSelectSceneManager>();
            if (_instance != null) { return _instance; }

            _instance = new GameObject(nameof(CharacterSelectSceneManager)).AddComponent<CharacterSelectSceneManager>();
            return _instance;
        }
    }

    public CinemachineVirtualCamera ch1 { get; private set; }
    public CinemachineVirtualCamera ch2 { get; private set; }
    public CinemachineVirtualCamera ch3 { get; private set; }
    public CinemachineVirtualCamera ch4 { get; private set; }

    protected override bool Init()
    {
        if (!base.Init()) return false;

        SceneType = Scenes.CharacterSelectScene;

        GameObject chinemachineCam = Instantiate(ResourceManager.Instance.LoadResource<GameObject>("Prefabs/CN_Camera"));
        ch1 = SettingCamera(chinemachineCam, new Vector3(-14.17f, 1.17f, 11.08f), new Vector3(0, 0, 0), 10, 40);
        ch2 = SettingCamera(Instantiate(chinemachineCam), new Vector3(-14.574f, 0.318f, 6.109f), new Vector3(345.5255f, 106.6496f, 357.1428f), 11, 40);
        ch3 = SettingCamera(Instantiate(chinemachineCam), new Vector3(-14.65f, 2.75f, 1.73f), new Vector3(24.8396f, 196.8873f, 8.3051f), 12, 40);
        ch4 = SettingCamera(Instantiate(chinemachineCam), new Vector3(-35.02f, 1.36f, 11.65f), new Vector3(0, 299.9238f, 0), 13, 40);

        ch1.gameObject.SetActive(true);
        ch2.gameObject.SetActive(false);
        ch3.gameObject.SetActive(false);
        ch4.gameObject.SetActive(false);

        UIManager.Instance.ShowSceneUI<UI_CharacterSelectScene>();

        return true;
    }

    private CinemachineVirtualCamera SettingCamera(GameObject go, Vector3 position, Vector3 rotation, int priority, float Fov)
    {
        go.transform.SetPositionAndRotation(position, Quaternion.Euler(rotation));
        CinemachineVirtualCamera cam = go.GetComponent<CinemachineVirtualCamera>();
        if (cam != null)
        {
            cam.Priority = priority;
            cam.m_Lens.FieldOfView = Fov;
        }
        return cam;
    }

    public override void Clear()
    {
        UIManager.Instance.CloseSceneUI();
    }
}
