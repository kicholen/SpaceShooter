using System;

public class ChangeEnemyDamageAction : IEnemyAction {
    const int defaultDmg = 10;

    int damage;

    public ChangeEnemyDamageAction(string damage) {
        try {
            this.damage = Convert.ToInt16(damage);
        }
        catch (FormatException exception) {
            this.damage = defaultDmg;
        }
    }

    public void Execute(EnemyModel model) {
        model.damage = damage;
    }
}