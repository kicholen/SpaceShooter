public class BottomButtonClickedEvent : GameEvent
{
    public PanelType type;

    public BottomButtonClickedEvent(PanelType type)
    {
        this.type = type;
    }
}