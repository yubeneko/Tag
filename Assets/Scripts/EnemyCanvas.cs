using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class EnemyCanvas : MonoBehaviour {

	private Enemy enemy;
	private GameObject mainCamera;
	private Text enemyText;
	[SerializeField]
	private IntReactiveProperty touchCount = new IntReactiveProperty(2);
	public IObservable <int> OnCountChanged
	{
		get { return touchCount; }
	}

	void Start ()
	{
		enemy = GetComponentInParent<Enemy>();
		enemyText = GetComponentInChildren<Text>();
		mainCamera = Camera.main.gameObject;


		enemy.OnTouchWithPlayer.Subscribe(_ =>
		{
			touchCount.Value--;
		});

		touchCount.Subscribe (x => enemyText.text = x.ToString());

		this.UpdateAsObservable().Subscribe (_ =>
		{
			transform.LookAt(mainCamera.transform);
		});
	}
}
