using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;
using Gungeon;

namespace ModdedMagnetMod
{
    public class ModdedMagnetItem : PassiveItem
    {
        public static void Init()
        {
            InitItems();
            InitGuns();
        }

        public static void InitItems()
        {
            string itemName = "Modded Item Magnet";
            string resourceName = "ModdedMagnetMod/Resources/ModdedItemsMagnet";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<ModdedMagnetItem>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Magnets Unusual Items";
            string longDesc = "Increases the loot weight of modded items. Weight increase can be changed through Mod the Gungeon Console's 'changeitemweight' command.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "spapi");
            item.quality = ItemQuality.EXCLUDED;
            item.type = ModdedMagnetItemType.ITEMS;
        }

        public static void InitGuns()
        {
            string itemName = "Modded Gun Magnet";
            string resourceName = "ModdedMagnetMod/Resources/ModdedGunsMagnet";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<ModdedMagnetItem>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Magnets Unusual Guns";
            string longDesc = "Increases the loot weight of modded guns. Weight increase can be changed through Mod the Gungeon Console's 'changegunweight' command.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "spapi");
            item.quality = ItemQuality.EXCLUDED;
            item.type = ModdedMagnetItemType.GUNS;
        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            if (!weightModified)
            {
                if (this.type == ModdedMagnetItemType.GUNS)
                {
                    this.weight = ModdedMagnetModule.ModGunWeight;
                    foreach (WeightedGameObject obj in GameManager.Instance.RewardManager.GunsLootTable.defaultItemDrops.elements)
                    {
                        if (obj.pickupId > 823 || obj.pickupId < 0)
                        {
                            obj.weight *= this.weight;
                        }
                    }
                }
                else if (this.type == ModdedMagnetItemType.ITEMS)
                {
                    this.weight = ModdedMagnetModule.ModItemWeight;
                    foreach (WeightedGameObject obj in GameManager.Instance.RewardManager.ItemsLootTable.defaultItemDrops.elements)
                    {
                        if (obj.pickupId > 823 || obj.pickupId < 0)
                        {
                            obj.weight *= this.weight;
                        }
                    }
                }
                weightModified = true;
            }
        }

        public override DebrisObject Drop(PlayerController player)
        {
            if (weightModified)
            {
                if (this.type == ModdedMagnetItemType.GUNS)
                {
                    this.weight = ModdedMagnetModule.ModGunWeight;
                    foreach (WeightedGameObject obj in GameManager.Instance.RewardManager.GunsLootTable.defaultItemDrops.elements)
                    {
                        if (obj.pickupId > 823 || obj.pickupId < 0)
                        {
                            obj.weight /= this.weight;
                        }
                    }
                }
                else if (this.type == ModdedMagnetItemType.ITEMS)
                {
                    this.weight = ModdedMagnetModule.ModItemWeight;
                    foreach (WeightedGameObject obj in GameManager.Instance.RewardManager.ItemsLootTable.defaultItemDrops.elements)
                    {
                        if (obj.pickupId > 823 || obj.pickupId < 0)
                        {
                            obj.weight /= this.weight;
                        }
                    }
                }
                weightModified = false;
            }
            return base.Drop(player);
        }

        protected override void OnDestroy()
        {
            if (weightModified)
            {
                if (this.type == ModdedMagnetItemType.GUNS)
                {
                    this.weight = ModdedMagnetModule.ModGunWeight;
                    foreach (WeightedGameObject obj in GameManager.Instance.RewardManager.GunsLootTable.defaultItemDrops.elements)
                    {
                        if (obj.pickupId > 823 || obj.pickupId < 0)
                        {
                            obj.weight /= this.weight;
                        }
                    }
                }
                else if (this.type == ModdedMagnetItemType.ITEMS)
                {
                    this.weight = ModdedMagnetModule.ModItemWeight;
                    foreach (WeightedGameObject obj in GameManager.Instance.RewardManager.ItemsLootTable.defaultItemDrops.elements)
                    {
                        if (obj.pickupId > 823 || obj.pickupId < 0)
                        {
                            obj.weight /= this.weight;
                        }
                    }
                }
                weightModified = false;
            }
            base.OnDestroy();
        }

        public ModdedMagnetItemType type;
        public float weight;
        public bool weightModified;

        public enum ModdedMagnetItemType
        {
            ITEMS,
            GUNS
        }
    }
}
