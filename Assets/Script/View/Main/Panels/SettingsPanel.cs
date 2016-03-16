using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : BaseGui, IPanel
{
    IServices services;

    public PanelType PanelType { get { return PanelType.SETTINGS; } }

    public SettingsPanel(Transform content, IServices services)
    {
        this.services = services;
        go = content.gameObject;
        init();
    }

    public void Disable()
    {
        services.SettingsService.Save();
    }

    public void Enable()
    {
    }

    void init()
    {
        fillContent();
        addListeners();
    }

    void fillContent()
    {
        getChild("SoundToggle").GetComponent<Toggle>().isOn = services.SettingsService.Sound;
        getChild("MusicToggle").GetComponent<Toggle>().isOn = services.SettingsService.Music;
        fillDifficulty();
        fillLanguage();
    }

    void fillDifficulty()
    {
        Dropdown dropdown = getChild("DifficultyDropdown").GetComponentInChildren<Dropdown>();
        dropdown.options = new List<Dropdown.OptionData>() { new Dropdown.OptionData("0"), new Dropdown.OptionData("1"), new Dropdown.OptionData("2"), new Dropdown.OptionData("3") };
        dropdown.value = services.SettingsService.Difficulty;
        getChild("DifficultyDropdown/Label").GetComponent<Text>().text = "Difficulty";
    }

    void fillLanguage()
    {
        Dropdown dropdown = getChild("LanguageDropdown").GetComponentInChildren<Dropdown>();
        dropdown.options = new List<Dropdown.OptionData>() { new Dropdown.OptionData("pl") };
        dropdown.value = 0;
        getChild("LanguageDropdown/Label").GetComponent<Text>().text = "Language";
    }

    void addListeners()
    {
        getChild("SoundToggle").GetComponent<Toggle>().onValueChanged.AddListener(value => services.SettingsService.SetSound(value));
        getChild("MusicToggle").GetComponent<Toggle>().onValueChanged.AddListener(value => services.SettingsService.SetMusic(value));
        getChild("DifficultyDropdown").GetComponentInChildren<Dropdown>().onValueChanged.AddListener(value => services.SettingsService.SetDifficulty(value));
        getChild("LanguageDropdown").GetComponentInChildren<Dropdown>().onValueChanged.AddListener(value => services.SettingsService.SetLanguage("pl"));
    }
}
