/**
	SpriteStudioPlayer
	
	Common utilities
	
	Copyright(C) Web Technology Corp. 
	
*/

//#define	_DEBUG

using UnityEngine;
using System.IO;
using System.Text;

static public class SsDebugLog
{
#if	_DEBUG
	static private StreamWriter	_writer;
	static private	string			_path;
	static private	bool _flushEverytime = true;

	static SsDebugLog()//string path = null, bool append = false)
	{
		string path = null;
		bool append = false;
		if (path == null)
			_path = "SsDebugLog.txt";
		else
			_path = path;
		
	#if true
		// write with SJIS encoding because the string inside .ssax is encoded with it.
		_writer = new StreamWriter(_path, append, SsEncoding.SJIS);
	#else
		// now converts SJIS to Unicode so write with Default encoding.
		_writer = new StreamWriter(_path, append);
	#endif
	}
#endif
	
	static public void Close()
	{
#if	_DEBUG
		_writer.Close();
		_writer = null;
#endif
	}
	
	static public void
	Printf(string format, object[] objs)
	{
#if	_DEBUG
		_writer.Write(format, objs);
		if (_flushEverytime)
			_writer.Flush();
		// use Unity console.
		//string str = String.Format(format, objs);
		//Debug.Log(str);
#endif
	}

	static public void
	Print(string str)
	{
#if	_DEBUG
		_writer.Write(str);
		if (_flushEverytime)
			_writer.Flush();
		// use Unity console.
		//string str = String.Format(format, objs);
		//Debug.Log(str);
#endif
	}

	static public void
	PrintLn(string str)
	{
#if	_DEBUG
		_writer.Write(str + "\n");
		if (_flushEverytime)
			_writer.Flush();
		// use Unity console.
		//string str = String.Format(format, objs);
		//Debug.Log(str);
#endif
	}
}

static public class SsEncoding
{
	static public Encoding SJIS { get{ return Encoding.GetEncoding(932); } }
	static public Encoding UTF8 { get{ return Encoding.GetEncoding("utf-8"); } } // code page: 65001

	static public string ConvertTo(Encoding srcEnc, Encoding dstEnc, string srcStr)
	{
		// Convert the string into a byte[].
		byte[] srcBytes = srcEnc.GetBytes(srcStr);

		// Perform the conversion from one encoding to the other.
		byte[] dstBytes = Encoding.Convert(srcEnc, dstEnc, srcBytes);

		// Convert the new byte[] into a char[] and then into a string.
		// This is a slightly different approach to converting to illustrate
		// the use of GetCharCount/GetChars.
		char[] dstChars = new char[dstEnc.GetCharCount(dstBytes, 0, dstBytes.Length)];
		dstEnc.GetChars(dstBytes, 0, dstBytes.Length, dstChars, 0);
		return new string(dstChars);
	}
}

public class SsVersion
{
	// convert string "MM.mm.rr" to hexadecimal 0x00MMmmrr. return -1 if failed.
	static public int
	ToInt(string versionStr)
	{
		string[] values = versionStr.Split('.');
		if (values.Length != 3)//values.Length < 1 || values.Length > 3)
		{
			Debug.LogError("The number of version digits are invalid: " + values.Length);
			return -1;
		}
		int major = _HexToInt(values[0]);
		int minor = _HexToInt(values[1]);
		int revision = _HexToInt(values[2]);
		//Debug.Log("Read Version: " + major + "." + minor + "." + revision);
		int ret = (major << 16) | (minor << 8) | revision;
		return ret;
	}
	
	// convert hexadecimal 0x00MMmmrr to string "MM.mm.rr". return null if failed.
	static public string
	ToString(int n)
	{
		int major		= (n >> 16) & 0xff;
		if (major == 0) return null;
		int minor		= (n >> 8) & 0xff;
		int revision	= (n & 0xff);
		string ret = System.String.Format("{0:X}.{1:X2}.{2:X2}", major, minor, revision);
		return ret;
	}
	
	static private int
	_HexToInt(string src)
	{
		return System.Convert.ToInt32(src, 16);
	}
}

public class SsTimer
{
	static	float _startTime;
	
	static public void
	StartTimer()
	{
		_startTime = Time.realtimeSinceStartup;
	}

	static public void
	EndTimer(string label)
	{
		float elapsedTime = (Time.realtimeSinceStartup - _startTime) * 1000;
		Debug.Log(label + ": " + elapsedTime.ToString("F4") + "msec");
	}

	static public float
	ElapsedTime()
	{
		float elapsedTime = (Time.realtimeSinceStartup - _startTime) * 1000;
		return elapsedTime;
	}
}
