////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	                                                                                                      //
//	Created:		11/07/2014 - 13:41	|	Adam Coulson                                                  //
//	Modified:		26/07/2014 - 0722	|	Tom Faircloth                                                 //
//	                                                                                                      //
//	Description:	                                                                                      //
//	                                                                                                      //
////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	                                                                                                      //
//	TODO: Disclaimer                                                                                      //
//	                                                                                                      //
////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Defines for Enums, Structs and Classes
// ---------------------------------------------------

/// <summary>
/// The Main Container for Everything About the Game.
/// Every Game Object Should have Access to This Class.
/// This Class has Access to All Other Game Components.
/// </summary>
public class GameManager : MonoBehaviour
{
	// Public Variables
	// ---------------------------------------------------
	
	
    // Game Mode
	//public eGameMode			gameMode			= eGameMode.EVE; // IRRELEVENT
	
    // Teams
	public GameObject			playerTeamPrefab	= null;
	public GameObject			aiTeamPrefab		= null;

	public GameObject			gameOverScreenNode	= null;
	
	public bool					m_DebuggerEnabled	= false;

	// Private Variables
	// ---------------------------------------------------
	
	private bool				m_GameInitialised	= false;
	private float				m_GameTime			= 0.0F;

	//private LevelData			m_Level				= null;

	private List<GameObject>	m_Teams;
    private List<GameObject>	m_ResourceNodes;
	
	// Other Managers, Attached to This, In This Order (Probably)
	private ItemManager			itemManager			= null;
	private InGameCamera		inGameCamera		= null;

	// On Screen Debugger Output
	private GUIText				m_DebugOutput		= null;
	private List<string>		m_Strings;
	
	public bool				m_GameOver			= false;
	public float cameraSpeed = 50.0F;

	// Unity Functions
	// ---------------------------------------------------

	// Use this for initialization
	void Start ()
	{
		// Remove Map Editor If it Exsists (Comes Back When Return to Edit Mode)
		GameObject mapEditor = GameObject.Find("Map Editor");
		if (mapEditor != null) { Destroy(mapEditor); }

		m_Teams = new List<GameObject>();
        m_ResourceNodes = new List<GameObject>();
		m_Strings = new List<string>();

		//LevelHelper.m_GameManager = this;

		// Check for Sub Components and Log If Dont Exsit
		// ---------------------------------------------------
		itemManager = gameObject.GetComponent<ItemManager>();
        if (itemManager == null) { Debug.LogError("The Game Manager Needs a ItemManager Attached!"); Debug.Break(); }
        else { itemManager.Initialise(); }
		
		inGameCamera = gameObject.GetComponent<InGameCamera>();
		if (inGameCamera == null) { Debug.LogError("The Game Manager Needs an InGameCamera Attached!"); Debug.Break(); }

		// Set Sub Componets Game Manager to This
		// ---------------------------------------------------
		itemManager.SetGameManager( this );
		//inGameCamera.SetGameManager( this );

		// Create Debug Output
		// ---------------------------------------------------
		GameObject obj = new GameObject("GameManagerDebugger");

		obj.transform.position = new Vector3(0.001F, 0.003F, 0.0F);

		if (m_DebuggerEnabled)
		{
			m_DebugOutput = (GUIText)obj.AddComponent(typeof(GUIText));
			m_DebugOutput.font = Resources.Load("CONSOLA", typeof(Font)) as Font;
			m_DebugOutput.anchor = TextAnchor.LowerLeft;
			m_DebugOutput.text = "";
		}

		//team1.SetGameManager( this );
		//team2.SetGameManager( this );

		Application.CancelQuit();

		InitialiseGame();
	}
	
	// Update is called once per frame
	void Update ()
	{
		m_GameTime += Time.deltaTime;

		if (m_DebuggerEnabled)
		{
#if UNITY_EDITOR
			// Reset On Screen Debugger Output
		
			m_DebugOutput.text = "";
		

			foreach (string line in m_Strings)
			{
				m_DebugOutput.text += line;
			}

			m_Strings.Clear();
#endif
		}

		if (Input.GetKeyDown(KeyCode.F8)) { RestartGame(); }
	}
	
	// Custom Functions
	// ---------------------------------------------------
	
	/// <summary>
	/// Initialises The Game and Its Section Managers.
	/// </summary>
	private void InitialiseGame()
	{
		// Check Game Does not Exsist
		if ( m_GameInitialised )	{ DestroyGame(); }
		
		// Initialise Map Manager
		// >---------------------------------------------------

		// Load Level
		//mapManager.LoadLevel();

		// Get Level From Map Manager
		//m_Level = mapManager.Level;

		// Set Game Initialise to True
		m_GameInitialised = true;
		
		m_GameTime = 0.0F;
	}
	
	/// <summary>
	/// Cleans up the Current Game and Its Section Managers.
	/// </summary>
	private void DestroyGame()
	{
		// Check Game Does Exsist
		if (!m_GameInitialised) { return; }

		// Destroy Teams
		// >---------------------------------------------------

		while (m_Teams.Count > 0)
		{
			Destroy(m_Teams[0]);
			m_Teams.RemoveAt(0);
		}

		m_Teams.Clear();

		// Destroy Unit Manager
		itemManager.Destory();

		// Destroy Pickup Manager
		//pickupManager.DestoryPickupManager();

		// Destroy Map Manager
		//mapManager.Shutdown();

		// Destroy In Game Camera
		//inGameCamera.Shutdown();

		// Set Game Initialise to True
		m_GameInitialised = false;
	}
	
	public void RestartGame()
	{
		DestroyGame();
		InitialiseGame();
	}

	/// <summary>
	/// Writes Data out to the On Screen Debugger Output.
	/// </summary>
	/// <param name="a_Data">A Preformatted String for Output</param>
	public void WriteDebug(string a_Data)
	{
		if (m_DebuggerEnabled)
		{
			m_Strings.Add(a_Data);
		}
	}

	/// <summary>
	/// Writes Data out to the On Screen Debugger Output.
	/// Creates a New Line After Output of String.
	/// </summary>
	/// <param name="a_Data"></param>
	public void WriteDebugLine(string a_Data)
	{
		if (m_DebuggerEnabled)
		{
			m_Strings.Add(a_Data + "\n");
		}
	}

	// Properties
	// ---------------------------------------------------

	// Properties for Game Section Managers
	public ItemManager GetItemManager() { return itemManager; }
	public InGameCamera GetInGameCamera() { return inGameCamera; }

	// Properties for Avalible Teams
	//public Team GetTeam(int a_ID) { return m_Teams[a_ID].GetComponent<Team>(); }

	public int TeamCount { get { return m_Teams.Count; } }
	//public LevelData Level { get { return m_Level; } }
	public List<GameObject> ResourceNodes { get { return m_ResourceNodes; } }
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////
//########################################################################################################//
//# 																									 #//
//#					CODE GRAVEYARD, ANYTHING BEYOND THIS POINT SHOULD BE DISREGARDED					 #//
//# 																									 #//
//########################################################################################################//
////////////////////////////////////////////////////////////////////////////////////////////////////////////
///------------------------------------------------------------------------------------------------------///

// Teams
/*if (gameMode == eGameMode.PVP)
		{
			// TODO: Multiplayer
			// Create Player vs Player
			
			// Init Player 1
			team1.InitialiseTeam( eTeam.PLAYER );
			
			// Init Player 2
			team2.InitialiseTeam( eTeam.PLAYER );
		}
		else if (gameMode == eGameMode.PVE)
		{
			// Create Player vs AI
			
			// Init Player
			team1.InitialiseTeam( eTeam.PLAYER );
			
			// Init AI
			team2.InitialiseTeam( eTeam.AI );
		}
		else if (gameMode == eGameMode.EVE)
		{
			// Create AI vs AI
			
			// Init AI 1
			team1.InitialiseTeam( eTeam.AI );
			
			// Init AI 2
			team2.InitialiseTeam( eTeam.AI );
		}*/

///------------------------------------------------------------------------------------------------------///

// Set Team Types
/*eTeam team1Type = eTeam.AI;
eTeam team2Type = eTeam.AI;
		
switch ( gameMode )
{
case eGameMode.EVE:	team1Type = eTeam.AI;		team2Type = eTeam.AI; 		break;
case eGameMode.PVE:	team1Type = eTeam.PLAYER;	team2Type = eTeam.AI;		break;
case eGameMode.PVP:	team1Type = eTeam.PLAYER;	team2Type = eTeam.PLAYER;	break;
}
		
// Init Teams
team1.InitialiseTeam( 0, team1Type );
team2.InitialiseTeam( 1, team2Type );*/

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
/// <summary>
/// Used to Determine What Types of Teams Are Generated.
/// </summary>
public enum eGameMode
{
	PVE,
	EVE,
	PVP
};

/// <summary>
/// Team Type Passed Onto Teams from the GameMode.
/// Determines Team Controller Type.
/// </summary>
public enum eTeam
{
	PLAYER,
	AI
};

        // Create Resource Nodes
        // - CURRENTLY MANUALLY SETTING THE TEAMS
        Team a_TeamZero = m_Teams[0].GetComponent<Team>();
        Team a_TeamOne = m_Teams[1].GetComponent<Team>();

        //FROM LEFT TO RIGHT OF GAME SCENE
        GameObject a_ResourceOne = itemManager.CreateResource(new Vector3(-3.164f, 3.755f, -13.729f), a_TeamZero, a_TeamOne);
        Resource a_ResourceHandleOne = a_ResourceOne.GetComponent<Resource>();

        GameObject a_ResourceTwo = itemManager.CreateResource(new Vector3(-8.953f, 3.755f, -0.345f), a_TeamZero, a_TeamOne);
        Resource a_ResourceHandleTwo = a_ResourceTwo.GetComponent<Resource>();

        GameObject a_ResourceThree = itemManager.CreateResource(new Vector3(6.930f, 3.755f, 11.300f), a_TeamZero, a_TeamOne);
        Resource a_ResourceHandleThree = a_ResourceThree.GetComponent<Resource>();

        GameObject a_ResourceFour = itemManager.CreateResource(new Vector3(-2.600f, 3.755f, 20.800f), a_TeamZero, a_TeamOne);
        Resource a_ResourceHandleFour = a_ResourceFour.GetComponent<Resource>();

        GameObject a_ResourceFive = itemManager.CreateResource(new Vector3(-0.300f, 3.755f, 30.240f), a_TeamZero, a_TeamOne);
        Resource a_ResourceHandleFive = a_ResourceFive.GetComponent<Resource>();

        //FROM LEFT TO RIGHT OF GAME SCENE
        m_ResourceNodes.Add(itemManager.CreateTowerPillar(new Vector3(4.206f, 3.702f, -19.375f), a_TeamZero, a_TeamOne, a_ResourceHandleOne, a_ResourceHandleOne));
        m_ResourceNodes.Add(itemManager.CreateTowerPillar(new Vector3(0.311f, 3.702f, -19.375f), a_TeamZero, a_TeamOne, a_ResourceHandleOne, a_ResourceHandleOne));

        m_ResourceNodes.Add(itemManager.CreateTowerPillar(new Vector3(-7.046f, 3.702f, -11.283f), a_TeamZero, a_TeamOne, a_ResourceHandleOne, a_ResourceHandleTwo));
        m_ResourceNodes.Add(itemManager.CreateTowerPillar(new Vector3(-10.933f, 3.702f, -11.283f), a_TeamZero, a_TeamOne, a_ResourceHandleOne, a_ResourceHandleTwo));

        m_ResourceNodes.Add(itemManager.CreateTowerPillar(new Vector3(-7.046f, 3.702f, 8.654f), a_TeamZero, a_TeamOne, a_ResourceHandleTwo, a_ResourceHandleThree));
        m_ResourceNodes.Add(itemManager.CreateTowerPillar(new Vector3(-10.933f, 3.702f, 8.654f), a_TeamZero, a_TeamOne, a_ResourceHandleTwo, a_ResourceHandleThree));

        m_ResourceNodes.Add(itemManager.CreateTowerPillar(new Vector3(4.700f, 3.702f, 18.810f), a_TeamZero, a_TeamOne, a_ResourceHandleThree, a_ResourceHandleFour));
        m_ResourceNodes.Add(itemManager.CreateTowerPillar(new Vector3(4.700f, 3.702f, 22.600f), a_TeamZero, a_TeamOne, a_ResourceHandleThree, a_ResourceHandleFour));

        m_ResourceNodes.Add(itemManager.CreateTowerPillar(new Vector3(-10.933f, 3.702f, 26.366f), a_TeamZero, a_TeamOne, a_ResourceHandleFour, a_ResourceHandleFive));
        m_ResourceNodes.Add(itemManager.CreateTowerPillar(new Vector3(-7.046f, 3.702f, 26.366f), a_TeamZero, a_TeamOne, a_ResourceHandleFour, a_ResourceHandleFive));

        m_ResourceNodes.Add(itemManager.CreateTowerPillar(new Vector3(0.268f, 3.702f, 37.756f), a_TeamZero, a_TeamOne, a_ResourceHandleFive, a_ResourceHandleFive));
        m_ResourceNodes.Add(itemManager.CreateTowerPillar(new Vector3(4.080f, 3.702f, 37.756f), a_TeamZero, a_TeamOne, a_ResourceHandleFive, a_ResourceHandleFive));

	private PickupManager		pickupManager		= null;
	private CameraHandler		cameraHandler		= null;
	private InputManager		inputManager		= null;
	private MapManager			mapManager			= null;

		mapManager = gameObject.GetComponent<MapManager>();
		if (mapManager == null) { Debug.LogError("The Game Manager Needs a MapManager Attached!"); Debug.Break(); }

		pickupManager = gameObject.GetComponent<PickupManager>();
		if (pickupManager == null) { Debug.LogError("The Game Manager Needs a PickupManager Attached!"); Debug.Break(); }

		cameraHandler = gameObject.GetComponent<CameraHandler>();
		if (cameraHandler == null) { Debug.LogError("The Game Manager Needs a CameraHandler Attached!"); Debug.Break(); }

		inputManager = gameObject.GetComponent<InputManager>();
		if (inputManager == null) { Debug.LogError("The Game Manager Needs an InputManager Attached!"); Debug.Break(); }

		mapManager.SetGameManager( this );
		pickupManager.SetGameManager( this );
		cameraHandler.SetGameManager( this );
		inputManager.SetGameManager( this );
		
			m_DebugOutput.text += "Camera:\n";
			m_DebugOutput.text += "  Position: " + cameraHandler.RequestCamera().transform.position.ToString("f4") + "\n";
			m_DebugOutput.text += "  Rotation: " + cameraHandler.RequestCamera().transform.rotation.ToString("f4") + (m_Strings.Count > 0 ? "\n" : "");

		if (!mapManager.mapEditorModeEnabled)
		{
			foreach (GameObject teamObj in m_Teams)
			{
				Team team = teamObj.GetComponent<Team>();

				if (team.Health <= 0)
				{
					m_GameOver = true;
					break;
				}
			}

			if (m_GameOver)
			{
				inGameCamera.disable = true;
				cameraHandler.RequestCamera().transform.position = Vector3.MoveTowards(cameraHandler.RequestCamera().transform.position, gameOverScreenNode.transform.position, Time.deltaTime * cameraSpeed);
				cameraHandler.RequestCamera().transform.rotation = Quaternion.RotateTowards(cameraHandler.RequestCamera().transform.rotation, gameOverScreenNode.transform.rotation, Time.deltaTime * cameraSpeed);
			}
		}

		// Create Teams
		// >---------------------------------------------------

		// Create Teams
		for (int i = 0; i < m_Level.TeamCount; i++)
		{
			GameObject type = aiTeamPrefab;
			TeamData data = m_Level.GetTeam(i);

			if (data.Type == eTeam.PLAYER) { type = playerTeamPrefab; }

			GameObject teamObj = Instantiate(type, data.Position, Quaternion.identity) as GameObject;

			m_Teams.Add(teamObj);

			if (!mapManager.mapEditorModeEnabled)
			{
				Team team = teamObj.GetComponent<Team>();

				team.v_Health = data.MaxHealth;
				team.SetItemManagerHandle(itemManager);
				team.SetUnitSpawnOffset(data.SpawnOffset);
				
				team.Initialise();
				team.InitialiseTeam(i, data.Type);

				team.ResourceController.AddGold(data.Gold);
				team.ResourceController.AddExperience(data.Experience);
			}
			else
			{
				Destroy(teamObj.GetComponent("Team"));
			}
		}

		// Initialise AI Teams
		if (!mapManager.mapEditorModeEnabled)
		{
			for (int i = 0; i < m_Teams.Count; i++)
			{
				if (m_Teams[i].GetComponent<Team>().TeamType == eTeam.AI)
				{
					m_Teams[i].GetComponent<Team>().InitialiseAIController();
				}
			}
		}
	public MapManager GetMapManager() { return mapManager; }
	public PickupManager GetPickupManager() { return pickupManager; }
	public CameraHandler GetCameraHandler() { return cameraHandler; }
	public InputManager GetInputManager() { return inputManager; }*/