using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeController
{
	/// <summary>
	/// アルファを弄る対象の現在の色
	/// </summary>
	private Image _fadeImage;

	/// <summary>
	/// アルファを弄る対象の元の色
	/// </summary>
	private Color _defaultColor;

	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="fadeImage"></param>
	public FadeController(Image fadeImage)
	{
		_fadeImage = fadeImage;
		_defaultColor = _fadeImage.color;
	}

	/// <summary>
	/// alpha set by default
	/// </summary>
	/// <param name="time"></param>
	/// <returns></returns>
	public IEnumerator FadeIn(float time)
	{
		return FadeIn(time, _defaultColor.a);
	}

	/// <summary>
	/// alpha set by default
	/// </summary>
	/// <param name="time"></param>
	/// <returns></returns>
	public IEnumerator FadeOut(float time)
	{
		return FadeOut(time, _defaultColor.a);
	}

	/// <summary>
	/// フェードインを実行するコルーチンメソッド
	/// </summary>
	/// <param name="time"></param>
	/// <param name="alphaLimit"></param>
	/// <returns></returns>
	public IEnumerator FadeIn(float time, float alphaLimit)
	{
		var alphaDistance = _fadeImage.color.a - alphaLimit;

		while(_fadeImage.color.a > alphaLimit)
		{
			_fadeImage.color -= new Color(0, 0, 0, GetAlphaDistancePerFrame(alphaDistance, time));
			Debug.Log("[Debug] : Fade In Updated! as " + _fadeImage.color.a); // 4debug
			yield return null;
		}
	}

	/// <summary>
	/// フェードアウトを実行するコルーチンメソッド
	/// </summary>
	/// <param name="time"></param>
	/// <param name="alphaLimit"></param>
	/// <returns></returns>
	public IEnumerator FadeOut(float time, float alphaLimit)
	{
		var alphaDistance = alphaLimit - _fadeImage.color.a;

		while(_fadeImage.color.a < alphaLimit)
		{
			_fadeImage.color += new Color(0, 0, 0, GetAlphaDistancePerFrame(alphaDistance, time));
			Debug.Log("[Debug] : Fade Out Updated! as " + _fadeImage.color.a); // 4debug
			yield return null;
		}
	}

	/// <summary>
	/// 1フレームあたりのアルファ差分を計算するメソッド
	/// </summary>
	/// <returns></returns>
	private float GetAlphaDistancePerFrame(float alphaDistance, float time)
	{
		// AlphaDistancePreFrame [-/F]
		// = AlphaDistance [-] / (_fadeSpeedSecond [s] * FramePerSecond [F/s])
		// = AlphaDistance [-] * deltaTime [s/F] / _fadeSpeedSecond [s]
		return alphaDistance * Time.deltaTime / time;
	}
}
