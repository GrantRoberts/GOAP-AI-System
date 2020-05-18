using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public float m_MoveSpeed = 3.0f;

	private Vector3 m_PlayerMovementInput = Vector3.zero;

	private Transform m_CameraTransform = null;

	private void Awake()
	{
		m_CameraTransform = transform.GetChild(0);
	}

	// Update is called once per frame
	void Update()
    {
		// Movement.
		m_PlayerMovementInput = Vector3.zero;

		m_PlayerMovementInput += m_CameraTransform.right * (Input.GetAxis("Horizontal") * m_MoveSpeed);
		m_PlayerMovementInput += m_CameraTransform.forward * (Input.GetAxis("Vertical") * m_MoveSpeed);

		m_PlayerMovementInput *= Time.deltaTime;

		transform.position += new Vector3(m_PlayerMovementInput.x, 0.0f, -m_PlayerMovementInput.y);

		// Camera Rotation.
		if (Input.GetMouseButton(1))
		{
			m_CameraTransform.eulerAngles += new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0.0f);
		}
    }
}