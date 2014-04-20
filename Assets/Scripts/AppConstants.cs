using UnityEngine;

public class AppConstants
{
	private static readonly int DefaultFontSize = 20;
	public static int GetDefaultFontSize()
	{
		if (Screen.dpi > 0) {
			return (int) (Screen.dpi / 7);
		}
		return DefaultFontSize;
	}

	public static int GetLargeFontSize()
	{
		return (int)(GetDefaultFontSize () * 1.4);
	}
}