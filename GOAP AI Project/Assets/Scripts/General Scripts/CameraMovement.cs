using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public float m_MoveSpeed = 3.0f;

	private Vector3 m_KeyboardInput = Vector3.zero;

	private Transform m_Camera = null;

	private void Awake()
	{
		m_Camera = transform.GetChild(0);
	}

	// Update is called once per frame
	void Update()
    {
		m_KeyboardInput = Vector3.zero;

		m_KeyboardInput += transform.right * Input.GetAxis("Horizontal");
		m_KeyboardInput += transform.forward * Input.GetAxis("Vertical");

		transform.position += ((m_KeyboardInput * m_MoveSpeed) * Time.deltaTime);

		// Camera Rotation.
		if (Input.GetMouseButton(1))
		{
			Cursor.lockState = CursorLockMode.Confined;
			transform.eulerAngles += new Vector3(0.0f, -Input.GetAxis("Mouse X"), 0.0f);
			m_Camera.eulerAngles += new Vector3(Input.GetAxis("Mouse Y"), 0.0f, 0.0f);
		}
		else
			Cursor.lockState = CursorLockMode.None;
    }
}