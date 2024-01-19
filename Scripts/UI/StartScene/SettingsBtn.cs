using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsBtn : MonoBehaviour
{
    [SerializeField] private GameObject VolumePanel;
    [SerializeField] private GameObject SensitivityPanel;
    [SerializeField] private GameObject ControlsPanel;
    [SerializeField] private GameObject HighlightVolume;
    [SerializeField] private GameObject HighlightSesitivity;
    [SerializeField] private GameObject HighlightControls;

    public void Settings_Returns()
    {
        AudioManager.Instance.PlaySFX(SoundType.EndBtnSound);
        gameObject.SetActive(false);
    }
    public void Settings_Returns_InGame()
    {
        GamePlaySceneManager.Instance.TogglePause();
    }

    public void Settings_ExitGame()
    {
        BtnSound();
        SceneLoadManager.Instance.LoadScene(Scenes.StartScene);
    }
    public void Settings_Volume()
    {
        BtnSound();
        SetVolumePanel(true);
        SetSensitivityPanel(false);
        SetControls(false);
    }
    public void Settings_Sensitivity()
    {
        BtnSound();
        SetVolumePanel(false);
        SetSensitivityPanel(true);
        SetControls(false);
    }
    public void Settings_Controls()
    {
        BtnSound();
        SetVolumePanel(false);
        SetSensitivityPanel(false);
        SetControls(true);
    }
    private void SetVolumePanel(bool value)
    {
        VolumePanel.SetActive(value);
        HighlightVolume.SetActive(value);
    }
    private void SetSensitivityPanel(bool value)
    {
        SensitivityPanel.SetActive(value);
        HighlightSesitivity.SetActive(value);
    }
    private void SetControls(bool value)
    {
        ControlsPanel.SetActive(value);
        HighlightControls.SetActive(value);
    }
    private void BtnSound()
    {
        AudioManager.Instance.PlaySFX(SoundType.SettingSound);
    }
}
