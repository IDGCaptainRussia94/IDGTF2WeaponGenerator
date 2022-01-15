using System;
using System.Collections.Generic;
using System.Text;

namespace IDGTF2WeaponGenerator.Classes
{
    public enum SlotTypes
    {
        Primary = 0,
        Secondary = 1,
        Melee = 2,
        PDA = 3,
        DisguiseKit = 4,
    }
    public interface IWeaponSlotType
    {


    }

    public abstract class WeaponSlotType : WeaponValuesBase
    {
        public int[] allowedClasses;
        public virtual int SlotType => (int)SlotTypes.Primary;
        public virtual string SlotString => "EMPTY";

        public WeaponSlotType()
        {
            allowedClasses = new int[] { (int)ClassTypes.Scout, (int)ClassTypes.Soldier, (int)ClassTypes.Pyro, (int)ClassTypes.Demo, (int)ClassTypes.Heavy, (int)ClassTypes.Engineer, (int)ClassTypes.Medic, (int)ClassTypes.Sniper, (int)ClassTypes.Spy };
        }
    }

    class WeaponSlotTypePrimary : WeaponSlotType, IWeaponSlotType
    {
        public override int SlotType => (int)SlotTypes.Primary;
        public override string SlotString => "Primary";
    }

    class WeaponSlotTypeSecondary : WeaponSlotType, IWeaponSlotType
    {
        public override int SlotType => (int)SlotTypes.Secondary;
        public override string SlotString => "Secondary";
    }

    class WeaponSlotTypeMelee : WeaponSlotType, IWeaponSlotType
    {
        public override int SlotType => (int)SlotTypes.Melee;
        public override string SlotString => "Melee";
    }
    class WeaponSlotTypePDA : WeaponSlotType, IWeaponSlotType
    {
        public override int SlotType => (int)SlotTypes.PDA;
        public override string SlotString => "Construction PDA";

        public WeaponSlotTypePDA() : base()
        {
            allowedClasses = new int[] {(int)ClassTypes.Engineer};
        }
    }
    class WeaponSlotTypeDisguiseKit : WeaponSlotType, IWeaponSlotType
    {
        public override int SlotType => (int)SlotTypes.PDA;
        public override string SlotString => "Disguise Kit";

        public WeaponSlotTypeDisguiseKit() : base()
        {
            allowedClasses = new int[] { (int)ClassTypes.Spy };
        }
    }


}
