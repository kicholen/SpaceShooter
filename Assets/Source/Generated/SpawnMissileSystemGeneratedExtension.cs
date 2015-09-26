namespace Entitas {
    public partial class Pool {
        public ISystem CreateSpawnMissileSystem() {
            return this.CreateSystem<SpawnMissileSystem>();
        }
    }
}