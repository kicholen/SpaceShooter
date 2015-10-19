namespace Entitas {
    public partial class Pool {
        public ISystem CreateSlowGameSystem() {
            return this.CreateSystem<SlowGameSystem>();
        }
    }
}