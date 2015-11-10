namespace Entitas {
    public partial class Pool {
        public ISystem CreateWaveSpawnerSystem() {
            return this.CreateSystem<WaveSpawnerSystem>();
        }
    }
}