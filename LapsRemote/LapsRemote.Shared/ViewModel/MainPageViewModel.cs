using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;
using LapsRemote.Logging;

namespace LapsRemote.ViewModel
{
	class MainPageViewModel : BindableBase
	{
		public MainPageViewModel()
		{
			GetTemperatureString = "100";
			GetOxySatString = "10";
			GetBPMString = "20";
			GetRespRateString = "19";

			Logger.LogDebug("App Started", LogFrom.MainPageViewModelcs);
		}

		private string _temperatureString;
		public string GetTemperatureString
		{
			get { return _temperatureString; }
			set { SetProperty(ref _temperatureString, value); }
		}

		private string _oxySat;
		public string GetOxySatString
		{
			get { return _oxySat; }
			set { SetProperty(ref _oxySat, value); }
		}

		private string _bpm;
		public string GetBPMString
		{
			get { return _bpm; }
			set { SetProperty(ref _bpm, value); }
		}

		private string _respRate;
		public string GetRespRateString
		{
			get { return _respRate; }
			set { SetProperty(ref _respRate, value); }
		}
	}
}
