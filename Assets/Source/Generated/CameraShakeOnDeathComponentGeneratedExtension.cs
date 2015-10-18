using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public CameraShakeOnDeathComponent cameraShakeOnDeath { get { return (CameraShakeOnDeathComponent)GetComponent(ComponentIds.CameraShakeOnDeath); } }

        public bool hasCameraShakeOnDeath { get { return HasComponent(ComponentIds.CameraShakeOnDeath); } }

        static readonly Stack<CameraShakeOnDeathComponent> _cameraShakeOnDeathComponentPool = new Stack<CameraShakeOnDeathComponent>();

        public static void ClearCameraShakeOnDeathComponentPool() {
            _cameraShakeOnDeathComponentPool.Clear();
        }

        public Entity AddCameraShakeOnDeath(int newType) {
            var component = _cameraShakeOnDeathComponentPool.Count > 0 ? _cameraShakeOnDeathComponentPool.Pop() : new CameraShakeOnDeathComponent();
            component.type = newType;
            return AddComponent(ComponentIds.CameraShakeOnDeath, component);
        }

        public Entity ReplaceCameraShakeOnDeath(int newType) {
            var previousComponent = hasCameraShakeOnDeath ? cameraShakeOnDeath : null;
            var component = _cameraShakeOnDeathComponentPool.Count > 0 ? _cameraShakeOnDeathComponentPool.Pop() : new CameraShakeOnDeathComponent();
            component.type = newType;
            ReplaceComponent(ComponentIds.CameraShakeOnDeath, component);
            if (previousComponent != null) {
                _cameraShakeOnDeathComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveCameraShakeOnDeath() {
            var component = cameraShakeOnDeath;
            RemoveComponent(ComponentIds.CameraShakeOnDeath);
            _cameraShakeOnDeathComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherCameraShakeOnDeath;

        public static AllOfMatcher CameraShakeOnDeath {
            get {
                if (_matcherCameraShakeOnDeath == null) {
                    _matcherCameraShakeOnDeath = new Matcher(ComponentIds.CameraShakeOnDeath);
                }

                return _matcherCameraShakeOnDeath;
            }
        }
    }
}
