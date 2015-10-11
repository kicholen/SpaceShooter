namespace Entitas {
    public partial class Pool {
        public ISystem CreateHomeMissileSpawnerSystem() {
            return this.CreateSystem<HomeMissileSpawnerSystem>();
        }
    }
}