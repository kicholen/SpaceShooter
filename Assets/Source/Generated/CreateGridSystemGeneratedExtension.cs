namespace Entitas {
    public partial class Pool {
        public ISystem CreateCreateGridSystem() {
            return this.CreateSystem<CreateGridSystem>();
        }
    }
}