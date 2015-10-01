namespace Entitas {
    public partial class Pool {
        public ISystem CreateSnapPositionSystem() {
            return this.CreateSystem<SnapPositionSystem>();
        }
    }
}