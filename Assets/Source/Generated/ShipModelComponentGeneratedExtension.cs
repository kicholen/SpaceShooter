using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ShipModelComponent shipModel { get { return (ShipModelComponent)GetComponent(ComponentIds.ShipModel); } }

        public bool hasShipModel { get { return HasComponent(ComponentIds.ShipModel); } }

        static readonly Stack<ShipModelComponent> _shipModelComponentPool = new Stack<ShipModelComponent>();

        public static void ClearShipModelComponentPool() {
            _shipModelComponentPool.Clear();
        }

        public Entity AddShipModel(string newName, float newMaxVelocity, int newHealth, bool newHasLaser, int newLaserDamage, bool newHasMissile, float newMissileVelocity, float newMissileSpawnDelay, int newMissileDamage, bool newHasSecondaryMissiles, float newSecondaryMissileVelocity, float newSecondaryMissileSpawnDelay, int newSecondaryMissileDamage, bool newHasHomeMissile, float newHomeMissileVelocity, float newHomeMissileSpawnDelay, int newHomeMissileDamage, bool newHasMagnetField, float newMagnetRadius) {
            var component = _shipModelComponentPool.Count > 0 ? _shipModelComponentPool.Pop() : new ShipModelComponent();
            component.name = newName;
            component.maxVelocity = newMaxVelocity;
            component.health = newHealth;
            component.hasLaser = newHasLaser;
            component.laserDamage = newLaserDamage;
            component.hasMissile = newHasMissile;
            component.missileVelocity = newMissileVelocity;
            component.missileSpawnDelay = newMissileSpawnDelay;
            component.missileDamage = newMissileDamage;
            component.hasSecondaryMissiles = newHasSecondaryMissiles;
            component.secondaryMissileVelocity = newSecondaryMissileVelocity;
            component.secondaryMissileSpawnDelay = newSecondaryMissileSpawnDelay;
            component.secondaryMissileDamage = newSecondaryMissileDamage;
            component.hasHomeMissile = newHasHomeMissile;
            component.homeMissileVelocity = newHomeMissileVelocity;
            component.homeMissileSpawnDelay = newHomeMissileSpawnDelay;
            component.homeMissileDamage = newHomeMissileDamage;
            component.hasMagnetField = newHasMagnetField;
            component.magnetRadius = newMagnetRadius;
            return AddComponent(ComponentIds.ShipModel, component);
        }

        public Entity ReplaceShipModel(string newName, float newMaxVelocity, int newHealth, bool newHasLaser, int newLaserDamage, bool newHasMissile, float newMissileVelocity, float newMissileSpawnDelay, int newMissileDamage, bool newHasSecondaryMissiles, float newSecondaryMissileVelocity, float newSecondaryMissileSpawnDelay, int newSecondaryMissileDamage, bool newHasHomeMissile, float newHomeMissileVelocity, float newHomeMissileSpawnDelay, int newHomeMissileDamage, bool newHasMagnetField, float newMagnetRadius) {
            var previousComponent = hasShipModel ? shipModel : null;
            var component = _shipModelComponentPool.Count > 0 ? _shipModelComponentPool.Pop() : new ShipModelComponent();
            component.name = newName;
            component.maxVelocity = newMaxVelocity;
            component.health = newHealth;
            component.hasLaser = newHasLaser;
            component.laserDamage = newLaserDamage;
            component.hasMissile = newHasMissile;
            component.missileVelocity = newMissileVelocity;
            component.missileSpawnDelay = newMissileSpawnDelay;
            component.missileDamage = newMissileDamage;
            component.hasSecondaryMissiles = newHasSecondaryMissiles;
            component.secondaryMissileVelocity = newSecondaryMissileVelocity;
            component.secondaryMissileSpawnDelay = newSecondaryMissileSpawnDelay;
            component.secondaryMissileDamage = newSecondaryMissileDamage;
            component.hasHomeMissile = newHasHomeMissile;
            component.homeMissileVelocity = newHomeMissileVelocity;
            component.homeMissileSpawnDelay = newHomeMissileSpawnDelay;
            component.homeMissileDamage = newHomeMissileDamage;
            component.hasMagnetField = newHasMagnetField;
            component.magnetRadius = newMagnetRadius;
            ReplaceComponent(ComponentIds.ShipModel, component);
            if (previousComponent != null) {
                _shipModelComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveShipModel() {
            var component = shipModel;
            RemoveComponent(ComponentIds.ShipModel);
            _shipModelComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherShipModel;

        public static IMatcher ShipModel {
            get {
                if (_matcherShipModel == null) {
                    _matcherShipModel = Matcher.AllOf(ComponentIds.ShipModel);
                }

                return _matcherShipModel;
            }
        }
    }
}
