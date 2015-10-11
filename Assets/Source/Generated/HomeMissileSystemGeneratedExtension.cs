namespace Entitas {
    public partial class Pool {
        public ISystem CreateHomeMissileSystem() {
            return this.CreateSystem<HomeMissileSystem>();
        }
    }
}