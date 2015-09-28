namespace Entitas {
    public partial class Pool {
        public ISystem CreateCreateCameraSystem() {
            return this.CreateSystem<CreateCameraSystem>();
        }
    }
}