namespace Entitas {
    public partial class Pool {
        public ISystem CreateParticlesOnDeathSystem() {
            return this.CreateSystem<ParticlesOnDeathSystem>();
        }
    }
}