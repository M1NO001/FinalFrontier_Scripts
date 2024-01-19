using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDocument
{

    public string CharacterName;
    public string CharacterWeaponName;
    public string CharacterWeaponDescription;
    public Sprite CharacterWeaponImg;

    public string SkillName;
    public string SkillDescription;
    public Sprite SkillImg;

    public string UltName;
    public string UltDescription;
    public Sprite UltImg;

    public CharacterDocument(
        string CharacterName,
        string CharacterWeaponName,
        string CharacterDescription,
        Sprite CharacterImg,

        string SkillName,
        string SkillDescription,
        Sprite SkillImg,

        string UltName,
        string UltDescription,
        Sprite UltImg)
    {
        this.CharacterName = CharacterName;
        this.CharacterWeaponName = CharacterWeaponName;
        this.CharacterWeaponDescription = CharacterDescription;
        this.CharacterWeaponImg = CharacterImg;

        this.SkillName = SkillName;
        this.SkillDescription = SkillDescription;
        this.SkillImg = SkillImg;

        this.UltName = UltName;
        this.UltDescription = UltDescription;
        this.UltImg = UltImg;
    }


}
