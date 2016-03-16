using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayPanel : BaseGui, IPanel
{
    public PanelType PanelType { get { return PanelType.PLAY; } }

    List<int> levels = new List<int>();
    IServices services;
    Transform levelsContent;

    public PlayPanel(Transform content, IServices services)
    {
        go = content.gameObject;
        levelsContent = getChild("Viewport/Content");
        this.services = services;
        createLevels();
    }

    public void Disable()
    {
    }

    public void Enable()
    {
    }

    void createLevels()
    {
        #if UNITY_EDITOR
        System.IO.FileInfo[] infos = new System.IO.DirectoryInfo(Application.dataPath + "/Resources/").GetFiles();
        foreach (System.IO.FileInfo info in infos)
            if (info.Name.StartsWith((typeof(LevelModelComponent)).Name) && info.Name.EndsWith(".json"))
                levels.Add(System.Convert.ToInt16(info.Name.Split('_')[1].Split('.')[0]));
        #else
        levels.Add(290);
		levels.Add(323);
		levels.Add(326);
		levels.Add(328);
		levels.Add(562);
		levels.Add(1044);
		levels.Add(1837);
		levels.Add(1951);
		levels.Add(2081);
		levels.Add(2536);
        #endif
        for (int i = 0; i < levels.Count; i++)
        {
            GameObject button = services.UIFactoryService.CreatePrefab("Element/SimpleButton");
            button.name = levels[i].ToString();
            button.transform.SetParent(levelsContent, false);
            services.UIFactoryService.AddText(button.transform, "Text", button.name);
            services.UIFactoryService.AddButton(button, onLevelClicked);
        }
    }

    void onLevelClicked()
    {
        int level = System.Convert.ToInt16(EventSystem.current.currentSelectedGameObject.name);
        services.LoadService.PrepareAndExecute(new StartGamePhase(services.ViewService, services.GameService, level));
    }
}
