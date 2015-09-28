namespace Entitas {
    public partial class Pool {
        public ISystem CreateRegularCameraSystem() {
            return this.CreateSystem<RegularCameraSystem>();
        }
    }
}