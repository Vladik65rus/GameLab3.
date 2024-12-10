using System;

public class Unit
{
    public string Name { get; } // Имя юнита
    public int Health { get; private set; } // Здоровье юнита
    public int AttackPower { get; } // Сила атаки
    public int Defense { get; } // Защита

    /// <summary>
    /// Конструктор для инициализации юнита.
    /// </summary>
    /// <param name="name">Имя юнита</param>
    /// <param name="health">Здоровье юнита</param>
    /// <param name="attackPower">Сила атаки</param>
    /// <param name="defense">Защита</param>
    public Unit(string name, int health, int attackPower, int defense)

    {
        Name = name;
        Health = health;
        AttackPower = attackPower;
        Defense = defense;
    }

    /// <summary>
    /// Проверяет, жив ли юнит.
    /// </summary>
    /// <returns>True, если юнит жив, иначе False</returns>
    public bool IsAlive()
    {
        return Health > 0;
    }

    /// <summary>
    /// Выполняет атаку на целевого юнита.
    /// </summary>
    /// <param name="target">Целевой юнит</param>
    public void Attack(Unit target)
    {
        if (!IsAlive())
        {
            throw new InvalidOperationException($"{Name} не может атаковать, так как мертв.");
        }

        int damage = Math.Max(0, AttackPower - target.Defense); // Рассчитываем урон
        target.TakeDamage(damage);
    }

    /// <summary>
    /// Обрабатывает получение урона.
    /// </summary>
    /// <param name="damage">Полученный урон</param>
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Health = 0; // Устанавливаем здоровье в 0, если оно отрицательное
        }
    }
}
