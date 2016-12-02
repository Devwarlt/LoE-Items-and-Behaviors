using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wServer.realm;
using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        _ LootLoE = () => Behav()
            .Init("Cube God",
                new State(
                //.. Behavior
                ),
                 new MostDamagers(5,
                    LootTemplates.StatIncreasePotionsLoot()
                     ),
                 new MostDamagers(3,
                     new OnlyOne(
                        new ItemLoot("Dirk of Cronus", whitebag),
                        new ItemLoot("Ancient Dirk of Cronus", blackbag)
                         ),
                    new EggLoot(EggRarity.Common, eggbag + goodloot),
                    new EggLoot(EggRarity.Uncommon, eggbag + greatloot),
                    new EggLoot(EggRarity.Rare, eggbag + awesomeloot),
                    new EggLoot(EggRarity.Legendary, eggbag),
                    new TierLoot(3, ItemType.Ring, mediumloot),
                    new TierLoot(4, ItemType.Ring, normalloot),
                    new TierLoot(5, ItemType.Ring, goodloot),
                    new TierLoot(4, ItemType.Ability, normalloot),
                    new TierLoot(5, ItemType.Ability, goodloot),
                    new TierLoot(7, ItemType.Armor, poorloot),
                    new TierLoot(8, ItemType.Weapon, mediumloot),
                    new TierLoot(8, ItemType.Armor, mediumloot),
                    new TierLoot(9, ItemType.Armor, mediumloot),
                    new TierLoot(9, ItemType.Weapon, normalloot),
                    new TierLoot(10, ItemType.Armor, normalloot),
                    new TierLoot(10, ItemType.Weapon, goodloot),
                    new TierLoot(11, ItemType.Armor, goodloot),
                    new TierLoot(11, ItemType.Weapon, goodloot)
                )
            )
            
            .Init("Skull Shrine",
                new State(
                //.. Behavior
                ),
                new MostDamagers(3,
                    new OnlyOne(
                        new ItemLoot("Orb of Conflict", whitebag)
                        ),
                    new EggLoot(EggRarity.Common, eggbag + goodloot),
                    new EggLoot(EggRarity.Uncommon, eggbag + greatloot),
                    new EggLoot(EggRarity.Rare, eggbag + awesomeloot),
                    new EggLoot(EggRarity.Legendary, eggbag),
                    new TierLoot(3, ItemType.Ring, mediumloot),
                    new TierLoot(4, ItemType.Ring, normalloot),
                    new TierLoot(5, ItemType.Ring, goodloot),
                    new TierLoot(8, ItemType.Weapon, mediumloot),
                    new TierLoot(9, ItemType.Weapon, normalloot),
                    new TierLoot(10, ItemType.Weapon, goodloot),
                    new TierLoot(11, ItemType.Weapon, goodloot),
                    new TierLoot(7, ItemType.Armor, poorloot),
                    new TierLoot(8, ItemType.Armor, mediumloot),
                    new TierLoot(9, ItemType.Armor, mediumloot),
                    new TierLoot(10, ItemType.Armor, normalloot),
                    new TierLoot(11, ItemType.Armor, normalloot),
                    new TierLoot(12, ItemType.Armor, goodloot),
                    new TierLoot(4, ItemType.Ability, normalloot),
                    new TierLoot(5, ItemType.Ability, goodloot)
                )
            )
            
            .Init("Hermit God",
                new State(
                //.. Behavior
                ),
                new MostDamagers(10,
                    new ItemLoot("Potion of Dexterity", 1)
                    ),
                new MostDamagers(5,
                    new OnlyOne(
                        new ItemLoot("Potion of Vitality", 1),
                        new ItemLoot("Helm of the Great Juggernaut", blackbag),
                        new ItemLoot("Helm of the Juggernaut", whitebag)
                    ),
                    new EggLoot(EggRarity.Common, eggbag + goodloot),
                    new EggLoot(EggRarity.Uncommon, eggbag + greatloot),
                    new EggLoot(EggRarity.Rare, eggbag + awesomeloot),
                    new EggLoot(EggRarity.Legendary, eggbag)
                )
            )
            
            
            .Init("Lord of the Lost Lands",
                new State(
                //.. Behavior
                ),
                new MostDamagers(5,
                    LootTemplates.StatIncreasePotionsLoot()
                    ),
                new MostDamagers(3,
                    new OnlyOne(
                        new ItemLoot("Ancient Shield of Ogmur", blackbag),
                        new ItemLoot("Shield of Ogmur", whitebag)
                        ),
                    new EggLoot(EggRarity.Common, eggbag + goodloot),
                    new EggLoot(EggRarity.Uncommon, eggbag + greatloot),
                    new EggLoot(EggRarity.Rare, eggbag + awesomeloot),
                    new EggLoot(EggRarity.Legendary, eggbag),
                    new TierLoot(8, ItemType.Weapon, mediumloot),
                    new TierLoot(9, ItemType.Weapon, normalloot),
                    new TierLoot(10, ItemType.Weapon, goodloot),
                    new TierLoot(11, ItemType.Weapon, goodloot),
                    new TierLoot(8, ItemType.Armor, mediumloot),
                    new TierLoot(9, ItemType.Armor, mediumloot),
                    new TierLoot(10, ItemType.Armor, normalloot),
                    new TierLoot(11, ItemType.Armor, normalloot),
                    new TierLoot(12, ItemType.Armor, goodloot),
                    new TierLoot(4, ItemType.Ability, normalloot),
                    new TierLoot(5, ItemType.Ability, goodloot),
                    new TierLoot(3, ItemType.Ring, mediumloot),
                    new TierLoot(4, ItemType.Ring, normalloot),
                    new TierLoot(5, ItemType.Ring, goodloot)
                )
            )
            
            
            .Init("shtrs the Forgotten King",
                new State(
                //.. Behavior
                ),
                new MostDamagers(10,
                    new ItemLoot("Potion of Life", 1),
                    new TierLoot(11, ItemType.Weapon, goodloot*2),
                    new TierLoot(12, ItemType.Weapon, greatloot*2),
                    new TierLoot(12, ItemType.Armor, goodloot*2),
                    new TierLoot(13, ItemType.Armor, greatloot*2),
                    new TierLoot(6, ItemType.Ability, greatloot*2),
                    new TierLoot(6, ItemType.Ring, greatloot*2)
                    ),
                new MostDamagers(5,
                    new OnlyOne(
                        new ItemLoot("The Forgotten Crown", whitebag),
                        new ItemLoot("Ancient Ring of Decades", blackbag),
                        new ItemLoot("The Ancient Forgotten Crown", blackbag),
                        new ItemLoot("The Tower Tarot Card", awesomeloot),
                        //Dark Knight Set
                        new ItemLoot("Broadsword of Bloodshed", awesomeloot),
                        new ItemLoot("Evil Shield of the Dark Knight", awesomeloot),
                        new ItemLoot("Chestguard of the Underworld", awesomeloot),
                        new ItemLoot("Amulet of the Dark Knight", awesomeloot),
                        //Woodland Sorcerer Set
                        new ItemLoot("Sprig of the Copse", awesomeloot),
                        new ItemLoot("Caduceus of Nature", awesomeloot),
                        new ItemLoot("Shroud of Sagebrush", awesomeloot),
                        new ItemLoot("Trinket of the Groves", awesomeloot),
                        //Ancient Colossus Set
                        new ItemLoot("Titanic Sword", awesomeloot),
                        new ItemLoot("Helm of the Titans", awesomeloot),
                        new ItemLoot("Armor of the Giants", awesomeloot),
                        new ItemLoot("Amulet of the Immortals", awesomeloot),
                        //The Spider Queen Set
                        new ItemLoot("Bow of the Spider Silk", awesomeloot),
                        new ItemLoot("Spider Web Trap", awesomeloot),
                        new ItemLoot("Nidus Armor", awesomeloot),
                        new ItemLoot("Spiders Fang", awesomeloot)
                        ),
                    new TierLoot(7, ItemType.Ability, awesomeloot*1.75),
                    new TierLoot(7, ItemType.Ring, awesomeloot*1.75)
                    )
            )
            
            ;
    }
}
