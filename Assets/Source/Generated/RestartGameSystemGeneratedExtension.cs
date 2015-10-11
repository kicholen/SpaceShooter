namespace Entitas {
    public partial class Pool {
        public ISystem CreateRestartGameSystem() {
            return this.CreateSystem<RestartGameSystem>();
        }
    }
}