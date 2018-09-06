using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UniRx;

public class GameManager : MonoBehaviour {

	private Text mainText;
	private Text timerText;
	private int countDown = 3;
	private bool timerflag = false;
	private int minute = 0;
	private float seconds = 0f;
	[SerializeField]
	private IntReactiveProperty enemynumber = new IntReactiveProperty (1);
	private Subject<Unit> countDownFinish = new Subject<Unit>();
	public IObservable<Unit> OnCountDownFinish
	{
		get { return countDownFinish; }
	}

	void Start ()
	{
		mainText = GameObject.Find("/GameManager/PlayerCanvas/MainText").GetComponentInChildren<Text>();
		mainText.text = "";
		timerText = GameObject.Find("/GameManager/PlayerCanvas/TimerText").GetComponentInChildren<Text>();
		timerText.text = minute.ToString("00") + "." + seconds.ToString ("00.000");;

		//スタートカウントダウン
		Observable.Interval(System.TimeSpan.FromSeconds(1))
			.Take(4)
			.Subscribe(_ =>
			{
				mainText.text = countDown.ToString();
				if (countDown == 0) AudioManager.Instance.PlaySE("SE_START");
				else AudioManager.Instance.PlaySE("SE_COUNTDOWN");
				countDown--;
			}, () =>
			{
				mainText.text = "Start!";
				timerflag = true;
				countDownFinish.OnNext(Unit.Default);
				Observable.Timer(System.TimeSpan.FromSeconds(1))
					.Subscribe (_ =>
					{
						mainText.text = "";
						AudioManager.Instance.PlayBGM("BGM_MAIN");
					});

				Observable.EveryUpdate()
					.Where(_ => timerflag)
					.Subscribe(_ =>
					{
						seconds += Time.deltaTime;
						if(seconds >= 60f) {
							minute++;
							seconds = seconds - 60;
						}
						
						timerText.text = minute.ToString("00") + "." + seconds.ToString ("00.000");
						
					});
			});
		
		enemynumber.Where(e => e == 0)
            .Subscribe(_ =>
			{
				timerflag = false;
				mainText.text = "Clear!\nTime : " + minute.ToString("00") + "." + seconds.ToString ("00.000");
			});
	}

	public void enemyNumverDecrement ()
	{
		enemynumber.Value -= 1;
	}
}
