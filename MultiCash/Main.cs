using BTD_Mod_Helper;
using MelonLoader;
using Il2CppAssets.Scripts.Simulation;
using Il2CppAssets.Scripts.Unity.UI_New.Popups;
using Il2CppTMPro;
using UnityEngine;

[assembly: MelonInfo(typeof(MultiCash.Main), "MultiCash", "0.0.1", "Mai")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
namespace MultiCash
{
    public class Main : BloonsTD6Mod
    {
        static float multi = 1;
        static bool KeyPressed;

        [HarmonyLib.HarmonyPatch(typeof(Simulation), "AddCash")]
        public class Cash
        {
            [HarmonyLib.HarmonyPrefix]
            public static bool Love(ref double c, ref Simulation.CashSource source)
            {
                c *= multi;
                return true;
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (KeyPressed)
            {
                if (PopupScreen.instance.GetFirstActivePopup() != null)
                {
                    PopupScreen.instance.GetFirstActivePopup().GetComponentInChildren<TMP_InputField>().characterValidation = TMP_InputField.CharacterValidation.None;
                    KeyPressed = false;
                }
            }

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.F10))
            {
                Il2CppSystem.Action<string>mod = (Il2CppSystem.Action<string>)delegate (string s)
                {
                    multi = float.Parse(s);
                };

                PopupScreen.instance.ShowSetNamePopup("Cash", "Multiply cash by", mod, multi.ToString());
                
                KeyPressed = true;
            }
        }
    }
}