using UnityEngine;

public class AppConstants
{
	public static readonly int DefaultFontSize = 20;
	public static int GetFontSize()
	{
		if (Screen.dpi > 0) {
			return (int) (Screen.dpi / 7);
		}
		return DefaultFontSize;
	}
}