using System;

public class ChangeWaveTypeAction : IWaveAction {
    int type;

    public ChangeWaveTypeAction(string type) {
        this.type = Convert.ToInt16(type);
    }

    public void Execute(WaveModel model) {
        model.type = type;
    }
}