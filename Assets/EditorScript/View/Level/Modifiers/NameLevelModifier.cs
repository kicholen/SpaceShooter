public class NameLevelModifier : ILevelModifier {

    string name;

    public NameLevelModifier(string name) {
        this.name = name;
    }

    public void Execute(LevelModelComponent component) {
        component.name = name;
    }
}
