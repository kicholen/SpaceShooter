using UnityEngine;

public class PlayPanel : BaseGui, IPanel
{
    public PanelType PanelType { get { return PanelType.PLAY; } }

    public PlayPanel(Transform content)
    {

    }

    public void Disable()
    {
    }

    public void Enable()
    {
    }
}
