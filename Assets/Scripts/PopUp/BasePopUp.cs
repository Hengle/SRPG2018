using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// popupに必要な、最低限の機能を示します。
/// 具体的な動きについては、メソッドUpdateをoverrideしてください。
/// </summary>
public abstract class BasePopUp : MonoBehaviour
{
	// 固定値
	[SerializeField]
	protected float existTime;

	// 変数
	protected Image _image;
	protected Text _text;

	/// <summary>
	/// ポップアップの初期設定をした後、動作させます
	/// </summary>
	/// <param name="text">表示したい文章</param>
	protected void Initialize(string text)
	{
		gameObject.SetActive(true);

		transform.localScale = new Vector3(1, 1, 1);

		// テキストと背景画像の準備
		SetUpText(text);
	}

	/// <summary>
	/// TextObjectの初期設定
	/// </summary>
	private void SetUpText(string text)
	{
		_text = GetComponentInChildren<Text>();
		_text.text = text;

		//取得したTextをピッタリ収まるようにサイズ変更(Heightが長い状態)
		_text.rectTransform.sizeDelta = new Vector2(_text.preferredWidth, _text.preferredHeight);

		//再度、ピッタリ収まるようにサイズ変更(Heightもピッタリ合うように)
		_text.rectTransform.sizeDelta = new Vector2(_text.preferredWidth, _text.preferredHeight);
	}

	/// <summary>
	/// 背景画像の初期設定
	/// </summary>
	private void SetUpImage()
	{
		_image = GetComponent<Image>();

		_image.rectTransform.sizeDelta = _text.rectTransform.sizeDelta;
	}

	/// <summary>
	/// これを実行すれば、後は自動で後片付けまでしてくれます
	/// </summary>
	/// <returns></returns>
	protected IEnumerator Act()
	{
		var coroutine = StartCoroutine(PopUpMove());

		yield return coroutine;

		Destroy(gameObject);
	}

	/// <summary>
	/// 処理の中心を書いてください
	/// </summary>
	/// <returns></returns>
	protected abstract IEnumerator PopUpMove();

	/// <summary>
	/// コルーチンの実行メソッド
	/// </summary>
	public abstract IEnumerator RunCoroutine();
}
