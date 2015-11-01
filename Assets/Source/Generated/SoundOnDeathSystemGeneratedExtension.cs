namespace Entitas {
    public partial class Pool {
        public ISystem CreateSoundOnDeathSystem() {
            return this.CreateSystem<SoundOnDeathSystem>();
        }
    }
}