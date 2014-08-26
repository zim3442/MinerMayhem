using UnityEngine;
using System.Collections;

public class ClickEffect : MonoBehaviour {

    public float m_fMaxDisplayTime = 0.5f;
    private float m_Timer = 0.0f;

	// Use this for initialization
	void Start () 
    {
        Initialise();
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_Timer -= 1.0f * Time.deltaTime;

        gameObject.transform.localScale = new Vector3(m_Timer, m_Timer, m_Timer);

        if (m_Timer <= 0.0f)
        {
            Destroy(this.gameObject);
        }
	}

    public void Initialise()
    {
        m_Timer = m_fMaxDisplayTime;
        gameObject.transform.localScale = new Vector3(m_Timer, m_Timer, m_Timer);
    }

}
