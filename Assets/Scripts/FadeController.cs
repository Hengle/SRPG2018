using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
	/// <summary>
	/// アルファをいじる対象の現在の色
	/// </summary>
	private Color _currentColor;

	/// <summary>
	/// アルファをいじる対象の元の色
	/// </summary>
	private Color _defaultColor;

	/// <summary>
	/// 初期化メソッド
	/// </summary>
	public void Initalize()
	{
		_defaultColor = GetComponent<Image>().color;
		_currentColor = _defaultColor;
	}

	/// <summary>
	/// ゲーム開始時のアルファまでフェードインを開始するメソッド
	/// </summary>
	/// <param name="time"></param>
	/// <returns></returns>
	public IEnumerator StartFadeInDefault(float time)
	{
		return StartFadeIn(time, _defaultColor.a);
	}

	/// <summary>
	/// フェードインを開始するメソッド
	/// </summary>
	/// <param name="time"></param>
	/// <returns></returns>
	public IEnumerator StartFadeIn(float time, float alphaLimit)
	{
		Debug.Log("[Debug] : Fade In Called!"); // 4debug

		var alphaDistance = _currentColor.a - alphaLimit;
		while(_currentColor.a > alphaLimit)
		{
			_currentColor -= new Color(0, 0, 0, GetAlphaDistancePerFrame(alphaDistance, time));
			yield return null;
		}
	}

	/// <summary>
	/// ゲーム開始時のアルファまでフェードアウトを開始するメソッド
	/// </summary>
	/// <param name="time"></param>
	/// <returns></returns>
	public IEnumerator StartFadeOutDefault(float time)
	{
		return StartFadeOut(time, _defaultColor.a);
	}

	/// <summary>
	/// フェードアウトを開始するメソッド
	/// </summary>
	/// <param name="time"></param>
	/// <returns></returns>
	public IEnumerator StartFadeOut(float time, float alphaLimit)
	{
		Debug.Log("[Debug] : Fade Out Called!"); // 4debug

		var alphaDistance = alphaLimit - _currentColor.a;
		while(_currentColor.a < alphaLimit)
		{
			_currentColor += new Color(0, 0, 0, GetAlphaDistancePerFrame(alphaDistance, time));
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
