namespace Entitas {
    public partial class Pool {
        public ISystem CreateIndicatorSystem() {
            return this.CreateSystem<IndicatorSystem>();
        }
    }
}