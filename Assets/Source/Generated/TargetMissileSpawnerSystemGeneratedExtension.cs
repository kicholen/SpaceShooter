namespace Entitas {
    public partial class Pool {
        public ISystem CreateTargetMissileSpawnerSystem() {
            return this.CreateSystem<TargetMissileSpawnerSystem>();
        }
    }
}