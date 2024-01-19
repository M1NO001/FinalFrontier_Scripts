using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterSelectScene : UI_Scene
{
    CinemachineVirtualCamera[] cameras;

    public TMP_Text CharacterName;
    public TMP_Text CharacterWeaponName;
    public TMP_Text CharacterWeapon;
    public Image CharacterWeaponImg;

    public TMP_Text CharacterSkillName;
    public TMP_Text CharacterSkill;
    public Image CharacterSkillImg;

    public TMP_Text CharacterUltName;
    public TMP_Text CharacterUlt;
    public Image CharacterUltImg;

    [SerializeField] private Button _nextSceneButton;
    [SerializeField] private Button _prevSceneButton;

    [SerializeField] private Button _rightSceneButton;
    [SerializeField] private Button _leftSceneButton;

    [SerializeField] public SkillSO[] playerSkill;

    int currentCharacterIdx;

    CharacterDocuments characterDocuments;

    private void Start()
    {
        cameras = new CinemachineVirtualCamera[4];
        cameras[0] = CharacterSelectSceneManager.Instance.ch1;
        cameras[1] = CharacterSelectSceneManager.Instance.ch2;
        cameras[2] = CharacterSelectSceneManager.Instance.ch3;
        cameras[3] = CharacterSelectSceneManager.Instance.ch4;

        currentCharacterIdx = 0;

        characterDocuments = GetComponent<CharacterDocuments>();

        _nextSceneButton.onClick.AddListener(NextScene);
        _prevSceneButton.onClick.AddListener(PreviousScene);
        _rightSceneButton.onClick.AddListener(RightArrow);
        _leftSceneButton.onClick.AddListener(LeftArrow);

        UpdateDocument();
    }

    public void RightArrow()
    {
        if (currentCharacterIdx == cameras.Length - 1)
        {
            cameras[0].gameObject.SetActive(true);

            cameras[currentCharacterIdx].gameObject.SetActive(false);

            currentCharacterIdx = 0;
        }
        else
        {
            cameras[currentCharacterIdx+1].gameObject.SetActive(true);

            cameras[currentCharacterIdx].gameObject.SetActive(false);

            currentCharacterIdx++;
        }

        UpdateDocument();
    }

    public void LeftArrow()
    {
        if (currentCharacterIdx == 0)
        {
            cameras[cameras.Length - 1].gameObject.SetActive(true);

            cameras[currentCharacterIdx].gameObject.SetActive(false);

            currentCharacterIdx = cameras.Length - 1;
        }
        else
        {
            cameras[currentCharacterIdx - 1].gameObject.SetActive(true);

            cameras[currentCharacterIdx].gameObject.SetActive(false);

            currentCharacterIdx--;
        }

        UpdateDocument();
    }

    public void UpdateDocument()
    {
        CharacterName.text = characterDocuments.documents[currentCharacterIdx].CharacterName;
        CharacterWeaponName.text = characterDocuments.documents[currentCharacterIdx].CharacterWeaponName;
        CharacterWeapon.text = characterDocuments.documents[currentCharacterIdx].CharacterWeaponDescription;
        CharacterWeaponImg.sprite = characterDocuments.documents[currentCharacterIdx].CharacterWeaponImg;

        CharacterSkillName.text = characterDocuments.documents[currentCharacterIdx].SkillName;
        CharacterSkill.text = characterDocuments.documents[currentCharacterIdx].SkillDescription;
        CharacterSkillImg.sprite = characterDocuments.documents[currentCharacterIdx].SkillImg;

        CharacterUltName.text = characterDocuments.documents[currentCharacterIdx].UltName;
        CharacterUlt.text = characterDocuments.documents[currentCharacterIdx].UltDescription;
        CharacterUltImg.sprite = characterDocuments.documents[currentCharacterIdx].UltImg;
    }

    public void NextScene()
    {
        GameManager.Instance.CharacterIdx = currentCharacterIdx;
        GameManager.Instance.playerName = CharacterName.text.ToString();
        GameManager.Instance.playerWeaponName = CharacterWeaponName.text.ToString();
        switch(CharacterWeaponName.text.ToString())
        {
            case "Rifle":
                GameManager.Instance.playerSkillData = playerSkill[0];
                break;
            case "Sniper":
                GameManager.Instance.playerSkillData = playerSkill[0];
                break;
            case "MachineGun":
                GameManager.Instance.playerSkillData = playerSkill[1];
                break;
            case "Pistol":
                GameManager.Instance.playerSkillData = playerSkill[1];
                break;
        }
        SceneLoadManager.Instance.LoadScene(Scenes.LoadingScene, Scenes.GamePlayScene);
    }

    public void PreviousScene()
    {
        SceneLoadManager.Instance.LoadScene(Scenes.StartScene);
    }
}
