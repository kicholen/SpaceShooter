namespace Entitas {
    public partial class Pool {
        public ISystem CreateTweenSystem() {
            return this.CreateSystem<TweenSystem>();
        }
    }
}