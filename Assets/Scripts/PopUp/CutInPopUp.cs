using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// プレイヤーが変わるときに、そのカットインを表示させます。
/// PopUpController以外からは、直接の参照予定は無いです
/// </summary>
public class CutInPopUp : BasePopUp
{
	// 固定値
	[SerializeField]
	private float _waitTime;
	[SerializeField]
	private float _fadeTime;
	[SerializeField, Range(0, 1f)]
	private float _fadeOutAlphaLimit;

	private FadeController _fc;

	/// <summary>
	/// 継承側のInitializeメソッド
	/// </summary>
	/// <param name="text"></param>
	public void Initialize(string text, FadeController fc)
	{
		_fc = fc;
		Initialize(text);

		StartCoroutine(RunCoroutine());
	}

	protected override IEnumerator PopUpMove()
	{
		if(!_fc) Debug.LogError("[Error] : TouchBlocker is not set!");
		if(!_fc.GetComponent<FadeController>()) Debug.LogError("[Error] : FadeController is not attached!");

		Debug.Log("Move Called and call StartFadeOutDefault Coroutine");	// 4debug
		// _fadeOutAlphaLimitまで_fadeTime秒でフェードアウト

		Debug.Log("Finished Fade out Coroutine");	// 4debug

		float time = 0f;
		float moveTime = (existTime - _waitTime) / 2;

		Vector3 start = new Vector3(-90, 0, 0);
		Vector3 mid = new Vector3(0, 0, 0);
		Vector3 end = new Vector3(90, 0, 0);

		while(time < moveTime)
		{
			float ratio = time / moveTime;
			transform.eulerAngles = Vector3.Lerp(start, mid, ratio);

			yield return null;
			time += Time.deltaTime;
		}

		while(time < moveTime + _waitTime)
		{
			yield return null;
			time += Time.deltaTime;
		}

		while(time < existTime)
		{
			float ratio = time - (moveTime + _waitTime) / moveTime;
			transform.eulerAngles = Vector3.Lerp(mid, end, ratio);

			yield return null;
			time += Time.deltaTime;
		}
	}

	public override IEnumerator RunCoroutine()
	{
		// TouchBlockerを有効化
		gameObject.SetActive(true);

		// _fadetime秒でフェードアウト
		yield return StartCoroutine(_fc.FadeOut(_fadeTime, 0.8f));

		// Textを有効化
		GetComponent<Text>().gameObject.SetActive(true);

		// Act()を実行
		yield return StartCoroutine(Act());

		// Textを無効化
		GetComponent<Text>().gameObject.SetActive(false);

		// _fadetime秒でフェードイン
		yield return StartCoroutine(_fc.FadeIn(_fadeTime));

		// TouchBlockerを無効化
		gameObject.SetActive(false);
	}
}
