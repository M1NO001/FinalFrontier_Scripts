using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MouseSensitivity : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float mouseSensitivity = 10f;
    public Transform playerBody;
    float xRotation = 0f;

    private void Start()
    {
        
        mouseSensitivity = PlayerPrefs.GetFloat("currentSensitivity", 10f);
        slider.value = mouseSensitivity/10;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void AdjustSpeed(float newSpeed)
    {
       
    }
}
