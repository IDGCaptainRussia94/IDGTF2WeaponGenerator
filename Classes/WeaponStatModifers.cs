using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDGTF2WeaponGenerator.Classes
{
    public interface IWeaponStatModifer
    {


    }

    public abstract class WeaponStatModifer : WeaponValuesBase
    {
        public WeaponStat weaponStats;
        public WeaponStatModifer furtherModifer;
        public bool applyFurtherModifier = false;
        public virtual bool ModiferChance => TF2WeaponGenerator.random.Next(100) < 15;
        public virtual bool dumbModifer => false;

        public virtual bool GoodStat => true;
        protected virtual string DisplayString => "";
        public string TrueDisplayString => DisplayString + (furtherModifer != default ? furtherModifer.TrueDisplayString : "");

        public virtual bool CanApplyModifer(WeaponStat stat)
        {
            return !stat.InvolvesOnKill;
        }
        public virtual void OnInit()
        {


        }

        public void ApplyModifer(WeaponStat stat)
        {
            weaponStats = stat;
            OnInit();

        }

        public WeaponStatModifer()
        {

        }
    }

    class WeaponStatModifer_AgainstClasses : WeaponStatModifer, IWeaponStatModifer
    {
        public override bool ModiferChance => true;
        protected int targetClass = 0;
        protected string[] classNames = {"Scouts","Soldiers","Pyros","Demomen","Heavies","Engineers","Medics","Snipers","Spies","Bots","Robots" };

        protected override string DisplayString => " against "+ classNames[targetClass];

        public override bool CanApplyModifer(WeaponStat stat)
        {
            return stat.InvolvesDamage && !stat.InvolvesOnKill;
        }

        public override void OnInit()
        {
            targetClass = TF2WeaponGenerator.random.Next(classNames.Length);
            weaponStats.cost /= 2;
        }
    }

    class WeaponStatModifer_WhileDeployed : WeaponStatModifer, IWeaponStatModifer
    {
        public override bool ModiferChance => TF2WeaponGenerator.random.Next(100) < 50;
        protected override string DisplayString => " while deployed";

        public override void OnInit()
        {
            weaponStats.cost /= 2;
        }
    }

    class WeaponStatModifer_WhileInWater : WeaponStatModifer, IWeaponStatModifer
    {
        protected override string DisplayString => " while underwater";

        public override void OnInit()
        {
            weaponStats.cost /= 2;
        }
    }

    class WeaponStatModifer_WhileAirborne : WeaponStatModifer, IWeaponStatModifer
    {
        protected override string DisplayString => " while airborne";

        public override void OnInit()
        {
            weaponStats.cost /= 2;
        }
    }

    class WeaponStatModifer_WhileBumperCars: WeaponStatModifer, IWeaponStatModifer
    {
        protected override string DisplayString => " while in Bumper Cars";

        public override void OnInit()
        {
            weaponStats.cost /= 3;
        }
    }
    class WeaponStatModifer_WhileTruce : WeaponStatModifer, IWeaponStatModifer
    {
        protected override string DisplayString => " while a Truce is called";

        public override void OnInit()
        {
            weaponStats.cost /= 3;
        }
    }

    class WeaponStatModifer_WhileInBase1 : WeaponStatModifer, IWeaponStatModifer
    {
        public override bool dumbModifer => true;
        protected override string DisplayString => " while in the Enemy Base";

        public override void OnInit()
        {
            weaponStats.cost /= 2;
        }
    }

    class WeaponStatModifer_WhileInBase2 : WeaponStatModifer_WhileInBase1, IWeaponStatModifer
    {
        protected override string DisplayString => " while in Your Base";

        public override void OnInit()
        {
            weaponStats.cost /= 2;
        }
    }


    class WeaponStatModifer_WhileInBase3 : WeaponStatModifer_WhileInBase1, IWeaponStatModifer
    {
        protected override string DisplayString => " while in No Man's Land";

        public override void OnInit()
        {
            weaponStats.cost /= 2;
        }
    }
    class WeaponStatModifer_WhileInBase4 : WeaponStatModifer_WhileInBase1, IWeaponStatModifer
    {
        protected override string DisplayString => " while in the Respawn Room";

        public override void OnInit()
        {
            weaponStats.cost /= 2;
        }
    }

    class WeaponStatModifer_WhileInBase5 : WeaponStatModifer_WhileInBase1, IWeaponStatModifer
    {
        public override bool dumbModifer => false;
        protected override string DisplayString => " while in Pyroland";
        public override void OnInit()
        {
            //
        }
    }

    class WeaponStatModifer_WhileDead : WeaponStatModifer_WhileInBase1, IWeaponStatModifer
    {
        protected override string DisplayString => " while dead";
        public override void OnInit()
        {
            weaponStats.cost /= 5;
        }
    }

    class WeaponStatModifer_WhileStandingStill : WeaponStatModifer, IWeaponStatModifer
    {
        protected override string DisplayString => " while standing still";

        public override void OnInit()
        {
            weaponStats.cost /= 3;
        }
    }

    class WeaponStatModifer_WhileObjective : WeaponStatModifer, IWeaponStatModifer
    {
        protected override string DisplayString => " while capping the objective";

        public override void OnInit()
        {
            weaponStats.cost /= 2;
        }
    }

    class WeaponStatModifer_WhileObjective2 : WeaponStatModifer_WhileObjective, IWeaponStatModifer
    {
        protected override string DisplayString => " while holding the Intel";
    }

    class WeaponStatModifer_WhileBurning : WeaponStatModifer, IWeaponStatModifer
    {
        protected override string DisplayString => " while burning";

        public override void OnInit()
        {
            weaponStats.cost /= 3;
        }
    }

    class WeaponStatModifer_WhileEnemyInWater : WeaponStatModifer_WhileInWater, IWeaponStatModifer
    {
        protected override string DisplayString => " against enemies underwater";

        public override void OnInit()
        {
           // weaponStats.cost /= 2;
        }
        public override bool CanApplyModifer(WeaponStat stat)
        {
            return stat.InvolvesDamage && !stat.InvolvesOnKill;
        }
    }

    class WeaponStatModifer_WhileDominated : WeaponStatModifer_WhileInWater, IWeaponStatModifer
    {
        protected override string DisplayString => " against dominated enemies";

        public override void OnInit()
        {
            //weaponStats.cost /= 2;
        }
    }

    class WeaponStatModifer_WhileEnemyUnusuals : WeaponStatModifer_WhileEnemyInWater, IWeaponStatModifer
    {
        protected override string DisplayString => " against enemies wearing unusuals";

        public override void OnInit()
        {
            weaponStats.cost /= 2;
        }
    }

    class WeaponStatModifer_WhileEnemyBurning : WeaponStatModifer_WhileEnemyInWater, IWeaponStatModifer
    {
        protected override string DisplayString => " against burning enemies";

        public override void OnInit()
        {
            weaponStats.cost /= 2;
        }
    }

    class WeaponStatModifer_WhileEnemyIsInAir : WeaponStatModifer_WhileEnemyInWater, IWeaponStatModifer
    {
        protected override string DisplayString => " against airborne enemies";
    }

    class WeaponStatModifer_WhilePlaying : WeaponStatModifer, IWeaponStatModifer
    {
        public override bool dumbModifer => true;
        protected string[] gamemodes = {"Competitive","Causal","MVM","with bots"};
        protected int theChosenValue = 0;
        protected override string DisplayString => " while playing " + gamemodes[theChosenValue];

        public override void OnInit()
        {
            theChosenValue = 0;
            weaponStats.cost /= 2;
        }
    }

    class WeaponStatModifer_WhilePlaying2 : WeaponStatModifer_WhilePlaying, IWeaponStatModifer
    {
        public override void OnInit()
        {
            base.OnInit();
            theChosenValue = 1;
        }
    }

    class WeaponStatModifer_WhilePlaying3 : WeaponStatModifer_WhilePlaying, IWeaponStatModifer
    {
        public override void OnInit()
        {
            base.OnInit();
            theChosenValue = 2;
        }
    }
    class WeaponStatModifer_WhilePlaying4 : WeaponStatModifer_WhilePlaying, IWeaponStatModifer
    {
        public override void OnInit()
        {
            base.OnInit();
            theChosenValue = 3;
        }
    }

    class WeaponStatModifer_LeaderRank : WeaponStatModifer_WhileEnemyInWater, IWeaponStatModifer
    {
        protected override string DisplayString => " against players with less Competitive Rank";

        public override void OnInit()
        {
            weaponStats.cost /= 2;

        }
    }


    class WeaponStatModifer_OnKill : WeaponStatModifer_WhileInWater, IWeaponStatModifer
    {
        protected string[] gamemodes;
        protected int theChosenValue = 0;
        protected int theChosenValue2 = 0;
        protected override string DisplayString => " "+gamemodes[theChosenValue] + " for "+ theChosenValue2+" seconds";
        public override bool dumbModifer => false;
        public override bool ModiferChance => true;

        public override void OnInit()
        {
            gamemodes = new string[]{ "Grants a speed boost", "Mini-crits become crits", "Regen HP", "inflict AOE bleeding around the victum", "grants you jarate swimming", "Summons a ghost to spook the area", "Stops enemies from respawning" };
            weaponStats.cost *= 1;
            theChosenValue = TF2WeaponGenerator.random.Next(gamemodes.Length);
            theChosenValue2 = TF2WeaponGenerator.random.Next(1, 60);
        }
        public override bool CanApplyModifer(WeaponStat stat)
        {
            return stat.InvolvesOnKill;
        }

    }

    class WeaponStatModifer_AndMore1: WeaponStatModifer_WhileInWater, IWeaponStatModifer
    {
        protected string[] gamemodes = { "Grants a speed boost","Makes enemies fall down", "Summons a rain of fire spell","Bonk Stuns","gets jarate swimming" };
        protected int theChosenValue = 0;
        protected override string DisplayString => weaponStats.GoodStat ? " and " + gamemodes[theChosenValue]+" On hit" : "\n and your attacker " + gamemodes[theChosenValue] + " when hit you";
        public override bool dumbModifer => false;
        public override bool ModiferChance => true;

        public override void OnInit()
        {
            weaponStats.cost *= 2;
            theChosenValue = TF2WeaponGenerator.random.Next(gamemodes.Length);
        }
        public override bool CanApplyModifer(WeaponStat stat)
        {
            return (stat.InvolvesRage || stat.InvolvesEating || stat.InvolvesDamage) && !stat.InvolvesOnKill && !stat.InvolvesPassive;
        }
    }


}
