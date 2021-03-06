﻿using Entitas;
using System.Collections.Generic;

public interface IServices : Updateable {
	void Init();
	IController Controller { get; }
	Pool Pool { get;}
	List<Updateable> Updateables { get; }
	ILoadService LoadService { get; }
	EventService EventService { get; }
	IGameService GameService { get; }
	IViewService ViewService { get; }
	IUIFactoryService UIFactoryService { get; }
    IWwwService WwwService { get; }
    ISettingsService SettingsService { get; }
    ITranslationService TranslationService { get; }
    IAnalyticsService AnalyticsService { get; }
    IShipService ShipService { get; }
    ICurrencyService CurrencyService { get; }
    IGamerService GamerService { get; }
    IShopService ShopService { get; }
    IAPService IAPService { get; }
    IAdService AdService { get; }
    ITimeService TimeService { get; }
}
