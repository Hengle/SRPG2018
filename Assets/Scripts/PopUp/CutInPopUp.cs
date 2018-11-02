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

	protected override IEnumerator PopUpMove()
	{
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
}
