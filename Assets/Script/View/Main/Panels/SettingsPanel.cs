using System;
using UnityEngine;

public class SettingsPanel : BaseGui, IPanel
{
    public PanelType PanelType { get { return PanelType.SETTINGS; } }

    public SettingsPanel(Transform content)
    {

    }

    public void Disable()
    {
    }

    public void Enable()
    {
    }
}
