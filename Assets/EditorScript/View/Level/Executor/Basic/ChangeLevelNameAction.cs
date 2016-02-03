public class ChangeLevelNameAction : ILevelAction {
    string name;

    public ChangeLevelNameAction(string name) {
        this.name = name;
    }

    public void Execute(LevelModelComponent component) {
        component.name = name;
    }
}
