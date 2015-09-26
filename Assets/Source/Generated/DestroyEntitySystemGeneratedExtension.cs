namespace Entitas {
    public partial class Pool {
        public ISystem CreateDestroyEntitySystem() {
            return this.CreateSystem<DestroyEntitySystem>();
        }
    }
}