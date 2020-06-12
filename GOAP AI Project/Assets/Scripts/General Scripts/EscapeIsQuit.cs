using System;
using UnityEngine;

public class EscapeIsQuit : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
