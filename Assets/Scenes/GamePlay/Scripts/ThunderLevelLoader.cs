using UnityEngine;
using System.Collections;
using System.IO;

public class ThunderLevelLoader : MonoBehaviour {

	public TextAsset csv;
	private StringReader reader;

	public bool[] CurrentThunderFallPoint;
	public float NextLoadTime;

	void Start ()
	{
		this.CurrentThunderFallPoint = new bool[6];
		this.reader = new StringReader (csv.text);

		// 最初の一行を飛ばす
		this.reader.ReadLine ();

	}

	void Update(){

		if (Input.GetKeyDown (KeyCode.R)) {
			this.LoadLine ();
		}

	}

	/// <summary>
	/// csvから一行読み込む.
	/// データはLevelLoaderのプロパティに代入される.
	/// </summary>
	/// <returns><c>true</c>, リード出来た場合, <c>false</c> 終了したか、読み込みができなかった場合.</returns>
	public bool LoadLine ()
	{

		if (this.reader.Peek () > -1) {
			string line = this.reader.ReadLine ();
			string[] values = line.Split (',');

			NextLoadTime = float.Parse (values [0]);

			for (int i = 0; i < 6; i++) {
				if (int.Parse (values [i + 1]) == 1) {
					CurrentThunderFallPoint [i] = true;
				}
				else {
					CurrentThunderFallPoint [i] = false;
				}
			}

			Debug.Log (line);
			return true;
		} else {
			Debug.Log ("End CSVData");
			return false;
		}

	}
}
