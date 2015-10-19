namespace Entitas {
    public partial class Pool {
        public ISystem CreateCircleMissileRotatedSpawnerSystem() {
            return this.CreateSystem<CircleMissileRotatedSpawnerSystem>();
        }
    }
}