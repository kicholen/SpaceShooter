using System;

public class ChangeEnemyModelWeaponAction : IEnemyModelCmpAction {
    int weapon;

    public ChangeEnemyModelWeaponAction(string weapon) {
        this.weapon = Convert.ToInt16(weapon);
    }
    public void Execute(EnemyModelComponent model) {
        model.weapon = weapon;
    }
}