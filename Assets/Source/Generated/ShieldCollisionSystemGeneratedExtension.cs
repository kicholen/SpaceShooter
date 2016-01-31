namespace Entitas {
    public partial class Pool {
        public ISystem CreateShieldCollisionSystem() {
            return this.CreateSystem<ShieldCollisionSystem>();
        }
    }
}