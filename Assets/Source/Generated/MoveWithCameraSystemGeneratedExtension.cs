namespace Entitas {
    public partial class Pool {
        public ISystem CreateMoveWithCameraSystem() {
            return this.CreateSystem<MoveWithCameraSystem>();
        }
    }
}