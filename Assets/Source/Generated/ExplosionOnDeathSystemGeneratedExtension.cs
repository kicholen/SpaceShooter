namespace Entitas {
    public partial class Pool {
        public ISystem CreateExplosionOnDeathSystem() {
            return this.CreateSystem<ExplosionOnDeathSystem>();
        }
    }
}