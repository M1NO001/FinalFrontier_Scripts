using System;
using UnityEngine;

public class RangedWeaponModifier : MonoBehaviour
{
    private Player player;
    private PlayerSO Data;


    public GameObject RangedWeapon;
    private void Awake()
    {
        player = GetComponent<Player>();
        Data = player.Data;
    }

    [field: SerializeField] public GameObject pfBulletProjectile;
    [field: SerializeField] public Transform spawnBulletPosition;




    //Projectile
    [HideInInspector]
    public float ProjectileDamage
    {
        get 
        {
            return Data.PlayerRangedWeaponData.ProjectileDamage
                   * ProjectileDamageModifier;
        }
    }
    [HideInInspector]
    public int ProjectileNumber
    {
        get
        {
            return (int)((Data.PlayerRangedWeaponData.ProjectileNumber
                   + ProjectileNumberModifier)
                   * ProjectileNumberPercentageModifier);
        }
    }
    [HideInInspector]
    public float ProjectileSize
    {
        get
        {
            return Data.PlayerRangedWeaponData.ProjectileSize
                   * ProjectileSizeModifier;
        }
    }
    [HideInInspector]
    public float ProjectileSpeed
    {
        get
        {
            return Data.PlayerRangedWeaponData.ProjectileSpeed
                   * ProjectileSpeedModifier;
        }
    }
    [HideInInspector]
    public int ProjectilePiercing
    {
        get
        {
            return Data.PlayerRangedWeaponData.ProjectilePiercing
                   + ProjectilePiercingModifier;
        }
    }
    [HideInInspector]
    public float ShootRange
    {
        get
        {
            return Data.PlayerRangedWeaponData.ShootRange
                   * ShootRangeModifier;
        }
    }

    [field: Header("RangedWeapon Projectile Modifier")]
    [field: SerializeField][field: Range(0f, 10f)] public float ProjectileDamageModifier;

    [field: SerializeField][field: Range(0f, 10f)] public int ProjectileNumberModifier;
    [field: SerializeField][field: Range(0f, 10f)] public float ProjectileNumberPercentageModifier;

    [field: SerializeField][field: Range(0f, 10f)] public float ProjectileSizeModifier;

    [field: SerializeField][field: Range(0f, 10f)] public float ProjectileSpeedModifier;

    [field: SerializeField][field: Range(0f, 10f)] public int ProjectilePiercingModifier;

    [field: SerializeField][field: Range(0f, 10f)] public float ShootRangeModifier;




    //Gun

    [HideInInspector]
    public int BulletCapacity 
    { 
        get
        {
            return (int)(Data.PlayerRangedWeaponData.BulletCapacity * BulletCapacityModifier);
        }
    }

    [HideInInspector]
    public float FireRatePerMinute
    {
        get
        {
            return (Data.PlayerRangedWeaponData.FireRatePerMinute * FireRatePerMinuteModifier);
        }
    }

    [HideInInspector]
    public float ReloadingTime
    {
        get
        {
            return (Data.PlayerRangedWeaponData.ReloadingTime
                * ReloadingTimeModifier
                * StateReloadingTimeModifier); ;
        }
    }
    public float ReloadingTimeModifier = 1;
    public float StateReloadingTimeModifier = 1;




    [HideInInspector]
    public float MinuteOfAngleMin
    {
        get
        {
            return Data.PlayerRangedWeaponData.MinuteOfAngleMin
                    * MinuteOfAngleMinModifier
                    * MinuteOfAngleMinStateModifier
                    * MinuteOfAngleMinAimModifier;
        }
    }
    [HideInInspector] public float MinuteOfAngleMinModifier = 1f;
    [HideInInspector] public float MinuteOfAngleMinStateModifier = 1f;
    [HideInInspector] public float MinuteOfAngleMinAimModifier = 1f;

    [HideInInspector]
    public float MinuteOfAngleMax
    {
        get
        {
            return Data.PlayerRangedWeaponData.MinuteOfAngleMax;
        }
    }

    [HideInInspector]
    public float MinuteOfAngleRecoveryPerSec
    {
        get
        {
            return Data.PlayerRangedWeaponData.MinuteOfAngleRecoveryPerSec
                 * MinuteOfAngleRecoveryModifier
                 * MinuteOfAngleRecoveryStateModifier
                 * MinuteOfAngleRecoveryAimModifier;
        }
    }
    public float MinuteOfAngleRecoveryModifier = 1f;
    public float MinuteOfAngleRecoveryStateModifier = 1f;
    public float MinuteOfAngleRecoveryAimModifier = 1f;

    [HideInInspector]
    public float MinuteOfAngleIncreasePerFire
    {
        get
        {
            return Data.PlayerRangedWeaponData.MinuteOfAngleIncreasePerFire
                 * MinuteOfAngleIncreasePerFireModifier
                 * MinuteOfAngleIncreasePerFireStateModifier
                 * MinuteOfAngleIncreasePerFireAimModifier;
        }
    }
    
    public float MinuteOfAngleIncreasePerFireModifier = 1f;
    
    public float MinuteOfAngleIncreasePerFireStateModifier = 1f;
    
    public float MinuteOfAngleIncreasePerFireAimModifier = 1f;



    [HideInInspector]
    public float HorizontalRecoilPerFire
    {
        get
        {
            return Data.PlayerRangedWeaponData.HorizontalRecoil
                * HorizontalRecoilModifier
                * HorizontalRecoilStateModifier
                * HorizontalRecoilAimModifier;
        }
    }
    [HideInInspector] public float HorizontalRecoilModifier = 1f;
    [HideInInspector] public float HorizontalRecoilStateModifier = 1f;
    [HideInInspector] public float HorizontalRecoilAimModifier = 0.5f;

    [HideInInspector]
    public float VerticalRecoilPerFire
    {
        get
        {
            return Data.PlayerRangedWeaponData.VerticalRecoil
                * VerticalRecoilModifier
                * VerticalRecoilStateModifier
                * VerticalRecoilAimModifier;
        }
    }
    [HideInInspector] public float VerticalRecoilModifier = 1f;
    [HideInInspector] public float VerticalRecoilStateModifier = 1f;
    [HideInInspector] public float VerticalRecoilAimModifier = 0.5f;

    public float CriticalDamageChance
    {
        get
        {
            return Data.PlayerRangedWeaponData.CriticalDamageChance
                + CriticalDamageChanceModifier;
        }
    }
    
    public float CriticalDamageChanceModifier = 0f;

    public float CriticalDamageMultiplier
    {
        get
        {
            return Data.PlayerRangedWeaponData.CriticalDamageMultiplier
                + CriticalDamageMultiplierModifier;
        }
    }
    
    public float CriticalDamageMultiplierModifier = 0f;


    [field: Header("RangedWeapon Stat Modifier")]
    [field: SerializeField][field: Range(0f, 10f)] public float BulletCapacityModifier;

    [field: SerializeField][field: Range(0f, 1000f)] public float FireRatePerMinuteModifier;

    [field: SerializeField][field: Range(0f, 10f)] public float ReloadingTimeStandingModifier;
    [field: SerializeField][field: Range(0f, 10f)] public float ReloadingTimeCrouchingModifier;

    [field: Header("RangedWeapon MinuteOfAngle Modifier")]
    [field: SerializeField][field: Range(0f, 10f)] public float MinuteOfAngleMinStandingModifier;
    [field: SerializeField][field: Range(0f, 10f)] public float MinuteOfAngleMinWalkModifier;
    [field: SerializeField][field: Range(0f, 10f)] public float MinuteOfAngleMinRunModifier;
    [field: SerializeField][field: Range(0f, 10f)] public float MinuteOfAngleMinCrouchModifier;
    [field: SerializeField][field: Range(0f, 10f)] public float MinuteOfAngleMinAirModifier;

    [field: SerializeField][field: Range(0f, 10f)] public float MinuteOfAngleMinNon_AimingModifier;
    [field: SerializeField][field: Range(0f, 10f)] public float MinuteOfAngleMinAimingModifier;


    [field: SerializeField][field: Range(0f, 10f)] public float MinuteOfAngleRecoveryPerSecStandingModifier;
    [field: SerializeField][field: Range(0f, 10f)] public float MinuteOfAngleRecoveryPerSecCrouchModifier;
    [field: SerializeField][field: Range(0f, 10f)] public float MinuteOfAngleRecoveryPerSecWalkModifier;
    [field: SerializeField][field: Range(0f, 10f)] public float MinuteOfAngleRecoveryPerSecRunModifier;
    [field: SerializeField][field: Range(0f, 10f)] public float MinuteOfAngleRecoveryPerSecAirModifier;

    [field: SerializeField][field: Range(0f, 10f)] public float MinuteOfAngleRecoveryPerSecAimingModifier;
    [field: SerializeField][field: Range(0f, 10f)] public float MinuteOfAngleRecoveryPerSecNon_AimingModifier;


    [field: SerializeField][field: Range(0f, 10f)] public float MinuteOfAngleIncreasePerFireStandingModifier;
    [field: SerializeField][field: Range(0f, 10f)] public float MinuteOfAngleIncreasePerFireCrouchModifier;
    [field: SerializeField][field: Range(0f, 10f)] public float MinuteOfAngleIncreasePerFireWalkModifier;
    [field: SerializeField][field: Range(0f, 10f)] public float MinuteOfAngleIncreasePerFireRunModifier;
    [field: SerializeField][field: Range(0f, 10f)] public float MinuteOfAngleIncreasePerFireAirModifier;

    [field: SerializeField][field: Range(0f, 10f)] public float MinuteOfAngleIncreasePerFireAimingModifier;
    [field: SerializeField][field: Range(0f, 10f)] public float MinuteOfAngleIncreasePerFireNon_AimingModifier;



    [field: Header("RangedWeapon Recoil Modifier")]
    [field: SerializeField][field: Range(0f, 10f)] public float RecoilStandingModifier;
    [field: SerializeField][field: Range(0f, 10f)] public float RecoilWalkModifier;
    [field: SerializeField][field: Range(0f, 10f)] public float RecoilRunModifier;
    [field: SerializeField][field: Range(0f, 10f)] public float RecoilCrouchModifier;
    [field: SerializeField][field: Range(0f, 10f)] public float RecoilAirModifier;

    [field: SerializeField][field: Range(0f, 10f)] public float RecoilAimingModifier = 0.1f;
    [field: SerializeField][field: Range(0f, 10f)] public float RecoilNon_AimingModifier = 0.3f;


    

    




}
