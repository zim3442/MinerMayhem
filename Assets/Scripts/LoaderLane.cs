using UnityEngine;
using System;
using System.Collections;
/*
public static class LoaderLane
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
					case "Lane": LevelHelper.NextLine(); LoadLane(); break;
				}
			}

			LevelHelper.NextLine();

			LevelHelper.DecreaseTabLevel();
		}
	}

	private static void LoadLane()
	{
		LevelHelper.m_LevelData.AddLane();

		while (LevelHelper.m_FileData[LevelHelper.m_CurrentLine].Substring(LevelHelper.GetTabLevel().Length) != "END")
		{
			if (!LevelHelper.IsValidLine()) { LevelHelper.NextLine(); continue; }

			LevelHelper.IncreaseTabLevel();

			LoadNode();

			LevelHelper.DecreaseTabLevel();

			LevelHelper.NextLine();
		}
	}

	private static void LoadNode()
	{
		string value = LevelHelper.m_FileData[LevelHelper.m_CurrentLine].Substring(LevelHelper.GetTabLevel().Length);

		value = value.Substring("BEGIN Node  ".Length);

		eNodeType type = eNodeType.EMPTY;
		Vector3 position = new Vector3(0.0F, 0.0F, 0.0F);

		while (value != "END")
		{
			int location = value.IndexOf(' ');

			switch (value.Substring(0, location))
			{
				case "Type":
					{
						value = value.Substring(location + 1);
						location = value.IndexOf("  ");

						type = TileHelper.IntToTile(Convert.ToInt32(value.Substring(0, location)));

						value = value.Substring(location + 2);
					}
					break;
				case "Position":
					{
						value = value.Substring(location + 1);
						location = value.IndexOf("  ");

						string vector = value.Substring(1, location - 1);
						int commaLocation = 0;

						commaLocation = vector.IndexOf(','); position.x = Convert.ToSingle(vector.Substring(0, commaLocation)); vector = vector.Substring(commaLocation + 2);
						commaLocation = vector.IndexOf(','); position.y = Convert.ToSingle(vector.Substring(0, commaLocation)); vector = vector.Substring(commaLocation + 2);
						commaLocation = vector.IndexOf(')'); position.z = Convert.ToSingle(vector.Substring(0, commaLocation));

						value = value.Substring(location + 2);
					}
					break;
			}
		}

		LevelHelper.m_LevelData.AddNode(type, position);
	}
}
*/