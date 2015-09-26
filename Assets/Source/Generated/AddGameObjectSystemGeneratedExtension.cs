namespace Entitas {
    public partial class Pool {
        public ISystem CreateAddGameObjectSystem() {
            return this.CreateSystem<AddGameObjectSystem>();
        }
    }
}