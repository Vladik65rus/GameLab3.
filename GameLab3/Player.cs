using System;
using System.Collections.Generic;
[Serializable]
public class Player
{
    public string Name { get; } // Имя игрока
    public int Health { get; set; } // Здоровье игрока
    public int Gold { get; set; } // Золото игрока
    public List<Unit> Units { get; } // Список юнитов игрока

    /// <summary>
    /// Конструктор для инициализации игрока.
    /// </summary>
    /// <param name="name">Имя игрока</param>
    /// <param name="health">Здоровье игрока</param>
    /// <param name="gold">Количество золота игрока</param>
    public Player(string name, int health = 100, int gold = 50)
    {
        Name = name;
        Health = health;
        Gold = gold;
        Units = new List<Unit>();
    }

    /// <summary>
    /// Возвращает список активных (живых) юнитов.
    /// </summary>
    /// <returns>Список активных юнитов</returns>
    public List<Unit> GetActiveUnits()
    {
        return Units.FindAll(unit => unit.IsAlive());
    }
}
