namespace Entitas {
    public partial class Pool {
        public ISystem CreateSoundSystem() {
            return this.CreateSystem<SoundSystem>();
        }
    }
}