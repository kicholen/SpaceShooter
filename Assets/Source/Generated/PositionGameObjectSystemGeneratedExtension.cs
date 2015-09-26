namespace Entitas {
    public partial class Pool {
        public ISystem CreatePositionGameObjectSystem() {
            return this.CreateSystem<PositionGameObjectSystem>();
        }
    }
}