namespace Entitas {
    public partial class Pool {
        public ISystem CreateCreateDifficultySystem() {
            return this.CreateSystem<CreateDifficultySystem>();
        }
    }
}