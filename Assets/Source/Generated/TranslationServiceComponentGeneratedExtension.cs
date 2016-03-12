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
        public TranslationServiceComponent translationService { get { return (TranslationServiceComponent)GetComponent(ComponentIds.TranslationService); } }

        public bool hasTranslationService { get { return HasComponent(ComponentIds.TranslationService); } }

        public Entity AddTranslationService(ITranslationService newService) {
            var component = CreateComponent<TranslationServiceComponent>(ComponentIds.TranslationService);
            component.service = newService;
            return AddComponent(ComponentIds.TranslationService, component);
        }

        public Entity ReplaceTranslationService(ITranslationService newService) {
            var component = CreateComponent<TranslationServiceComponent>(ComponentIds.TranslationService);
            component.service = newService;
            ReplaceComponent(ComponentIds.TranslationService, component);
            return this;
        }

        public Entity RemoveTranslationService() {
            return RemoveComponent(ComponentIds.TranslationService);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherTranslationService;

        public static IMatcher TranslationService {
            get {
                if (_matcherTranslationService == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.TranslationService);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherTranslationService = matcher;
                }

                return _matcherTranslationService;
            }
        }
    }
}
