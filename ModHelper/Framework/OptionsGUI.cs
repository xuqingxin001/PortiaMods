﻿using UnityEngine;

public static class OptionsGUI
{
    public static GUIStyle Style
    {
        get
        {
            var background = new Texture2D(1, 1);
            background.SetPixel(0, 0, new Color(0.35f, 0.35f, 0.35f, 1f));
            background.Apply();

            return new GUIStyle { normal = { background = background }, padding = { top = 5, bottom = 5, left = 5, right = 5 } };
        }
    }
}