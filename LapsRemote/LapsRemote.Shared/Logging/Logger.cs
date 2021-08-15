using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;
using System.Collections;
using System.Threading;

namespace LapsRemote.Logging
{
	static class Logger
	{
		public static async void Init()
		{
			_queue = new Queue<Message>();
#if NETFX_CORE
			_localFolder = ApplicationData.Current.RoamingFolder;
			_logFilePath = await _localFolder.CreateFileAsync("application.log", CreationCollisionOption.OpenIfExists);

#elif HAS_UNO_SKIA_WPF || HAS_UNO_WASM
			_applicationDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LapsRemote");
			_logFilePath = Path.Combine(_applicationDataPath, "application.log");

			if (!Directory.Exists(_applicationDataPath)) { Directory.CreateDirectory(_applicationDataPath); }

			if (!File.Exists(_logFilePath)) { File.Create(_logFilePath); }

			_streamWriter = File.AppendText(_logFilePath);
#endif
			_isLogging = true;
			new Thread(() => LoggingLoop()).Start();
		}

		private static bool _isLogging;
		private static Queue<Message> _queue;
#if NETFX_CORE
		private static StorageFolder _localFolder;
		private static StorageFile _logFilePath;

#elif HAS_UNO_SKIA_GTK || HAS_UNO_SKIA_GTK || HAS_UNO_SKIA_WPF || HAS_UNO_WASM
		private static string _applicationDataPath;
		private static string _logFilePath;
		private static StreamWriter _streamWriter;
#endif

		public static async void LoggingLoop()
		{
			while (_isLogging)
			{
				if (_queue.Count != 0) { await LogToDisk(_queue.Peek()); }
			}
		}

		public static void LogFatal(string LogMessage, LogFrom LoggingFrom)
		{
			Message Msg = new Message
			{
				LogMessage = LogMessage,
				LoggingFrom = LoggingFrom,
				LogLevel = Level.Fatal,
				LogTime = DateTime.Now
			};
			_queue.Enqueue(Msg);
		}

		public static void LogError(string LogMessage, LogFrom LoggingFrom)
		{
			Message Msg = new Message
			{
				LogMessage = LogMessage,
				LoggingFrom = LoggingFrom,
				LogLevel = Level.Error,
				LogTime = DateTime.Now
			};
			_queue.Enqueue(Msg);
		}

		public static void LogWarn(string LogMessage, LogFrom LoggingFrom)
		{
			Message Msg = new Message
			{
				LogMessage = LogMessage,
				LoggingFrom = LoggingFrom,
				LogLevel = Level.Warning,
				LogTime = DateTime.Now
			};
			_queue.Enqueue(Msg);
		}

		public static void LogInfo(string LogMessage, LogFrom LoggingFrom)
		{
			Message Msg = new Message
			{
				LogMessage = LogMessage,
				LoggingFrom = LoggingFrom,
				LogLevel = Level.Info,
				LogTime = DateTime.Now
			};
			_queue.Enqueue(Msg);
		}

		public static void LogDebug(string LogMessage, LogFrom LoggingFrom)
		{
			Message Msg = new Message
			{
				LogMessage = LogMessage,
				LoggingFrom = LoggingFrom,
				LogLevel = Level.Debug,
				LogTime = DateTime.Now
			};
			_queue.Enqueue(Msg);
		}

		public static async Task LogToDisk(Message Msg)
		{
#if NETFX_CORE
			string StrToSave = $"{Msg.LogTime.ToString()} [{Msg.LoggingFrom} / {Msg.LogLevel}] {Msg.LogMessage} \n";
			await FileIO.AppendTextAsync(_logFilePath, StrToSave);
#elif HAS_UNO_SKIA_WPF || HAS_UNO_WASM
			string StrToSave = $"{Msg.LogTime.ToString()} [{Msg.LoggingFrom}] {Msg.LoggingFrom}";
			await _streamWriter.WriteLineAsync(StrToSave);
			_streamWriter.Flush();
#endif
			_queue.Dequeue();
		}

		public static void StopLogging()
		{
			_isLogging = false;
			if (_queue.Count != 0) { _queue.Clear(); }
#if HAS_UNO_SKIA_GTK || HAS_UNO_SKIA_GTK || HAS_UNO_SKIA_WPF || HAS_UNO_WASM
			_streamWriter.Flush();
			_streamWriter.Close();
#endif
		}

	}
}