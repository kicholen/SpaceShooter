using System;

public class ChangeWaveCountAction : IWaveAction {
    int count;

    public ChangeWaveCountAction(string count) {
        this.count = Convert.ToInt16(count);
    }

    public void Execute(WaveModel model) {
        model.count = count;
    }
}