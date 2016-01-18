using System;

public class ChangeEnemyHealthAction : IEnemyAction {
    const int defaultHealth = 50;

    int health;

    public ChangeEnemyHealthAction(string health) {
        try {
            this.health = Convert.ToInt16(health);
        }
        catch (FormatException exception) {
            this.health = defaultHealth;
        }
    }

    public void Execute(EnemyModel model) {
        model.health = health;
    }
}