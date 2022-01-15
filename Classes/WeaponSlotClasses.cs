using System;
using System.Collections.Generic;
using System.Text;

namespace IDGTF2WeaponGenerator.Classes
{
    public enum ClassTypes
    {
        Scout = 0,
        Soldier = 1,
        Pyro = 2,
        Demo = 3,
        Heavy = 4,
        Engineer = 5,
        Medic = 6,
        Sniper = 7,
        Spy = 8,
    }

    public interface IWeaponSlotPlayerClass
    {


    }

    public abstract class WeaponSlotPlayerClass : WeaponValuesBase
    {
        public virtual int ClassType => (int)ClassTypes.Scout;
        public virtual string ClassString => "EMPTY";
    }

    public class WeaponSlotClassScout : WeaponSlotPlayerClass, IWeaponSlotPlayerClass
    {
        public override int ClassType => (int)ClassTypes.Scout;
        public override string ClassString => "Scout";
    }
    public class WeaponSlotClassSoldier : WeaponSlotPlayerClass, IWeaponSlotPlayerClass
    {
        public override int ClassType => (int)ClassTypes.Soldier;
        public override string ClassString => "Soldier";
    }
    public class WeaponSlotClassPyro : WeaponSlotPlayerClass, IWeaponSlotPlayerClass
    {
        public override int ClassType => (int)ClassTypes.Pyro;
        public override string ClassString => "Pyro";
    }
    public class WeaponSlotClassDemo : WeaponSlotPlayerClass, IWeaponSlotPlayerClass
    {
        public override int ClassType => (int)ClassTypes.Demo;
        public override string ClassString => "Demoman";
    }
    public class WeaponSlotClassHeavy : WeaponSlotPlayerClass, IWeaponSlotPlayerClass
    {
        public override int ClassType => (int)ClassTypes.Heavy;
        public override string ClassString => "Heavy";
    }
    public class WeaponSlotClassEngineer : WeaponSlotPlayerClass, IWeaponSlotPlayerClass
    {
        public override int ClassType => (int)ClassTypes.Engineer;
        public override string ClassString => "Engineer";
    }
    public class WeaponSlotClassMedic : WeaponSlotPlayerClass, IWeaponSlotPlayerClass
    {
        public override int ClassType => (int)ClassTypes.Medic;
        public override string ClassString => "Medic";
    }
    public class WeaponSlotClassSniper : WeaponSlotPlayerClass, IWeaponSlotPlayerClass
    {
        public override int ClassType => (int)ClassTypes.Sniper;
        public override string ClassString => "Sniper";
    }
    public class WeaponSlotClassSpy : WeaponSlotPlayerClass, IWeaponSlotPlayerClass
    {
        public override int ClassType => (int)ClassTypes.Spy;
        public override string ClassString => "Spy";
    }

}
