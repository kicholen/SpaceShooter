public class InfoBoxShowEvent : GameEvent {
    public string text;

    public InfoBoxShowEvent(string text) {
        this.text = text;
    }
}