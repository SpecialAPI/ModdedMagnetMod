using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItemAPI;
using UnityEngine.Events;

namespace ModdedMagnetMod
{
    public class ModdedMagnetModule : ETGModule
    {
        public override void Init()
        {
        }

        public override void Start()
        {
            ItemBuilder.Init();
            ModdedMagnetItem.Init();
            ETGModConsole.Commands.AddGroup("modded_magnet", Help);
            ETGModConsole.Commands.GetGroup("modded_magnet").AddUnit("help", Help);
            ETGModConsole.Commands.GetGroup("modded_magnet").AddUnit("moditemweight", delegate(string[] args)
            {
                bool shouldLogWeightInstead = false;
                float newWeight = 0f;
                if(args.Length <= 0)
                {
                    shouldLogWeightInstead = true;
                }
                else if (!float.TryParse(args[0], out newWeight))
                {
                    shouldLogWeightInstead = true;
                }
                if (shouldLogWeightInstead)
                {
                    ETGModConsole.Log("[Modded Magnet Mod] Current modded item weight multiplier: " + ModItemWeight);
                    ETGModConsole.Log("[Modded Magnet Mod] Default modded item weight multiplier: 3");
                }
                else
                {
                    ETGModConsole.Log("[Modded Magnet Mod] Old modded item weight multiplier: " + ModItemWeight);
                    ModItemWeight = newWeight;
                    ETGModConsole.Log("[Modded Magnet Mod] New modded item weight multiplier: " + ModItemWeight);
                }
            }); 
            ETGModConsole.Commands.GetGroup("modded_magnet").AddUnit("modgunweight", delegate (string[] args)
            {
                bool shouldLogWeightInstead = false;
                float newWeight = 0f;
                if (args.Length <= 0)
                {
                    shouldLogWeightInstead = true;
                }
                else if (!float.TryParse(args[0], out newWeight))
                {
                    shouldLogWeightInstead = true;
                }
                if (shouldLogWeightInstead)
                {
                    ETGModConsole.Log("[Modded Magnet Mod] Current modded gun weight multiplier: " + ModGunWeight);
                    ETGModConsole.Log("[Modded Magnet Mod] Default modded gun weight multiplier: 6");
                }
                else
                {
                    ETGModConsole.Log("[Modded Magnet Mod] Old modded gun weight multiplier: " + ModGunWeight);
                    ModGunWeight = newWeight;
                    ETGModConsole.Log("[Modded Magnet Mod] New modded gun weight multiplier: " + ModGunWeight);
                }
            });
            List<string> toLog = new List<string>();
            toLog.Add("Modded Magnet Mod initialized");
            toLog.Add("[Modded Magnet Mod] To give yourself modded magnets, you can use these commands:");
            toLog.Add("     <color=#ff0000>* give spapi:modded_item_magnet</color>");
            toLog.Add("     <color=#ff0000>* give spapi:modded_gun_magnet</color>");
            toLog.Add("[Modded Magnet Mod] To show help use this command: <color=#ff0000>modded_magnet help</color>");
            ETGModConsole.Log(string.Join("\n", toLog.ToArray()));
        }

        public void Help(string[] args)
        {
            List<string> toLog = new List<string>();
            toLog.Add("Modded Magnet Mod help:");
            toLog.Add("     Commands:");
            toLog.Add("          <color=#ff0000>* modded_magnet help</color> - shows the help you see right now.");
            toLog.Add("          <color=#ff0000>* modded_magnet modgunweight [weight]</color> - changes Modded Gun Magnet's modded gun weight multiplier. If <color=#ff0000>weight</color> argument isn't given, then it will show the current " +
                "modded gun weight multiplier");
            toLog.Add("          <color=#ff0000>* modded_magnet moditemweight [weight]</color> - changes Modded Item Magnet's modded item weight multiplier. If <color=#ff0000>weight</color> argument isn't given, then it will show the current " +
                "modded item weight multiplier");
            toLog.Add("     Items:");
            toLog.Add("          <color=#00ffff>* Modded Item Magnet</color> - when picked up, it will increase the weight of modded items by the current modded item weight multiplier. Modded item weight multiplier can be changed using the " +
                "<color=#ff0000>modded_magnet moditemweight</color>. Give command: <color=#ff0000>give spapi:modded_item_magnet</color>");
            toLog.Add("          <color=#00ffff>* Modded Gun Magnet</color> - when picked up, it will increase the weight of modded guns by the current modded gun weight multiplier. Modded gun weight multiplier can be changed using the " +
                "<color=#ff0000>modded_magnet modgunweight</color>. Give command: <color=#ff0000>give spapi:modded_gun_magnet</color>");
            ETGModConsole.Log(string.Join("\n", toLog.ToArray()));
        }

        public override void Exit()
        {
        }

        public static float ModGunWeight = 6;
        public static float ModItemWeight = 3;
    }
}
