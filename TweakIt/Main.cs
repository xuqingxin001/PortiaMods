﻿using System;
using System.Reflection;
using Harmony12;
using JetBrains.Annotations;
using UnityEngine;
using UnityModManagerNet;

namespace TweakIt
{
    public static class Main
    {
        public static bool Enabled;
        public static Settings Settings;

        public static UnityModManager.ModEntry.ModLogger Logger { get; private set; }

        [UsedImplicitly]
        private static bool Load(UnityModManager.ModEntry modEntry)
        {
            try
            {
                Logger = modEntry.Logger;
                Settings = UnityModManager.ModSettings.Load<Settings>(modEntry);

                modEntry.OnToggle = OnToggle;
                modEntry.OnGUI = OnGUI;
                modEntry.OnSaveGUI = OnSaveGUI;

                var harmony = HarmonyInstance.Create($"com.{modEntry.Info.Author}.{modEntry.Info.Id}");
                harmony.PatchAll(Assembly.GetExecutingAssembly());

                return true;
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return false;
            }
        }

        private static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            if (Settings == null) return;

            GUILayout.BeginVertical(OptionsGUI.Style);

            Settings.SortChestsByName = GUILayout.Toggle(Settings.SortChestsByName, new GUIContent(
                "Sort chests",
                "Sorts chest by name. Changing requires save reload."
            ));
            Settings.DetailedNotifications = GUILayout.Toggle(Settings.DetailedNotifications, new GUIContent(
                "Detailed notifications",
                "Provides more details (total, next level, etc.) in items, money, experience, reputation notifications."
            ));
            Settings.DefaultCraftToMax = GUILayout.Toggle(Settings.DefaultCraftToMax, new GUIContent(
                "Set craft to max",
                "Sets craft/refuel amount to max by default for everything except worktable."
            ));
            Settings.RemoveCookingStun = GUILayout.Toggle(Settings.RemoveCookingStun, new GUIContent(
                "Disable cooking stun",
                "Otherwise player and controls are locked for a second after cooking ingredient is added. Doesn't affect animation."
            ));

            GUILayout.Space(20);
            GUILayout.Label(GUI.tooltip.IfNullOrEmpty("Hover over an option to provide more details."));

            GUILayout.EndVertical();
        }

        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            Enabled = value;
            return true;
        }

        private static void OnSaveGUI(UnityModManager.ModEntry modEntry) => Settings.Save(modEntry);
    }
}