using UnityEngine;

public class MovePointGenerete : MonoBehaviour {

	[SerializeField]
	private int _genereteNum = 30;

	void Start ()
	{
		var parent = new GameObject ("MovePoints");
		for (int i = 1; i <= _genereteNum; i++)
		{
			var nextMovePoint = new GameObject();
			nextMovePoint.name = ("MovePoint(" + i + ")");
			nextMovePoint.tag = "MovePoint";
			nextMovePoint.layer = 9;
			nextMovePoint.AddComponent<SphereCollider>();
			nextMovePoint.AddComponent<NextMovePosition>();
			var rb = nextMovePoint.AddComponent<Rigidbody>();
			rb.useGravity = false;
			rb.constraints = RigidbodyConstraints.FreezePosition;
			nextMovePoint.transform.parent = parent.transform;
			nextMovePoint.transform.position = new Vector3 (Random.Range(-50, 50), 0, Random.Range(-50, 50));
		}
	}
}
