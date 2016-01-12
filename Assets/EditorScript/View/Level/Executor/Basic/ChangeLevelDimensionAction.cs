public class ChangeLevelDimensionAction : ILevelAction {
    float x;
    float y;

    public ChangeLevelDimensionAction(float x, float y) {
        this.x = x;
        this.y = y;
    }

    public void Execute(LevelModelComponent component) {
        component.size.x = x;
        component.size.y = y;
    }
}
