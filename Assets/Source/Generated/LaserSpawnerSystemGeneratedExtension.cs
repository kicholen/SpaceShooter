namespace Entitas {
    public partial class Pool {
        public ISystem CreateLaserSpawnerSystem() {
            return this.CreateSystem<LaserSpawnerSystem>();
        }
    }
}