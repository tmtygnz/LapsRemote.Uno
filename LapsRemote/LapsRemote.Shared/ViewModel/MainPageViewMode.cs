using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace LapsRemote.ViewModel
{
	class MainPageViewMode : BindableBase
	{
		private string _temperatureString;
		public string GetTemperatureString
		{
			get { return _temperatureString; }
			set { SetProperty(ref _temperatureString, value); }
		}

		private string _oxySat;
		public string GetOxyStat
		{
			get {  return _oxySat; }
			set { SetProperty(ref _oxySat, value); }
		}

		private string _bpm;
		public string GetBPM
		{
			get { return _bpm; }
			set { SetProperty(ref _oxySat, value); }
		}

		private string _respRate;
		public string GetRespRate
		{
			get { return _respRate; }
			set { SetProperty(ref _respRate, value);  }
		}
	}
}
