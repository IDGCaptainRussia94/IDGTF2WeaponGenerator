using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDGTF2WeaponGenerator.Classes
{
    public interface IWeaponStat
    {


    }

    public abstract class WeaponStat : WeaponValuesBase
    {
        public int modiferChance = 25;
        public virtual bool dumbModifer => false;
        public GeneratedWeapon weapon;
        public WeaponStatModifer modifer;
        public Vector2 textSize;
        public int cost = 0;

        public virtual bool InvolvesDamage => false;
        public virtual bool InvolvesEating => false;
        public virtual bool InvolvesOnKill => false;
        public virtual bool InvolvesRage => false;
        public virtual bool InvolvesPassive => true;

        public virtual bool GoodStat => true;
        public virtual string DisplayString => "";
        public string TrueDisplayString => DisplayString + (modifer != default ? modifer.TrueDisplayString : "");

        public virtual bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return true;
        }

        public virtual void OnInit()
        {

        }

        public virtual void LateTweatValues()
        {

        }

        public virtual void ApplyModifiers()
        {
            if (TF2WeaponGenerator.random.Next(100) < modiferChance)
            {
                List<WeaponStatModifer> modifers = TF2WeaponGenerator.weaponSystem.allStatModifer.Where(testby => testby.CanApplyModifer(this) && testby.ModiferChance).OrderBy(testby => TF2WeaponGenerator.random.Next()).ToList();
                if (modifers.Count > 0)
                {
                    WeaponStatModifer locmodifer = modifers[0];
                    modifer = locmodifer;
                    locmodifer.ApplyModifer(this);

                    /*
                    if (locmodifer.applyFurtherModifier)
                    {
                        
                        modifers = TF2WeaponGenerator.weaponSystem.allStatModifer.Where(testby => testby.CanApplyModifer(this) && testby.ModiferChance).OrderBy(testby => TF2WeaponGenerator.random.Next()).ToList();
                        if (modifers.Count > 0)
                        {

                            WeaponStatModifer locmodifer2 = modifers[0];
                            modifer = locmodifer;
                            locmodifer.ApplyModifer(this);
                        }
                    }
                    */

                }

                
            }


        }

        public void ApplyStats(GeneratedWeapon weaponSoFar)
        {
            modifer = default;
            weapon = weaponSoFar;
            OnInit();
            LateTweatValues();
        }

        public WeaponStat()
        {
            textSize = default;
        }
    }

    class WeaponStatJoke1 : WeaponStat, IWeaponStat
    {
        public override bool dumbModifer => true;
        public override string DisplayString => "Gives you sick gamer clout";

        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return base.CanApplyStat(weaponSoFar) && TF2WeaponGenerator.random.Next(100)<25;
        }

        public override void OnInit()
        {
            modiferChance = 10;
            cost = 50;
        }
    }

    class WeaponStatJoke2 : WeaponStatJoke1, IWeaponStat
    {
        public override bool dumbModifer => true;
        public override string DisplayString => "Instantly wins you the match";
        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return base.CanApplyStat(weaponSoFar) && TF2WeaponGenerator.random.Next(100) < 20;
        }

        public override void OnInit()
        {
            modiferChance = 10;
            cost = 750;
        }
    }

    class WeaponStatMovementBoost : WeaponStat, IWeaponStat
    {
        protected int movementBoost;
        public override string DisplayString => "+"+movementBoost + "% movement speed";

        public override void OnInit()
        {
            modiferChance = 10;
            cost = 200;
            movementBoost = TF2WeaponGenerator.random.Next(0, TF2WeaponGenerator.random.Next(20, 101));
        }
    }

    class WeaponStatMovementCloak : WeaponStatMovementBoost, IWeaponStat
    {
        public override string DisplayString => "+" + movementBoost + "% cloak meter";
        public override void OnInit()
        {
            modiferChance = 10;
            cost = 200;
            movementBoost = TF2WeaponGenerator.random.Next(0, TF2WeaponGenerator.random.Next(20, 101));
        }
        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return ((weaponSoFar.weaponClass.ClassType == (int)ClassTypes.Spy));
        }
    }

    class WeaponStatMovementMetal: WeaponStatMovementBoost, IWeaponStat
    {
        public override string DisplayString => "+" + movementBoost + "% metal on wearer";
        public override void OnInit()
        {
            modiferChance = 10;
            cost = 200;
            movementBoost = TF2WeaponGenerator.random.Next(0, TF2WeaponGenerator.random.Next(20, 101));
        }
        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return ((weaponSoFar.weaponClass.ClassType == (int)ClassTypes.Engineer));
        }
    }

    class WeaponStatMovementStealthVis : WeaponStatMovementBoost, IWeaponStat
    {
        public override string DisplayString => "Detects nearby Cloaked Spies";

        public override void OnInit()
        {
            modiferChance = 10;
            cost = 400;
        }
        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return weaponSoFar.namedType.passiveWeapon || TF2WeaponGenerator.random.Next(100) < 20;
        }
    }

    class WeaponStatMovementDisguiseVis : WeaponStatMovementBoost, IWeaponStat
    {
        public override string DisplayString => "Detects nearby Disuised Spies";

        public override void OnInit()
        {
            modiferChance = 10;
            cost = 400;
        }
        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return (weaponSoFar.weaponSlot.SlotType == (int)SlotTypes.DisguiseKit) || TF2WeaponGenerator.random.Next(100) < 20;
        }
    }

    class WeaponStatMovementDisguise2 : WeaponStatMovementBoost, IWeaponStat
    {
        protected string[] gamemodes = { "Disguising feigns your death", "Disguising instantly cloaks you","While disguised, you can see the locations of enemies","Can enter enemy respawn rooms" };
        protected int theChosenValue = 0;
        public override string DisplayString => gamemodes[theChosenValue];

        public override void OnInit()
        {
            theChosenValue = TF2WeaponGenerator.random.Next(gamemodes.Length);
            modiferChance = 10;
            cost = 250;
        }
        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return (weaponSoFar.weaponSlot.SlotType == (int)SlotTypes.DisguiseKit);
        }
    }


    class WeaponStatFiringSpeed : WeaponStatMovementBoost, IWeaponStat
    {
        public override string DisplayString => "+" + movementBoost + "% firing Speed";

        public override void OnInit()
        {
            cost = 250;
            movementBoost = TF2WeaponGenerator.random.Next(0, TF2WeaponGenerator.random.Next(20, 101));
        }
        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return !weaponSoFar.namedType.passiveWeapon && weaponSoFar.namedType.canDoDamage;
        }
    }
    class WeaponStatAmmoCapacity : WeaponStatMovementBoost, IWeaponStat
    {
        public override string DisplayString => "+" + movementBoost + "% clip size";

        public override void OnInit()
        {
            cost = 250;
            movementBoost = TF2WeaponGenerator.random.Next(0, TF2WeaponGenerator.random.Next(20, 101));
        }
        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return !weaponSoFar.namedType.passiveWeapon && weaponSoFar.namedType.canDoDamage;
        }
    }

    class WeaponStatJumpBoost : WeaponStatMovementBoost, IWeaponStat
    {
        public override string DisplayString => "+" + movementBoost + "% jump height";

        public override void OnInit()
        {
            modiferChance = 10;
            cost = (weapon.weaponClass.ClassType == (int)ClassTypes.Scout ? 100 : 150);
            movementBoost = TF2WeaponGenerator.random.Next(0, TF2WeaponGenerator.random.Next(20, GoodStat ? 201 : 101));
        }
    }

    class WeaponStatExtraHealth : WeaponStatMovementBoost, IWeaponStat
    {
        public override string DisplayString => "+" + movementBoost + " health on wearer";

        public override void OnInit()
        {
            cost = 300;
            int maxHP = (weapon.weaponClass.ClassType == (int)ClassTypes.Heavy ? 200 : 100);
            movementBoost = TF2WeaponGenerator.random.Next(0, TF2WeaponGenerator.random.Next(20, maxHP));
        }
    }

    class WeaponStatDamageBoost : WeaponStat, IWeaponStat
    {
        public override bool InvolvesDamage => true;

        protected int damageBoost;
        public override string DisplayString => "+"+damageBoost+"% damage";

        public override void OnInit()
        {
            cost = 250;
            damageBoost = TF2WeaponGenerator.random.Next(0, TF2WeaponGenerator.random.Next(25, 101));
        }

        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return weaponSoFar.namedType.canDoDamage;
        }
    }

    class WeaponStatBleed : WeaponStatDamageBoost, IWeaponStat
    {
        public override string DisplayString => "+" + damageBoost + " seconds of bleed on hit";
        public override bool InvolvesPassive => false;

        public override void OnInit()
        {
            cost = 200;
            damageBoost = TF2WeaponGenerator.random.Next(5, TF2WeaponGenerator.random.Next(10, 60));
        }

        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return weaponSoFar.namedType.canDoDamage;
        }
    }

    class WeaponStatFallDamage : WeaponStatJumpBoost, IWeaponStat
    {
        public override string DisplayString => "-" + movementBoost + "% decreased fall damage";
    }

    class WeaponStatKnockback : WeaponStatJumpBoost, IWeaponStat
    {
        public override string DisplayString => "+" + movementBoost + "% reduced knockback taken";
    }

    class WeaponStatMiniCritToCrit : WeaponStat, IWeaponStat
    {
        public override bool InvolvesDamage => true;
        public override string DisplayString => "Mini-crits become crits";

        public override bool InvolvesPassive => false;

        public override void OnInit()
        {
            cost = 350;
        }

        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return weaponSoFar.namedType.canDoDamage;
        }
    }

    class WeaponStatPlague : WeaponStatMiniCritToCrit, IWeaponStat
    {
        public override string DisplayString => "Your attacks spread the Plague on hit";
        public override void OnInit()
        {
            cost = 500;
        }
    }

    class WeaponStatResistance : WeaponStat, IWeaponStat
    {
        public override bool InvolvesDamage => false;
        public override string DisplayString => DisplayString2+(weapon.namedType.canEat ? " when eaten" : " passively");
        public virtual string DisplayString2 => "Grants the Resistance Powerup";
        public override void OnInit()
        {
            cost = 500;
        }
        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return !weaponSoFar.namedType.canDoDamage;
        }
    }

    class WeaponStatStrength : WeaponStatResistance, IWeaponStat
    {
        public override bool InvolvesDamage => false;
        public override string DisplayString2 => "Grants the Strength Powerup";
        public override void OnInit()
        {
            cost = 500;
        }
    }


    class WeaponStatReflect : WeaponStatResistance, IWeaponStat
    {
        public override bool InvolvesDamage => false;
        public override string DisplayString2 => "Grants the Reflect Powerup";
        public override void OnInit()
        {
            cost = 500;
        }
    }

    class WeaponStatMiniAlwaysCritWithCondition : WeaponStat, IWeaponStat
    {
        public override bool InvolvesDamage => true;
        public override string DisplayString => "Guaranteed crits";
        public override bool InvolvesPassive => false;

        public override void OnInit()
        {
            modiferChance = 1000;
            cost = 500;
        }

        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return weaponSoFar.namedType.canDoDamage;
        }
    }

    class WeaponStatApplySlowingWithCondition : WeaponStatDamageBoost, IWeaponStat
    {
        public override bool InvolvesDamage => true;
        public override bool InvolvesPassive => false;
        public override string DisplayString => "Slow on hit for "+ damageBoost+" seconds";

        public override void OnInit()
        {
            modiferChance = 1000;
            cost = 300;
            damageBoost = TF2WeaponGenerator.random.Next(5, TF2WeaponGenerator.random.Next(20, 50));
        }

        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return weaponSoFar.namedType.canDoDamage;
        }
    }


    class WeaponStatEatToHeal : WeaponStatDamageBoost, IWeaponStat
    {
        public override string DisplayString => "When eaten, heals "+damageBoost+" Health";
        public override bool InvolvesPassive => false;
        public override bool InvolvesEating => true;

        public override void OnInit()
        {
            cost = 150;
            damageBoost = TF2WeaponGenerator.random.Next(10, TF2WeaponGenerator.random.Next(50, 200));
        }

        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return weaponSoFar.namedType.canEat;
        }
    }

    class WeaponStatEatToSpeed : WeaponStatEatToHeal, IWeaponStat
    {
        public override string DisplayString => "When eaten, grants a speed boost for " + damageBoost + " seconds";

        public override void OnInit()
        {
            cost = 150;
            damageBoost = TF2WeaponGenerator.random.Next(5, TF2WeaponGenerator.random.Next(20, 50));
        }
    }

    class WeaponStatFasterCharge : WeaponStatEatToHeal, IWeaponStat
    {
        public override string DisplayString => "+"+damageBoost + "% faster "+(weapon.namedType.hasScopeMeter ? "Scope Meter" : "Recharge") +" rate";

        public override void OnInit()
        {
            cost = 150;
            damageBoost = TF2WeaponGenerator.random.Next(5, TF2WeaponGenerator.random.Next(25, 100));
        }
        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return weaponSoFar.namedType.hasScopeMeter || weaponSoFar.namedType.canEat || weaponSoFar.namedType.canShieldBash;
        }
    }
    class WeaponStatOnKillCondition : WeaponStatDamageBoost, IWeaponStat
    {
        public override bool InvolvesOnKill => true;
        public override bool InvolvesDamage => true;
        public override bool InvolvesPassive => false;
        public override string DisplayString => "On kill: ";

        public override void OnInit()
        {
            modiferChance = 1000;
            cost = 250;
        }

        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return weaponSoFar.namedType.canDoDamage;
        }
    }

    class WeaponStatOnHeadshotCondition : WeaponStatOnKillCondition, IWeaponStat
    {
        public override string DisplayString => "On headshot: ";

        public override void OnInit()
        {
            modiferChance = 1000;
            cost = 150;
        }

        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return weaponSoFar.namedType.canHeadShot || weaponSoFar.gainedHeadshot;
        }
    }

    class WeaponStatOnBackstabCondition : WeaponStatOnKillCondition, IWeaponStat
    {
        public override string DisplayString => "On backstab: ";

        public override void OnInit()
        {
            modiferChance = 1000;
            cost = 150;
        }

        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return weaponSoFar.namedType.canBackstab || weaponSoFar.gainedBackstab;
        }
    }

    class WeaponStatCanBackstab : WeaponStatDamageBoost, IWeaponStat
    {
        public override string DisplayString => "Can backstab";
        public override bool InvolvesPassive => false;

        public override void OnInit()
        {
            cost = 300;
            weapon.gainedBackstab = true;
        }

        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return weaponSoFar.namedType.limitedReach;
        }
    }

    class WeaponStatCanHeadshot : WeaponStatDamageBoost, IWeaponStat
    {
        public override string DisplayString => "Can headshot";
        public override bool InvolvesPassive => false;

        public override void OnInit()
        {
            cost = 400;
            weapon.gainedHeadshot = true;
        }

        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return weaponSoFar.namedType.canDoDamage && !weaponSoFar.namedType.limitedReach;
        }
    }

    class WeaponStatPowerPunch : WeaponStatDamageBoost, IWeaponStat
    {
        public override string DisplayString => "Knockout Powerup in effect";
        public override bool InvolvesPassive => false;

        public override void OnInit()
        {
            cost = 300;
        }

        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return weaponSoFar.namedType.limitedReach;
        }
    }

    class WeaponStatRage : WeaponStatEatToHeal, IWeaponStat
    {
        protected string[] gamemodes = { "Slows all enemies nearby", "you kill yourself", "Grants everyone mini-crits", "Prevents dying when above 1 HP","Summons the Horseless Headless Horseman", "Summons Merasmus", "Summons MONOCULUS","Activates Truce for a minute","Instantly picks up the objective","Kills all enemies on the map","Grants all allies a spell pickup", "Gives you a rare spell pickup", "grants your team jarate swimming", "Gives you the plague Powerup", "Gives you the King Powerup","Does nothing","Summons Rain of Fire above nearby enemies","Cloaks all allies around you","Teleports you to the respawn room","Gives you several seconds of Crits"};
        protected string[] gamemodes2 = { "Rage", "Mmmmpphh","Charge" };
        protected string[] gamemodes3 = { "Attack enemies to build up ","Over time build up ","While running you build up ","By taking damage you build up ","By taunting you build "};
        protected string[] gamemodes4 = { "When acivated","On taking damage","When full","At random"};
        protected int theChosenValue = 0;
        protected int theChosenValue2 = 1;
        protected int theChosenValue3 = 2;
        protected int theChosenValue4 = 3;
        public override bool InvolvesRage => true;

        public override string DisplayString => gamemodes3[theChosenValue3] + gamemodes2 [theChosenValue2] + "\n"+ gamemodes4[theChosenValue4]+": " + gamemodes[theChosenValue];

        public override void OnInit()
        {
            cost = 150;
            damageBoost = TF2WeaponGenerator.random.Next(5, TF2WeaponGenerator.random.Next(25, 100));
            theChosenValue = TF2WeaponGenerator.random.Next(gamemodes.Length);
            theChosenValue2 = TF2WeaponGenerator.random.Next(gamemodes2.Length);
            theChosenValue3 = TF2WeaponGenerator.random.Next(gamemodes3.Length);
            theChosenValue4 = TF2WeaponGenerator.random.Next(gamemodes4.Length);

        }
        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return weaponSoFar.namedType.canBuildRage || (weaponSoFar.namedType.canDoDamage && TF2WeaponGenerator.random.Next(100)<20);
        }
    }


    #region DamageResists
    class WeaponStatDamageResist : WeaponStat
    {
        public override bool InvolvesDamage => false;
        protected virtual string DamageResistType => "";
        protected int resistBoost;
        public override string DisplayString => "+" + resistBoost + "% " + DamageResistType + " resistance";

        public override void OnInit()
        {
            cost = 250;
            resistBoost = TF2WeaponGenerator.random.Next(0,TF2WeaponGenerator.random.Next(25, 101));
        }
    }

    class WeaponStatDamageResistMelee : WeaponStatDamageResist, IWeaponStat
    {
        public override bool InvolvesDamage => true && TF2WeaponGenerator.random.Next(100) < 40;
        protected override string DamageResistType => "Melee";
    }
    
    class WeaponStatDamageResistExplosive : WeaponStatDamageResist, IWeaponStat
    {

        protected override string DamageResistType => "Explosive";
    }
    class WeaponStatDamageResistBullet : WeaponStatDamageResist, IWeaponStat
    {

        protected override string DamageResistType => "Bullet";
    }
    class WeaponStatDamageResistFire : WeaponStatDamageResist, IWeaponStat
    {
        protected override string DamageResistType => "Fire";
    }
    class WeaponStatDamageResistCrit : WeaponStatDamageResist, IWeaponStat
    {
        protected override string DamageResistType => "Crit";
    }
    #endregion
    


    //Downsides











    #region DamageVuln
    class WeaponStatDamageVulnerable : WeaponStatDamageResist
    {
        public override bool GoodStat => false;
        public override string DisplayString => "+" + resistBoost + "% " + DamageResistType + " vulnerability";

        public override void OnInit()
        {
            cost = 250;
            resistBoost = TF2WeaponGenerator.random.Next(0, TF2WeaponGenerator.random.Next(25, 101));
        }
    }

    class WeaponStatDamageVulnerableMelee : WeaponStatDamageVulnerable, IWeaponStat
    {
        public override bool InvolvesDamage => true && TF2WeaponGenerator.random.Next(100) < 40;
        protected override string DamageResistType => "Melee";
    }

    class WeaponStatDamageVulnerableExplosive : WeaponStatDamageVulnerable, IWeaponStat
    {

        protected override string DamageResistType => "Explosive";
    }
    class WeaponStatDamageVulnerableBullet : WeaponStatDamageVulnerable, IWeaponStat
    {

        protected override string DamageResistType => "Bullet";
    }
    class WeaponStatDamageVulnerableFire : WeaponStatDamageVulnerable, IWeaponStat
    {
        protected override string DamageResistType => "Fire";
    }
    class WeaponStatDamageVulnerableCrit : WeaponStatDamageVulnerable, IWeaponStat
    {
        protected override string DamageResistType => "Crit";
    }
    #endregion

    class WeaponStatDance : WeaponStatBleed, IWeaponStat
    {
        public override bool InvolvesDamage => true;
        public override string DisplayString => "Forced to Taunt on Kill";

        public override bool InvolvesPassive => false;
        public override bool GoodStat => false;

        public override void OnInit()
        {
            cost = 350;
        }

        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return weaponSoFar.namedType.canDoDamage;
        }
    }
    class WeaponStatCyberbullied : WeaponStatDance, IWeaponStat
    {
        public override string DisplayString => "You get cyber bullied for using it";
        public override bool dumbModifer => true;
        public override void OnInit()
        {
            cost = -250;
        }
        public override bool CanApplyStat(GeneratedWeapon weaponSoFar)
        {
            return base.CanApplyStat(weaponSoFar) && TF2WeaponGenerator.random.Next(100) < 25;
        }
    }

    class WeaponStatWhenHurt : WeaponStatDance, IWeaponStat
    {
        protected string[] gamemodes = { "You get spooked", "You get bonk stunned", "you taunt", "your attacker gets mini-crits", "you get slowed","You instantly die", "You get jarated", "You get mad-milked", "You set on fire" };
        protected int theChosenValue = 0;
        public override string DisplayString => gamemodes[theChosenValue] +" when hurt";
        public override bool dumbModifer => true;
        public override void OnInit()
        {
            theChosenValue = TF2WeaponGenerator.random.Next(theChosenValue);
            cost = 350;
        }
    }

    class WeaponStatFallDamageBad : WeaponStatJumpBoost, IWeaponStat
    {
        public override string DisplayString => "+" + movementBoost + "% increased fall damage";
        public override bool GoodStat => false;
    }

    class WeaponStatKnockbackBad : WeaponStatJumpBoost, IWeaponStat
    {
        public override string DisplayString => "-" + movementBoost + "% increased knockback taken";
        public override bool GoodStat => false;
    }

    class WeaponStatFiringSpeedBad: WeaponStatFiringSpeed, IWeaponStat
    {
        public override string DisplayString => "-" + movementBoost + "% firing Speed";
        public override bool GoodStat => false;
    }
    class WeaponStatAmmoCapacityBad : WeaponStatAmmoCapacity, IWeaponStat
    {
        public override string DisplayString => "-" + movementBoost + "% clip size";
        public override bool GoodStat => false;
    }

    class WeaponStatJumpBoostBad : WeaponStatJumpBoost, IWeaponStat
    {
        public override string DisplayString => "-" + movementBoost + "% jump height";
        public override bool GoodStat => false;

    }

    class WeaponStatExtraHealthBad : WeaponStatExtraHealth, IWeaponStat
    {
        public override string DisplayString => "-" + movementBoost + " health on wearer";
        public override bool GoodStat => false;

        public override void OnInit()
        {
            cost = 300;
            int maxHP = (weapon.weaponClass.ClassType == (int)ClassTypes.Heavy ? 200 : 100);
            movementBoost = TF2WeaponGenerator.random.Next(0, TF2WeaponGenerator.random.Next(20, maxHP));
        }
    }

    class WeaponStatDamageBoostBad : WeaponStatDamageBoost, IWeaponStat
    {
        public override string DisplayString => "-" + damageBoost + "% damage";
        public override bool GoodStat => false;
    }

}
