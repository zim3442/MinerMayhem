using UnityEngine;
using System.Collections;

public class ShieldEffect : MonoBehaviour {

    public float m_fMaxDisplayTime = 1.0f;
    private float m_Timer = 0.0f;

    // Use this for initialization
    void Start()
    {
        Initialise();
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer -= 1.0f * Time.deltaTime;

        float a_Radius = Mathf.Sin(m_Timer) + 2;

        gameObject.transform.localScale = new Vector3(a_Radius, a_Radius, a_Radius);

        if (m_Timer <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }

    public void Initialise()
    {
        m_Timer = m_fMaxDisplayTime;
        gameObject.transform.localScale = new Vector3(2, 2, 2);
        gameObject.transform.localPosition = new Vector3(0, 1, 0);
    }

}
