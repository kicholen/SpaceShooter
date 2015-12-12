namespace Entitas {
    public partial class Pool {
        public ISystem CreateAddCollisionToGameObjectSystem() {
            return this.CreateSystem<AddCollisionToGameObjectSystem>();
        }
    }
}