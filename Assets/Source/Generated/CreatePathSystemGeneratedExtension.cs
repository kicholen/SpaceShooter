namespace Entitas {
    public partial class Pool {
        public ISystem CreateCreatePathSystem() {
            return this.CreateSystem<CreatePathSystem>();
        }
    }
}