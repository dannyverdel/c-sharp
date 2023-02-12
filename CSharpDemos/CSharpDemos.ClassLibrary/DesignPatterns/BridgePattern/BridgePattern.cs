using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.BridgePattern
{
	public class InvokeBridgePattern : IInvokeMethod
	{
        /*
         * The Bridge design pattern is a structural design pattern that separates the abstraction (interface) from its implementation, 
         * so that they can be developed independently and can be changed dynamically at runtime.
         * 
         * In simple terms, the Bridge design pattern allows you to create an interface that can have multiple implementations, which can be switched out dynamically. 
         * This means that you can change the behavior of your system by swapping out the implementation of an interface, without changing the code that uses that interface.
         * 
         * With this example, you can create multiple implementations of IWeapon (Bow and Explosives), 
         * and you can switch the implementation of IWeapon dynamically by changing the constructor of the Mob objects.
         * 
         * You can see that the Skeleton is given a bow, while the Creeper is given explosives, 
         * because they were constructed with different implementations of IWeapon. 
         * You can also change the implementation of the weapon dynamically by creating a new instance of the Mob class with a different IWeapon implementation.
         */

        public void InvokeMethod()
		{
            IWeapon skeleton_weapon = new Bow();
            IWeapon creeper_weapon = new Explosives();

            Mob skeleton = new Skeleton(skeleton_weapon);
            Mob creeper = new Creeper(creeper_weapon);

            Console.WriteLine(skeleton);
            Console.WriteLine(creeper);
        }
    }

    public interface IWeapon
    {
        string Weapon { get; }
    }

    public class Bow : IWeapon
    {
        public string Weapon => "Bow";
    }

    public class Explosives : IWeapon
    {
        public string Weapon => "Explosives";
    }

    public abstract class Mob
    {
        protected IWeapon weapon;
        public Mob(IWeapon weapon) => this.weapon = weapon;
        public abstract string Name { get; }
        public override string ToString() => $"Giving {weapon.Weapon} to {Name}";
    }

    public class Skeleton : Mob
    {
        public Skeleton(IWeapon weapon) : base(weapon) { }
        public override string Name => "Skeleton";
    }

    public class Creeper : Mob
    {
        public Creeper(IWeapon weapon) : base(weapon) { }
        public override string Name => "Creeper";
    }
}

