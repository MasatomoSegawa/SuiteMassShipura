using UnityEngine;
using System.Collections;
using System.IO;

public class GameLevelInfo{
	public WeatherState Weather;
	public float NextLoadTime;
	public float OkashiFallDuration;
	public float ItemSpeed;
	public float MusiFallDuration;
	public float MaxRandamizeSpeed;
	public float MusiSpeed;
}

public class LevelLoader : MonoBehaviour
{
	public int initLine = 0;
	public TextAsset csv;
	private StringReader reader;

	public GameLevelInfo gameLevelInfo;

	void Start ()
	{
		this.reader = new StringReader (csv.text);

		// 最初の行読み込み位置をあわせる
		//for (int i = 0; i < initLine; i++) {
			this.reader.ReadLine ();
		//}

		this.gameLevelInfo = new GameLevelInfo ();
	}

	void Update(){

		if (Input.GetKeyDown (KeyCode.Escape)) {
			FadeManager.Instance.LoadLevel ("newTitle", 1.0f);
		}

	}
		
	/// <summary>
	/// csvから一行読み込む.
	/// データはLevelLoaderのプロパティに代入される.
	/// </summary>
	/// <returns><c>true</c>, リード出来た場合, <c>false</c> 終了したか、読み込みができなかった場合.</returns>
	public GameLevelInfo ReadLine ()
	{

		if (this.reader.Peek () > -1) {
			string line = this.reader.ReadLine ();
			string[] values = line.Split (',');

			gameLevelInfo.NextLoadTime = float.Parse (values [0]);

			WeatherState temp = (WeatherState)System.Enum.Parse (System.Type.GetType ("WeatherState"), values [1]);
			gameLevelInfo.Weather = temp;

			gameLevelInfo.OkashiFallDuration = float.Parse (values [2]);
			gameLevelInfo.ItemSpeed = float.Parse (values [3]);
			gameLevelInfo.MusiFallDuration = float.Parse (values [4]);
			gameLevelInfo.MaxRandamizeSpeed = float.Parse (values [5]);
			gameLevelInfo.MusiSpeed = float.Parse (values [6]);

			Debug.Log ("MusiFallDuration:" + gameLevelInfo.MusiFallDuration);

			Debug.Log (line);
			return gameLevelInfo;
		} else {
			Debug.Log ("End CSVData");
			return null;
		}

	}
}
