using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace ROUNDSCommons
{
    public abstract class BaseRoundsPlugin : BaseUnityPlugin
    {
        public static BaseRoundsPlugin Instance { get; private set; }

        public abstract string ModId { get; set; }

        void Awake()
        {
            Instance = this;

            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }
    }
}
