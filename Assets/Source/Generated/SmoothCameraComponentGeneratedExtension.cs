using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public SmoothCameraComponent smoothCamera { get { return (SmoothCameraComponent)GetComponent(ComponentIds.SmoothCamera); } }

        public bool hasSmoothCamera { get { return HasComponent(ComponentIds.SmoothCamera); } }

        static readonly Stack<SmoothCameraComponent> _smoothCameraComponentPool = new Stack<SmoothCameraComponent>();

        public static void ClearSmoothCameraComponentPool() {
            _smoothCameraComponentPool.Clear();
        }

        public Entity AddSmoothCamera(UnityEngine.Vector3 newOffset) {
            var component = _smoothCameraComponentPool.Count > 0 ? _smoothCameraComponentPool.Pop() : new SmoothCameraComponent();
            component.offset = newOffset;
            return AddComponent(ComponentIds.SmoothCamera, component);
        }

        public Entity ReplaceSmoothCamera(UnityEngine.Vector3 newOffset) {
            var previousComponent = hasSmoothCamera ? smoothCamera : null;
            var component = _smoothCameraComponentPool.Count > 0 ? _smoothCameraComponentPool.Pop() : new SmoothCameraComponent();
            component.offset = newOffset;
            ReplaceComponent(ComponentIds.SmoothCamera, component);
            if (previousComponent != null) {
                _smoothCameraComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveSmoothCamera() {
            var component = smoothCamera;
            RemoveComponent(ComponentIds.SmoothCamera);
            _smoothCameraComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherSmoothCamera;

        public static IMatcher SmoothCamera {
            get {
                if (_matcherSmoothCamera == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.SmoothCamera);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherSmoothCamera = matcher;
                }

                return _matcherSmoothCamera;
            }
        }
    }
}
