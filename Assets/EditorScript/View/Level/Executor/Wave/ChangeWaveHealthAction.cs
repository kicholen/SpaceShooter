﻿using System;

public class ChangeWaveHealthAction : IWaveAction {
    const int defaultHealth = 50;

    int health;

    public ChangeWaveHealthAction(string health) {
        try {
            this.health = Convert.ToInt16(health);
        }
        catch (FormatException exception) {
            this.health = defaultHealth;
        }
    }

    public void Execute(WaveModel model) {
        model.health = health;
    }
}