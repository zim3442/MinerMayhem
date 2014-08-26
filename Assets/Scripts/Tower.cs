////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	                                                                                                      //
//	Created:		11/07/2014 - 13:41	|	Adam Coulson                                                  //
//	Modified:		18/08/2014 - 15.51	|	Ranjit Singh                                                  //
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

// Defines for Enums, Structs and Classes
// ---------------------------------------------------
/*
#if (UNITY_EDITOR)
using UnityEditor;

[CustomEditor(typeof(Tower))]
[CanEditMultipleObjects]
public class TowerEditor : EntityEditor
{
	bool m_TowerFoldout = true;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (EditorApplication.isPlaying)
		{
			m_TowerFoldout = EditorGUILayout.Foldout(m_TowerFoldout, "Tower");

			if (m_TowerFoldout)
			{
				Tower myTarget = (Tower)target;

				EditorGUILayout.LabelField("Tower Type", myTarget.m_towerType.ToString());

				if (GUI.changed) { EditorUtility.SetDirty(myTarget); }
			}
		}
	}
}
#endif

public class Tower : Entity
{
	// Public/Private Variables
	// ---------------------------------------------------

	public GameObject m_Projectile;

	public TowerPillar m_TowerPillar = null;

	public eTowerType m_towerType = eTowerType.NONE;

	protected float m_fFireTimer = 0.0f;
	

	// Unity Functions
	// ---------------------------------------------------

	public override void Initialise()
	{
		// Base Class Initialization
		// Set Entity Type
		SetEntityType(eEntityType.TOWER);

		// Initialise Base Class
		base.Initialise();

		InitialiseHealthBar(new Vector3(0.0F, 3.0F, 0.0F), new Vector2(1.0f, 0.2f));
	}

	public override void Process()
	{
		base.Process();
	}

	public override void FixedProcess()
	{
		base.FixedProcess();
	}

	public override void LateProcess()
	{
		base.LateProcess();
	}

	public override void Shutdown()
	{
		base.Shutdown();
	}

	// Custom Functions
	// ---------------------------------------------------

	protected void RotateTowardEnemy()
	{
		if (EnemyInAttackZone != null)
		{
			Quaternion m_TargetRot;
			// Get the target rotation we want to face
			m_TargetRot = Quaternion.LookRotation(EnemyInAttackZone.Position - Position);

			// Only rotate on the y axis
			m_TargetRot.x = 0;
			m_TargetRot.z = 0;

			// And lerp using fixed delta time
			transform.rotation = Quaternion.RotateTowards(transform.rotation, m_TargetRot, Time.deltaTime * 90.0f);
		}
	}

	// Properties
	// ---------------------------------------------------

}*/

////////////////////////////////////////////////////////////////////////////////////////////////////////////
//########################################################################################################//
//# 																									 #//
//#					CODE GRAVEYARD, ANYTHING BEYOND THIS POINT SHOULD BE DISREGARDED					 #//
//# 																									 #//
//########################################################################################################//
////////////////////////////////////////////////////////////////////////////////////////////////////////////
///------------------------------------------------------------------------------------------------------///
///

/*

	// Public Variables
	// ---------------------------------------------------

	public int damage = 1;

	public int delay = 1;

	public int range = 15;

	// Protected Variables
	// ---------------------------------------------------

	protected eTowerType m_towerType;

	protected int m_TowerTeam = 0;

	public bool m_IsTeamSet = false;

	protected float m_fTimer = 1.0f;

	protected float m_fShootTimer = 0.0f;

	// Unit closest to the tower
	public Unit m_closestUnit = null;
	protected float m_fLowestDistance = 0.0f;

	private Quaternion m_TargetRot;

	// Private Variables
	// ---------------------------------------------------



	// Unity Functions
	// ---------------------------------------------------

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	public void Update()
	{
		base.Update();
	}

	// Custom Functions
	// ---------------------------------------------------

	public void Initialise()
	{

	}

	public void AttachInfoCentre()
	{
		TowerInformationCentre TowerInfo = gameObject.AddComponent("TowerInformationCentre") as TowerInformationCentre;

		TowerInfo.TowerOwner = this;
		TowerInfo.gameCamera = GetGameManager().GetCameraHandler().RequestCamera();
		TowerInfo.Initialise();
	}

	public void RotateTowardEnemy()
	{
		if (m_closestUnit != null)
		{
			// Get the target rotation we want to face
			m_TargetRot = Quaternion.LookRotation(m_closestUnit.transform.position - transform.position);

			// Only rotate on the y axis
			m_TargetRot.x = 0;
			m_TargetRot.z = 0;

			// And lerp using fixed delta time
			transform.rotation = Quaternion.RotateTowards(transform.rotation, m_TargetRot, Time.deltaTime * 90.0f);
		}
	}

	public void SetTowerTeam(int a_ID)
	{
		m_IsTeamSet = true;
		m_TowerTeam = a_ID;
	}

	// Properties
	// ---------------------------------------------------

	public int TeamID { get { return m_TowerTeam; } }
	public eTowerType TowerType { get { return m_towerType; } }

*/
