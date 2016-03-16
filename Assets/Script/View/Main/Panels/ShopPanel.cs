using System;
using UnityEngine;

public class ShopPanel : BaseGui, IPanel
{
    public PanelType PanelType { get { return PanelType.SHOP; } }

    public ShopPanel(Transform content)
    {

    }

    public void Disable()
    {
    }

    public void Enable()
    {
    }
}
