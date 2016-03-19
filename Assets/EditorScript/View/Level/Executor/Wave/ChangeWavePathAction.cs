using System;

public class ChangeWavePathAction : IWaveAction {
    int path;

    public ChangeWavePathAction(string path) {
        this.path = Convert.ToInt16(path);
    }

    public void Execute(WaveSpawnModel model) {
        model.path = path;
    }
}