using UnityEngine;
using System;
using System.Collections;
/*
public static class LoaderCameraPath
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
					case "Node": LevelHelper.NextLine(); LoadCameraNode(); break;
				}
			}

			LevelHelper.NextLine();

			LevelHelper.DecreaseTabLevel();
		}

		LevelHelper.m_GameManager.GetInGameCamera().CreateCameraPath();
	}

	private static void LoadCameraNode()
	{
		CameraData returnVal = new CameraData();

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
				case "Type":
					{
						returnVal.Type = CameraHelper.IntToTile(Convert.ToInt32(data));
					}
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
				case "Rotation":
					{
						int commaLocation = 0;

						commaLocation = data.IndexOf('(') + 1;
						data = data.Substring(commaLocation);

						commaLocation = data.IndexOf(','); returnVal.Rotation.x = Convert.ToSingle(data.Substring(0, commaLocation)); data = data.Substring(commaLocation + 2);
						commaLocation = data.IndexOf(','); returnVal.Rotation.y = Convert.ToSingle(data.Substring(0, commaLocation)); data = data.Substring(commaLocation + 2);
						commaLocation = data.IndexOf(','); returnVal.Rotation.z = Convert.ToSingle(data.Substring(0, commaLocation)); data = data.Substring(commaLocation + 2);
						commaLocation = data.IndexOf(')'); returnVal.Rotation.w = Convert.ToSingle(data.Substring(0, commaLocation));
					}
					break;
			}

			LevelHelper.DecreaseTabLevel();

			LevelHelper.NextLine();
		}

		LevelHelper.m_LevelData.AddCamera(returnVal);
	}
}
*/