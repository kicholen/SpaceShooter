namespace Entitas {
    public partial class Pool {
        public ISystem CreateTrailSystem() {
            return this.CreateSystem<TrailSystem>();
        }
    }
}