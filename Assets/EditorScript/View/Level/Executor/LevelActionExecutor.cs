using UnityEngine;

public class LevelActionExecutor {
    LevelModelComponent component;

    public LevelActionExecutor(LevelModelComponent component) {
        this.component = component;
    }

    public void Execute(ILevelAction modifier) {
        modifier.Execute(component);
    }

    public string getName() {
        return component.name;
    }

    public Vector2 getPosition() {
        return component.position;
    }

    public Vector2 getSize() {
        return component.size;
    }
}