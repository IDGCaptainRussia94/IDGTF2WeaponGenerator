using System;
using System.Collections.Generic;
using System.Text;

namespace IDGTF2WeaponGenerator.Classes
{

    public interface IWeaponNamesType
    {


    }

    public abstract class WeaponNamedType : WeaponValuesBase
    {
        public int[] allowedClasses;
        public int[] allowedSlots;
        public bool medigun = false;
        public bool canRepairBuildings = false;
        public bool canBuildBuildings = false;
        public bool canBackstab = false;
        public bool canDoDamage = true;
        public bool canHeadShot = false;
        public bool canShieldBash = false;
        public bool canEat = false;
        public bool canThrow = false;
        public bool canBuildRage = false;
        public bool hasScopeMeter = false;
        public bool limitedReach = false;
        public bool invisWatch = false;
        public bool passiveWeapon = false;

        public WeaponSlotPlayerClass playerClass;
        public WeaponSlotType slotType;
        public virtual string ItemTypeNameString => "EMPTY";

        public WeaponNamedType()
        {
            allowedClasses = new int[] { (int)ClassTypes.Scout, (int)ClassTypes.Soldier, (int)ClassTypes.Pyro, (int)ClassTypes.Demo, (int)ClassTypes.Heavy, (int)ClassTypes.Engineer, (int)ClassTypes.Medic, (int)ClassTypes.Sniper, (int)ClassTypes.Spy };
            allowedSlots = new int[] { (int)SlotTypes.Melee };
        }
    }

#region basicWeapons

    class WeaponNamedType_Melee : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Melee";

        public WeaponNamedType_Melee() : base()
        {
            limitedReach = true;
            allowedSlots = new int[] { (int)SlotTypes.Melee };
            allowedClasses = new int[] { (int)ClassTypes.Scout, (int)ClassTypes.Soldier, (int)ClassTypes.Pyro, (int)ClassTypes.Demo, (int)ClassTypes.Heavy, (int)ClassTypes.Medic, (int)ClassTypes.Sniper};
        }
    }

/*
    class WeaponNamedType_Secondary : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Secondary";

        public WeaponNamedType_Secondary() : base()
        {
            allowedSlots = new int[] { (int)SlotTypes.Secondary };
        }
    }

    class WeaponNamedType_Primary : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Primary";

        public WeaponNamedType_Primary() : base()
        {
            allowedSlots = new int[] { (int)SlotTypes.Primary };
        }
    }
*/

    class WeaponNamedType_PDA : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Construction PDA";

        public WeaponNamedType_PDA() : base()
        {
            canBuildBuildings = true;
            canDoDamage = false;
            allowedSlots = new int[] { (int)SlotTypes.PDA, (int)SlotTypes.DisguiseKit };
            allowedClasses = new int[] { (int)ClassTypes.Engineer};
        }
    }
    class WeaponNamedType_DisguiseKit : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Disguise Kit";

        public WeaponNamedType_DisguiseKit() : base()
        {
            canDoDamage = false;
            allowedSlots = new int[] { (int)SlotTypes.PDA, (int)SlotTypes.DisguiseKit };
            allowedClasses = new int[] { (int)ClassTypes.Spy };
        }
    }
    #endregion

    class WeaponNamedType_Knife : WeaponNamedType_Melee, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Knife";

        public WeaponNamedType_Knife() : base()
        {
            canBackstab = true;
            allowedClasses = new int[] { (int)ClassTypes.Spy };
        }
    }

    class WeaponNamedType_Wrench : WeaponNamedType_Melee, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Wrench";

        public WeaponNamedType_Wrench() : base()
        {
            canRepairBuildings = true;
            allowedClasses = new int[] { (int)ClassTypes.Engineer };
        }
    }

    class WeaponNamedType_Sword : WeaponNamedType_Melee, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Sword";

        public WeaponNamedType_Sword() : base()
        {
            allowedClasses = new int[] { (int)ClassTypes.Demo };
        }
    }

    class WeaponNamedType_Scattergun : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Scattergun";

        public WeaponNamedType_Scattergun() : base()
        {
            allowedSlots = new int[] { (int)SlotTypes.Primary };
            allowedClasses = new int[] { (int)ClassTypes.Scout};
        }
    }

    class WeaponNamedType_RocketLauncher : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Rocket Launcher";

        public WeaponNamedType_RocketLauncher() : base()
        {
            allowedSlots = new int[] { (int)SlotTypes.Primary };
            allowedClasses = new int[] { (int)ClassTypes.Soldier};
        }
    }
    class WeaponNamedType_FlameThrower : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Flame Thrower";

        public WeaponNamedType_FlameThrower() : base()
        {
            allowedSlots = new int[] { (int)SlotTypes.Primary };
            allowedClasses = new int[] { (int)ClassTypes.Pyro};
        }
    }
    class WeaponNamedType_GrenadeLauncher : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Grenade Launcher";

        public WeaponNamedType_GrenadeLauncher() : base()
        {
            allowedSlots = new int[] { (int)SlotTypes.Primary };
            allowedClasses = new int[] { (int)ClassTypes.Demo};
        }
    }
    class WeaponNamedType_Minigun : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Minigun";

        public WeaponNamedType_Minigun() : base()
        {
            allowedSlots = new int[] { (int)SlotTypes.Primary };
            allowedClasses = new int[] { (int)ClassTypes.Heavy};
        }
    }
    class WeaponNamedType_SyringeGun : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Syringe Gun";

        public WeaponNamedType_SyringeGun() : base()
        {
            allowedSlots = new int[] { (int)SlotTypes.Primary };
            allowedClasses = new int[] { (int)ClassTypes.Medic};
        }
    }
    class WeaponNamedType_SniperRifle : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Sniper Rifle";

        public WeaponNamedType_SniperRifle() : base()
        {
            hasScopeMeter = true;
            canHeadShot = true;
            allowedSlots = new int[] { (int)SlotTypes.Primary };
            allowedClasses = new int[] { (int)ClassTypes.Sniper};
        }
    }
    class WeaponNamedType_Revolver : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Revolver";

        public WeaponNamedType_Revolver() : base()
        {
            canHeadShot = true;
            allowedSlots = new int[] { (int)SlotTypes.Primary };
            allowedClasses = new int[] { (int)ClassTypes.Spy};
        }
    }

    class WeaponNamedType_Pistol : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Pistol";

        public WeaponNamedType_Pistol() : base()
        {
            allowedSlots = new int[] { (int)SlotTypes.Secondary };
            allowedClasses = new int[] { (int)ClassTypes.Scout, (int)ClassTypes.Engineer };
        }
    }

    class WeaponNamedType_Shotgun : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Shotgun";

        public WeaponNamedType_Shotgun() : base()
        {
            allowedSlots = new int[] { (int)SlotTypes.Secondary };
            allowedClasses = new int[] { (int)ClassTypes.Heavy, (int)ClassTypes.Soldier, (int)ClassTypes.Pyro };
        }
    }

    class WeaponNamedType_ShotgunPrimary : WeaponNamedType_Shotgun, IWeaponNamesType
    {

        public WeaponNamedType_ShotgunPrimary() : base()
        {
            allowedSlots = new int[] { (int)SlotTypes.Primary };
            allowedClasses = new int[] { (int)ClassTypes.Engineer };
        }
    }

    class WeaponNamedType_Banner : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Banner";

        public WeaponNamedType_Banner() : base()
        {
            canBuildRage = true;
            canDoDamage = false;
            allowedSlots = new int[] { (int)SlotTypes.Secondary };
            allowedClasses = new int[] { (int)ClassTypes.Soldier };
        }
    }

    class WeaponNamedType_FlareGun : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Flare Gun";

        public WeaponNamedType_FlareGun() : base()
        {
            allowedSlots = new int[] { (int)SlotTypes.Secondary };
            allowedClasses = new int[] {(int)ClassTypes.Pyro };
        }
    }

    class WeaponNamedType_Shield : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Shield";

        public WeaponNamedType_Shield() : base()
        {
            passiveWeapon = true;
            canShieldBash = true;
            allowedSlots = new int[] { (int)SlotTypes.Secondary };
            allowedClasses = new int[] { (int)ClassTypes.Demo };
        }
    }

    class WeaponNamedType_Sticky : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Sticky Bomb Launcher";

        public WeaponNamedType_Sticky() : base()
        {
            allowedSlots = new int[] { (int)SlotTypes.Secondary };
            allowedClasses = new int[] { (int)ClassTypes.Demo };
        }
    }

    class WeaponNamedType_LunchBox : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Lunch Box Item";

        public WeaponNamedType_LunchBox() : base()
        {
            canEat = true;
            canDoDamage = false;
            allowedSlots = new int[] { (int)SlotTypes.Secondary };
            allowedClasses = new int[] { (int)ClassTypes.Scout, (int)ClassTypes.Heavy };
        }
    }

    class WeaponNamedType_Medigun : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Medigun";

        public WeaponNamedType_Medigun() : base()
        {
            medigun = true;
            canDoDamage = false;
            allowedSlots = new int[] { (int)SlotTypes.Secondary };
            allowedClasses = new int[] { (int)ClassTypes.Medic};
        }
    }

    class WeaponNamedType_Throwable : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Thrown Consumable";

        public WeaponNamedType_Throwable() : base()
        {
            canThrow = true;
            allowedSlots = new int[] { (int)SlotTypes.Secondary };
            allowedClasses = new int[] { (int)ClassTypes.Scout, (int)ClassTypes.Pyro, (int)ClassTypes.Sniper };
        }
    }

    class WeaponNamedType_Backpack : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Backpack";

        public WeaponNamedType_Backpack() : base()
        {
            passiveWeapon = true;
            canDoDamage = false;
            allowedSlots = new int[] { (int)SlotTypes.Secondary };
            allowedClasses = new int[] { (int)ClassTypes.Sniper };
        }
    }

    class WeaponNamedType_Boots : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Boots";

        public WeaponNamedType_Boots() : base()
        {
            passiveWeapon = true;
            canDoDamage = false;
            allowedSlots = new int[] { (int)SlotTypes.Primary };
            allowedClasses = new int[] { (int)ClassTypes.Soldier, (int)ClassTypes.Demo };
        }
    }

    class WeaponNamedType_InvisWatch : WeaponNamedType, IWeaponNamesType
    {
        public override string ItemTypeNameString => "Invis Watch";

        public WeaponNamedType_InvisWatch() : base()
        {
            invisWatch = true;
            canDoDamage = false;
            allowedSlots = new int[] { (int)SlotTypes.Secondary };
            allowedClasses = new int[] { (int)ClassTypes.Spy };
        }
    }

}
