using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingSceneTips : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI TMPtext;
    [SerializeField] private LoadingSceneTipsSO loadingSceneTips;
    int textNum;
    // Start is called before the first frame update
    void Start()
    {
        textNum = Random.Range(0, loadingSceneTips.tipTexts.Length);
        TMPtext.text = loadingSceneTips.tipTexts[textNum];
    }
}
