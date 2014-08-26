using UnityEngine;
using System;
using System.Collections;
/*
public static class LoaderSettings
{
	public static void Load()
	{
		while (!LevelHelper.IsValidLine()) { LevelHelper.NextLine(); }

		while (LevelHelper.IsValidLine())
		{
			string value = LevelHelper.m_FileData[LevelHelper.m_CurrentLine];

			if (value.Contains("BEGIN")) { return; }

			int indexFirst = value.IndexOf(' ');
			int indexLast = value.LastIndexOf(' ') + 1;
			string data = value.Substring(indexLast);
			value = value.Substring(0, indexFirst);

			switch (value)
			{
				case "PathHeight": LevelHelper.m_LevelData.SetPathHeight(Convert.ToSingle(data)); break;
			}

			LevelHelper.NextLine();
		}
	}
}
*/