using System;
using UnityEngine;

public class ShipPanel : BaseGui, IPanel
{
    public PanelType PanelType { get { return PanelType.SHIP; } }

    public ShipPanel(Transform content)
    {

    }

    public void Disable()
    {
    }

    public void Enable()
    {
    }
}
