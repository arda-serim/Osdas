using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFixer : MonoBehaviour
{
	private static float fixedFov = 60 / (16.0f / 9); 

	void Start()
	{
		// Force fixed width
		float ratio = (float)Screen.height / (float)Screen.width;
		GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = ratio * fixedFov;
	}
}
