////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	                                                                                                      //
//	Created:		13/07/2014 - 09:25	|	Thomas Faircloth                                              //
//	Modified:		13/07/2014 - 09:25	|	Thomas Faircloth                                              //
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

public enum eItemType
{
	ENTITY,
	RESOURCE,
	BOULDER,
	PROJECTILE,
	TOWERPILLAR,
	NONE
}

public enum eEntityType
{
	TEAM,
	UNIT,
	TOWER,
	NONE
};

public enum eUnitType
{
	MINER,
	RANGED,
	HEAVY,
	NONE
};

public enum eTowerType
{
	ARBALEST,
	CATAPULT,
	TESLA,
	NONE
};

public class ItemManager : MonoBehaviour
{
	// Public Variables
	// ---------------------------------------------------

	// Entity Prefabs
	public GameObject minerPrefab = null;
	public GameObject rangedPrefab = null;
	public GameObject heavyPrefab = null;

	// Tower Prefabs
	public GameObject arbalestPrefab = null;
	public GameObject catapultPrefab = null;

	// Item Prefabs
	public GameObject m_ProjectilesPrefab = null;

	// Private Variables
	// ---------------------------------------------------

	// Game Manager
	private GameManager m_GameManager = null;
	private bool m_IsGameManagerSet = false;

	// Unit ID
	private int m_NextItemID = 0;

	// List of Units in the Scene
	private List<GameObject> m_Items;

	void Start()
	{
		//m_Items = new List<GameObject>();
	}

    public void Initialise()
    {
        m_Items = new List<GameObject>();
    }

	// Update is called once per frame
	void Update()
	{
		// Destory Dead Entities
		for (int i = m_Items.Count - 1; i >= 0; i--)
		{
			Item a_Item = m_Items[i].GetComponent<Item>();

			if (a_Item)
			{
				if (a_Item.IsRemovable)
				{
					Destroy(m_Items[i]);
					m_Items.RemoveAt(i);
				}
			}
		}
	}

	public GameManager GetGameManager()
	{
		if (m_GameManager)
		{
			return m_GameManager;
		}

		return m_GameManager;
	}

	public void SetGameManager(GameManager a_Manager)
	{
		if (!m_IsGameManagerSet)
		{
			m_GameManager = a_Manager;

			m_IsGameManagerSet = true;
		}
	}

	public void Destory()
	{
		while (m_Items.Count > 0) { Destroy(m_Items[0]); m_Items.RemoveAt(0); }

		m_Items.Clear();
	}

	public void DestroyItem(int a_ItemID)
	{
		for (int i = 0; i < m_Items.Count; i++)
		{
			Item a_Item = m_Items[i].GetComponent<Item>();

			if (a_Item)
			{
				if (a_Item.ItemID == a_ItemID)
				{
					Destroy(m_Items[i]);
					m_Items.RemoveAt(i);
				}
			}
		}
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
	public GameObject minerEnemyPrefab = null;
	public GameObject rangedEnemyPrefab = null;
	public GameObject heavyEnemyPrefab = null;
	public GameObject m_ResourcePrefab = null;
	public GameObject m_BoulderPrefab = null;
	public GameObject m_TowerPillerPrefab = null;



    public GameObject CreateResource( Vector3 a_Position, Team a_TeamOne, Team a_TeamTwo )
    {
        GameObject a_Resource;
        a_Resource = Instantiate(m_ResourcePrefab) as GameObject;

        Resource a_Temp = a_Resource.GetComponent<Resource>();
        a_Temp.SetItemID(m_NextItemID++);
        a_Temp.SetItemManagerHandle(this);
        a_Temp.Initialise();
        a_Temp.Position = a_Position;
        a_Temp.SetTeamHandles(a_TeamOne, a_TeamTwo);

        m_Items.Add(a_Resource);

        return a_Resource;
    }

    public GameObject CreateTowerPillar(Vector3 a_Position, Team a_TeamOne, Team a_TeamTwo, Resource a_NodeOne, Resource a_NodeTwo)
    {
        GameObject a_TowerPillar;
        a_TowerPillar = Instantiate(m_TowerPillerPrefab) as GameObject;

        TowerPillar a_Temp = a_TowerPillar.GetComponent<TowerPillar>();
        a_Temp.SetItemID(m_NextItemID++);
        a_Temp.SetItemManagerHandle(this);
        a_Temp.Initialise();
        a_Temp.Position = a_Position;
        a_Temp.SetTeamHandles(a_TeamOne, a_TeamTwo);
        a_Temp.SetResourceHandle(a_NodeOne, a_NodeTwo);

        m_Items.Add(a_TowerPillar);

        return a_TowerPillar;
    }

	public GameObject CreateUnit(eUnitType a_Type, eTeam a_TeamType)
	{
		GameObject unit;

		if (a_TeamType == eTeam.PLAYER)
		{
			switch (a_Type)
			{
				default:
				case eUnitType.MINER: unit = Instantiate(minerPrefab) as GameObject; break;
				case eUnitType.RANGED: unit = Instantiate(rangedPrefab) as GameObject; break;
				case eUnitType.HEAVY: unit = Instantiate(heavyPrefab) as GameObject; break;
			}
		}
		else
		{
			switch (a_Type)
			{
				default:
				case eUnitType.MINER: unit = Instantiate(minerEnemyPrefab) as GameObject; break;
				case eUnitType.RANGED: unit = Instantiate(rangedEnemyPrefab) as GameObject; break;
				case eUnitType.HEAVY: unit = Instantiate(heavyEnemyPrefab) as GameObject; break;
			}
		}

		Unit temp = unit.GetComponent<Unit>();

		if (temp)
		{
			//temp.SetItemType(eItemType.ENTITY);
			//temp.m_EntityType = eEntityType.UNIT;
			//temp.m_UnitType = a_Type;
			temp.SetItemID(m_NextItemID++);
			temp.SetItemManagerHandle(this);
			temp.Initialise();
		}

		m_Items.Add(unit);

		return unit;
	}

	public GameObject CreateTower(eTowerType a_Type, int a_TeamID)
	{
		GameObject tower;

		switch (a_Type)
		{
			default:
			case eTowerType.ARBALEST: tower = Instantiate(arbalestPrefab) as GameObject; break;
			case eTowerType.CATAPULT: tower = Instantiate(catapultPrefab) as GameObject; break;
		}

		tower.AddComponent<Button>();

		Tower temp = tower.GetComponent<Tower>();

		if (temp)
		{
			//temp.m_ItemType = eItemType.ENTITY;
			//temp.m_EntityType = eEntityType.TOWER;
			//temp.m_towerType = a_Type;
			temp.SetItemID(m_NextItemID++);
			temp.SetItemManagerHandle(this);
			temp.SetTeam(m_GameManager.GetTeam(a_TeamID));
			temp.Initialise();
		}

		m_Items.Add(tower);

		return tower;
	}

	public GameObject CreateProjectile(Team a_Team)
	{
		GameObject projectile = Instantiate(m_ProjectilesPrefab) as GameObject;

		Projectile temp = projectile.GetComponent<Projectile>();

		if (temp)
		{
			//temp.m_ItemType = eItemType.PROJECTILE;
			temp.SetItemID(m_NextItemID++);
			temp.SetItemManagerHandle(this);
			temp.m_Team = a_Team;
			temp.Initialise();
		}

		m_Items.Add(projectile);

		return projectile;
	}
*/