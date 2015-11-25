namespace Entitas {
    public partial class Pool {
        public ISystem CreateAddUIGameObjectSystem() {
            return this.CreateSystem<AddUIGameObjectSystem>();
        }
    }
}