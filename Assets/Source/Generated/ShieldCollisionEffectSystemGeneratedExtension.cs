namespace Entitas {
    public partial class Pool {
        public ISystem CreateShieldCollisionEffectSystem() {
            return this.CreateSystem<ShieldCollisionEffectSystem>();
        }
    }
}