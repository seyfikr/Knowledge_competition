using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject LoginPanel, SýgnInPanel;
  private bool panelSwitch=false;
  public void PanelSwitch()
    {
        if (!panelSwitch)
        {
            LoginPanel.SetActive(true);
            SýgnInPanel.SetActive(false);
            panelSwitch=true;

        }
        else if (panelSwitch)
        {
            LoginPanel.SetActive(false);
            SýgnInPanel.SetActive(true);
            panelSwitch=false;
            
        }
    }
}
