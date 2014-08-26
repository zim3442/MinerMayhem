using UnityEngine;
using System.Collections;

#if (UNITY_EDITOR)
using UnityEditor;

[CustomEditor(typeof(Item))]
[CanEditMultipleObjects]
public class ItemEditor : Editor
{
	bool m_ItemFoldout = true;
	bool m_Remove = false;

	public override void OnInspectorGUI()
	{
		if (EditorApplication.isPlaying)
		{
			m_ItemFoldout = EditorGUILayout.Foldout(m_ItemFoldout, "Item");

			if (m_ItemFoldout)
			{
				Item myTarget = (Item)target;

				// Functions
				m_Remove = EditorGUILayout.Toggle("Remove Item", m_Remove);
				if (m_Remove) { myTarget.MarkItemForRemoval(); }

				// Data
				EditorGUILayout.LabelField("Item ID", myTarget.ItemID.ToString());
				EditorGUILayout.LabelField("Item Type", myTarget.ItemType.ToString());

				if (GUI.changed) { EditorUtility.SetDirty(myTarget); }
			}
		}
	}
}
#endif

public class Item : MonoBehaviour
{
	// Variables
	// >---------------------------------------------------
	// Class Variables
	private int			m_ItemID				= -1;
	private eItemType	m_ItemType				= eItemType.NONE;
	private bool		m_IsRemovable			= false;
	private ItemManager	m_HandleToItemManager	= null;

	// Management Variables
	private bool		m_IsSetItemID			= false;
	private bool		m_IsSetItemType			= false;
	private bool		m_IsSetItemManager		= false;

	// Unity Functions
	// To Call Our Custom Virtual Functions
	// >---------------------------------------------------

	void Start()		{}
	void Update()		{ Process(); }
	void FixedUpdate()	{ FixedProcess(); }
	void LateUpdate()	{ LateProcess(); }
	void OnDestroy()	{ Shutdown(); }

	// Custom Overridable Functions
	// >---------------------------------------------------

	public virtual void Initialise()	{}
	public virtual void Process()		{}
	public virtual void FixedProcess()	{}
	public virtual void LateProcess()	{}
	public virtual void Shutdown()		{}

	// Custom Class Level Functions
	// >---------------------------------------------------
	public void	SetItemID(int a_iItemID)
	{
		if (!m_IsSetItemType)
		{
			m_IsSetItemType = true;
			m_ItemID = a_iItemID;
		}
	}

	public void	SetItemType(eItemType a_eItemType)
	{
		if (!m_IsSetItemID)
		{
			m_IsSetItemID = true;
			m_ItemType = a_eItemType;
		}
	}

	public void SetItemManagerHandle(ItemManager a_ItemManager)
	{
		if (!m_IsSetItemManager)
		{
			m_IsSetItemManager = true;
			m_HandleToItemManager = a_ItemManager;
		}
	}

	// SetPositon
	public void SetPosX(float a_Value)
	{
		Vector3 pos = Position;

		pos.x = a_Value;

		Position = pos;
	}

	public void SetPosY(float a_Value)
	{
		Vector3 pos = Position;

		pos.y = a_Value;

		Position = pos;
	}

	public void SetPosZ(float a_Value)
	{
		Vector3 pos = Position;

		pos.z = a_Value;

		Position = pos;
	}

	public void SetPosXZ(float a_ValueX, float a_ValueZ)
	{
		Vector3 pos = Position;

		pos.x = a_ValueX;
		pos.z = a_ValueZ;

		Position = pos;
	}

	public GameManager	GetGameManager()		{ return m_HandleToItemManager.GetGameManager(); }
	public ItemManager	GetItemManager()		{ return m_HandleToItemManager; }

	public void			MarkItemForRemoval()	{ m_IsRemovable = true; }

	// Custom Class Level Properties
	// >---------------------------------------------------
	public int			ItemID		{ get { return m_ItemID; } }
	public eItemType	ItemType	{ get { return m_ItemType; } }
	public bool			IsRemovable	{ get { return m_IsRemovable; } }

	public Vector3		Position	{ get { return gameObject.transform.position; } set { gameObject.transform.position = value; } }
	public Quaternion	Rotation	{ get { return gameObject.transform.rotation; } set { gameObject.transform.rotation = value; } }
}