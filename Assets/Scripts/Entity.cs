////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	                                                                                                      //
//	Created:		11/07/2014 - 13:41	|	Adam Coulson                                                  //
//	Modified:		13/07/2014 - 09:23	|	Tom Faircloth                                                 //
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
// >---------------------------------------------------

#if (UNITY_EDITOR)
using UnityEditor;

[CustomEditor(typeof(Entity))]
[CanEditMultipleObjects]
public class EntityEditor : ItemEditor
{
	bool m_EntityFoldout = true;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		m_EntityFoldout = EditorGUILayout.Foldout(m_EntityFoldout, "Entity");

		if (m_EntityFoldout)
		{
			Entity myTarget = (Entity)target;

			if (!EditorApplication.isPlaying)
			{
				// Variables
				myTarget.v_Health			= EditorGUILayout.IntField("Health", myTarget.v_Health);
				myTarget.v_AttackingDamage	= EditorGUILayout.IntField("Attacking Damage", myTarget.v_AttackingDamage);
				myTarget.v_AttackingDelay	= EditorGUILayout.FloatField("Attacking Delay", myTarget.v_AttackingDelay);
			}
			else
			{
				// Data
				EditorGUILayout.LabelField("Entity Type", myTarget.EntityType.ToString());
				ProgressBar((float)myTarget.Health / (float)myTarget.MaxHealth, "Current Health (" + myTarget.Health + "/" + myTarget.MaxHealth + ")");
			}

			if (GUI.changed) { EditorUtility.SetDirty(myTarget); }
		}
	}

	void ProgressBar(float a_Value, string a_Label)
	{
		Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
		EditorGUI.ProgressBar(rect, a_Value, a_Label);
	}
}
#endif

public class Entity : Item
{
	// Settings
	// >---------------------------------------------------
	public int			v_Health				= 100;
	public int			v_AttackingDamage		= 10;
	public float		v_AttackingDelay		= 1.0F;

	// Class Variables
	// >---------------------------------------------------
    private eEntityType	m_EntityType			= eEntityType.NONE;

    // Health
    private int			m_MaxHealth				= 100;
	private int			m_CurrentHealth			= 100;

    // Combat
    private int			m_AttackDamage			= 10;
    private float		m_TimeBetweenAttacks	= 1.0F;
	private float		m_CurrentAttackCooldown	= 0.0F;
	private Unit		m_EnemyInAttackZone		= null;

	// Management
	private bool		m_IsSetEntityType		= false;

	// Base Class Override Functions
	// >---------------------------------------------------
	public override void Initialise()
	{
		// Base Class Initialization
		// Set Item Type
		SetItemType(eItemType.ENTITY);

		// Initialise Base Class
		base.Initialise();

		// Set Defaults
		m_MaxHealth				= v_Health;
		m_AttackDamage			= v_AttackingDamage;
		m_TimeBetweenAttacks	= v_AttackingDelay;

		// Set Health of Entity to its Maximum
		m_CurrentHealth = m_MaxHealth;
	}

	public override void Process()
	{
		base.Process();

		if (IsAlive() == false) { MarkItemForRemoval(); }

		// Increment Attack Cooldown Timer
		m_CurrentAttackCooldown += Time.deltaTime;
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

	// Custom Class Level Functions
	// >---------------------------------------------------

	public void SetEntityType(eEntityType a_eEntityType)
	{
		if (!m_IsSetEntityType)
		{
			m_IsSetEntityType = true;
			m_EntityType = a_eEntityType;
		}
	}

	// Health Functions
	public void DamageHealth(int a_Damage)		{ m_CurrentHealth -= a_Damage; }
	public void RestoreHealth(int a_Restore)	{ m_CurrentHealth += a_Restore; }
	public void KillEntity()					{ m_CurrentHealth = 0; }
	public void HealEntity()					{ m_CurrentHealth = m_MaxHealth; }

	public bool IsAlive()
	{
        if (m_CurrentHealth <= 0) { return false; }
		return true;
	}

	public bool CanAttack(bool a_Reset = false)
	{
		bool ReturnVal = false;

		if (m_CurrentAttackCooldown >= m_TimeBetweenAttacks)
		{
			// Reset Cooldown
			if (a_Reset) { m_CurrentAttackCooldown = 0.0F; }

			ReturnVal = true;
		}

		return ReturnVal;
	}

	// Custom Class Level Properties
	// >---------------------------------------------------
	public eEntityType	EntityType			{ get { return m_EntityType; } }
	public int			Health				{ get { return m_CurrentHealth; } }
	public int			MaxHealth			{ get { return m_MaxHealth; } }
	public int			AttackDamage		{ get { return m_AttackDamage; } }

	public Unit			EnemyInAttackZone	{ get { return m_EnemyInAttackZone; } set { m_EnemyInAttackZone = value; } }
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
///
/*
    private Team		m_Team					= null;
	private HealthBar	m_HealthBar				= null;
	public void InitialiseHealthBar(Vector3 a_Offset, Vector2 a_Scale)
	{
		GameObject gameObj = new GameObject("Information Centre");

		m_HealthBar = gameObj.AddComponent<HealthBar>();

		m_HealthBar.Size = a_Scale;
		m_HealthBar.PositionOffset = a_Offset;
		m_HealthBar.SetCameraHandler(GetGameManager().GetCameraHandler());

		m_HealthBar.Initialise(this);
	}

	public void			SetTeam(Team a_Team)	{ m_Team = a_Team; }
	public Team			GetTeam()				{ return m_Team; }
	public HealthBar	GetHealthBar()			{ return m_HealthBar; }

		if (m_HealthBar != null)
		{
			Destroy(m_HealthBar.gameObject);
		}*/