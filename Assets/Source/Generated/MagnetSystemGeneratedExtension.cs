namespace Entitas {
    public partial class Pool {
        public ISystem CreateMagnetSystem() {
            return this.CreateSystem<MagnetSystem>();
        }
    }
}