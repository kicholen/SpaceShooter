using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public MotherShipComponent motherShip { get { return (MotherShipComponent)GetComponent(ComponentIds.MotherShip); } }

        public bool hasMotherShip { get { return HasComponent(ComponentIds.MotherShip); } }

        static readonly Stack<MotherShipComponent> _motherShipComponentPool = new Stack<MotherShipComponent>();

        public static void ClearMotherShipComponentPool() {
            _motherShipComponentPool.Clear();
        }

        public Entity AddMotherShip(float newTime, float newDuration, float newTimeRandomFactor, int newSpawnedDronesCount, int newDroneSpawnLimit, int newDroneType, int newDroneHealth, int newDroneDamage, float newDroneSpeed) {
            var component = _motherShipComponentPool.Count > 0 ? _motherShipComponentPool.Pop() : new MotherShipComponent();
            component.time = newTime;
            component.duration = newDuration;
            component.timeRandomFactor = newTimeRandomFactor;
            component.spawnedDronesCount = newSpawnedDronesCount;
            component.droneSpawnLimit = newDroneSpawnLimit;
            component.droneType = newDroneType;
            component.droneHealth = newDroneHealth;
            component.droneDamage = newDroneDamage;
            component.droneSpeed = newDroneSpeed;
            return AddComponent(ComponentIds.MotherShip, component);
        }

        public Entity ReplaceMotherShip(float newTime, float newDuration, float newTimeRandomFactor, int newSpawnedDronesCount, int newDroneSpawnLimit, int newDroneType, int newDroneHealth, int newDroneDamage, float newDroneSpeed) {
            var previousComponent = hasMotherShip ? motherShip : null;
            var component = _motherShipComponentPool.Count > 0 ? _motherShipComponentPool.Pop() : new MotherShipComponent();
            component.time = newTime;
            component.duration = newDuration;
            component.timeRandomFactor = newTimeRandomFactor;
            component.spawnedDronesCount = newSpawnedDronesCount;
            component.droneSpawnLimit = newDroneSpawnLimit;
            component.droneType = newDroneType;
            component.droneHealth = newDroneHealth;
            component.droneDamage = newDroneDamage;
            component.droneSpeed = newDroneSpeed;
            ReplaceComponent(ComponentIds.MotherShip, component);
            if (previousComponent != null) {
                _motherShipComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveMotherShip() {
            var component = motherShip;
            RemoveComponent(ComponentIds.MotherShip);
            _motherShipComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherMotherShip;

        public static IMatcher MotherShip {
            get {
                if (_matcherMotherShip == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.MotherShip);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherMotherShip = matcher;
                }

                return _matcherMotherShip;
            }
        }
    }
}
