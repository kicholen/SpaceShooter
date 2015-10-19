namespace Entitas {
    public partial class Pool {
        public ISystem CreateFirstBossSystem() {
            return this.CreateSystem<FirstBossSystem>();
        }
    }
}