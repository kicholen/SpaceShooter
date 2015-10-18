using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public CameraShakeComponent cameraShake { get { return (CameraShakeComponent)GetComponent(ComponentIds.CameraShake); } }

        public bool hasCameraShake { get { return HasComponent(ComponentIds.CameraShake); } }

        static readonly Stack<CameraShakeComponent> _cameraShakeComponentPool = new Stack<CameraShakeComponent>();

        public static void ClearCameraShakeComponentPool() {
            _cameraShakeComponentPool.Clear();
        }

        public Entity AddCameraShake(float newTime, float newTotalTime, float newTotalOffsetX, float newOffsetX, int newDirection, float newVelocity, bool newVelocityCalculated) {
            var component = _cameraShakeComponentPool.Count > 0 ? _cameraShakeComponentPool.Pop() : new CameraShakeComponent();
            component.time = newTime;
            component.totalTime = newTotalTime;
            component.totalOffsetX = newTotalOffsetX;
            component.offsetX = newOffsetX;
            component.direction = newDirection;
            component.velocity = newVelocity;
            component.velocityCalculated = newVelocityCalculated;
            return AddComponent(ComponentIds.CameraShake, component);
        }

        public Entity ReplaceCameraShake(float newTime, float newTotalTime, float newTotalOffsetX, float newOffsetX, int newDirection, float newVelocity, bool newVelocityCalculated) {
            var previousComponent = hasCameraShake ? cameraShake : null;
            var component = _cameraShakeComponentPool.Count > 0 ? _cameraShakeComponentPool.Pop() : new CameraShakeComponent();
            component.time = newTime;
            component.totalTime = newTotalTime;
            component.totalOffsetX = newTotalOffsetX;
            component.offsetX = newOffsetX;
            component.direction = newDirection;
            component.velocity = newVelocity;
            component.velocityCalculated = newVelocityCalculated;
            ReplaceComponent(ComponentIds.CameraShake, component);
            if (previousComponent != null) {
                _cameraShakeComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveCameraShake() {
            var component = cameraShake;
            RemoveComponent(ComponentIds.CameraShake);
            _cameraShakeComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherCameraShake;

        public static AllOfMatcher CameraShake {
            get {
                if (_matcherCameraShake == null) {
                    _matcherCameraShake = new Matcher(ComponentIds.CameraShake);
                }

                return _matcherCameraShake;
            }
        }
    }
}
