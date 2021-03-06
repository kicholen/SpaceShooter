﻿using System;
using UnityEngine;

public class ChangeEnemyHealthAction : IEnemyAction {
    const int defaultHealth = 50;

    int health;

    public ChangeEnemyHealthAction(string health) {
        try {
            this.health = Convert.ToInt16(health);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
            this.health = defaultHealth;
        }
    }

    public void Execute(EnemySpawnModel model) {
        model.health = health;
    }
}