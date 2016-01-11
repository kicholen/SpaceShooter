public class LevelModifierExecutor {

    LevelModelComponent component;

    public LevelModifierExecutor(LevelModelComponent component) {
        this.component = component;
    }

    public void Execute(ILevelModifier modifier) {
        modifier.Execute(component);
    }
}