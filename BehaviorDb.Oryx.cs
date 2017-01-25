#region

using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

#endregion

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ Oryx = () => Behav()
            .Init("Oryx the Mad God 2",
                new State(
                    new State("Attack",
                        new Follow(.1, 15, 3),
                        new Wander(.05),
                        new Shoot(25, projectileIndex: 0, count: 8, shootAngle: 45, coolDown: 1500, coolDownOffset: 1500),
                        new Shoot(25, projectileIndex: 1, count: 3, shootAngle: 10, coolDown: 1000, coolDownOffset: 1000),
                        new Shoot(25, projectileIndex: 2, count: 3, shootAngle: 10, predictive: 0.2, coolDown: 1000,
                            coolDownOffset: 1000),
                        new Shoot(25, projectileIndex: 3, count: 2, shootAngle: 10, predictive: 0.4, coolDown: 1000,
                            coolDownOffset: 1000),
                        new Shoot(25, projectileIndex: 4, count: 3, shootAngle: 10, predictive: 0.6, coolDown: 1000,
                            coolDownOffset: 1000),
                        new Shoot(25, projectileIndex: 5, count: 2, shootAngle: 10, predictive: 0.8, coolDown: 1000,
                            coolDownOffset: 1000),
                        new Shoot(25, projectileIndex: 6, count: 3, shootAngle: 10, predictive: 1, coolDown: 1000,
                            coolDownOffset: 1000),
                        new Taunt(1, 6000, "Puny mortals! My {HP} HP will annihilate you!"),
                        new Reproduce("Henchman of Oryx", 8, 3, 1, 10000),
                        new HpLessTransition(.3, "prepareRage")
                    ),
                    new State("prepareRage",
                        new Follow(.3, 15, 3),
                        new Taunt("Can't... keep... henchmen... alive... anymore! ARGHHH!!!"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Shoot(25, 30, fixedAngle: 0, projectileIndex: 7, coolDown: 4000, coolDownOffset: 4000),
                        new Shoot(25, 30, fixedAngle: 30, projectileIndex: 8, coolDown: 4000, coolDownOffset: 4000),
                        new TimedTransition(10000, "beforeRage")
                    ),
                    new State("beforeRage",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Follow(.5, 15, 3),
                        new Shoot(25, 30, projectileIndex: 7, coolDown: 3000, coolDownOffset: 1000),
                        new Shoot(25, 30, projectileIndex: 8, coolDown: 3000, coolDownOffset: 1500),
                        new Shoot(25, projectileIndex: 0, count: 8, shootAngle: 45, coolDown: 1000, coolDownOffset: 1500),
                        new Shoot(25, projectileIndex: 1, count: 3, shootAngle: 10, coolDown: 1000, coolDownOffset: 1000),
                        new Shoot(25, projectileIndex: 2, count: 3, shootAngle: 10, predictive: 0.2, coolDown: 1000,
                            coolDownOffset: 1000),
                        new Shoot(25, projectileIndex: 3, count: 2, shootAngle: 10, predictive: 0.4, coolDown: 1000,
                            coolDownOffset: 1000),
                        new Shoot(25, projectileIndex: 4, count: 3, shootAngle: 10, predictive: 0.6, coolDown: 1000,
                            coolDownOffset: 1000),
                        new Shoot(25, projectileIndex: 5, count: 2, shootAngle: 10, predictive: 0.8, coolDown: 1000,
                            coolDownOffset: 1000),
                        new Shoot(25, projectileIndex: 6, count: 3, shootAngle: 10, predictive: 1, coolDown: 1000,
                            coolDownOffset: 1000),
                        new Shoot(25, 30, fixedAngle: 0, projectileIndex: 7, coolDown: 4000, coolDownOffset: 4000),
                        new Shoot(25, 30, fixedAngle: 30, projectileIndex: 8, coolDown: 4000, coolDownOffset: 4000),
                        new TimedTransition(10000, "rage")
                        ),
                    new State("rage",
                        new Prioritize(
                            new Follow(0.8, 15, 3),
                            new Shoot(25, 30, projectileIndex: 7, coolDown: 90000001, coolDownOffset: 8000),
                            new Shoot(25, 30, projectileIndex: 8, coolDown: 90000001, coolDownOffset: 8500),
                            new Shoot(25, projectileIndex: 0, count: 8, shootAngle: 45, coolDown: 1000, coolDownOffset: 1500),
                            new Shoot(25, projectileIndex: 1, count: 3, shootAngle: 10, coolDown: 1000, coolDownOffset: 1000),
                            new Shoot(25, projectileIndex: 2, count: 3, shootAngle: 10, predictive: 0.2, coolDown: 1000,
                                coolDownOffset: 1000),
                            new Shoot(25, projectileIndex: 3, count: 2, shootAngle: 10, predictive: 0.4, coolDown: 1000,
                                coolDownOffset: 1000),
                            new Shoot(25, projectileIndex: 4, count: 3, shootAngle: 10, predictive: 0.6, coolDown: 1000,
                                coolDownOffset: 1000),
                            new Shoot(25, projectileIndex: 5, count: 2, shootAngle: 10, predictive: 0.8, coolDown: 1000,
                                coolDownOffset: 1000),
                            new Shoot(25, projectileIndex: 6, count: 3, shootAngle: 10, predictive: 1, coolDown: 1000,
                                coolDownOffset: 1000)
                            ),
                        new State("rageTaunt",
                            new Taunt(1, 6000, "Puny mortals! My {HP} HP will annihilate you!"),
                            new PlayerWithinTransition(5, "rageToss")
                            ),
                        new State("rageToss",
                            new TossObject("Monstrosity Scarab", 7, 0, coolDown: 1500),
                            new TossObject("Monstrosity Scarab", 7, 60, coolDown: 1500),
                            new TossObject("Monstrosity Scarab", 7, 120, coolDown: 1500),
                            new TossObject("Monstrosity Scarab", 7, 180, coolDown: 1500),
                            new TossObject("Monstrosity Scarab", 7, 240, coolDown: 1500),
                            new TossObject("Monstrosity Scarab", 7, 300, coolDown: 1500),
                            new TimedTransition(10000, "rageTaunt")
                            )
                    )
                ),
                new MostDamagers(15,
                    new ItemLoot("Potion of Vitality", 0.5),
                    new ItemLoot("Potion of Attack", 0.5),
                    new ItemLoot("Potion of Defense", 0.5),
                    new ItemLoot("Potion of Wisdom", 0.5)
                    ),
                new MostDamagers(10,
                    new ItemLoot("Greater Potion of Vitality", 0.15),
                    new ItemLoot("Greater Potion of Attack", 0.15),
                    new ItemLoot("Greater Potion of Defense", 0.15),
                    new ItemLoot("Greater Potion of Wisdom", 0.15),
                    new TierLoot(10, ItemType.Weapon, goodloot*2),
                    new TierLoot(11, ItemType.Weapon, goodloot*2),
                    new TierLoot(12, ItemType.Weapon, greatloot*2),
                    new TierLoot(5, ItemType.Ability, goodloot*2),
                    new TierLoot(6, ItemType.Ability, greatloot*2),
                    new TierLoot(11, ItemType.Armor, normalloot*2),
                    new TierLoot(12, ItemType.Armor, goodloot*2),
                    new TierLoot(13, ItemType.Armor, greatloot*2),
                    new TierLoot(5, ItemType.Ring, goodloot*2),
                    new OnlyOne(
                        new ItemLoot("Blade of the Mad God", blackbag*2),
                        new ItemLoot("Ancient Almandine Armor of Anger", blackbag*2),
                        new ItemLoot("Ancient Almandine Ring of Wrath", blackbag*2),
                        new ItemLoot("Arch-Blade of the Mad God", superbag*2),
                        new ItemLoot("Royal Almandine Armor of Anger", superbag*2),
                        new ItemLoot("Ancient Almandine Crown of Wrath", superbag*2)
                        )
                )
            )
            .Init("Henchman of Oryx",
                new State(
                    new Prioritize(
                        new Protect(1.2, "Oryx the Mad God 2", 15, 12, 2),
                        new Orbit(0.4, 2, target: "Oryx the Mad God 2", radiusVariance: 1),
                        new Follow(.3, 8, 3, coolDown: 0)
                        ),
                    new Reproduce("Aberrant of Oryx", 8, 1, 1, 15000),
                    new Reproduce("Abomination of Oryx", 8, 2, 1, 15000),
                    new Reproduce("Monstrosity of Oryx", 8, 1, 1, 15000),
                    new Reproduce("Vintner of Oryx", 8, 2, 1, 15000),
                    //new Spawn("Bile of Oryx", 1, 1, 10000),
                    new Shoot(8, projectileIndex: 0, predictive: 1, coolDown: 1000),
                    new Shoot(8, projectileIndex: 1, count: 3, shootAngle: 20, coolDown: 1500, coolDownOffset: 500)
                    )
            )

        .Init("Aberrant of Oryx",
            new State(
                new Protect(0.8, "Henchman of Oryx", 15, 8, 2),
                new Wander(0.4),
                new TossObject("Aberrant Blaster", 14, coolDown: 1500)
                )
            )
        .Init("Aberrant Blaster",
            new State(
                new State("searching",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new PlayerWithinTransition(8, "creeping")
                    ),
                new State("creeping",
                    new UnsetConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Shoot(radius: 10,  projectileIndex: 0, count: 6, predictive: 1),
                    new Decay(500)
                    )
                )
            )
        .Init("Abomination of Oryx",
            new State(
                new State("start",
                    new Protect(0.8, "Henchman of Oryx", 15, 12, 2),
                    new Wander(0.6),
                    new Charge(2, 10, 3000),
                    new Follow(0.8, range: 0),
                    new Shoot(10, 3, 10, 0, coolDown: 3000),
                    new Shoot(10, 5, 10, 1, coolDown: 3000, coolDownOffset: 100),
                    new Shoot(10, 7, 10, 2, coolDown: 3000, coolDownOffset: 200),
                    new Shoot(10, 5, 10, 3, coolDown: 3000, coolDownOffset: 300),
                    new Shoot(10, 3, 10, 4, coolDown: 3000, coolDownOffset: 400)
                    )
                )
            )
        .Init("Monstrosity of Oryx",
            new State(
                new Protect(0.8, "Henchman of Oryx", 15, 8, 2),
                new Wander(0.4),
                new TossObject("Monstrosity Scarab", 10, coolDown: 1000)
                )
            )
        .Init("Vintner of Oryx",
            new State(
                new Wander(0.4),
                new StayBack(1.2, 8),
                new Shoot(10, 1, 10, 0, coolDown: 1000, predictive: 1)
                )
            )
        .Init("Bile of Oryx",
            new State(
                new Wander(0.4),
                new Spawn("Purple Goo", 8, 1, 1000)
                )
            )
        .Init("Purple Goo",
            new State(
                new Shoot(2, 1, 10, 0, coolDown: 500, predictive: 1),
                new ChangeSize(-10, 0),
                new Decay(6000)
                )
            )

            .Init("Monstrosity Scarab",
                new State(
                    new State("searching",
                        new Prioritize(
                            new Follow(3, range: 0)
                            ),
                        new PlayerWithinTransition(2, "creeping")//,
                        //new TimedTransition(5000, "creeping")
                        ),
                    new State("creeping",
                        new Shoot(3, 20, 18, fixedAngle: 0),
                        new Decay(0)
                        )
                    )
            )
            .Init("Oryx the Mad God 1",
                new State(
                    new DropPortalOnDeath("Locked Wine Cellar Portal", 100, PortalDespawnTimeSec: 120),
                    new HpLessTransition(.2, "rage"),
                    new State("Slow",
                        new Taunt("Fools! I still have {HP} hitpoints!"),
                        new Spawn("Minion of Oryx", 5, 0, 350000),
                        new Reproduce("Minion of Oryx", 10, 5, 1500),
                        new Shoot(25, 4, 10, 4, coolDown: 1000),
                        new TimedTransition(20000, "Dance 1")
                        ),
                    new State("Dance 1",
                        new Flash(0xf389E13, 0.5, 60),
                        new Taunt("BE SILENT!!!"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Shoot(50, 8, projectileIndex: 6, coolDown: 700, coolDownOffset: 200),
                        new TossObject("Ring Element", 9, 24, 320000),
                        new TossObject("Ring Element", 9, 48, 320000),
                        new TossObject("Ring Element", 9, 72, 320000),
                        new TossObject("Ring Element", 9, 96, 320000),
                        new TossObject("Ring Element", 9, 120, 320000),
                        new TossObject("Ring Element", 9, 144, 320000),
                        new TossObject("Ring Element", 9, 168, 320000),
                        new TossObject("Ring Element", 9, 192, 320000),
                        new TossObject("Ring Element", 9, 216, 320000),
                        new TossObject("Ring Element", 9, 240, 320000),
                        new TossObject("Ring Element", 9, 264, 320000),
                        new TossObject("Ring Element", 9, 288, 320000),
                        new TossObject("Ring Element", 9, 312, 320000),
                        new TossObject("Ring Element", 9, 336, 320000),
                        new TossObject("Ring Element", 9, 360, 320000),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Grenade(4, 150, 12, coolDown: 2000),
                        //new Grenade(radius: 4, damage: 150, fixedAngle: new Random().Next(0, 359), range: 5, coolDown: 2000),
                        //new Grenade(radius: 4, damage: 150, fixedAngle: new Random().Next(0, 359), range: 5, coolDown: 2000),
                        //new Grenade(radius: 4, damage: 150, fixedAngle: new Random().Next(0, 359), range: 5, coolDown: 2000),
                        new TimedTransition(25000, "artifacts")
                        ),
                    new State("artifacts",
                        new Taunt("My Artifacts will protect me!"),
                        new Flash(0xf389E13, 0.5, 60),
                        new Shoot(50, 3, projectileIndex: 9, coolDown: 1500, coolDownOffset: 200),
                        new Shoot(50, 10, projectileIndex: 8, coolDown: 2000, coolDownOffset: 200),
                        new Shoot(50, 10, projectileIndex: 7, coolDown: 500, coolDownOffset: 200),
                        new Grenade(4, 150, 12, coolDown: 2000),
                        //Inner Elements
                        new TossObject("Guardian Element 1", 1, 0, 90000001, 1000),
                        new TossObject("Guardian Element 1", 1, 90, 90000001, 1000),
                        new TossObject("Guardian Element 1", 1, 180, 90000001, 1000),
                        new TossObject("Guardian Element 1", 1, 270, 90000001, 1000),
                        new TossObject("Guardian Element 2", 9, 0, 90000001, 1000),
                        new TossObject("Guardian Element 2", 9, 90, 90000001, 1000),
                        new TossObject("Guardian Element 2", 9, 180, 90000001, 1000),
                        new TossObject("Guardian Element 2", 9, 270, 90000001, 1000),
                        new TimedTransition(25000, "gaze")
                        ),

                    #region gaze
                    new State("gaze",
                        new Taunt("All who looks upon my face shall die."),
                        new Shoot(count: 2, coolDown: 1000, projectileIndex: 1, radius: 7, shootAngle: 10,
                            coolDownOffset: 800),
                        new Grenade(4, 150, 12, coolDown: 2000),
                        new TimedTransition(10000, "Dance 2")
                        #endregion gaze

                        ),

                    #region Dance 2
                    new State("Dance 2",
                        new Flash(0xf389E13, 0.5, 60),
                        new Taunt("Time for more dancing!"),
                        new Shoot(50, 8, projectileIndex: 6, coolDown: 700, coolDownOffset: 200),
                        new TossObject("Ring Element", 9, 24, 320000),
                        new TossObject("Ring Element", 9, 48, 320000),
                        new TossObject("Ring Element", 9, 72, 320000),
                        new TossObject("Ring Element", 9, 96, 320000),
                        new TossObject("Ring Element", 9, 120, 320000),
                        new TossObject("Ring Element", 9, 144, 320000),
                        new TossObject("Ring Element", 9, 168, 320000),
                        new TossObject("Ring Element", 9, 192, 320000),
                        new TossObject("Ring Element", 9, 216, 320000),
                        new TossObject("Ring Element", 9, 240, 320000),
                        new TossObject("Ring Element", 9, 264, 320000),
                        new TossObject("Ring Element", 9, 288, 320000),
                        new TossObject("Ring Element", 9, 312, 320000),
                        new TossObject("Ring Element", 9, 336, 320000),
                        new TossObject("Ring Element", 9, 360, 320000),
                        new TimedTransition(1000, "Dance2, 1")
                        ),
                    new State("Dance2, 1",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Shoot(0, projectileIndex: 8, count: 4, shootAngle: 90, fixedAngle: 0, coolDown: 170),
                        new TimedTransition(200, "Dance2, 2")
                        ),
                    new State("Dance2, 2",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Shoot(0, projectileIndex: 8, count: 4, shootAngle: 90, fixedAngle: 30, coolDown: 170),
                        new TimedTransition(200, "Dance2, 3")
                        ),
                    new State("Dance2, 3",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Shoot(0, projectileIndex: 8, count: 4, shootAngle: 90, fixedAngle: 15, coolDown: 170),
                        new TimedTransition(200, "Dance2, 4")
                        ),
                    new State("Dance2, 4",
                        new Shoot(0, projectileIndex: 8, count: 4, shootAngle: 90, fixedAngle: 45, coolDown: 170),
                        new TimedTransition(200, "Dance2, 1")
                        ),

                    #endregion Dance 2
                    new State("rage",
                        new ChangeSize(10, 200),
                        new Taunt(.3, "I HAVE HAD ENOUGH OF YOU!!!",
                            "ENOUGH!!!",
                            "DIE!!!"),
                        new Spawn("Minion of Oryx", 10, 0, 350000),
                        new Reproduce("Minion of Oryx", 10, 5, 1500),
                        new Shoot(count: 2, coolDown: 1500, projectileIndex: 1, radius: 7, shootAngle: 10,
                            coolDownOffset: 2000),
                        new Shoot(count: 5, coolDown: 1500, projectileIndex: 16, radius: 7, shootAngle: 10,
                            coolDownOffset: 2000),
                        new Follow(0.85, range: 1, coolDown: 0),
                        new Flash(0xfFF0000, 0.5, 9000001)
                        )
                    ),
                new MostDamagers(15,
                    new ItemLoot("Potion of Attack", 0.85),
                    new ItemLoot("Potion of Defense", 0.85),
                    new TierLoot(9, ItemType.Armor, mediumloot*2),
                    new TierLoot(10, ItemType.Armor, normalloot*2),
                    new TierLoot(11, ItemType.Armor, normalloot*2),
                    new TierLoot(12, ItemType.Armor, goodloot*2),
                    new TierLoot(9, ItemType.Weapon, normalloot*2),
                    new TierLoot(10, ItemType.Weapon, goodloot*2),
                    new TierLoot(11, ItemType.Weapon, goodloot*2),
                    new TierLoot(4, ItemType.Ability, normalloot*2),
                    new TierLoot(5, ItemType.Ability, goodloot*2),
                    new TierLoot(3, ItemType.Ring, mediumloot*2),
                    new TierLoot(4, ItemType.Ring, normalloot*2),
                    new TierLoot(5, ItemType.Ring, goodloot*2)
                    ),
                new MostDamagers(3,
                    new OnlyOne(
                        new ItemLoot("Sword of the Mad God", awesomeloot),
                        new ItemLoot("Onyx Shield of the Mad God", awesomeloot),
                        new ItemLoot("Almandine Armor of Anger", awesomeloot),
                        new ItemLoot("Almandine Ring of Wrath", awesomeloot)
                        )
                    )
            )
            .Init("Ring Element",
                new State(
                    new State(
                        new Shoot(50, 12, projectileIndex: 0, coolDown: 700, coolDownOffset: 200),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new TimedTransition(20000, "Despawn")
                        ),
                    new State("Despawn", //new Decay(time:0)
                        new Suicide()
                        )
                    )
            )
            .Init("Minion of Oryx",
                new State(
                    new Wander(0.4),
                    new Shoot(3, 3, 10, 0, coolDown: 1000),
                    new Shoot(3, 3, projectileIndex: 1, shootAngle: 10, coolDown: 1000)
                    ),
                new TierLoot(7, ItemType.Weapon, 0.2),
                new ItemLoot("Health Potion", 0.03)
            )
            .Init("Guardian Element 1",
                new State(
                    new State(
                        new Orbit(1, 1, target: "Oryx the Mad God 1", radiusVariance: 0),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Shoot(25, 3, 10, 0, coolDown: 1000),
                        new TimedTransition(10000, "Grow")
                        ),
                    new State("Grow",
                        new ChangeSize(100, 200),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Orbit(1, 1, target: "Oryx the Mad God 1", radiusVariance: 0),
                        new Shoot(3, 1, 10, 0, coolDown: 700),
                        new TimedTransition(10000, "Despawn")
                        ),
                    new State("Despawn",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Orbit(1, 1, target: "Oryx the Mad God 1", radiusVariance: 0),
                        new ChangeSize(100, 100),
                        new Suicide()
                        )
                    )
            )
            .Init("Guardian Element 2",
                new State(
                    new State(
                        new Orbit(1.3, 9, target: "Oryx the Mad God 1", radiusVariance: 0),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Shoot(25, 3, 10, 0, coolDown: 1000),
                        new TimedTransition(20000, "Despawn")
                        ),
                    new State("Despawn", new Suicide()
                        )
                    )
            );
    }
}
