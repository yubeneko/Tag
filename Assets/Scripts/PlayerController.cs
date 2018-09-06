using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerController : MonoBehaviour
{

	[SerializeField]
    private float speed = 5f;
	[SerializeField]
    private float rotateSpeed = 120f;
	[SerializeField]
	private GameManager gameManager;
	private bool isMove = false;

	void Start ()
	{
		gameManager.OnCountDownFinish.Subscribe(_ => isMove = true);
		//垂直移動
		this.FixedUpdateAsObservable()
			.Where(_ => isMove)
			.Select (_ => new Vector3 (0, 0, Input.GetAxis ("Vertical")))
			.Select (velocity => transform.TransformDirection (velocity))
			.Subscribe (velocity =>
			{
				transform.localPosition += velocity * speed * Time.fixedDeltaTime;
			});
		
		//方向変換
		this.FixedUpdateAsObservable()
			.Where (_ => isMove)
			.Subscribe(_ =>
			{
				transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.fixedDeltaTime, 0);
			});
	}
}