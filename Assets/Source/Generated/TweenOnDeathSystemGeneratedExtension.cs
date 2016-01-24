namespace Entitas {
    public partial class Pool {
        public ISystem CreateTweenOnDeathSystem() {
            return this.CreateSystem<TweenOnDeathSystem>();
        }
    }
}