namespace Entitas {
    public partial class Pool {
        public ISystem CreateCreateStaticElementsSystem() {
            return this.CreateSystem<CreateStaticElementsSystem>();
        }
    }
}