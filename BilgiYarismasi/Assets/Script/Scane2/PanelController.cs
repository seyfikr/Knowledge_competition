using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject LoginPanel, S�gnInPanel;
  private bool panelSwitch=false;
  public void PanelSwitch()
    {
        if (!panelSwitch)
        {
            LoginPanel.SetActive(true);
            S�gnInPanel.SetActive(false);
            panelSwitch=true;

        }
        else if (panelSwitch)
        {
            LoginPanel.SetActive(false);
            S�gnInPanel.SetActive(true);
            panelSwitch=false;
            
        }
    }
}
