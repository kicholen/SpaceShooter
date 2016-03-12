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
        static readonly KillInfoComponent killInfoComponent = new KillInfoComponent();

        public bool isKillInfo {
            get { return HasComponent(ComponentIds.KillInfo); }
            set {
                if (value != isKillInfo) {
                    if (value) {
                        AddComponent(ComponentIds.KillInfo, killInfoComponent);
                    } else {
                        RemoveComponent(ComponentIds.KillInfo);
                    }
                }
            }
        }

        public Entity IsKillInfo(bool value) {
            isKillInfo = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherKillInfo;

        public static IMatcher KillInfo {
            get {
                if (_matcherKillInfo == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.KillInfo);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherKillInfo = matcher;
                }

                return _matcherKillInfo;
            }
        }
    }
}
