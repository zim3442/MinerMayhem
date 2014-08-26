////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	                                                                                                      //
//	Created:		13/07/2014 - 11:12	|	Tom Faircloth                                                 //
//	Modified:		13/07/2014 - 11:12	|	Tom Faircloth                                                 //
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
// >---------------------------------------------------

#if (UNITY_EDITOR)
using UnityEditor;

[CustomEditor(typeof(Unit))]
[CanEditMultipleObjects]
public class UnitEditor : EntityEditor
{
	bool m_UnitFoldout = true;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		m_UnitFoldout = EditorGUILayout.Foldout(m_UnitFoldout, "Unit");

		if (m_UnitFoldout)
		{
			Unit myTarget = (Unit)target;

			if (!EditorApplication.isPlaying)
			{
				// Variables
				myTarget.v_MovementSpeed = EditorGUILayout.FloatField("Movement Speed", myTarget.v_MovementSpeed);
			}
			else
			{
				// Data
				EditorGUILayout.LabelField("Unit Type", myTarget.UnitType.ToString());
				EditorGUILayout.LabelField("Movment Override", myTarget.m_bSkipMoving.ToString());
				EditorGUILayout.LabelField("Is Passable", myTarget.m_bPassable.ToString());
			}

			if (GUI.changed) { EditorUtility.SetDirty(myTarget); }
		}
	}
}
#endif

public class Unit : Entity
{
	// Settings
	// >---------------------------------------------------
	public float		v_MovementSpeed			= 1.0F;

	// Class Variables
	// >---------------------------------------------------
    private eUnitType	m_UnitType				= eUnitType.NONE;
    private float		m_MovementSpeed			= 10.0F;

	// Movement
	private Unit		m_UnitInFront			= null;

	private int			m_LaneID;
	private int			m_NodeID;

	public bool			m_bReachedBase			= false;
	public bool			m_bReachedLaneEnd		= false;
    public bool			m_bSkipMoving			= false;
    public bool			m_bPassable				= false;

	// Management
	private bool		m_IsSetUnitType			= false;

	// Base Class Override Functions
	// >---------------------------------------------------
    public override void Initialise()
	{
		// Base Class Initialization
		// Set Entity Type
		SetEntityType(eEntityType.UNIT);

		// Initialise Base Class
		base.Initialise();

		// Set Defaults
		m_MovementSpeed	= v_MovementSpeed;
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

	// Custom Class Level Functions
	// >---------------------------------------------------
	public void SetUnitType(eUnitType a_UnitType)
	{
		if (!m_IsSetUnitType)
		{
			m_IsSetUnitType = true;
			m_UnitType = a_UnitType;
		}
	}

	public void SetLaneInfo(int a_iLaneID, int a_iNodeID)
	{
		m_LaneID = a_iLaneID;
		m_NodeID = a_iNodeID;
	}

	// Custom Class Level Properties
	// >---------------------------------------------------
	public eUnitType	UnitType	{ get { return m_UnitType; } }

	public Unit			UnitInFront	{ get { return m_UnitInFront; } set { m_UnitInFront = value; } }
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////
//########################################################################################################//
//# 																									 #//
//#					CODE GRAVEYARD, ANYTHING BEYOND THIS POINT SHOULD BE DISREGARDED					 #//
//# 																									 #//
//########################################################################################################//
////////////////////////////////////////////////////////////////////////////////////////////////////////////
///------------------------------------------------------------------------------------------------------///



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
public enum eMovement
{
	ADVANCE,
	RETREAT
}
	private eMovement	m_DirectionOfMovement	= eMovement.ADVANCE;

	private void UpdateTarget(LaneData a_Lane, NodeData a_NodeTarget)
	{
		//DETERMINE IF AT TARGET NODE
		//UPDATE TO NEW TARGET
		//ACCOUNTS FOR DIRECTION OF TRAVEL ie how to iterate the nodes

		if (Vector3.Distance(Position, a_NodeTarget.Position) < 0.1f)
		{
			//IF AT TARGET NODE THEN FIND NEXT TARGET
			if (GetTeam().DirectionOfTravel[m_LaneID] > 0)
			{
				if (m_DirectionOfMovement == eMovement.ADVANCE)
				{
					//Increment ++
					if (a_NodeTarget.m_NodeID < a_Lane.NodeCount - 1)
					{
							
						m_NodeID++;
					}
					else
					{
						//HAVE REACHED BASE?
						m_bReachedLaneEnd = true;
					}
				}
				else
				{
					//Increment --
					if (a_NodeTarget.m_NodeID > 0)
					{
						m_NodeID--;
					}
					else
					{
						//HAVE REACHED BASE?
						m_bReachedLaneEnd = true;
					}
				}
			}
			else
			{
				if (m_DirectionOfMovement == eMovement.ADVANCE)
				{
					//Increment --
					if (a_NodeTarget.m_NodeID > 0)
					{
						m_NodeID--;
					}
					else
					{ 
						//HAVE REACHED BASE?
						m_bReachedLaneEnd = true;
					}
				}
				else
				{
					//Increment ++
					if (a_NodeTarget.m_NodeID < a_Lane.NodeCount - 1)
					{
						m_NodeID++;
					}
					else
					{
						//HAVE REACHED BASE?
						m_bReachedLaneEnd = true;
					}
				}
			}
		}	
	}

	private void RotateToTargetNode(LaneData a_Lane, NodeData a_NodeTarget)
	{
		if (a_NodeTarget != null)
		{
			Quaternion m_TargetRot;
			m_TargetRot = Quaternion.LookRotation(a_NodeTarget.Position - Position);
			m_TargetRot.x = 0;
			m_TargetRot.z = 0;

			// And lerp using fixed delta time
			transform.rotation = Quaternion.RotateTowards(transform.rotation, m_TargetRot, Time.deltaTime * 90.0f);
		}
	}

	public void MoveToBase()
	{
		// IF UNIT BLOCKING MOVEMENT
		if (m_UnitInFront != null)
		{
			if (Vector3.Distance(m_UnitInFront.Position, Position) >= 1.0f
				|| (m_UnitInFront.GetTeam().TeamID == GetTeam().TeamID && m_UnitInFront.m_bPassable))
			{
				m_UnitInFront = null;
			}
			else
			{
				return;
			}
		}

		if (m_EnemyTeamHandle == null)
		{
			if( GetTeam().TeamID == 0 )
			{
				m_EnemyTeamHandle = GetGameManager().GetTeam(1);
			}

			if( GetTeam().TeamID == 1 )
			{
				m_EnemyTeamHandle = GetGameManager().GetTeam(0);
			}
		}

		if (m_EnemyTeamHandle != null)
		{
			if (Vector3.Distance(Position, m_EnemyTeamHandle.Position) > 5.0f)
			{
				float a_Movement = m_MovementSpeed * Time.deltaTime;
				Position = Vector3.MoveTowards(Position, m_EnemyTeamHandle.Position, a_Movement);
			}
			else
			{
				m_bReachedBase = true;
			}
		}
	}

	public virtual void Move()
	{
		//IF BLOCKED BY TOWER
		if (m_TowerInFront != null)
		{
			if (Vector3.Distance(m_TowerInFront.Position, Position) >= 3.0f
				|| (m_TowerInFront.GetTeam().TeamID == GetTeam().TeamID))
			{
				m_UnitInFront = null;
			}
			else
			{
				return;
			}
		}

		// IF UNIT BLOCKING MOVEMENT
		if (m_UnitInFront != null)
		{
			if (Vector3.Distance(m_UnitInFront.Position, Position) >= 2.0f
                || (m_UnitInFront.GetTeam().TeamID == GetTeam().TeamID && m_UnitInFront.m_bPassable) )
			{
				m_UnitInFront = null;
			}
			else 
			{
				return;
			}
		}

		LaneData a_Lane = GetGameManager().GetMapManager().Level.GetLane(m_LaneID);
		NodeData a_NodeTarget = a_Lane.GetNode(m_NodeID);

		if(a_Lane != null)
		{
			if(a_NodeTarget != null)
			{
				UpdateTarget(a_Lane, a_NodeTarget);
				float a_Movement = m_MovementSpeed * Time.deltaTime;
				Position = Vector3.MoveTowards(Position, a_NodeTarget.Position, a_Movement);
				RotateToTargetNode(a_Lane, a_NodeTarget);
			}
		}
	}

        if (!m_bSkipMoving)
        {
			if (!m_bReachedLaneEnd)
			{
				Move();
			}
			else
			{
				MoveToBase();
			}
        }
 */