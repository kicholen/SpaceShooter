namespace Entitas {
    public partial class Pool {
        public ISystem CreateAlphaSystem() {
            return this.CreateSystem<AlphaSystem>();
        }
    }
}