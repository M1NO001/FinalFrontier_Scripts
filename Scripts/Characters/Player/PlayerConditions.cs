using System;
using System.Collections;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public interface IDamagable
{
    void TakePhysicalDamage(int damageAmount);
}

[System.Serializable]
public class Condition
{
    [HideInInspector]
    public float curValue;
    public float maxValue;
    public float minValue;

    public float regenValue;
    public float decayValue;

    public float regenRate;
    public float decayRate;

    public Image uiBar;
    public TMP_Text uiText;

    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, minValue);
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }

}


public class PlayerConditions : MonoBehaviour, IDamagable
{
    private Player player;

    public Condition health;
    public Condition stamina;

    public Condition minuteOfAngle;

    public UnityEvent onTakeDamage;
    public Action onDie;

    private bool isDead = false;

    PlayerExp playerExp;

    public bool isStaminaDecay = false;
    public bool isHealthDecay = false;
    public bool isFiring = false;

    [SerializeField] Image Dot1;
    [SerializeField] Image Dot2;
    [SerializeField] Image Dot3;
    [SerializeField] Image Dot4;

    [SerializeField] Image EXPBar;

    [SerializeField] Image GameOverUI;
    [SerializeField] Image GameOverUI2;
    [SerializeField] TMP_Text GameOverUI3;
    [SerializeField] Button RestartBtn;
    [SerializeField] Button MainMenuBtn;

    [SerializeField] AudioClip GameOverSFX;

    private void Awake()
    {
        //health = new Condition();
        //stamina = new Condition();

        minuteOfAngle = new Condition();


        player = GetComponent<Player>();


        playerExp = new PlayerExp();
        playerExp.Initialize();
    }

    void Start()
    {
        health.maxValue = player._playerModifier.HealthPoint;
        health.curValue = health.maxValue;
        health.regenValue = player._playerModifier.HealthPointRecovery;

        stamina.maxValue = player._playerModifier.StaminaPoint;
        stamina.curValue = stamina.maxValue;
        stamina.regenValue = player._playerModifier.StaminaPointRecovery;
        stamina.decayValue = player._playerModifier.StaminaPointConsumption;

        player._playerModifier.LevelUp += UpdateStatus;


        minuteOfAngle.maxValue = player._rangedWeaponModifier.MinuteOfAngleMax;
        minuteOfAngle.minValue = player._rangedWeaponModifier.MinuteOfAngleMin;
        minuteOfAngle.curValue = minuteOfAngle.minValue; 
    }

    void Update()
    {
        if (!isStaminaDecay)
        {
            stamina.Add(stamina.regenValue * Time.deltaTime);
        }

        if (!isHealthDecay)
        {
            health.Add(health.regenValue * Time.deltaTime);
        }

        if (minuteOfAngle.curValue > minuteOfAngle.minValue && !isFiring)
        {
            minuteOfAngle.Subtract(player._rangedWeaponModifier.MinuteOfAngleRecoveryPerSec * Time.deltaTime);
        }

        minuteOfAngle.minValue = player._rangedWeaponModifier.MinuteOfAngleMin;
        if(minuteOfAngle.curValue < minuteOfAngle.minValue)
        {
            minuteOfAngle.curValue = minuteOfAngle.minValue;
        }

        if(health.maxValue != player._playerModifier.HealthPoint || stamina.maxValue != player._playerModifier.StaminaPoint)
        {
            UpdateStatus();
        }
        
        if (player._playerModifier.EXP >= playerExp.RequiredEXP[player._playerModifier.PlayerLevel - 1] && player._playerModifier.PlayerLevel < 20)
        {
            player._playerModifier.EXP = player._playerModifier.EXP - playerExp.RequiredEXP[player._playerModifier.PlayerLevel - 1];
            player._playerModifier.PlayerLevel += 1;
        }
        
        if (EXPBar) EXPBar.fillAmount = (float)player._playerModifier.EXP / (float)playerExp.RequiredEXP[player._playerModifier.PlayerLevel - 1];

        Dot1.rectTransform.anchoredPosition = new Vector3(0, minuteOfAngle.curValue*1000, 0);
        Dot2.rectTransform.anchoredPosition = new Vector3(0, -minuteOfAngle.curValue*1000, 0);
        Dot3.rectTransform.anchoredPosition = new Vector3(minuteOfAngle.curValue*1000, 0, 0);
        Dot4.rectTransform.anchoredPosition = new Vector3(-minuteOfAngle.curValue*1000, 0, 0);

        health.uiBar.fillAmount = health.GetPercentage();
        stamina.uiBar.fillAmount = stamina.GetPercentage();

        health.uiText.text = ((int)health.curValue).ToString();
        stamina.uiText.text = ((int)stamina.curValue).ToString();

        //health.uiBar.fillAmount = health.GetPercentage();
        //stamina.uiBar.fillAmount = stamina.GetPercentage();
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public bool UseStamina()
    {
        float amount = stamina.decayValue * Time.deltaTime;
        if (stamina.curValue - amount < 0)
            return false;

        stamina.Subtract(amount);
        return true;
    }

    public bool UseStamina(float amount)
    {
        if (stamina.curValue - amount < 0)
            return false;

        stamina.Subtract(amount);
        return true;
    }

    public void Die()
    {
        player._playerInput.enabled = false;
        GameOver();
        onDie?.Invoke();
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        if (health.curValue > 0)
        {
            health.Subtract(damageAmount);
            if (health.curValue < 1.0f && !isDead)
                Die();
            onTakeDamage?.Invoke();
        }
    }

    public void UpdateStatus()
    {
        float tmp = health.maxValue;
        health.maxValue = player._playerModifier.HealthPoint;
        health.curValue += health.maxValue - tmp;
        health.regenValue = player._playerModifier.HealthPointRecovery;

        tmp = stamina.maxValue;
        stamina.maxValue = player._playerModifier.StaminaPoint;
        stamina.curValue += stamina.maxValue - tmp;
        stamina.regenValue = player._playerModifier.StaminaPointRecovery;
        stamina.decayValue = player._playerModifier.StaminaPointConsumption;
    }

    public void OnDisable()
    {
        player._playerModifier.LevelUp -= UpdateStatus;
    }


    public void GameOver()
    {
        isDead = true;
        player._animator.SetTrigger(player.AnimationData.DieParameterHash);

        if (GameOverUI && GameOverUI2 && GameOverUI3)
        {
            player._playerInput.enabled = false;
            

            StartCoroutine(SmoothImageFloat(GameOverUI, 0, 0.3f));
            StartCoroutine(SmoothImageFloat(GameOverUI2, 0, 0.8f));
            StartCoroutine(SmoothTextFloat(GameOverUI3, 0, 1f));
        }
    }

    IEnumerator SmoothImageFloat(Image target, float min, float max)
    {
        Color color = target.color; 
        color.a = min;

        while (true)
        {
            if (color.a >= max) break;
            color.a += Time.deltaTime*0.2f;
            target.color = color;

            yield return null;
        }

        yield break;
    }

    IEnumerator SmoothTextFloat(TMP_Text target, float min, float max)
    {
        Color color = target.color;
        color.a = min;

        while (true)
        {
            if (color.a >= max) break;
            color.a += Time.deltaTime * 0.2f;
            target.color = color;

            yield return null;
        }

        RestartBtn.gameObject.SetActive(true);
        MainMenuBtn.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        yield break;
    }

    public void RestartScene()
    {
        SceneLoadManager.Instance.LoadScene(Scenes.LoadingScene, Scenes.GamePlayScene);
    }

    public void StartScene()
    {
        SceneLoadManager.Instance.LoadScene(Scenes.StartScene);
    }
}