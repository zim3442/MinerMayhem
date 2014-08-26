////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	                                                                                                      //
//	Created:		29/07/2014 - 12:20	|	Ranjit Singh                                                  //
//	Modified:		16/08/2014 - 15:43	|	Ranjit Singh                                                  //
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

#if (UNITY_EDITOR)
using UnityEditor;

[CustomEditor(typeof(Projectile))]
[CanEditMultipleObjects]
public class ProjectileEditor : ItemEditor
{
	bool m_ProjectileFoldout = true;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (EditorApplication.isPlaying)
		{
			m_ProjectileFoldout = EditorGUILayout.Foldout(m_ProjectileFoldout, "Projectile");

			if (m_ProjectileFoldout)
			{
				Projectile myTarget = (Projectile)target;

				EditorGUILayout.LabelField("Damage", myTarget.Damage.ToString());

				if (GUI.changed) { EditorUtility.SetDirty(myTarget); }
			}
		}
	}
}
#endif

public class Projectile : Item
{
	private int m_Damage = 0;
	private float m_fTimer = 0.0f;

	public override void Initialise()
	{
		// Base Class Initialization
		// Set Item Type
		SetItemType(eItemType.PROJECTILE);

		// Initialise Base Class
		base.Initialise();
	}

	public override void Process()
	{
		base.Process();
	}

	public override void FixedProcess()
	{
		base.FixedProcess();

		m_fTimer += Time.fixedDeltaTime;

		if (m_fTimer > 3.0f)
		{
			MarkItemForRemoval();
		}
	}

	public override void LateProcess()
	{
		base.LateProcess();
	}

	public override void Shutdown()
	{
		base.Shutdown();
	}


	// Properties
	// ---------------------------------------------------

	public int Damage { get { return m_Damage; } set { m_Damage = value; } }

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
	public int TeamID { get { return m_Team.TeamID; } }

				if (myTarget.m_Team == null)
				{
					EditorGUILayout.LabelField("Owned By Team ID", "-1");
				}
				else
				{
					EditorGUILayout.LabelField("Owned By Team ID", myTarget.m_Team.TeamID.ToString());
				}
	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "PROMPT")
		{
			Unit unit = collider.GetComponent<Unit>();

			if (unit)
			{
				if (m_Team.TeamID != unit.GetTeam().TeamID)
				{
					unit.DamageHealth(m_Damage);

					MarkItemForRemoval();
				}
			}
		}
	}*/