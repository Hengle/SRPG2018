using UnityEngine;
using System.Collections;

/// <summary>
/// プレイヤーが変わるときに、そのカットインを表示させます。
/// PopUpController以外からは、直接の参照予定は無いです
/// </summary>
public class CutInPopUp : BasePopUp
{
	// 固定値
	[SerializeField]
	private float WaitTime;
	[SerializeField]
	private GameObject _touchBlocker;
	[SerializeField]
	private float _fadeTime;
	[SerializeField, Range(0, 1f)]
	private float _fadeOutAlphaLimit;

	protected IEnumerable Act()
	{
		var coroutine = StartCoroutine(Move());

		yield return coroutine;

		Destroy(gameObject);
	}

	protected override IEnumerator Move()
	{
		if(!_touchBlocker) Debug.LogError("[Error] : TouchBlocker is not set!");
		if(!_touchBlocker.GetComponent<FadeController>()) Debug.LogError("[Error] : FadeController is not attached!");

		Debug.Log("Move Called and call StartFadeOutDefault Coroutine");	// 4debug
		// _fadeOutAlphaLimitまで_fadeTime秒でフェードアウト
		_touchBlocker.GetComponent<FadeController>().StartFadeOutDefault(_fadeTime);
		Debug.Log("Finished Fade out Coroutine");	// 4debug

		float time = 0f;
		float moveTime = (existTime - WaitTime) / 2;

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

		while(time < moveTime + WaitTime)
		{
			yield return null;
			time += Time.deltaTime;
		}

		while(time < existTime)
		{
			float ratio = time - (moveTime + WaitTime) / moveTime;
			transform.eulerAngles = Vector3.Lerp(mid, end, ratio);

			yield return null;
			time += Time.deltaTime;
		}

		// _fadeTime秒で元のアルファまでフェードイン
		// yield return _touchBlocker.GetComponent<FadeController>().StartFadeInDefault(_fadeTime);
	}
}
