public class LoadProgressEvent : GameEvent {
	
	public float progress;
	
	public LoadProgressEvent(float progress) {
		this.progress = progress;
	}
	
}