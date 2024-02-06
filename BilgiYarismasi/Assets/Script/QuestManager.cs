using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<Image> questPanels = new List<Image>();

    private void Start()
    {
        foreach (var panel in questPanels)
        {
            panel.gameObject.SetActive(false);
        }

        OpenRandomPanel();
    }

    public void OpenRandomPanel()
    {

        int randomIndex = Random.Range(0, questPanels.Count);

        for (int i = 0; i < questPanels.Count; i++)
        {
            if (i == randomIndex)
            {
                questPanels[i].gameObject.SetActive(true);
            }
            else
            {
                questPanels[i].gameObject.SetActive(false);
            }
        }
    }
}
