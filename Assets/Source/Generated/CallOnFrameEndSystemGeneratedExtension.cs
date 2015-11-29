namespace Entitas {
    public partial class Pool {
        public ISystem CreateCallOnFrameEndSystem() {
            return this.CreateSystem<CallOnFrameEndSystem>();
        }
    }
}