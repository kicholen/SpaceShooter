public class PanelSwitchedEvent : GameEvent
{
    public PanelType type;

    public PanelSwitchedEvent(PanelType type)
    {
        this.type = type;
    }
}