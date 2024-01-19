using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDocuments : MonoBehaviour
{

    public string AssualtRifleCharacterName;
    public string AssualtRifleCharacterWeaponName;
    public string AssualtRifleCharacterDescription;
    public Sprite AssualtRifleCharacterImg;

    public string AssualtRifleSkillName;
    public string AssualtRifleSkillDescription;
    public Sprite AssualtRifleSkillImg;

    public string AssualtRifleUltName;
    public string AssualtRifleUltDescription;
    public Sprite AssualtRifleUltImg;


    public string MachineGunCharacterName;
    public string MachineGunCharacterWeaponName;
    public string MachineGunCharacterDescription;
    public Sprite MachineGunCharacterImg;

    public string MachineGunSkillName;
    public string MachineGunSkillDescription;
    public Sprite MachineGunSkillImg;

    public string MachineGunUltName;
    public string MachineGunUltDescription;
    public Sprite MachineGunUltImg;


    public string SniperCharacterName;
    public string SniperCharacterWeaponName;
    public string SniperCharacterDescription;
    public Sprite SniperCharacterImg;

    public string SniperSkillName;
    public string SniperSkillDescription;
    public Sprite SniperSkillImg;

    public string SniperUltName;
    public string SniperUltDescription;
    public Sprite SniperUltImg;


    public string PistolCharacterName;
    public string PistolCharacterWeaponName;
    public string PistolCharacterDescription;
    public Sprite PistolCharacterImg;

    public string PistolSkillName;
    public string PistolSkillDescription;
    public Sprite PistolSkillImg;

    public string PistolUltName;
    public string PistolUltDescription;
    public Sprite PistolUltImg;

    public CharacterDocument[] documents;


    private void Awake()
    {
        documents = new CharacterDocument[4];

        documents[0] = new CharacterDocument(
            AssualtRifleCharacterName,
            AssualtRifleCharacterWeaponName,
            AssualtRifleCharacterDescription,
            AssualtRifleCharacterImg,

            AssualtRifleSkillName,
            AssualtRifleSkillDescription,
            AssualtRifleSkillImg,

            AssualtRifleUltName,
            AssualtRifleUltDescription,
            AssualtRifleUltImg);

        documents[1] = new CharacterDocument(
            MachineGunCharacterName,
            MachineGunCharacterWeaponName,
            MachineGunCharacterDescription,
            MachineGunCharacterImg,

            MachineGunSkillName,
            MachineGunSkillDescription,
            MachineGunSkillImg,

            MachineGunUltName,
            MachineGunUltDescription,
            MachineGunUltImg);

        documents[2] = new CharacterDocument(
            SniperCharacterName,
            SniperCharacterWeaponName,
            SniperCharacterDescription,
            SniperCharacterImg,

            SniperSkillName,
            SniperSkillDescription,
            SniperSkillImg,

            SniperUltName,
            SniperUltDescription,
            SniperUltImg);

        documents[3] = new CharacterDocument(
            PistolCharacterName,
            PistolCharacterWeaponName,
            PistolCharacterDescription,
            PistolCharacterImg,

            PistolSkillName,
            PistolSkillDescription,
            PistolSkillImg,

            PistolUltName,
            PistolUltDescription,
            PistolUltImg);
    }

}
