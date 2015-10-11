namespace Entitas {
    public partial class Pool {
        public ISystem CreateDeadPlayerSystem() {
            return this.CreateSystem<DeadPlayerSystem>();
        }
    }
}