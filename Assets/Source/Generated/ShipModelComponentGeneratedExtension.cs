//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ComponentsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Entitas {
    public partial class Entity {
        public ShipModelComponent shipModel { get { return (ShipModelComponent)GetComponent(ComponentIds.ShipModel); } }

        public bool hasShipModel { get { return HasComponent(ComponentIds.ShipModel); } }

        public Entity AddShipModel(string newName, float newMaxVelocity, int newHealth, bool newHasLaser, int newLaserDamage, bool newHasMissile, float newMissileVelocity, float newMissileSpawnDelay, int newMissileDamage, bool newHasSecondaryMissiles, float newSecondaryMissileVelocity, float newSecondaryMissileSpawnDelay, int newSecondaryMissileDamage, bool newHasHomeMissile, float newHomeMissileVelocity, float newHomeMissileSpawnDelay, int newHomeMissileDamage, bool newHasMagnetField, float newMagnetRadius) {
            var component = CreateComponent<ShipModelComponent>(ComponentIds.ShipModel);
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
            var component = CreateComponent<ShipModelComponent>(ComponentIds.ShipModel);
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
            return this;
        }

        public Entity RemoveShipModel() {
            return RemoveComponent(ComponentIds.ShipModel);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherShipModel;

        public static IMatcher ShipModel {
            get {
                if (_matcherShipModel == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.ShipModel);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherShipModel = matcher;
                }

                return _matcherShipModel;
            }
        }
    }
}
