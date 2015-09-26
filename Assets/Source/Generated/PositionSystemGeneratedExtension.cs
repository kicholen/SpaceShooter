namespace Entitas {
    public partial class Pool {
        public ISystem CreatePositionSystem() {
            return this.CreateSystem<PositionSystem>();
        }
    }
}