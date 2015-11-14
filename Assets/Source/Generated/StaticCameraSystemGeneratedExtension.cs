namespace Entitas {
    public partial class Pool {
        public ISystem CreateStaticCameraSystem() {
            return this.CreateSystem<StaticCameraSystem>();
        }
    }
}