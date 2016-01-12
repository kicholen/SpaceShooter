public class ChangeStartPositionAction : ILevelAction {
    float x;
    float y;

    public ChangeStartPositionAction(float x, float y) {
        this.x = x;
        this.y = y;
    }

    public void Execute(LevelModelComponent component) {
        component.position.x = x;
        component.position.y = y;
    }
}
