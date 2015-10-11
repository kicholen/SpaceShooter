namespace Entitas {
    public partial class Pool {
        public ISystem CreateTimeSystem() {
            return this.CreateSystem<TimeSystem>();
        }
    }
}