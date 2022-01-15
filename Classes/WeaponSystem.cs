using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IDGTF2WeaponGenerator.Classes
{
    public class WeaponSystem
    {
        public GeneratedWeapon currentWeapon;
		public List<WeaponSlotType> allSlots;
        public List<WeaponSlotPlayerClass> allPlayerClasses;
        public List<WeaponNamedType> allNamedWeaponTypes;
        public List<WeaponStatModifer> allStatModifer;

        public List<WeaponStat> allGoodStats = new List<WeaponStat>();
        public List<WeaponStat> allBadStats = new List<WeaponStat>();



        public Vector2 topTextLength;
        public string topTextString = "NEW ITEM ACQUIRED!";

        public Vector2 classWeaponTextLength;
        public string classWeaponText = "";


        public void LoadTypes()
        {

            allSlots = new List<WeaponSlotType>();
            allPlayerClasses = new List<WeaponSlotPlayerClass>();
            allNamedWeaponTypes = new List<WeaponNamedType>();
            allStatModifer = new List<WeaponStatModifer>();


            Assembly ass = typeof(TF2WeaponGenerator).Assembly;
            foreach (Type typeoff in ass.GetTypes())
            {
                if (typeoff.GetInterfaces().Contains(typeof(IWeaponSlotType)))
                {
                    WeaponSlotType slotType = (ass.CreateInstance(typeoff.FullName) as WeaponSlotType);
                    Logging.ConsoleLog("added Weapon Slot Type: " + slotType.GetType().Name);
                    allSlots.Add(slotType);
                }
                if (typeoff.GetInterfaces().Contains(typeof(IWeaponSlotPlayerClass)))
                {
                    WeaponSlotPlayerClass slotType = (ass.CreateInstance(typeoff.FullName) as WeaponSlotPlayerClass);
                    Logging.ConsoleLog("added Weapon Player Class Type: " + slotType.GetType().Name);
                    allPlayerClasses.Add(slotType);
                }
                if (typeoff.GetInterfaces().Contains(typeof(IWeaponNamesType)))
                {
                    WeaponNamedType slotType = (ass.CreateInstance(typeoff.FullName) as WeaponNamedType);
                    Logging.ConsoleLog("added Weapon Named Type: " + slotType.GetType().Name);
                    allNamedWeaponTypes.Add(slotType);
                }
                if (typeoff.GetInterfaces().Contains(typeof(IWeaponStatModifer)))
                {
                    WeaponStatModifer slotType = (ass.CreateInstance(typeoff.FullName) as WeaponStatModifer);
                    Logging.ConsoleLog("added Weapon Modifer: " + slotType.GetType().Name);
                    allStatModifer.Add(slotType);
                }
                if (typeoff.GetInterfaces().Contains(typeof(IWeaponStat)))
                {
                    WeaponStat statType = (ass.CreateInstance(typeoff.FullName) as WeaponStat);
                    Logging.ConsoleLog("added Weapon Stat: " + statType.GetType().Name);
                    if (statType.GoodStat)
                        allGoodStats.Add(statType);
                    else
                        allBadStats.Add(statType);
                }
            }
        }


        public WeaponSystem()
        {
            LoadTypes();

        }

        public void MakeNewWeapon()
        {
            currentWeapon = new GeneratedWeapon();
            classWeaponTextLength = default;
            classWeaponText = "A new " + currentWeapon.namedType.ItemTypeNameString + " for the " + currentWeapon.weaponClass.ClassString+"!";
        }

        public void DrawWeapon()
        {
            if (topTextLength == default)
            topTextLength = TF2WeaponGenerator.TF2Font.MeasureString(topTextString);

            if (classWeaponTextLength == default)
                classWeaponTextLength = TF2WeaponGenerator.TF2Font.MeasureString(classWeaponText);


            SpriteBatch sb = TF2WeaponGenerator.SpriteBatch;

            sb.DrawString(TF2WeaponGenerator.TF2Font, topTextString, new Vector2(TF2WeaponGenerator.screenSize.X/2f,8), Color.Orange, 0, new Vector2(topTextLength.X/2f,0), 1f, SpriteEffects.None, 0);

            sb.DrawString(TF2WeaponGenerator.TF2Font, classWeaponText, new Vector2(TF2WeaponGenerator.screenSize.X / 2f, 92), Color.Orange, 0, new Vector2(classWeaponTextLength.X / 2f, 0), new Vector2(0.4f,0.4f), SpriteEffects.None, 0);

            float yIndex = 136;

            foreach(WeaponStat weaponstat in currentWeapon.goodStats)
            {
                //if (weaponstat.textSize == default)
                    weaponstat.textSize = TF2WeaponGenerator.TF2FontBold.MeasureString(weaponstat.TrueDisplayString);

                sb.DrawString(TF2WeaponGenerator.TF2FontBold, weaponstat.TrueDisplayString, new Vector2(TF2WeaponGenerator.screenSize.X / 2f, yIndex), Color.Black, 0, new Vector2(weaponstat.textSize.X / 2f, 0), new Vector2(0.3f, 0.3f), SpriteEffects.None, 0);
                yIndex += weaponstat.textSize.Y*0.30f;
            }

            yIndex += 24;

            Vector2 vevce = new Vector2(((TF2WeaponGenerator.screenSize.ToVector2().X-128f) / (TF2WeaponGenerator.whiteTex.Width)), 0.10f);

            sb.Draw(TF2WeaponGenerator.whiteTex, new Vector2(64, yIndex-18), null, Color.Black, 0, Vector2.Zero, vevce, SpriteEffects.None, 0);

            foreach (WeaponStat weaponstat in currentWeapon.badStats)
            {
                //if (weaponstat.textSize == default)
                    weaponstat.textSize = TF2WeaponGenerator.TF2FontBold.MeasureString(weaponstat.TrueDisplayString);

                sb.DrawString(TF2WeaponGenerator.TF2FontBold, weaponstat.TrueDisplayString, new Vector2(TF2WeaponGenerator.screenSize.X / 2f, yIndex), Color.Maroon, 0, new Vector2(weaponstat.textSize.X / 2f, 0), new Vector2(0.3f, 0.3f), SpriteEffects.None, 0);
                yIndex += weaponstat.textSize.Y * 0.30f;
            }



        }


    }




    public class GeneratedWeapon
    {
        public WeaponSlotType weaponSlot;
        public WeaponSlotPlayerClass weaponClass;
        public WeaponNamedType namedType;
        public List<WeaponStat> goodStats = new List<WeaponStat>();
        public List<WeaponStat> badStats = new List<WeaponStat>();
        public bool gainedHeadshot = false;
        public bool gainedBackstab = false;

        public GeneratedWeapon()
        {
            //Class
            List<WeaponSlotPlayerClass> weaponClassToCheck = TF2WeaponGenerator.weaponSystem.allPlayerClasses.OrderBy(testby => TF2WeaponGenerator.random.Next()).ToList();

            weaponClass = weaponClassToCheck[0];
            Logging.ConsoleLog("Generated weapon class: " + weaponClass.ClassString);

            //Slot Type
            List<WeaponSlotType> weaponSlotToCheck = TF2WeaponGenerator.weaponSystem.allSlots.Where(testby => testby.allowedClasses.Contains(weaponClass.ClassType)).OrderBy(testby => TF2WeaponGenerator.random.Next()).ToList();

            weaponSlot = weaponSlotToCheck[0];
            Logging.ConsoleLog("Generated weapon slot: " + weaponSlot.SlotString);

            //Named Weapons Types (boots, melee, Scattergun...)

            List<WeaponNamedType> weaponNamedToCheck = TF2WeaponGenerator.weaponSystem.allNamedWeaponTypes.
                Where(testby => testby.allowedClasses.Contains(weaponClass.ClassType) && testby.allowedSlots.Contains(weaponSlot.SlotType)).//only accepted class AND slot types
                OrderBy(testby => TF2WeaponGenerator.random.Next()).ToList();

            namedType = weaponNamedToCheck[0];
            namedType.playerClass = weaponClass;
            namedType.slotType = weaponSlot;


            Logging.ConsoleLog("Generated weapon named type: " + namedType.ItemTypeNameString);

            PickStats();

        }

        public void PickStats()
        {
            int moneyToSpend = 1000;
            int moneySpent = 0;

            //Slot Type
            List<WeaponStat> goodStatsLeft = TF2WeaponGenerator.weaponSystem.allGoodStats.Where(testby => testby.CanApplyStat(this)).OrderBy(testby => TF2WeaponGenerator.random.Next()).ToList();
            List<WeaponStat> badStatsLeft = TF2WeaponGenerator.weaponSystem.allBadStats.Where(testby => testby.CanApplyStat(this)).OrderBy(testby => TF2WeaponGenerator.random.Next()).ToList();

            while (moneyToSpend > 0 && goodStatsLeft.Count>0)
            {
                WeaponStat statHere = goodStatsLeft[0];
                statHere.ApplyStats(this);
                statHere.ApplyModifiers();

                moneyToSpend -= statHere.cost;
                moneySpent += statHere.cost;

                goodStats.Add(statHere);

                goodStatsLeft.RemoveAt(0);
            }

            while (moneySpent > 0 && badStatsLeft.Count > 0)
            {
                WeaponStat statHere = badStatsLeft[0];
                statHere.ApplyStats(this);
                statHere.ApplyModifiers();

                moneySpent -= statHere.cost;

                badStats.Add(statHere);

                badStatsLeft.RemoveAt(0);
            }





        }


    }

    public abstract class WeaponValuesBase
    {



    }



}
