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
/*
#if (UNITY_EDITOR)
using UnityEditor;

[CustomEditor(typeof(Miner))]
[CanEditMultipleObjects]
public class MinerEditor : UnitEditor
{
	bool m_MinerFoldout = true;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		m_MinerFoldout = EditorGUILayout.Foldout(m_MinerFoldout, "Miner");

		if (m_MinerFoldout)
		{
			Miner myTarget = (Miner)target;

			if (!EditorApplication.isPlaying)
			{

			}
			else
			{
				EditorGUILayout.LabelField("Is Mining", myTarget.m_bMining.ToString());
			}

			if (GUI.changed) { EditorUtility.SetDirty(myTarget); }
		}
	}
}
#endif

public class Miner : Unit {

	private int m_KillExperience = 100;

    // Public Variables
    // ---------------------------------------------------
    public bool m_bMining = false;
    private Resource m_ResourceHandle = null;
    
    // Unity Functions
    // ---------------------------------------------------

    public override void Initialise()
    {
		base.Initialise();
		
		SetUnitType(eUnitType.MINER);
    }


    public override void Process()
    {
        if (m_bMining)
        {
            m_bSkipMoving = true;
            m_bPassable = true;
        }
        else 
        {
            m_bSkipMoving = false;
            m_bPassable = false;
        }

        base.Process();
		Attack();
        Mine();
    }

    public override void FixedProcess()
    {
        base.FixedProcess();
        CreateResourceConnection();
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
    // ------------------------------------------------------

	private void Attack()
    {
		if (m_bReachedBase)
		{
			if (m_EnemyTeamHandle != null)
			{
				if (CanAttack(true))
				{
					m_EnemyTeamHandle.DamageHealth(AttackDamage);
					if (m_EnemyTeamHandle.Health <= 0)
					{
						GetTeam().ResourceController.AddExperience(m_KillExperience);
						m_KillExperience += 25;
					}
				}
			}
		}

		if (EnemyInAttackZone != null)
		{
			m_bPassable = false;

			if (CanAttack(true))
			{
				EnemyInAttackZone.DamageHealth(AttackDamage);
			}

            if (m_bMining)
            {
                BreakResourceConnection();
                m_bMining = false;
            }

		}
    }

    private void Mine()
    {
        if (m_ResourceHandle != null && m_bMining)
        {
            if (CanAttack(true))
            {
                //Mining shares the attack cooldown as an action
                m_ResourceHandle.MineMineral(this);
            }
        }
    }

    public void SetResourceHandle( Resource a_Resource )
    {
        m_ResourceHandle = a_Resource;
    }

    public void BreakResourceConnection()
    {
        if (m_ResourceHandle != null)
        {
            m_ResourceHandle.BreakMinerConnection(this);
            m_ResourceHandle = null;
        }
    }

    public void CreateResourceConnection()
    {
        if (m_ResourceHandle != null && !m_bMining)
        {
            if (m_ResourceHandle.CheckMineralVacancy(this))
            {
                m_ResourceHandle.SetMinerHandle(this);
                m_bMining = true;
            }
            else 
            {
                m_ResourceHandle = null;
            }
        }
    }

	private void MoveToMineral()
    { 
        
    }

	private void ReturnToLane()
    { 
    
    }

	private void IdleAtMineral()
    { 
    
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