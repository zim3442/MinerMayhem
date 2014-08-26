using UnityEngine;
using System;
using System.Collections;
/*
public static class LoaderBase
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
					case "PLAYER": LevelHelper.NextLine(); LoadBase(eTeam.PLAYER); break;
					case "AI": LevelHelper.NextLine(); LoadBase(eTeam.AI); break;
				}
			}

			LevelHelper.NextLine();

			LevelHelper.DecreaseTabLevel();
		}
	}

	private static void LoadBase(eTeam a_Type)
	{
		TeamData returnVal = new TeamData();
		returnVal.Type = a_Type;

		while (LevelHelper.m_FileData[LevelHelper.m_CurrentLine].Substring(LevelHelper.GetTabLevel().Length) != "END")
		{
			LevelHelper.IncreaseTabLevel();

			string value = LevelHelper.m_FileData[LevelHelper.m_CurrentLine].Substring(LevelHelper.GetTabLevel().Length);
			int index = value.LastIndexOf(' ');
			string data = value.Substring(index + 1);
			index = value.IndexOf(' ');
			value = value.Substring(0, index);

			if (LevelHelper.m_FileData[LevelHelper.m_CurrentLine].Substring(LevelHelper.GetTabLevel().Length).Contains("("))
			{
				value = LevelHelper.m_FileData[LevelHelper.m_CurrentLine].Substring(LevelHelper.GetTabLevel().Length);
				index = value.IndexOf('(');
				data = value.Substring(index);
				index = value.IndexOf(' ');
				value = value.Substring(0, index);
			}

			switch (value)
			{
				case "StartHealth":
					returnVal.StartHealth = Convert.ToInt32(data);
					break;
				case "HealthMax":
					returnVal.MaxHealth = Convert.ToInt32(data);
					break;
				case "Gold":
					returnVal.Gold = Convert.ToInt32(data);
					break;
				case "Experience":
					returnVal.Experience = Convert.ToInt32(data);
					break;
				case "Position":
					{
						int commaLocation = 0;

						commaLocation = data.IndexOf('(') + 1;
						data = data.Substring(commaLocation);

						commaLocation = data.IndexOf(','); returnVal.Position.x = Convert.ToSingle(data.Substring(0, commaLocation)); data = data.Substring(commaLocation + 2);
						commaLocation = data.IndexOf(','); returnVal.Position.y = Convert.ToSingle(data.Substring(0, commaLocation)); data = data.Substring(commaLocation + 2);
						commaLocation = data.IndexOf(')'); returnVal.Position.z = Convert.ToSingle(data.Substring(0, commaLocation));
					}
					break;
				case "SpawnOffset":
					{
						int commaLocation = 0;

						commaLocation = data.IndexOf('(') + 1;
						data = data.Substring(commaLocation);

						commaLocation = data.IndexOf(','); returnVal.SpawnOffset.x = Convert.ToSingle(data.Substring(0, commaLocation)); data = data.Substring(commaLocation + 2);
						commaLocation = data.IndexOf(','); returnVal.SpawnOffset.y = Convert.ToSingle(data.Substring(0, commaLocation)); data = data.Substring(commaLocation + 2);
						commaLocation = data.IndexOf(')'); returnVal.SpawnOffset.z = Convert.ToSingle(data.Substring(0, commaLocation));
					}
					break;
			}

			LevelHelper.DecreaseTabLevel();

			LevelHelper.NextLine();
		}

		LevelHelper.m_LevelData.AddTeam(returnVal);
	}
}
*/