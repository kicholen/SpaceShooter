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
        public EnemySpawnerComponent enemySpawner { get { return (EnemySpawnerComponent)GetComponent(ComponentIds.EnemySpawner); } }

        public bool hasEnemySpawner { get { return HasComponent(ComponentIds.EnemySpawner); } }

        public Entity AddEnemySpawner(LevelModelComponent newModel) {
            var component = CreateComponent<EnemySpawnerComponent>(ComponentIds.EnemySpawner);
            component.model = newModel;
            return AddComponent(ComponentIds.EnemySpawner, component);
        }

        public Entity ReplaceEnemySpawner(LevelModelComponent newModel) {
            var component = CreateComponent<EnemySpawnerComponent>(ComponentIds.EnemySpawner);
            component.model = newModel;
            ReplaceComponent(ComponentIds.EnemySpawner, component);
            return this;
        }

        public Entity RemoveEnemySpawner() {
            return RemoveComponent(ComponentIds.EnemySpawner);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherEnemySpawner;

        public static IMatcher EnemySpawner {
            get {
                if (_matcherEnemySpawner == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.EnemySpawner);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherEnemySpawner = matcher;
                }

                return _matcherEnemySpawner;
            }
        }
    }
}
