namespace Entitas {
    public partial class Pool {
        public ISystem CreateCreateLevelSystem() {
            return this.CreateSystem<CreateLevelSystem>();
        }
    }
}