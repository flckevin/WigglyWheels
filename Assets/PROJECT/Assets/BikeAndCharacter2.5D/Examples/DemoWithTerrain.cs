using Kamgam.BikeAndCharacter25D.Helpers;
using System.Collections.Generic;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Kamgam.BikeAndCharacter25D
{
	public class DemoWithTerrain : MonoBehaviour
	{
		[Header("Prefabs")]
		public GameObject BikeAndCharPrefabPrefab;

		[Header("References")]
		public Transform BikeSpawnPosition;
		public Cameraman2D Cameraman2D;
		public GameObject KeyUI;
		public GameObject TouchUI;

		// Runtime References
		[System.NonSerialized]
		public BikeAndCharacter BikeAndCharacter;

		protected List<GameObject> Balls = new List<GameObject>();

		public void Start()
		{
			Application.targetFrameRate = 60;

			Restart();
			BikeAndCharacter.Bike.IsSpeedingUp = true;
		}

		public static bool UseTouchInput => Input.touchSupported;

		public void OnEnable()
		{
			bool useTouch = UseTouchInput;
			TouchUI.SetActive(useTouch);
			KeyUI.SetActive(!useTouch);
		}

		public void Restart()
		{
			// destroy old balls
			if (Balls.Count > 0)
			{
				foreach (var ball in Balls)
				{
					Destroy(ball);
				}
				Balls.Clear();
			}

			// destroy old bike
			if (BikeAndCharacter != null)
			{
				Destroy(BikeAndCharacter.gameObject);
				BikeAndCharacter = null;
				Cameraman2D.SetObjectToTrack(null);
			}

			// create new
			BikeAndCharacter = Utils.SmartInstantiatePrefab<BikeAndCharacter>(BikeAndCharPrefabPrefab, null, false);
			BikeAndCharacter.transform.position = BikeSpawnPosition.position;
			BikeAndCharacter.gameObject.SetActive(true);

			if (UseTouchInput)
				BikeAndCharacter.TouchInput = TouchUI.GetComponent<IBikeTouchInput>();

			// inform cameraman
			Cameraman2D.SetObjectToTrack(BikeAndCharacter.Character.TorsoBody);
			Cameraman2D.SetCameraToMove(Camera.main.transform);
		}

		public async void Update()
		{
			if (mousePressed() && !UseTouchInput)
			{
				var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				Vector3 pos;
				IntersectMousePosWithXYPlane(out pos, mousePosition(), Vector3.up * 5f);
				go.transform.position = pos;
				Destroy(go.GetComponent<SphereCollider>());

				await System.Threading.Tasks.Task.Yield();

				go.AddComponent<CircleCollider2D>();
				var rb = go.AddComponent<Rigidbody2D>();
				rb.mass = 10;
				rb.simulated = true;
				rb.interpolation = RigidbodyInterpolation2D.Interpolate;
				rb.WakeUp();

				Balls.Add(go);
			}

			if (restartPressed())
			{
				Restart();
			}
		}

		protected bool restartPressed()
		{
#if ENABLE_INPUT_SYSTEM
			return ((Keyboard.current != null && Keyboard.current.backspaceKey.wasPressedThisFrame) || (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame));
#else
			return Input.GetKey(KeyCode.Backspace);
#endif
		}

		protected Vector2 mousePosition()
		{
#if ENABLE_INPUT_SYSTEM
			if (Mouse.current == null)
				return Vector2.zero;
			return Mouse.current.position.ReadValue();
#else
			return Input.mousePosition;
#endif
		}

		protected bool mousePressed()
		{
#if ENABLE_INPUT_SYSTEM
			return Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;
#else
			return Input.GetMouseButtonDown(0);
#endif
		}

		/// <summary>
		/// Calculates the interection point of a ray from the center of the EditorSceneView camera forward.
		/// </summary>
		/// <param name="position">The result (world space position of the hit).</param>
		/// <param name="defaultPosition">position value will be set to this if not hit.</param>
		/// <returns>True if hit, False if not hit.</returns>
		public static bool IntersectMousePosWithXYPlane(out Vector3 position, Vector2 mousePos, Vector3 defaultPosition)
		{
			// raycast from the editor camera center to the controller XY plane and save result in position.
			var cam = Camera.main;
			if (cam != null)
			{
				var ray = cam.ScreenPointToRay(mousePos);
				Plane plane = new Plane(Vector3.forward, Vector3.zero);
				float enter;
				if (plane.Raycast(ray, out enter))
				{
					Vector3 hitPoint = ray.GetPoint(enter);
					position = hitPoint;
					return true;
				}
			}

			position = defaultPosition;
			return false;
		}
	}
}
