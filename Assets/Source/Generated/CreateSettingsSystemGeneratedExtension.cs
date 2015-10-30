namespace Entitas {
    public partial class Pool {
        public ISystem CreateCreateSettingsSystem() {
            return this.CreateSystem<CreateSettingsSystem>();
        }
    }
}