namespace Entitas {
    public partial class Pool {
        public ISystem CreateBackgroundSystem() {
            return this.CreateSystem<BackgroundSystem>();
        }
    }
}