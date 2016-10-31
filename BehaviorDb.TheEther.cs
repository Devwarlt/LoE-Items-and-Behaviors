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
                        new Shoot(radius: 15, count: 5, angleOffset: 30 / 3, projectileIndex: 0, coolDown: 500),
                        new Shoot(radius: 35, projectileIndex: 3, count: 32, coolDown: 60000),
                        new Shoot(radius: 8.4, count: 6, shootAngle: 60, projectileIndex: 1, coolDown:900),
                        new HpLessTransition(0.15, "Boo")
                    ),
                    new State("Boo",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Follow(1.5, acquireRange: 14, range:10),
                        new Shoot(radius: 15, count: 5, angleOffset: 30 / 3, projectileIndex: 4, coolDown: 500, predictive: 1),
                        new Taunt(1.0, true, "NOOO!!"),
                        new TimedTransition(4344, "ripp")
                    ),
                    new State("ripp",
                        new Shoot(35, projectileIndex: 3, count: 30),
                        new Shoot(35, projectileIndex: 4, count: 30),
                        new Suicide()
                        )
                    ),
                new MostDamagers(5,
                    new TierLoot(11, ItemType.Weapon, awesomeloot),
                    new TierLoot(12, ItemType.Weapon, awesomeloot),
                    new TierLoot(11, ItemType.Armor, awesomeloot),
                    new TierLoot(12, ItemType.Armor, awesomeloot),
                    new TierLoot(5, ItemType.Ring, awesomeloot),
                    new TierLoot(6, ItemType.Ring, awesomeloot),
                    new TierLoot(5, ItemType.Ability, awesomeloot),
                    new TierLoot(6, ItemType.Ability, awesomeloot),
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
                    new Shoot(15, 5, 5, angleOffset: 30 / 3, projectileIndex: 0, coolDown: 120)
                ),
                new MostDamagers(5,
                    new OnlyOne(
                        new ItemLoot("Bone Dagger", awesomeloot)
                        ),
                        new TierLoot(5, ItemType.Weapon, awesomeloot),
                        new TierLoot(6, ItemType.Weapon, awesomeloot),
                        new TierLoot(7, ItemType.Weapon, awesomeloot),
                        new TierLoot(9, ItemType.Armor, awesomeloot),
                        new TierLoot(8, ItemType.Armor, awesomeloot),
                        new TierLoot(7, ItemType.Armor, awesomeloot)
                        )
                )
            ;
    }
}
