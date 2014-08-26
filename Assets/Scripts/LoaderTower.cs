using UnityEngine;
using System.Collections;
/*
public static class LoaderTower
{
	public static void Load()
	{
		while (LevelHelper.m_FileData[LevelHelper.m_CurrentLine].Substring(LevelHelper.GetTabLevel().Length) != "END")
		{
			if (!LevelHelper.IsValidLine()) { LevelHelper.NextLine(); continue; }

			LevelHelper.IncreaseTabLevel();

			if (LevelHelper.m_FileData[LevelHelper.m_CurrentLine].Contains("BEGIN"))
			{
				string value = LevelHelper.m_FileData[LevelHelper.m_CurrentLine].Substring((LevelHelper.GetTabLevel() + "BEGIN ").Length);

				switch (value)
				{
					case "TowerPillar": break;
				}
			}

			LevelHelper.NextLine();

			LevelHelper.DecreaseTabLevel();
		}
	}
}*/