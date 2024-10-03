using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    abstract class GameObject
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public GameObject(int id, string name, int x, int y)
        {
            Id = id;
            Name = name;
            X = x;
            Y = y;
        }

        public int GetId() => Id;
        public string GetName() => Name;
        public int GetX() => X;
        public int GetY() => Y;
    }

    abstract class Building: GameObject
    {
        public Building(int id, string name, int x, int y) : base(id, name, x, y) { }

        public abstract bool IsBuilt();
    }

    abstract class Unit: GameObject
    {
        public float Hp { get; private set; }

        public Unit(int id, string name, int x, int y, float hp) : base(id, name, x, y)
        {
            Hp = hp;
        }

        public bool IsAlive() => Hp > 0;
        public float GetHp() => Hp;

        public void Damage(float damage)
        {
            Hp -= damage;
            if (Hp < 0) Hp = 0;
        }
    }

    interface Attacker
    {
        void Attack(Unit unit);
    }

    interface Moveable
    {
        void Move(int newX, int newY);
    }

    class Fort: Building, Attacker
    {
        public Fort(int id, string name, int x, int y) : base(id, name, x, y) { }

        public override bool IsBuilt() => true;

        public void Attack(Unit unit)
        {
            Console.WriteLine($"{GetName()} атакует {unit.GetName()}!");
        }
    }

    class MobileHome: Building, Moveable
    {
        public MobileHome(int id, string name, int x, int y) : base(id, name, x, y) { }

        public override bool IsBuilt() => true;

        public void Move(int newX, int newY)
        {
            Console.WriteLine($"{GetName()} перемещение на ({newX}, {newY})");
        }
    }

    class Archer: Unit, Attacker, Moveable
    {
        public Archer(int id, string name, int x, int y, float hp) : base(id, name, x, y, hp) { }

        public void Attack(Unit unit)
        {
            Console.WriteLine($"{GetName()} стреляет в {unit.GetName()}");
            unit.Damage(10);
        }

        public void Move(int newX, int newY)
        {
            Console.WriteLine($"{GetName()} перемещается на ({newX}, {newY})!");
        }
    }
    internal class Lab2
    {
        static void Main(string[] args)
        {
            Fort fort = new Fort(1, "Fortress", 10, 20);
            MobileHome mobile = new MobileHome(2, "Mobile Home", 15, 25);
            Archer archer = new Archer(3, "Archer", 5, 10, 100);

            Console.WriteLine($"fort {fort.IsBuilt()}");

            mobile.Move(20, 30);
            Console.WriteLine(mobile.Name);

            fort.Attack(archer);
            Console.WriteLine($"archer hp {archer.GetHp()}");

            archer.Attack(archer);
            Console.WriteLine($"мазохизм {archer.GetHp()}");
        }
    }
}