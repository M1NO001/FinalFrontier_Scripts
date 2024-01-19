using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class SensitivitySettingIngameMenu : MonoBehaviour
{
    [SerializeField] private Slider sensitivitySlider;
    private const string save = "Sensitivity";
    public Player player { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        player = GamePlaySceneManager.Instance.Player.GetComponentInChildren<Player>();
        float sensitivity = PlayerPrefs.GetFloat(save,1f);
        sensitivitySlider.value = sensitivity;
        SensitivityToPlayer(sensitivity);
        gameObject.SetActive(false);
    }

    public void Sensitivity_InGame()
    {
        float sensitivity = sensitivitySlider.value;
        SensitivityToPlayer(sensitivity);
        PlayerPrefs.SetFloat(save, sensitivity);
    }
    private void SensitivityToPlayer(float sensitivity)
    {
        player.LookSensitivity = 1f * sensitivity;
        player.AimSensitivity = 0.7f * sensitivity;
    }
}

