using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public string name;
    public int experience = 0;

    public Character()
    {
        name = "No Name !";
    }

    public Character(string name, int experience)
    {
        this.name = name;
        this.experience = experience;
    }

    public virtual void WriteInfo()
    {
        Debug.LogFormat("Hero {0} - {1} Experience", name, experience);
    }

    private void Reset()
    {
        this.name = "No Name!";
        experience = 0;
    }

}


public class Paladin : Character
{
    public Weapon weapon;

    public Paladin(string name, int experience, Weapon weapon) : base(name, experience)
    {
        this.weapon = weapon;
    }

    public override void WriteInfo()
    {
        Debug.LogFormat("Hello {0} - Take your {1}", name, weapon.name);

    }


}



public struct Weapon
{
    public string name;
    public int damage;

    public Weapon(string name, int damage)
    {
        this.name = name;
        this.damage = damage;
    }

    public void WriteWeaponInfo()
    {
        Debug.LogFormat("Weapon {0} - {1} Damage:", name, damage);

    }

}


// Classes are reference types. (Sýnýflar reerans tiplidir)
// Structs are value types.  (Structlar deðer tiplidir)