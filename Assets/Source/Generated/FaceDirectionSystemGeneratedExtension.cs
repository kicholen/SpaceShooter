namespace Entitas {
    public partial class Pool {
        public ISystem CreateFaceDirectionSystem() {
            return this.CreateSystem<FaceDirectionSystem>();
        }
    }
}