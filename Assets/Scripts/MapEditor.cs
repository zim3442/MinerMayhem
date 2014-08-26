////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	                                                                                                      //
//	Created:		11/07/2014 - 13:41	|	Adam Coulson                                                  //
//	Modified:		13/07/2014 - 12:37	|	Adam Coulson                                                  //
//	                                                                                                      //
//	Description:	                                                                                      //
//	                                                                                                      //
////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	                                                                                                      //
//	TODO: Disclaimer                                                                                      //
//	                                                                                                      //
////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

// Defines for Enums, Structs and Classes
// ---------------------------------------------------

#if (UNITY_EDITOR)
using UnityEditor;

// URLs
//
// Custom Editor Docs
// > http://docs.unity3d.com/ScriptReference/EditorGUI.html
// > http://docs.unity3d.com/ScriptReference/EditorGUILayout.html
// > http://docs.unity3d.com/ScriptReference/GUILayout.html
// > http://docs.unity3d.com/ScriptReference/GUISkin.html
// > http://docs.unity3d.com/ScriptReference/GUIStyle.html
//
// Gizmos for Rendering Icon in Scene for MapEditor Objects
// > http://docs.unity3d.com/ScriptReference/Gizmos.DrawIcon.html
//

[CustomEditor(typeof(MapEditor))]
[CanEditMultipleObjects]
public class MapEditorEditor : Editor
{
	int m_LaneToRemove = 0;
	int m_TowerPillarToRemove = 0;
	bool m_RemoveConfirm = false;

	public override void OnInspectorGUI()
	{
		MapEditor myTarget = (MapEditor)target;

		if (m_RemoveConfirm)
		{
			EditorGUILayout.HelpBox("Are you sure you want to Continue.\nThis Will Remove ALL the Editor Controlled GameObjects.\nMake Sure you have Saved Any Changes to File.\nThis Is your Last Chance to Go Back!", MessageType.Warning);
			
			if (GUILayout.Button("Go Back!"))
			{
				m_RemoveConfirm = false;
			}
			if (GUILayout.Button("Confirm"))
			{
				myTarget.RemoveGameObjectsFromScene();
				m_RemoveConfirm = false;
			}

			return;
		}

		GUI.color = Color.red;
		if (GUILayout.Button("Cleanup GameObjects from Scene")) { m_RemoveConfirm = true; }
		GUI.color = Color.white;

		// File IO
		// >---------------------------------------------------
		EditorGUILayout.Foldout(true, "Save / Load");
		myTarget.LevelFileName = EditorGUILayout.TextField("Level Name", myTarget.LevelFileName);
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Save To File")) { myTarget.SaveLevel(); }
		if (GUILayout.Button("Load From File")) { myTarget.LoadLevel(); }
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.Space();

		// Lane Data
		// >---------------------------------------------------
		EditorGUILayout.Foldout(true, "Lane Data");
		EditorGUILayout.LabelField("Lane Count", myTarget.LaneCount.ToString());
		EditorGUILayout.LabelField("Node Count", myTarget.LaneCount.ToString());

		if (myTarget.LaneCount > 0)
		{
			EditorGUILayout.BeginHorizontal();
			m_LaneToRemove = EditorGUILayout.IntSlider("Remove Lane", m_LaneToRemove, 1, myTarget.LaneCount);
			if (GUILayout.Button("Remove")) { myTarget.RemoveLane(m_LaneToRemove); }
			EditorGUILayout.EndHorizontal();
		}

		EditorGUILayout.BeginHorizontal();
		GUI.color = Color.yellow;
		if (GUILayout.Button("Reset", GUILayout.MaxWidth(50))) { myTarget.CreateLane(); }
		GUI.color = Color.white;
		if (GUILayout.Button("Create Lane")) { myTarget.CreateLane(); }
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.Space();

		// Lane Data
		// >---------------------------------------------------
		EditorGUILayout.Foldout(true, "Tower Base Data");
		EditorGUILayout.LabelField("Tower Base Count", myTarget.TowerPillarCount.ToString());

		if (myTarget.TowerPillarCount > 0)
		{
			EditorGUILayout.BeginHorizontal();
			m_TowerPillarToRemove = EditorGUILayout.IntSlider("Remove Base", m_TowerPillarToRemove, 1, myTarget.TowerPillarCount);
			if (GUILayout.Button("Remove")) { myTarget.RemoveTowerPillar(m_TowerPillarToRemove); }
			EditorGUILayout.EndHorizontal();
		}

		EditorGUILayout.BeginHorizontal();
		GUI.color = Color.yellow;
		if (GUILayout.Button("Reset", GUILayout.MaxWidth(50))) { myTarget.CreateTowerPillar(); }
		GUI.color = Color.white;
		if (GUILayout.Button("Create Tower Base")) { myTarget.CreateTowerPillar(); }
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.Space();

		// Camera
		// >---------------------------------------------------
		EditorGUILayout.Foldout(true, "Camera");
		EditorGUILayout.LabelField("Camera Node Count", myTarget.LaneCount.ToString());

		if (myTarget.TowerPillarCount > 0)
		{
			EditorGUILayout.BeginHorizontal();
			m_TowerPillarToRemove = EditorGUILayout.IntSlider("Remove Base", m_TowerPillarToRemove, 1, myTarget.TowerPillarCount);
			if (GUILayout.Button("Remove")) { myTarget.RemoveTowerPillar(m_TowerPillarToRemove); }
			EditorGUILayout.EndHorizontal();
		}

		EditorGUILayout.BeginHorizontal();
		GUI.color = Color.yellow;
		if (GUILayout.Button("Reset", GUILayout.MaxWidth(50))) { myTarget.ResetCameraNodes(); }
		GUI.color = Color.white;
		if (GUILayout.Button("Create Node")) { myTarget.CreateCameraNode(); }
		EditorGUILayout.EndHorizontal();


		// Display Messages
		for (int i = 0; i < myTarget.Messages.Count; i++)
		{
			EditorGUILayout.HelpBox(myTarget.Messages[i].Message, myTarget.Messages[i].Type);
		}
	}
}

public class MapEditorMessage
{
	public MapEditorMessage(MessageType a_Type, string a_Message, float a_TimeToLive)
	{
		Type = a_Type;
		Message = a_Message;
		TTL = a_TimeToLive;
		TimeAdded = Time.realtimeSinceStartup;
	}

	public MessageType Type;
	public string Message;
	public float TTL;
	public float TimeAdded;
}

//    __  __          _____    ______ _____ _____ _______ ____  _____  
//   |  \/  |   /\   |  __ \  |  ____|  __ \_   _|__   __/ __ \|  __ \ 
//   | \  / |  /  \  | |__) | | |__  | |  | || |    | | | |  | | |__) |
//   | |\/| | / /\ \ |  ___/  |  __| | |  | || |    | | | |  | |  _  / 
//   | |  | |/ ____ \| |      | |____| |__| || |_   | | | |__| | | \ \ 
//   |_|  |_/_/    \_\_|      |______|_____/_____|  |_|  \____/|_|  \_\
//                                                                     

[ExecuteInEditMode]
public class MapEditor : MonoBehaviour
{
	private string m_LevelFileName = "Default";
	private List<MapEditorMessage> m_Messages;

	private List<int> m_Lanes;
	private List<int> m_Teams;
	private List<int> m_Towers;
	private List<int> m_CameraNodes;

	// Maintenance
	// >---------------------------------------------------
	public MapEditor()
	{
		m_Messages = new List<MapEditorMessage>();

		m_Lanes = new List<int>();
		m_Teams = new List<int>();
		m_Towers = new List<int>();
		m_CameraNodes = new List<int>();
	}

	void OnEnable()  { EditorApplication.update += UpdateMessageStatus; }
	void OnDisable() { EditorApplication.update -= UpdateMessageStatus; }

	void Update()
	{
		// Move Editor to Origin of Scene
		if (gameObject.transform.position != Vector3.zero)
		{
			gameObject.transform.position = Vector3.zero;
		}
	}

	private void UpdateMessageStatus()
	{
		for (int i = 0; i < m_Messages.Count;)
		{
			if (Time.realtimeSinceStartup >= m_Messages[i].TimeAdded + m_Messages[i].TTL)
			{
				m_Messages.RemoveAt(i);
				continue;
			}
			i++;
		}
	}

	public void RemoveGameObjectsFromScene()
	{

	}

	// Lane Data
	// >---------------------------------------------------
	public void CreateLane()
	{
		m_Lanes.Add(0);
	}

	public void RemoveLane(int a_ID)
	{
		if (a_ID == -1) { return; }
		m_Lanes.RemoveAt(a_ID - 1);
	}

	// Tower Pillar Data
	// >---------------------------------------------------
	public void CreateTowerPillar()
	{
		m_Towers.Add(0);
	}

	public void RemoveTowerPillar(int a_ID)
	{
		if (a_ID == -1) { return; }
		m_Towers.RemoveAt(a_ID - 1);
	}

	// Camera Pathing
	// >---------------------------------------------------
	public void CreateCameraNode()
	{
		m_CameraNodes.Add(0);
	}

	public void ResetCameraNodes()
	{
		m_CameraNodes.Clear();
	}

	// File IO
	// >---------------------------------------------------
	public void SaveLevel()
	{
		/*LevelHelper.Reset();

		string data = "BEGIN CameraPath\n";
		LevelHelper.IncreaseTabLevel();

		for (int i = 0; i < m_Lanes.Count; i++)
		{
			data += m_Lanes[i].ToString();//.ExportData();
		}

		LevelHelper.DecreaseTabLevel();
		data += "END\n";

		LevelHelper.SaveDataToFile(m_LevelFileName, data);

		m_Messages.Add(new MapEditorMessage(MessageType.Info, "Saved Successfully!", 3.0F));*/
	}

	public void LoadLevel()
	{
		/*LevelHelper.Reset();

		string data = "BEGIN CameraPath\n";
		LevelHelper.IncreaseTabLevel();

		for (int i = 0; i < m_Lanes.Count; i++)
		{
			data += m_Lanes[i].ToString();//.ExportData();
		}

		LevelHelper.DecreaseTabLevel();
		data += "END\n";

		LevelHelper.SaveDataToFile(m_LevelFileName, data);

		m_Messages.Add(new MapEditorMessage(MessageType.Info, "Saved Successfully!", 3.0F));*/
	}

	// Properties
	// >---------------------------------------------------
	public List<MapEditorMessage> Messages { get { return m_Messages; } }

	public int LaneCount { get { return m_Lanes.Count; } }
	public int NodeCount { get { return m_Lanes.Count; } }

	public int TowerPillarCount { get { return m_Towers.Count; } }

	public int CameraNodeCount { get { return m_CameraNodes.Count; } }

	public string LevelFileName { get { return m_LevelFileName; } set { m_LevelFileName = value; } }
}

#endif

//    _   _  ____  _____  ______   _____       _______       
//   | \ | |/ __ \|  __ \|  ____| |  __ \   /\|__   __|/\    
//   |  \| | |  | | |  | | |__    | |  | | /  \  | |  /  \   
//   | . ` | |  | | |  | |  __|   | |  | |/ /\ \ | | / /\ \  
//   | |\  | |__| | |__| | |____  | |__| / ____ \| |/ ____ \ 
//   |_| \_|\____/|_____/|______| |_____/_/    \_\_/_/    \_\
//                                                           

/*public class NodeData
{
	public eNodeType Type;
	public Vector3 Position;
	public bool IsAttachedToNodeManipulator;
	
	//The position of the node inside the lane list
	public int m_NodeID;

	public NodeData(eNodeType a_Type, Vector3 a_Position, int a_NodeID)
	{
		m_NodeID = a_NodeID;
		Type = a_Type;
		Position = a_Position;
	}

	public string ExportData(float a_PathHeight)
	{
		string data = "";

		Position.y -= a_PathHeight;

		data += LevelHelper.GetTabLevel() + "BEGIN Node  ";
		data += "Type " + TileHelper.NodeToInt(Type).ToString() + "  ";
		data += "Position " + Position.ToString("f4") + "  ";
		data += "END\n";

		Position.y += a_PathHeight;

		return data;
	}
}

//    _               _   _ ______   _____       _______       
//   | |        /\   | \ | |  ____| |  __ \   /\|__   __|/\    
//   | |       /  \  |  \| | |__    | |  | | /  \  | |  /  \   
//   | |      / /\ \ | . ` |  __|   | |  | |/ /\ \ | | / /\ \  
//   | |____ / ____ \| |\  | |____  | |__| / ____ \| |/ ____ \ 
//   |______/_/    \_\_| \_|______| |_____/_/    \_\_/_/    \_\
//                                                             

public class LaneData
{
	private List<NodeData>	m_Nodes;
	private float			m_Height;

	public LaneData(float a_Height)
	{
		m_Nodes = new List<NodeData>();
		m_Height = a_Height;
	}

	public void AddNode(eNodeType a_Type, Vector3 a_Position)
	{
		a_Position.y = m_Height;
		m_Nodes.Add(new NodeData(a_Type, a_Position, m_Nodes.Count));
	}

	public void AddNode(eNodeType a_Type, Vector3 a_Position, int a_NodeID)
	{
		a_Position.y = m_Height;
		m_Nodes.Add(new NodeData(a_Type, a_Position, a_NodeID));
	}

	public void SetHeight(float a_Height)
	{
		if (m_Height == a_Height) { return; }

		m_Height = a_Height;

		foreach ( NodeData node in m_Nodes )
		{
			node.Position.y = m_Height;
		}
	}

	public NodeData GetNode(int a_ID)
	{
		if (a_ID < 0) { return null; }
		if (a_ID > m_Nodes.Count - 1) { return null; }

		return m_Nodes[a_ID];
	}

	public Vector3 GetRandomPointOnLane()
	{
		if (m_Nodes.Count == 0) { return Vector3.zero; }
		if (m_Nodes.Count == 1) { return m_Nodes[0].Position; }

		int nodeID = UnityEngine.Random.Range(0, m_Nodes.Count - 1);

		Vector3 pos = Vector3.zero;

		Vector3 nodePos1 = m_Nodes[nodeID].Position;
		Vector3 nodePos2 = m_Nodes[nodeID + 1].Position;

		float range = UnityEngine.Random.Range(0.0F, 1.0F);

		pos = Vector3.Lerp(nodePos1, nodePos2, range);

		return pos;
	}

	public void DrawLanes()
	{
		if (m_Nodes.Count < 2) { return; }

		for (int i = 0; i < m_Nodes.Count - 1; i++)
		{
			Debug.DrawLine(m_Nodes[i].Position, m_Nodes[i+1].Position);
		}
	}

	public string ExportData()
	{
		string data = "";

		data += LevelHelper.GetTabLevel() + "BEGIN Lane\n";
		LevelHelper.IncreaseTabLevel();

		foreach ( NodeData node in m_Nodes )
		{
			data += node.ExportData(m_Height);
		}

		LevelHelper.DecreaseTabLevel();
		data += LevelHelper.GetTabLevel() + "END\n";

		return data;
	}

	public int NodeCount { get { return m_Nodes.Count; } }
}

public class TeamData
{
	public eTeam	Type		= eTeam.AI;
	public int		StartHealth	= 50;
	public int		MaxHealth	= 50;
	public int		Gold		= 0;
	public int		Experience	= 0;
	public Vector3	Position	= Vector3.zero;
	public Vector3	SpawnOffset = Vector3.zero;

	public string ExportData()
	{
		string data = "";

		data += LevelHelper.GetTabLevel() + "BEGIN " + Type.ToString() + "\n";
		LevelHelper.IncreaseTabLevel();

		data += LevelHelper.GetTabLevel() + "StartHealth  " + StartHealth.ToString() + "\n";
		data += LevelHelper.GetTabLevel() + "HealthMax    " + MaxHealth.ToString() + "\n";
		data += LevelHelper.GetTabLevel() + "Gold         " + Gold.ToString() + "\n";
		data += LevelHelper.GetTabLevel() + "Experience   " + Experience.ToString() + "\n";
		data += LevelHelper.GetTabLevel() + "Position     " + Position.ToString("f4") + "\n";
		data += LevelHelper.GetTabLevel() + "SpawnOffset  " + SpawnOffset.ToString("f4") + "\n";

		LevelHelper.DecreaseTabLevel();
		data += LevelHelper.GetTabLevel() + "END\n";

		return data;
	}
}

public class TowerData
{
	public Vector3	Position;

	public string ExportData()
	{
		string data = "";

		data += LevelHelper.GetTabLevel() + "BEGIN TowerPillar\n";
		LevelHelper.IncreaseTabLevel();

		data += LevelHelper.GetTabLevel() + "Position  " + Position.ToString("f4") + "\n";

		LevelHelper.DecreaseTabLevel();
		data += LevelHelper.GetTabLevel() + "END\n";

		return data;
	}
}

public class CameraData
{
	public eCameraNodeType Type	= eCameraNodeType.PATH;
	public Vector3 Position		= Vector3.zero;
	public Quaternion Rotation	= Quaternion.identity;

	public string ExportData()
	{
		string data = "";

		data += LevelHelper.GetTabLevel() + "BEGIN Node\n";
		LevelHelper.IncreaseTabLevel();

		data += LevelHelper.GetTabLevel() + "Type      " + CameraHelper.NodeToInt(Type) + "\n";
		data += LevelHelper.GetTabLevel() + "Position  " + Position.ToString("f4") + "\n";
		data += LevelHelper.GetTabLevel() + "Rotation  " + Rotation.ToString("f4") + "\n";

		LevelHelper.DecreaseTabLevel();
		data += LevelHelper.GetTabLevel() + "END\n";

		return data;
	}
}

//    _      ________      ________ _        _____       _______       
//   | |    |  ____\ \    / /  ____| |      |  __ \   /\|__   __|/\    
//   | |    | |__   \ \  / /| |__  | |      | |  | | /  \  | |  /  \   
//   | |    |  __|   \ \/ / |  __| | |      | |  | |/ /\ \ | | / /\ \  
//   | |____| |____   \  /  | |____| |____  | |__| / ____ \| |/ ____ \ 
//   |______|______|   \/   |______|______| |_____/_/    \_\_/_/    \_\
//                                                                     

public class LevelData
{
	private List<LaneData> m_Lanes;
	private List<TeamData> m_Teams;
	private List<TowerData> m_Towers;
	private List<CameraData> m_CameraNodes;
	private float m_Height;

	// Constructor
	// >---------------------------------------------------
	public LevelData()
	{
		m_Lanes = new List<LaneData>();
		m_Teams = new List<TeamData>();
		m_Towers = new List<TowerData>();
		m_CameraNodes = new List<CameraData>();
	}

	// Debug
	// >---------------------------------------------------
	public void DEBUG_DrawLines()
	{
		foreach (LaneData lane in m_Lanes)
		{
			lane.DrawLanes();
		}
	}

	// Nodes
	// >---------------------------------------------------
	public void AddNode(eNodeType a_Type, Vector3 a_Position)
	{
		m_Lanes[m_Lanes.Count - 1].AddNode(a_Type, a_Position);
	}

	// Lanes
	// >---------------------------------------------------
	public void AddLane()
	{
		m_Lanes.Add(new LaneData(m_Height));
	}

	public LaneData GetLane(int a_ID)
	{
		if (a_ID < 0) { return null; }
		if (a_ID > m_Lanes.Count - 1) { return null; }

		return m_Lanes[a_ID];
	}

	public void SetPathHeight(float a_Height)
	{
		m_Height = a_Height;

		if (m_Lanes.Count > 0)
		{
			for (int i = 0; i < m_Lanes.Count; i++)
			{
				m_Lanes[i].SetHeight(m_Height);
			}
		}
	}

	public Vector3 GetRandomPointOnTrack()
	{
		int trackID = UnityEngine.Random.Range(0, m_Lanes.Count);

		return m_Lanes[trackID].GetRandomPointOnLane();
	}

	// Teams
	// >---------------------------------------------------
	public void AddTeam(TeamData a_Data)
	{
		m_Teams.Add(a_Data);
	}

	public TeamData GetTeam(int a_ID)
	{
		if (a_ID < 0) { return null; }
		if (a_ID > m_Teams.Count - 1) { return null; }

		return m_Teams[a_ID];
	}

	// Camera
	// >---------------------------------------------------
	public void AddCamera(CameraData a_Data)
	{
		m_CameraNodes.Add(a_Data);

		LevelHelper.m_GameManager.GetInGameCamera().AddCameraNode(a_Data);
	}

	// Export
	// >---------------------------------------------------
	public string ExportData()
	{
		string data = "";

		// Export Settings
		// >-----------------------------------------------
		data += "PathHeight  " + m_Height.ToString("f4") + "\n";

		data += "\n";

		// Export Base Data
		// >-----------------------------------------------
		data += "BEGIN Bases\n";
		LevelHelper.IncreaseTabLevel();

		for (int i = 0; i < m_Teams.Count; i++)
		{
			data += m_Teams[i].ExportData();
		}

		LevelHelper.DecreaseTabLevel();
		data += "END\n";

		data += "\n";

		// Export Camera Path Data
		// >-----------------------------------------------
		data += "BEGIN CameraPath\n";
		LevelHelper.IncreaseTabLevel();

		for (int i = 0; i < m_CameraNodes.Count; i++)
		{
			data += m_CameraNodes[i].ExportData();
		}

		LevelHelper.DecreaseTabLevel();
		data += "END\n";

		data += "\n";

		// Export Lane Data
		// >-----------------------------------------------
		data += "BEGIN Lanes\n";
		LevelHelper.IncreaseTabLevel();

		for (int i = 0; i < m_Lanes.Count; i++)
		{
			data += m_Lanes[i].ExportData();
		}

		LevelHelper.DecreaseTabLevel();
		data += "END\n";

		data += "\n";

		// Export Tower Data
		// >-----------------------------------------------
		data += "BEGIN Towers\n";
		LevelHelper.IncreaseTabLevel();

		for (int i = 0; i < m_Towers.Count; i++)
		{
			data += m_Towers[i].ExportData();
		}

		LevelHelper.DecreaseTabLevel();
		data += "END\n";

		data += "\n";

		return data;
	}

	public int LaneCount { get { return m_Lanes.Count; } }
	public int TeamCount { get { return m_Teams.Count; } }
	public int TowerCount { get { return m_Towers.Count; } }
	public float PathHeight { get { return m_Height; } }
}

//    _      ________      ________ _        _    _ ______ _      _____  ______ _____  
//   | |    |  ____\ \    / /  ____| |      | |  | |  ____| |    |  __ \|  ____|  __ \ 
//   | |    | |__   \ \  / /| |__  | |      | |__| | |__  | |    | |__) | |__  | |__) |
//   | |    |  __|   \ \/ / |  __| | |      |  __  |  __| | |    |  ___/|  __| |  _  / 
//   | |____| |____   \  /  | |____| |____  | |  | | |____| |____| |    | |____| | \ \ 
//   |______|______|   \/   |______|______| |_|  |_|______|______|_|    |______|_|  \_\
//                                                                                     

public static class LevelHelper
{
	private static int			m_CurrentTabLevel		= 0;
	private static string		m_FolderLocation		= "./Assets/Resources/";
	private static string		m_LevelLocation			= "Levels/";
	private static string		m_FileExtension			= ".txt";
	private static string		m_CurrentFileVersion	= "2";
	private static string		m_Nomenclature			= "# Miner Mayhem Level Data File\n# File Structure Designed By Adam Coulson";
	
	public static int			m_CurrentLine			= 0;
	public static string[]		m_FileData				= null;
	public static LevelData		m_LevelData				= null;

	public static GameManager	m_GameManager			= null;

	public static void SaveLevel(string a_LevelName, LevelData a_LevelData)
	{
#if !(UNITY_WEBPLAYER)
		string data = "";
		
		data += "# Version " + m_CurrentFileVersion + "\n\n";//#\n" + m_Nomenclature + "\n\n";
		data += a_LevelData.ExportData();

		// Check Folders Exsists
		if (!Directory.Exists(m_FolderLocation))
		{
			Directory.CreateDirectory(m_FolderLocation);
		}

		if (!Directory.Exists(m_FolderLocation + m_LevelLocation))
		{
			Directory.CreateDirectory(m_FolderLocation + m_LevelLocation);
		}

		File.WriteAllText(m_FolderLocation + m_LevelLocation + a_LevelName + m_FileExtension, data);
#endif
	}

	public static void SaveDataToFile(string a_LevelName, string a_Data)
	{
#if !(UNITY_WEBPLAYER)
		// Check Folders Exsists
		if (!Directory.Exists(m_FolderLocation))
		{
			Directory.CreateDirectory(m_FolderLocation);
		}

		if (!Directory.Exists(m_FolderLocation + m_LevelLocation))
		{
			Directory.CreateDirectory(m_FolderLocation + m_LevelLocation);
		}

		File.WriteAllText(m_FolderLocation + m_LevelLocation + a_LevelName + m_FileExtension, a_Data);
#endif
	}

	public static LevelData LoadLevel(string a_LevelName)
	{
		TextAsset levelFile = Resources.Load(m_LevelLocation + a_LevelName) as TextAsset;
		
		if (levelFile == null) { LevelData level = new LevelData(); level.AddLane(); return level; }

		m_LevelData = new LevelData();
		m_FileData = levelFile.text.Split("\n"[0]);

		// Check for Carrage Return At End of Line
		int lineCunt = 0;
		foreach (string line in m_FileData)
		{
			if ( line.Contains("\r") ) { m_FileData[lineCunt] = line.Substring(0, line.Length - 1); }
			lineCunt++;
		}

		LoaderSettings.Load();

		// Load Data
		while (m_CurrentLine < m_FileData.Length)
		{
			if (!IsValidLine()) { NextLine(); continue; }

			if (m_FileData[m_CurrentLine].Contains("BEGIN"))
			{
				string value = m_FileData[m_CurrentLine].Substring((GetTabLevel() + "BEGIN ").Length);

				switch (value)
				{
					case "Bases": NextLine(); LoaderBase.Load(); break;
					case "CameraPath": NextLine(); LoaderCameraPath.Load(); break;
					case "Lanes": NextLine(); LoaderLane.Load(); break;
					case "Towers": NextLine(); LoaderTower.Load(); break;
				}
			}

			NextLine();
		}

		return m_LevelData;
	}

	public static void IncreaseTabLevel() { m_CurrentTabLevel++; }
	public static void DecreaseTabLevel() { m_CurrentTabLevel--; if (m_CurrentTabLevel < 0) { m_CurrentTabLevel = 0; } }

	public static string GetTabLevel()
	{
		string data = "";

		for (int i = 0; i < m_CurrentTabLevel; i++ )
		{
			data += "  ";
		}

		return data;
	}

	public static void NextLine()
	{
		m_CurrentLine++;
	}

	public static bool IsValidLine()
	{
		if (m_FileData[m_CurrentLine].Length <= 0) { return false; }
		if (m_FileData[m_CurrentLine][0] == '#') { return false; }

		return true;
	}

	public static void Reset()
	{
		m_CurrentLine = 0;
		m_FileData = null;
		m_LevelData = null;
		m_CurrentTabLevel = 0;
	}
}*/

////////////////////////////////////////////////////////////////////////////////////////////////////////////
//########################################################################################################//
//# 																									 #//
//#					CODE GRAVEYARD, ANYTHING BEYOND THIS POINT SHOULD BE DISREGARDED					 #//
//# 																									 #//
//########################################################################################################//
////////////////////////////////////////////////////////////////////////////////////////////////////////////
///------------------------------------------------------------------------------------------------------///

/*public class MapEditor
{
    // Public Variables
    // ---------------------------------------------------

	public MapManager	mapManager				= null;
	public GameManager	gameManager				= null;

	public float		activationDistance		= 3.0F;

    // Private Variables
    // ---------------------------------------------------

	private GUIText		m_DebugOutput			= null;
	private Camera		m_Camera				= null;
	private float		m_CameraMoveSpeed		= 16.7F;
	private float		m_CameraScaleSpeed		= 10.0F;
	private float		m_CameraScaleMax		= 50.0F;
	private float		m_CameraScaleMin		= 5.0F;
	private float		m_FrameMoveSpeed		= 0.0F;

	private eNodeType	m_CurrentPlaceType		= eNodeType.EMPTY;
	private eRotation	m_CurrentPlaceRotation	= eRotation.UP;

	private bool		m_PlaceNode				= false;
	private int			m_NodeManipulatorID		= 0;
	
	public Vector2		m_MousePos;
	public Vector3		m_MousePosLast;
	public Vector3		m_MouseWorldPos;

    // Functions
    // ---------------------------------------------------

	public void Initialise()
	{
		m_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();

		if (m_Camera)
		{
			m_Camera.orthographic = true;

			m_Camera.transform.rotation = Quaternion.AngleAxis(90, new Vector3(1, 0, 0));
			m_Camera.transform.position = new Vector3(0, 50, 18);
			m_Camera.orthographicSize = 30.0F;
		}

		// Create Debug Output
		GameObject obj = new GameObject("MapEditorDebugger");

		obj.transform.position = new Vector3(0.001F, 1.0F, 0.0F);
		
		m_DebugOutput = (GUIText)obj.AddComponent(typeof(GUIText));
		m_DebugOutput.font = Resources.Load("CONSOLA", typeof(Font)) as Font;
	}

	public void Update()
	{
		// Camera Controller
		if (m_Camera)
		{
			// Calculate Camera Speed Based on Scale for Current Frame
			m_FrameMoveSpeed = m_CameraMoveSpeed * (m_Camera.orthographicSize / 30.0F);

			// Zoom In
			if (Input.GetKey(KeyCode.Q))
			{
				m_Camera.orthographicSize -= m_CameraScaleSpeed * Time.deltaTime;
				if (m_Camera.orthographicSize < m_CameraScaleMin) { m_Camera.orthographicSize = m_CameraScaleMin; }
			}

			// Zoom Out
			if (Input.GetKey(KeyCode.E))
			{
				m_Camera.orthographicSize += m_CameraScaleSpeed * Time.deltaTime;
				if (m_Camera.orthographicSize > m_CameraScaleMax) { m_Camera.orthographicSize = m_CameraScaleMax; }
			}
		}

		// Update Mouse Position
		m_MousePos = GetMapCoordsFromMouse();
		m_MouseWorldPos = new Vector3(m_MousePos.x, 0.0F, m_MousePos.y);

		// Draw Activation Distance
		int resolution = 36;
		float rad = 6.28318F / resolution;

		for (int i = 0; i < resolution; i++)
		{
			Debug.DrawLine(new Vector3(Mathf.Sin(i * rad) * activationDistance, 5.0F, Mathf.Cos(i * rad) * activationDistance) + m_MouseWorldPos, new Vector3(Mathf.Sin((i + 1) * rad) * activationDistance, 5.0F, Mathf.Cos((i + 1) * rad) * activationDistance) + m_MouseWorldPos);
		}

		// TODO: FIX THIS BASS
		for (int i = 0; i < mapManager.Level.LaneCount; i++)
		{
			for (int j = 0; j < mapManager.Level.GetLane(i).NodeCount; j++)
			{
				NodeData node = mapManager.Level.GetLane(i).GetNode(j);

				Vector3 pos = node.Position;
				float length = Vector3.Magnitude(pos - m_MouseWorldPos);

				m_DebugOutput.text += "Length: " + length.ToString("f4");

				Vector2 nodePos = new Vector2(pos.x, pos.z);

				if (Vector2.Distance(nodePos, m_MousePos) < activationDistance && !node.IsAttachedToNodeManipulator)
				{
					GameObject obj = new GameObject("Node Manipulator " + m_NodeManipulatorID.ToString());
					m_NodeManipulatorID++;

					obj.transform.position = node.Position;

					NodeManipulator nodeManipulator = obj.AddComponent(typeof(NodeManipulator)) as NodeManipulator;

					nodeManipulator.mapEditor = this;
					nodeManipulator.node = node;

					node.IsAttachedToNodeManipulator = true;

					Debug.Log("Added");
				}
			}
		}

		// Mouse Inputs
		// >-----------------------------------------------

		// Place Tile
		if (Input.GetMouseButtonDown(0))
		{
			if (m_PlaceNode)
			{
				mapManager.Level.AddNode(m_CurrentPlaceType, m_MouseWorldPos);
			}
		}

		// Move Map Around
		if (Input.GetMouseButtonDown(2))
		{
			m_MousePosLast = Input.mousePosition;
		}

		if (Input.GetMouseButton(2))
		{
			Vector3 drag = m_MousePosLast - Input.mousePosition;

			m_Camera.transform.position += new Vector3(drag.x, 0.0F, drag.y) / m_FrameMoveSpeed;

			m_MousePosLast = Input.mousePosition;
		}

		// Keyboard Inputs
		// >-----------------------------------------------

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			m_PlaceNode = !m_PlaceNode;
		}

		// Save Level
		if (Input.GetKeyDown(KeyCode.K))
		{
			//LevelData level = new LevelData(5, 20);
			LevelHelper.SaveLevel("Level Main", mapManager.Level);
		}

		// On Screen Output
		// >-----------------------------------------------

		// Output Debug Info
		m_DebugOutput.text = "Mouse Map Pos: " + m_MouseWorldPos.ToString("f4") + "\n";
		m_DebugOutput.text += "Placing Node: " + m_PlaceNode.ToString() + "\n";
		//m_DebugOutput.text += "Current Placing: Tile(Type: " + m_CurrentPlaceType.ToString() + " | Rot: " + m_CurrentPlaceRotation.ToString() + ")\n";
	}

	private Vector2 GetMapCoordsFromMouse()
	{
		Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);

		return new Vector2(ray.origin.x, ray.origin.z);
	}
}*/

//    _   _  ____  _____  ______   __  __          _   _ _____ _____  _    _ _            _______ ____  _____  
//   | \ | |/ __ \|  __ \|  ____| |  \/  |   /\   | \ | |_   _|  __ \| |  | | |        /\|__   __/ __ \|  __ \ 
//   |  \| | |  | | |  | | |__    | \  / |  /  \  |  \| | | | | |__) | |  | | |       /  \  | | | |  | | |__) |
//   | . ` | |  | | |  | |  __|   | |\/| | / /\ \ | . ` | | | |  ___/| |  | | |      / /\ \ | | | |  | |  _  / 
//   | |\  | |__| | |__| | |____  | |  | |/ ____ \| |\  |_| |_| |    | |__| | |____ / ____ \| | | |__| | | \ \ 
//   |_| \_|\____/|_____/|______| |_|  |_/_/    \_\_| \_|_____|_|     \____/|______/_/    \_\_|  \____/|_|  \_\
//                                                                                                             

// TODO: Add Physical Triggers to GameObject that Display Directions and Can Be Dragged to Move
// TODO: Hold Shift to Snap to .1
// TODO: Click Node to Display Node Data
// TODO: Make all Nodes Created have a Node Manipulator

/*public class NodeManipulator : MonoBehaviour
{
	public MapEditor mapEditor = null;
	public NodeData node = null;

	void Start()
	{
	}

	void Update()
	{
		if (!node.IsAttachedToNodeManipulator) { node.IsAttachedToNodeManipulator = true; }

		Vector2 nodePos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);

		if (Vector2.Distance(mapEditor.m_MousePos, nodePos) > mapEditor.activationDistance)
		{
			node.IsAttachedToNodeManipulator = false;
			Destroy(gameObject);
			Debug.Log("Destoryed");
		}

		if (node != null)
		{
			float MoveSpeed = 0.8F * Time.deltaTime;

			if (Input.GetKey(KeyCode.UpArrow)) { node.Position.z += MoveSpeed; }
			if (Input.GetKey(KeyCode.DownArrow)) { node.Position.z -= MoveSpeed; }
			if (Input.GetKey(KeyCode.LeftArrow)) { node.Position.x -= MoveSpeed; }
			if (Input.GetKey(KeyCode.RightArrow)) { node.Position.x += MoveSpeed; }

			gameObject.transform.position = node.Position;
		}

		Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(1.0F, 0.0F, 0.0F), Color.red);
		Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(0.0F, 0.0F, 1.0F), Color.blue);
	}
}*/

///------------------------------------------------------------------------------------------------------///

//Debug.Log(value);

/*bool IsEnd = false;

int location = value.IndexOf("  ");

Debug.Log(value.Substring(0, location));

if (m_FileData[m_CurrentLine].Contains("END")) { IsEnd = true; }*/

///------------------------------------------------------------------------------------------------------///

/*foreach ( string line in strings )
{
	// Line Skip Checks
	if (line.Length <= 0)	{ continue; }
	if (line[0] == '#')		{ continue; }

	if (!levelSettings.IsInitialised())
	{
		LoadLevelSettings(levelSettings);
	}
	else
	{
		if (levelData == null)
		{
			levelData = new LevelData(levelSettings.Width, levelSettings.Height);
		} 
	}

	// Print Line
	Debug.Log(line);
}*/

///------------------------------------------------------------------------------------------------------///

//float x = ray.origin.x;
//float y = ray.origin.z;

//x += mapManager.MapScaleX / 2.0F;
//y += mapManager.MapScaleY / 2.0F;

//if (x < 0) { x = 0; }
//if (y < 0) { y = 0; }

///------------------------------------------------------------------------------------------------------///

//TileData tile = mapManager.Level.GetTile((int)pos.x, (int)pos.y);
//debugOutput.text += "Current Hovering: Tile(Type: " + tile.Type.ToString() + " | Rot: " + tile.Rotation.ToString() + ")\n";

///------------------------------------------------------------------------------------------------------///