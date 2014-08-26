using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{

	private float m_fBackgroundMusicLevel = 1.0f;
	private float m_fSFXLevel = 1.0f;
	private float m_fBrightnessLevel = 1.0f;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public float BackgroundMusicLevel { get { return m_fBackgroundMusicLevel; } set { m_fBackgroundMusicLevel = value; } }
	public float SFXLevel { get { return m_fSFXLevel; } set { m_fSFXLevel = value; } }
	public float BrightnessLevel { get { return m_fBrightnessLevel; } set { m_fBrightnessLevel = value; } }

}