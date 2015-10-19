namespace Entitas {
    public partial class Pool {
        public ISystem CreateMultipleMissileSpawnerSystem() {
            return this.CreateSystem<MultipleMissileSpawnerSystem>();
        }
    }
}