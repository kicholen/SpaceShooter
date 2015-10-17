namespace Entitas {
    public partial class Pool {
        public ISystem CreateDestroyEntityDelayedSystem() {
            return this.CreateSystem<DestroyEntityDelayedSystem>();
        }
    }
}