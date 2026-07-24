using SQLite4Unity3d;
using UnityEngine;
#if !UNITY_EDITOR
using System.IO;
#endif

public class DataService  {

	private SQLiteConnection _connection;

	public DataService(string DatabaseName){
		#if UNITY_EDITOR
			var dbPath = $"Assets/StreamingAssets/{DatabaseName}";
		#else
			var filepath = $"{Application.persistentDataPath}/{DatabaseName}";

			if (!File.Exists(filepath))
			{
				Debug.Log("Database not in Persistent path");

				#if UNITY_ANDROID
					var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);
					while (!loadDb.isDone) { }
					File.WriteAllBytes(filepath, loadDb.bytes);

				#elif UNITY_IOS
					var loadDb = Application.dataPath + "/Raw/" + DatabaseName;
					File.Copy(loadDb, filepath);

				#elif UNITY_WP8
					var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;
					File.Copy(loadDb, filepath);

				#elif UNITY_WINRT
					var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;
					File.Copy(loadDb, filepath);

				#elif UNITY_STANDALONE_OSX
					var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;
					File.Copy(loadDb, filepath);

				#else
					var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;
					File.Copy(loadDb, filepath);

				#endif
					Debug.Log("Database written");
			}

			var dbPath = filepath;

		#endif
			_connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
			Debug.Log("Final PATH: " + dbPath);
	}

	public SQLiteConnection GetConnection()
	{
		return _connection;
	}
}
