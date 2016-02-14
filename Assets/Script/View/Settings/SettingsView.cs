using System.Collections.Generic;
using UnityEngine.UI;

public class SettingsView : View, IView
{
    ISettingsService settingsService;
    IViewService viewService;

    public SettingsView(ISettingsService settingsService, IViewService viewService) : base("View/SettingsView") {
        this.settingsService = settingsService;
        this.viewService = viewService;
    }

    public override void Init() {
        base.Init();
        fillContent();
        addListeners();
    }

    void fillContent() {
        getChild("SoundToggle").GetComponent<Toggle>().isOn = settingsService.Sound;
        getChild("MusicToggle").GetComponent<Toggle>().isOn = settingsService.Music;
        fillDifficulty();
        fillLanguage();
    }

    void fillDifficulty() {
        Dropdown dropdown = getChild("DifficultyDropdown").GetComponentInChildren<Dropdown>();
        dropdown.options = new List<Dropdown.OptionData>() { new Dropdown.OptionData("0"), new Dropdown.OptionData("1"), new Dropdown.OptionData("2"), new Dropdown.OptionData("3") };
        dropdown.value = settingsService.Difficulty;
        getChild("DifficultyDropdown/Label").GetComponent<Text>().text = "Difficulty";
    }

    void fillLanguage() {
        Dropdown dropdown = getChild("LanguageDropdown").GetComponentInChildren<Dropdown>();
        dropdown.options = new List<Dropdown.OptionData>() { new Dropdown.OptionData("pl") };
        dropdown.value = 0;
        getChild("LanguageDropdown/Label").GetComponent<Text>().text = "Language";
    }

    void addListeners() {
        getChild("SoundToggle").GetComponent<Toggle>().onValueChanged.AddListener(value => settingsService.SetSound(value));
        getChild("MusicToggle").GetComponent<Toggle>().onValueChanged.AddListener(value => settingsService.SetMusic(value));
        getChild("DifficultyDropdown").GetComponentInChildren<Dropdown>().onValueChanged.AddListener(value => settingsService.SetDifficulty(value));
        getChild("LanguageDropdown").GetComponentInChildren<Dropdown>().onValueChanged.AddListener(value => settingsService.SetLanguage("pl"));
        getChild("BackButton").GetComponent<Button>().onClick.AddListener(saveAndGoBack);
    }

    void saveAndGoBack() {
        settingsService.Save();
        viewService.SetView(ViewTypes.LANDING);
    }
}