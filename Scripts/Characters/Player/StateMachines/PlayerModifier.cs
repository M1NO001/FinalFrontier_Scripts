using System;
using UnityEngine;

public class PlayerModifier : MonoBehaviour
{
    private Player player;
    private PlayerSO Data;
    private void Awake()
    {
        player = GetComponent<Player>();
        Data = player.Data;
    }

    [field: Header("Player Camera")]
    [field: SerializeField][field: Range(0f, 100f)] public float Fov;
    [field: SerializeField][field: Range(0f, 10f)] public float LookSensitivity = 1f;
    [field: SerializeField][field: Range(0f, 10f)] public float AimSeneitivity = 0.8f;
    [field: SerializeField][field: Range(0f, 1f)] public float CameraSide = 1f;


    //Player Movement
    [HideInInspector]
    public float MovementSpeed
    {
        get
        {
            return (Data.PlayerConditionData.MovementSpeed
                                   * MovementSpeedModifier
                                   * MovementStateModifier
                                   * MovementDirectionModifier
                                   * MovementSideDirectionModifier
                                   * MovementAimgModifier);
        }
    }

    public float MovementSpeedModifier = 1f;
    public float MovementStateModifier = 1f;
    public float MovementDirectionModifier = 1f;
    public float MovementSideDirectionModifier = 1f;
    public float MovementAimgModifier = 1f;

    [HideInInspector]
    public float JumpForce => (Data.PlayerConditionData.JumpForce * JumpForceModifier);

    public float JumpForceModifier = 1f;


    [field: Header("Player Movement Modifier")]

    [field: SerializeField][field: Range(0f, 10f)] public float IdleSpeedModifier = 0f;
    [field: SerializeField][field: Range(0f, 10f)] public float WalkSpeedModifier = 0.5f;
    [field: SerializeField][field: Range(0f, 10f)] public float RunSpeedModifier = 1f;
    [field: SerializeField][field: Range(0f, 10f)] public float CrouchSpeedModifier = 0.2f;
    [field: SerializeField][field: Range(0f, 10f)] public float AirSpeedModifier = 0.2f;

    [field: SerializeField][field: Range(0f, 10f)] public float ForwardSpeedModifier = 1f;
    [field: SerializeField][field: Range(0f, 10f)] public float BackwardSpeedModifier = 0.3f;

    [field: SerializeField][field: Range(0f, 10f)] public float Non_SideSpeedModifier = 1f;
    [field: SerializeField][field: Range(0f, 10f)] public float SideSpeedModifier = 0.8f;

    [field: SerializeField][field: Range(0f, 10f)] public float Non_AimingMoveSpeedModifier = 1f;
    [field: SerializeField][field: Range(0f, 10f)] public float AimingMoveSpeedModifier = 0.7f;




    //Player Condition
    [HideInInspector]
    private int playerLevel = 1;
    public int PlayerLevel { 
        get
        {
            return playerLevel;
        }
        set
        {
            if (playerLevel != value && value <= Data.PlayerConditionData.MaxLevel)
            {
                playerLevel = value;
                LevelUp?.Invoke();
            }
        }
    }

    public event Action LevelUp;

    
    [HideInInspector] public int EXP = 0;

    
    public float HealthPoint
    {
        get
        {
            return (Data.PlayerConditionData.StartMaxHealthPoint
                               + (Data.PlayerConditionData.IncreaseOfHealthPointPerLevel
                               * PlayerLevel))
                               * HealthPointModifier
                               + HealthPointNumModifier;
        }
    }
    [HideInInspector]
    public float HealthPointRecovery
    {
        get
        {
            return (Data.PlayerConditionData.StartHealthPointRecovery
                                       + (Data.PlayerConditionData.IncreaseOfHealthPointRecoveryPerLevel
                                       * PlayerLevel))
                                       * HealthPointRecoveryModifier
                                       + HealthPointRecoveryNumModifier;
        }
    }
    [HideInInspector]
    public float StaminaPoint
    {
        get
        {
            return (Data.PlayerConditionData.StartMaxStaminaPoint
                               + (Data.PlayerConditionData.IncreaseOfStaminaPointPerLevel
                               * PlayerLevel))
                               * StaminaPointModifier
                               + StaminaPointNumModifier;
        }
    }
    [HideInInspector]
    public float StaminaPointRecovery
    {
        get
        {
            return (Data.PlayerConditionData.StartStaminaPointRecovery
                                       + (Data.PlayerConditionData.IncreaseOfStaminaPointRecoveryPerLevel
                                       * PlayerLevel))
                                       * StaminaPointRecoveryModifier
                                       + StaminaPointRecoveryNumModifier;
        }
    }
    [HideInInspector]
    public float StaminaPointConsumption
    {
        get
        {
            return Data.PlayerConditionData.StaminaPointConsumption
                                         * StaminaPointConsumptionModifier;
        }
    }

    
    [field: Header("Player Condition Modifier")]
    [field: SerializeField][field: Range(0f, 100f)] public float HealthPointModifier = 1f;
    [field: SerializeField][field: Range(0f, 100f)] public int HealthPointNumModifier = 0;

    [field: SerializeField][field: Range(0f, 100f)] public float HealthPointRecoveryModifier = 1f;
    [field: SerializeField][field: Range(0f, 100f)] public float HealthPointRecoveryNumModifier = 1f;

    [field: SerializeField][field: Range(0f, 100f)] public float StaminaPointModifier = 1f;
    [field: SerializeField][field: Range(0f, 100f)] public float StaminaPointNumModifier = 0f;

    [field: SerializeField][field: Range(0f, 100f)] public float StaminaPointRecoveryModifier = 1f;
    [field: SerializeField][field: Range(0f, 100f)] public float StaminaPointRecoveryNumModifier = 0f;

    [field: SerializeField][field: Range(0f, 100f)] public float StaminaPointConsumptionModifier = 1f;

}
