namespace Entitas {
    public partial class Pool {
        public ISystem CreateRotateSystem() {
            return this.CreateSystem<RotateSystem>();
        }
    }
}