using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStatUI : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerLevel;
    public TextMeshProUGUI playerWeapon;
    public Image playerSkillImage;

    void Update()
    {
        if(gameObject.activeSelf)
        {
            ShowPlayerStatUI();
        }
    }

    void ShowPlayerStatUI()
    {
        if (gameObject.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            SetTextData();

        }
    }

    void SetTextData()
    {
        string playerNameData = GameManager.Instance.playerName;
        string playerLevelData = SkillManager.Instance.player._playerModifier.PlayerLevel.ToString();
        string playerWeaponData = GameManager.Instance.playerWeaponName;
        playerSkillImage.sprite = GameManager.Instance.playerSkillData.icon; 

        playerName.text = playerNameData;
        playerLevel.text = playerLevelData;
        playerWeapon.text = playerWeaponData;
    }
}
