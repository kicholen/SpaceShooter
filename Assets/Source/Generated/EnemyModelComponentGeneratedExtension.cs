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
        public EnemyModelComponent enemyModel { get { return (EnemyModelComponent)GetComponent(ComponentIds.EnemyModel); } }

        public bool hasEnemyModel { get { return HasComponent(ComponentIds.EnemyModel); } }

        public Entity AddEnemyModel(int newId, int newType, string newResource, int newWeapon, int newAmount, float newTime, float newSpawnDelay, string newWeaponResource, float newVelocity, int newAngle, int newWaves, int newAngleOffset, UnityEngine.Vector2 newStartVelocity, float newFollowDelay, float newSelfDestructionDelay, float newTimeDelay, float newDelay, float newRandomPositionOffsetX, bool newFaceDirection, int newShakeCamera, float newRandomRotation, int newScore) {
            var component = CreateComponent<EnemyModelComponent>(ComponentIds.EnemyModel);
            component.id = newId;
            component.type = newType;
            component.resource = newResource;
            component.weapon = newWeapon;
            component.amount = newAmount;
            component.time = newTime;
            component.spawnDelay = newSpawnDelay;
            component.weaponResource = newWeaponResource;
            component.velocity = newVelocity;
            component.angle = newAngle;
            component.waves = newWaves;
            component.angleOffset = newAngleOffset;
            component.startVelocity = newStartVelocity;
            component.followDelay = newFollowDelay;
            component.selfDestructionDelay = newSelfDestructionDelay;
            component.timeDelay = newTimeDelay;
            component.delay = newDelay;
            component.randomPositionOffsetX = newRandomPositionOffsetX;
            component.faceDirection = newFaceDirection;
            component.shakeCamera = newShakeCamera;
            component.randomRotation = newRandomRotation;
            component.score = newScore;
            return AddComponent(ComponentIds.EnemyModel, component);
        }

        public Entity ReplaceEnemyModel(int newId, int newType, string newResource, int newWeapon, int newAmount, float newTime, float newSpawnDelay, string newWeaponResource, float newVelocity, int newAngle, int newWaves, int newAngleOffset, UnityEngine.Vector2 newStartVelocity, float newFollowDelay, float newSelfDestructionDelay, float newTimeDelay, float newDelay, float newRandomPositionOffsetX, bool newFaceDirection, int newShakeCamera, float newRandomRotation, int newScore) {
            var component = CreateComponent<EnemyModelComponent>(ComponentIds.EnemyModel);
            component.id = newId;
            component.type = newType;
            component.resource = newResource;
            component.weapon = newWeapon;
            component.amount = newAmount;
            component.time = newTime;
            component.spawnDelay = newSpawnDelay;
            component.weaponResource = newWeaponResource;
            component.velocity = newVelocity;
            component.angle = newAngle;
            component.waves = newWaves;
            component.angleOffset = newAngleOffset;
            component.startVelocity = newStartVelocity;
            component.followDelay = newFollowDelay;
            component.selfDestructionDelay = newSelfDestructionDelay;
            component.timeDelay = newTimeDelay;
            component.delay = newDelay;
            component.randomPositionOffsetX = newRandomPositionOffsetX;
            component.faceDirection = newFaceDirection;
            component.shakeCamera = newShakeCamera;
            component.randomRotation = newRandomRotation;
            component.score = newScore;
            ReplaceComponent(ComponentIds.EnemyModel, component);
            return this;
        }

        public Entity RemoveEnemyModel() {
            return RemoveComponent(ComponentIds.EnemyModel);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherEnemyModel;

        public static IMatcher EnemyModel {
            get {
                if (_matcherEnemyModel == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.EnemyModel);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherEnemyModel = matcher;
                }

                return _matcherEnemyModel;
            }
        }
    }
}
