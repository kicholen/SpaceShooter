using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public RegularCameraComponent regularCamera { get { return (RegularCameraComponent)GetComponent(ComponentIds.RegularCamera); } }

        public bool hasRegularCamera { get { return HasComponent(ComponentIds.RegularCamera); } }

        static readonly Stack<RegularCameraComponent> _regularCameraComponentPool = new Stack<RegularCameraComponent>();

        public static void ClearRegularCameraComponentPool() {
            _regularCameraComponentPool.Clear();
        }

        public Entity AddRegularCamera(UnityEngine.Camera newCamera, UnityEngine.Vector3 newOffset) {
            var component = _regularCameraComponentPool.Count > 0 ? _regularCameraComponentPool.Pop() : new RegularCameraComponent();
            component.camera = newCamera;
            component.offset = newOffset;
            return AddComponent(ComponentIds.RegularCamera, component);
        }

        public Entity ReplaceRegularCamera(UnityEngine.Camera newCamera, UnityEngine.Vector3 newOffset) {
            var previousComponent = hasRegularCamera ? regularCamera : null;
            var component = _regularCameraComponentPool.Count > 0 ? _regularCameraComponentPool.Pop() : new RegularCameraComponent();
            component.camera = newCamera;
            component.offset = newOffset;
            ReplaceComponent(ComponentIds.RegularCamera, component);
            if (previousComponent != null) {
                _regularCameraComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveRegularCamera() {
            var component = regularCamera;
            RemoveComponent(ComponentIds.RegularCamera);
            _regularCameraComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherRegularCamera;

        public static AllOfMatcher RegularCamera {
            get {
                if (_matcherRegularCamera == null) {
                    _matcherRegularCamera = new Matcher(ComponentIds.RegularCamera);
                }

                return _matcherRegularCamera;
            }
        }
    }
}
