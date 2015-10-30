namespace Entitas {
    public partial class Pool {
        public ISystem CreateDifficultyControllerSystem() {
            return this.CreateSystem<DifficultyControllerSystem>();
        }
    }
}