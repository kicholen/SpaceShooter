namespace Entitas {
    public partial class Pool {
        public ISystem CreateDispersionMissileSpawnerSystem() {
            return this.CreateSystem<DispersionMissileSpawnerSystem>();
        }
    }
}