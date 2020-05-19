using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public float m_MoveSpeed = 3.0f;

	public float m_MaxZoomOut = 12.0f;

	public float m_MinZoomOut = 3.0f;

	public float m_MouseScrollMultiplier = 5.0f;

	public float m_MouseSensitivity = 3.0f;

	private Vector3 m_Input = Vector3.zero;

	private Transform m_Camera = null;

	private void Awake()
	{
		m_Camera = transform.GetChild(0);
	}

	// Update is called once per frame
	void Update()
    {
		m_Input = Vector3.zero;

		m_Input += transform.right * Input.GetAxis("Horizontal");
		m_Input += transform.forward * Input.GetAxis("Vertical");
		m_Input += transform.up * (-Input.GetAxis("Mouse ScrollWheel") * m_MouseScrollMultiplier);

		Vector3 pos = transform.position + ((m_Input * m_MoveSpeed) * Time.deltaTime);

		pos.y = Mathf.Clamp(pos.y, m_MinZoomOut, m_MaxZoomOut);

		transform.position = pos;

		// Camera Rotation.
		if (Input.GetMouseButton(1))
		{
			Cursor.lockState = CursorLockMode.Confined;
			transform.eulerAngles += new Vector3(0.0f, Input.GetAxis("Mouse X") * m_MouseSensitivity, 0.0f);
			m_Camera.eulerAngles += new Vector3(Mathf.Clamp(-Input.GetAxis("Mouse Y") * m_MouseSensitivity, -25.0f, 70.0f), 0.0f, 0.0f);
		}
		else
			Cursor.lockState = CursorLockMode.None;
    }
}