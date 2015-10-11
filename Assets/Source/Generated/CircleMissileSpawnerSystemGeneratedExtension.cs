namespace Entitas {
    public partial class Pool {
        public ISystem CreateCircleMissileSpawnerSystem() {
            return this.CreateSystem<CircleMissileSpawnerSystem>();
        }
    }
}