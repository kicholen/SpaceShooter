using Newtonsoft.Json;
using System.Collections.Generic;

public class LanguageModel
{
    public long id;
    public string name;

    [JsonIgnore]
    public Dictionary<string, string> data = new Dictionary<string, string>();
    public List<Translation> translations;

    public LanguageModel(long id, string name, List<Translation> translations) {
        this.id = id;
        this.name = name;
        this.translations = translations;
    }

    public void Init() {
        foreach (Translation translation in translations)
            data[translation.x] = translation.y;
    }
}

public class Translation
{
    public string x;
    public string y;

    public Translation(string x, string y) {
        this.x = x;
        this.y = y;
    }
}
