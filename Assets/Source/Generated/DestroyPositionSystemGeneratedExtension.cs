namespace Entitas {
    public partial class Pool {
        public ISystem CreateDestroyPositionSystem() {
            return this.CreateSystem<DestroyPositionSystem>();
        }
    }
}