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

[CustomEditor(typeof(Melee))]
[CanEditMultipleObjects]
public class MeleeEditor : UnitEditor
{
	bool m_MeleeFoldout = true;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		// Until This Does Something
		return;

		m_MeleeFoldout = EditorGUILayout.Foldout(m_MeleeFoldout, "Melee");

		if (m_MeleeFoldout)
		{
			Melee myTarget = (Melee)target;

			if (!EditorApplication.isPlaying)
			{

			}
			else
			{

			}

			if (GUI.changed) { EditorUtility.SetDirty(myTarget); }
		}
	}
}
#endif

public class Melee : Unit {

	private int m_KillExperience = 100;

    // Unity Functions
    // ---------------------------------------------------

    public override void Initialise()
    {
		base.Initialise();

		SetUnitType(eUnitType.HEAVY);
    }

    public override void Process()
    {
		base.Process();
		Attack();
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

		if (m_TowerInFront != null)
		{
			if (CanAttack(true))
			{
				m_TowerInFront.DamageHealth(AttackDamage);
			}
		}

		if (EnemyInAttackZone != null)
		{
			if (CanAttack(true))
			{
				EnemyInAttackZone.DamageHealth(AttackDamage);
			}
		}    
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