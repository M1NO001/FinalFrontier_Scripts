using UnityEngine;

public class MeleeWeaponModifier : MonoBehaviour
{

    private Player player;
    private PlayerSO Data;

    public GameObject MeleeWeapon;

    private void Awake()
    {
        player = GetComponent<Player>();
        Data = player.Data;
        MeleeWeapon.SetActive(false);

    }


    //Projectile
    [HideInInspector]
    public int Damage
    {
        get
        {
            return (int)(Data.PlayerMeleeWeaponData.MeleeWeaponAttackDamage
                   * MeleeWeaponAttackDamageModifier);
        }
    }
    [HideInInspector]
    public float Speed
    {
        get
        {
            return Data.PlayerMeleeWeaponData.MeleeWeaponAttackSpeed
                   * MeleeWeaponAttackSpeedModifier;
        }
    }
    [HideInInspector]
    public float KnockBackForce
    {
        get
        {
            return Data.PlayerMeleeWeaponData.MeleeWeaponKnockBackForce
                   * MeleeWeaponKnockBackForceModifier;
        }
    }

    [field: Header("MeleeWeapon Modifier")]
    [field: SerializeField][field: Range(0f, 10f)] public float MeleeWeaponAttackDamageModifier;

    [field: SerializeField][field: Range(0f, 10f)] public float MeleeWeaponAttackSpeedModifier;

    [field: SerializeField][field: Range(0f, 10f)] public float MeleeWeaponKnockBackForceModifier;
    

    [HideInInspector]
    public float UseStamina
    {
        get
        {
            return Data.PlayerMeleeWeaponData.MeleeWeaponUseStamina
                   * MeleeWeaponUseStaminaModifier;
        }
    }
    private float MeleeWeaponUseStaminaModifier = 1f;

    public void AttackStart()
    {
        if (MeleeWeapon.GetComponent<WeaponBase>().Air) MeleeWeapon.GetComponent<WeaponBase>().Air.Play();
        MeleeWeapon.GetComponent<WeaponBase>().col.enabled = true;
    }

    public void AttackEnd()
    {
        MeleeWeapon.GetComponent<WeaponBase>().col.enabled = false;
    }
}
