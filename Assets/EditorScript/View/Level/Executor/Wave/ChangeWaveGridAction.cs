using System;

public class ChangeWaveGridAction : IWaveAction {
    int grid;

    public ChangeWaveGridAction(string grid) {
        this.grid = Convert.ToInt16(grid);
    }

    public void Execute(WaveSpawnModel model) {
        model.grid = grid;
    }
}