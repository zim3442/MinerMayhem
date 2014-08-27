using UnityEngine;
using System.Collections;

public class HeroScript : MonoBehaviour {

    private NavMeshAgent m_AgentHandle = null;
    public GameObject m_ClickEffectPREFAB = null;
    public GameObject m_ShieldEffectPREFAB = null;
    private float m_AttackRangeMaxDist = 2.0f;

	// Use this for initialization
	void Start () 
    {
        m_AgentHandle = GetComponentInChildren<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        PrimaryClickCheck();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject a_ShieldEffect = Instantiate(m_ShieldEffectPREFAB) as GameObject;
            a_ShieldEffect.transform.parent = gameObject.transform;
        }
	}

    private void PrimaryClickCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "PROMPT")
                {
                    m_AgentHandle.SetDestination(hit.collider.gameObject.transform.position);
                    m_AgentHandle.stoppingDistance = m_AttackRangeMaxDist;
                }
                else
                {
                    m_AgentHandle.stoppingDistance = 0.0f;

                    m_AgentHandle.SetDestination(hit.point);

                    GameObject a_ClickEffect = Instantiate(m_ClickEffectPREFAB) as GameObject;
                    a_ClickEffect.transform.position = hit.point;
                }
            }
        }    
    }
}
