namespace Entitas {
    public partial class Pool {
        public ISystem CreateGridSystem() {
            return this.CreateSystem<GridSystem>();
        }
    }
}