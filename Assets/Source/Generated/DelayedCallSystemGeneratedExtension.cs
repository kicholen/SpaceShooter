namespace Entitas {
    public partial class Pool {
        public ISystem CreateDelayedCallSystem() {
            return this.CreateSystem<DelayedCallSystem>();
        }
    }
}