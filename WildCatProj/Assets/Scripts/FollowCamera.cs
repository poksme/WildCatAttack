using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

	[SerializeField] private Transform BoundsPlayer1Left;
	[SerializeField] private Transform BoundsPlayer1Right;
	[SerializeField] private Transform BoundsPlayer2Left;
	[SerializeField] private Transform BoundsPlayer2Right;
	[SerializeField] private float MaxUnzoom = 100f;
	[SerializeField] private float MinZoom = 30f;
	[SerializeField] private float UnzoomStep = 0.5f;
	[SerializeField] private float PanningSmoothingStep = 1f;
	[SerializeField] private float ZoomingSmoothingStep = 10f;
	private Vector3 center;
	private Plane[] planes;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		center = ((BoundsPlayer2Right.position - BoundsPlayer1Left.position)/2.0f) + BoundsPlayer1Left.position;
//		center.z = transform.position.z;
		center.y += 2f;
//		transform.position = center;
		transform.position = new Vector3(Mathf.Lerp(transform.position.x, center.x, Time.deltaTime * PanningSmoothingStep), 
		                                 Mathf.Lerp(transform.position.y, center.y, Time.deltaTime * PanningSmoothingStep),
		                                 transform.position.z);
		transform.LookAt(new Vector3(transform.position.x, transform.position.y, center.z));

		float fovBck = Camera.main.fieldOfView;

		Camera.main.fieldOfView = MinZoom;
		planes =  GeometryUtility.CalculateFrustumPlanes(Camera.main);
		while (!seeBothPlayers() && Camera.main.fieldOfView < MaxUnzoom) {
			planes =  GeometryUtility.CalculateFrustumPlanes(Camera.main);
			Camera.main.fieldOfView += UnzoomStep;
		}
		Camera.main.fieldOfView = Mathf.Lerp(fovBck, Camera.main.fieldOfView, Time.deltaTime * ZoomingSmoothingStep);
	}

	private bool seeBothPlayers() {
		return GeometryUtility.TestPlanesAABB(planes, BoundsPlayer1Left.collider.bounds) 
			&& GeometryUtility.TestPlanesAABB(planes, BoundsPlayer1Right.collider.bounds)
			&& GeometryUtility.TestPlanesAABB(planes, BoundsPlayer2Left.collider.bounds)
			&& GeometryUtility.TestPlanesAABB(planes, BoundsPlayer2Right.collider.bounds);
	}
}
