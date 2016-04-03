public interface IPanel
{
    PanelType PanelType { get; }
    void Enable();
    void Disable();
    void Destroy();
}
