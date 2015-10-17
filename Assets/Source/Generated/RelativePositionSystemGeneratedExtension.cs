namespace Entitas {
    public partial class Pool {
        public ISystem CreateRelativePositionSystem() {
            return this.CreateSystem<RelativePositionSystem>();
        }
    }
}