namespace Entitas {
    public partial class Pool {
        public ISystem CreatePathSystem() {
            return this.CreateSystem<PathSystem>();
        }
    }
}