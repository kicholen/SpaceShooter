namespace Entitas {
    public partial class Pool {
        public ISystem CreateActiveSystem() {
            return this.CreateSystem<ActiveSystem>();
        }
    }
}