using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class SensitivitySetting : MonoBehaviour
{
    [SerializeField] private Slider sensitivitySlider;
    private const string save = "Sensitivity";
    public Player player { get; private set; }
    // Start is called before the first frame update
    void Start()
    {        
        float sensitivity = PlayerPrefs.GetFloat(save,1f);
        sensitivitySlider.value = sensitivity;
    }

    public void Sensitivity_Setting()
    {
        float sensitivity = sensitivitySlider.value;
        PlayerPrefs.SetFloat(save, sensitivity);
    }
}

