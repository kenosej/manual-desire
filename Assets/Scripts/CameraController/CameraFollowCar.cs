using UnityEngine;

namespace CameraController
{
	public class CameraFollowCar : MonoBehaviour 
	{
		public Transform car;
		public float distance = 6.4f;
		public float height = 1.4f;
		public float rotationDamping = 3.0f;
		public float heightDamping = 2.0f;
		public float zoomRatio = 0.5f;
		public float defaultFOV = 60f;

		private Vector3 _rotationVector;
		private Rigidbody _rB;
		private Camera _camera;

		private void Awake()
		{
			_rB = car.GetComponent<Rigidbody>();
			_camera = GetComponent<Camera>();
		}

		void LateUpdate(){
			float wantedAngle = _rotationVector.y;
			float wantedHeight = car.position.y + height;
			float myAngle = transform.eulerAngles.y;
			float myHeight = transform.position.y;

			myAngle = Mathf.LerpAngle(myAngle, wantedAngle, rotationDamping*Time.deltaTime);
			myHeight = Mathf.Lerp(myHeight, wantedHeight, heightDamping*Time.deltaTime);

			Quaternion currentRotation = Quaternion.Euler(0, myAngle, 0);
			transform.position = car.position;
			transform.position -= currentRotation * Vector3.forward*distance;
			Vector3 temp = transform.position; //temporary variable so Unity doesn't complain
			temp.y = myHeight;
			transform.position = temp;
			transform.LookAt(car);
		}

		void FixedUpdate(){
			Vector3 localVelocity = car.InverseTransformDirection(_rB.velocity);
		
			if (localVelocity.z < -0.1f)
			{
				Vector3 temp = _rotationVector; //because temporary variables seem to be removed after a closing bracket "}" we can use the same variable name multiple times.
				temp.y = car.eulerAngles.y + 180;
				_rotationVector = temp;
			}
			else
			{
				Vector3 temp = _rotationVector;
				temp.y = car.eulerAngles.y;
				_rotationVector = temp;
			}
		
			float acc = _rB.velocity.magnitude;
			_camera.fieldOfView = defaultFOV + acc * zoomRatio * Time.deltaTime;  //he removed * Time.deltaTime but it works better if you leave it like this.
		}
	}
}
