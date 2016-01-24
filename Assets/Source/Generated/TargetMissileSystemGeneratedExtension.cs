namespace Entitas {
    public partial class Pool {
        public ISystem CreateTargetMissileSystem() {
            return this.CreateSystem<TargetMissileSystem>();
        }
    }
}