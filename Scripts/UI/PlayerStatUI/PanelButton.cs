using UnityEngine;
using UnityEngine.EventSystems;

public class PanelButton : MonoBehaviour
{
    public GameObject statUI;
    public GameObject skillTreeUI;

    private string statPanelName;
    private string skillTreePanelName;


    private void Start()
    {
        statPanelName = "StatsPanel";
        skillTreePanelName = "SkillTreePanel";
    }


    public void OnClickBtn()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        string objectName = clickObject.name;

        SetBaseUI();
        
        if(objectName.Equals(statPanelName))
        {
            statUI.SetActive(true);
        }

        else if(objectName.Equals(skillTreePanelName))
        {
            skillTreeUI.SetActive(true);
        }

    }

    void SetBaseUI()
    {
        statUI.SetActive(false);
        skillTreeUI.SetActive(false);
    }
}
