//////////////////////////////////////////////////////////////////////////////////////////////////////////////
//																											//
//	Created:		21/07/2014 - 13:05	|	Ranjit Singh													//
//	Modified:		23/07/2014 - 16:15	|	Ranjit Singh													//
//																											//
//	Description:																							//
//																											//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////
//																											//
//	TODO: Disclaimer																						//
//																											//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Defines for Enums, Structs and Classes
// ------------------------------------------------------------------

public class InGameCamera : MonoBehaviour
{
	void Start()
	{
		
	}

	void Update()
	{

	}
}

///------------------------------------------------------------------------------------------------------///
///------------------------------------------------------------------------------------------------------///
//     ____  _      _____      _____  _____   ____       _ ______ _____ _______ 
//    / __ \| |    |  __ \    |  __ \|  __ \ / __ \     | |  ____/ ____|__   __|
//   | |  | | |    | |  | |   | |__) | |__) | |  | |    | | |__ | |       | |   
//   | |  | | |    | |  | |   |  ___/|  _  /| |  | |_   | |  __|| |       | |   
//   | |__| | |____| |__| |   | |    | | \ \| |__| | |__| | |___| |____   | |   
//    \____/|______|_____/    |_|    |_|  \_\\____/ \____/|______\_____|  |_|   
//                                                                              
///------------------------------------------------------------------------------------------------------///
///------------------------------------------------------------------------------------------------------///
/*
	// Unity Functions
	// --------------------------------------------------------------

	// Use this for initialization
	void Start()
	{
		m_Links = new List<CameraLink>();
		m_cameraNodes = new List<CameraData>();

		m_zoomedOutView = new CameraData();
	}

	// Update is called once per frame
	void Update()
	{

#if UNITY_EDITOR
		// DEBUG: Camera Path
		if (m_cameraNodes.Count > 1)
		{
			for (int i = 0; i < m_cameraNodes.Count - 1; i++)
			{
				Debug.DrawLine(m_cameraNodes[i].Position, m_cameraNodes[i + 1].Position);
			}

			Debug.DrawLine(m_camPos, m_zoomedOutView.Position);
		}
#endif

		if (!m_inputManager.Dragging())
		{
			m_fAccumulator += Time.deltaTime;
			while (m_fAccumulator >= m_fFrameTime)
			{
				// Apply friction
				m_inputManager.DragValue *= 1.0F - friction;

				m_fAccumulator -= m_fFrameTime;
			}
		}

		m_fCurrentPosition = Mathf.Lerp(m_fCurrentPosition, m_fCurrentPosition + m_inputManager.DragValue, smoothness * Time.deltaTime);

		// Keep it as a ratio
		m_fCurrentPosition = Mathf.Clamp(m_fCurrentPosition, 0, 1);

		int linkID = -1;
		foreach (CameraLink link in m_Links)
		{
			linkID++;
			// Check Is Inside This Link
			bool IsInside = true;

			if (m_fCurrentPosition < link.MinValue) { IsInside = false; }
			if (m_fCurrentPosition > link.MaxValue) { IsInside = false; }

			// Not Inside Continue
			if (!IsInside) { continue; }

			float percentInLink = ((m_fCurrentPosition - link.MinValue) / (link.MaxValue - link.MinValue)) * 1.0F;

			m_camPos = Vector3.Lerp(link.node1.Position, link.node2.Position, percentInLink);
			m_camRot = Quaternion.Lerp(link.node1.Rotation, link.node2.Rotation, percentInLink);

			//Debug.Log("LinkID: " + linkID + " | Perc: " + percentInLink);

			break;
		}

#if UNITY_ANDROID
		zoomLevel += m_inputManager.PinchZoom() * 0.05f;
#endif

#if UNITY_EDITOR || UNITY_STANDALONE
		zoomLevel -= Input.GetAxis("Mouse ScrollWheel");
#endif

		m_fCurrentZoomLevel = Mathf.Lerp(m_fCurrentZoomLevel, zoomLevel, 5.0f * Time.deltaTime);

		zoomLevel = Mathf.Clamp(zoomLevel, 0, 1);
		m_fCurrentZoomLevel = Mathf.Clamp(m_fCurrentZoomLevel, 0, 1);

		if (!m_GameManager.GetMapManager().mapEditorModeEnabled)
		{
			if (!disable)
			{
				GetCamera().transform.position = Vector3.Lerp(m_camPos, m_zoomedOutView.Position, m_fCurrentZoomLevel);
				GetCamera().transform.rotation = Quaternion.Lerp(m_camRot, m_zoomedOutView.Rotation, m_fCurrentZoomLevel);
			}
		}
	}

	// Custom Functions
	// --------------------------------------------------------------

	public void SetGameManager(GameManager a_Manager)
	{
		if (!m_IsGameManagerSet)
		{
			m_GameManager = a_Manager;

			m_IsGameManagerSet = true;

			m_cameraHandler = m_GameManager.GetCameraHandler();
			m_inputManager = m_GameManager.GetInputManager();
		}
	}

	public void Shutdown()
	{
		m_Links.Clear();
		m_cameraNodes.Clear();
		m_zoomedOutView = null;
	}

	public void CreateCameraPath()
	{
		// Calculate Total Distance
		if (m_cameraNodes.Count > 1)
		{
			for (int i = 0; i < m_cameraNodes.Count - 1; i++)
			{
				float distance = Vector3.Magnitude(m_cameraNodes[i].Position - m_cameraNodes[i + 1].Position);
				m_fTotalDistance += distance;
			}

			float traveled = 0.0F;

			for (int i = 0; i < m_cameraNodes.Count - 1; i++)
			{
				float distance = Vector3.Magnitude(m_cameraNodes[i].Position - m_cameraNodes[i + 1].Position);

				CameraLink link = new CameraLink();

				link.Distance = distance;
				link.MinValue = traveled / m_fTotalDistance;
				link.MaxValue = (traveled + distance) / m_fTotalDistance;

				traveled += distance;

				link.node1 = m_cameraNodes[i];
				link.node2 = m_cameraNodes[i + 1];

				m_Links.Add(link);
			}
		}
		else
		{
			Debug.Log("Unable to Create Camera Path");
		}
	}

	public void AddCameraNode(CameraData a_Data)
	{
		switch (a_Data.Type)
		{
			case eCameraNodeType.PATH: m_cameraNodes.Add(a_Data); break;
			case eCameraNodeType.ZOOM: m_zoomedOutView = a_Data; break;
		}
	}

	public void Reset()
	{
		// Reset current camera position
		m_fCurrentPosition = 0.0f;
	}

	// Requests camera from the camera handler
	private Camera GetCamera()
	{
		return m_cameraHandler.RequestCamera();
	}

public static class CameraHelper
{
	public static int NodeToInt(eCameraNodeType a_Type)
	{
		int value = 0;

		switch (a_Type)
		{
			case eCameraNodeType.PATH: value = 0; break;
			case eCameraNodeType.ZOOM: value = 1; break;
		}

		return value;
	}

	public static eCameraNodeType IntToTile(int a_Type)
	{
		eCameraNodeType value = eCameraNodeType.PATH;

		switch (a_Type)
		{
			case 0: value = eCameraNodeType.PATH; break;
			case 1: value = eCameraNodeType.ZOOM; break;
		}

		return value;
	}
}
	// Public Variables
	// --------------------------------------------------------------

	public float friction = 0.2f;

	public float smoothness = 10.0f;

	public float zoomLevel = 0.0f;

	public bool disable = false;

	// Private Variables
	// --------------------------------------------------------------

	private GameManager m_GameManager = null;
	private bool m_IsGameManagerSet = false;

	private CameraHandler m_cameraHandler;
	private InputManager m_inputManager;

	private float m_fTotalDistance = 0.0F;

	private List<CameraData> m_cameraNodes;

	private CameraData m_zoomedOutView;

	private List<CameraLink> m_Links;

	private float m_fCurrentPosition = 0.0f;

	private float m_fAccumulator = 0.0f;

	private float m_fFrameTime = 1.0f / 10.0f;

	private Vector3 m_camPos = Vector3.zero;

	private Quaternion m_camRot = Quaternion.identity;

	private float m_fCurrentZoomLevel = 0.0f;

public enum eCameraNodeType
{
	PATH,
	ZOOM
};

public class CameraLink
{
	public float Distance;
	public float MinValue;
	public float MaxValue;

	public CameraData node1;
	public CameraData node2;

	public void LogData()
	{
		Debug.Log("Link: Dist(" + Distance + ") Min(" + MinValue + ") Max(" + MaxValue + ")");
	}
}*/