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
	/// 初期化メソッド
	/// </summary>
	public void Initalize()
	{
		_currentColor = GetComponent<Image>().color;
	}

	/// <summary>
	/// フェードインを開始するメソッド
	/// </summary>
	/// <param name="time"></param>
	/// <returns></returns>
	public IEnumerator StartFadeIn(float time, float alphaLimit)
	{
		var alphaDistance = _currentColor.a - alphaLimit;
		while(_currentColor.a > alphaLimit)
		{
			_currentColor -= new Color(0, 0, 0, GetAlphaDistancePerFrame(alphaDistance, time));
			yield return null;
		}
	}

	/// <summary>
	/// フェードアウトを開始するメソッド
	/// </summary>
	/// <param name="time"></param>
	/// <returns></returns>
	public IEnumerator StartFadeOut(float time, float alphaLimit)
	{
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
