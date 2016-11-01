using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wServer.logic.behaviors;
using System.Threading.Tasks;
using wServer.logic.transitions;
using wServer.logic.loot;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ TheEther = () => Behav()
            
            .Init("Lord of Death",
                new State(
                    new State(
                        new Wander(0.5),
                        new Shoot(radius: 15, count: 5, angleOffset: 30 / 3, projectileIndex: 0, coolDown: 700),
                        new Shoot(radius: 35, projectileIndex: 3, count: 32, coolDown: 60000),
                        new Shoot(radius: 8.4, count: 6, shootAngle: 60, projectileIndex: 1, coolDown:2000),
                        new HpLessTransition(0.15, "Boo")
                    ),
                    new State("BOO",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Follow(1.5, acquireRange: 14, range:10),
                        new Shoot(radius: 15, count: 5, angleOffset: 30 / 3, projectileIndex: 4, coolDown: 500, predictive: 1),
                        new HpLessTransition(0.50, "YOU HAVE CROSSED ME FOR THE LAST TIME")
                    ),
                    new State("YOU HAVE CROSSED ME FOR THE LAST TIME,
                              new Wander(0.5)
                              new Shoot(radius: 15, count: 8, angleoffset: 30 / 3, projectileIndex: 0, coolDown: 450),
                              new Shoot(radius: 32, projectileIndex: 3, count: 34, coolDown: 50000),
                              new Shoot(radius: 7.4, count: 10, shootAngle: 60, projectileIndex: 1, coolDown: 700),
                              new TimedTransition(4344, "RIPP")
                              
                
                        new State("RIPP",
                        new Shoot(35, projectileIndex: 3, count: 30),
                        new Shoot(35, projectileIndex: 4, count: 30),
                        new Suicide()
                        )
                    ),
                new MostDamagers(5,
                    new TierLoot(11, ItemType.Weapon, goodloot),
                    new TierLoot(12, ItemType.Weapon, greatloot),
                    new TierLoot(11, ItemType.Armor, normalloot),
                    new TierLoot(12, ItemType.Armor, goodloot),
                    new TierLoot(5, ItemType.Ring, goodloot),
                    new TierLoot(6, ItemType.Ring, greatloot),
                    new TierLoot(5, ItemType.Ability, goodloot),
                    new TierLoot(6, ItemType.Ability, greatloot),
                    new ItemLoot("Bone Dagger", awesomeloot)
                    ),
                new MostDamagers(3,
                    new OnlyOne(
                        new ItemLoot("Bone Marrow", superbag),
                        new ItemLoot("Quiver of Skulls", superbag),
                        new ItemLoot("Bonemail Hide", superbag),
                        new ItemLoot("Cursed Crown", superbag)
                             ),
                        new ItemLoot("Skeleton Warrior Skin", awesomeloot),
                        new ItemLoot("Wine Cellar Incantation", awesomeloot)
                    )
            )

        .Init("Ether Skeleton",
                new State(
                    new Follow(1.5, acquireRange: 14, range: 10),
                    new Shoot(15, 3, 5, angleOffset: 30 / 3, projectileIndex: 0, coolDown: 120)
                ),
                new MostDamagers(5,
                    new OnlyOne(
                        new ItemLoot("Bone Dagger", awesomeloot)
                        ),
                        new TierLoot(5, ItemType.Weapon, poorloot),
                        new TierLoot(6, ItemType.Weapon, poorloot),
                        new TierLoot(7, ItemType.Weapon, poorloot),
                        new TierLoot(9, ItemType.Armor, mediumloot),
                        new TierLoot(8, ItemType.Armor, mediumloot),
                        new TierLoot(7, ItemType.Armor, poorloot)
                        )
                )
            ;
    }
}
