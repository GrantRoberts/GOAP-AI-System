using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	/// <summary>
	/// The speed at which the camera moves.
	/// </summary>
	public float m_MoveSpeed = 3.0f;

	/// <summary>
	/// Maximum distance the camera can zoom out (max height).
	/// </summary>
	public float m_MaxZoomOut = 12.0f;

	/// <summary>
	/// Minimum distanec the camera can zoom out (min height).
	/// </summary>
	public float m_MinZoomOut = 3.0f;

	/// <summary>
	/// How much to increase mouse scroll speed.
	/// </summary>
	public float m_MouseScrollMultiplier = 5.0f;

	/// <summary>
	/// How much the camera rotates with the mouse.
	/// </summary>
	public float m_MouseSensitivity = 3.0f;

	/// <summary>
	/// Movement input from the keyboard.
	/// </summary>
	private Vector3 m_Input = Vector3.zero;

	/// <summary>
	/// The camera.
	/// </summary>
	private Transform m_Camera = null;

	private void Awake()
	{
		m_Camera = transform.GetChild(0);
	}

	// Update is called once per frame
	void Update()
    {
		// Camera Movement.
		m_Input = Vector3.zero;

		// Get movement on the x,y, and z axies.
		m_Input += transform.right * Input.GetAxis("Horizontal");
		m_Input += transform.forward * Input.GetAxis("Vertical");
		m_Input += transform.up * (-Input.GetAxis("Mouse ScrollWheel") * m_MouseScrollMultiplier);

		// Create a vec3 of the new position.
		Vector3 pos = transform.position + ((m_Input * m_MoveSpeed) * Time.deltaTime);

		// Clamp how much the camera can move up and down.
		pos.y = Mathf.Clamp(pos.y, m_MinZoomOut, m_MaxZoomOut);

		// Apply the new position.
		transform.position = pos;

		// Camera Rotation.
		if (Input.GetMouseButton(1))
		{
			Cursor.lockState = CursorLockMode.Confined;
			transform.eulerAngles += new Vector3(0.0f, Input.GetAxis("Mouse X") * m_MouseSensitivity, 0.0f);

			// Clamp how much the camera can look up and down.
			m_Camera.eulerAngles += new Vector3(Mathf.Clamp(-Input.GetAxis("Mouse Y") * m_MouseSensitivity, -15.0f, 50.0f), 0.0f, 0.0f);
		}
		else
			Cursor.lockState = CursorLockMode.None;
    }
}