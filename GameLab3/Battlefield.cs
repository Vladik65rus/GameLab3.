using System;
using System.Collections.Generic;

[Serializable]
public class Battlefield
{
    public Player Player1 { get; private set; }
    public Player Player2 { get; private set; }
    public Action<string> LogEvent { get; set; } // Делегат для логирования событий

    // Конструктор, принимающий двух игроков
    public Battlefield(Player player1, Player player2)
    {
        Player1 = player1 ?? throw new ArgumentNullException(nameof(player1), "Player1 не может быть null.");
        Player2 = player2 ?? throw new ArgumentNullException(nameof(player2), "Player2 не может быть null.");
    }

    // Метод для начала битвы
    public void StartBattle()
    {
        if (!ValidateBattleSetup())
        {
            return;
        }

        LogEvent?.Invoke("Битва началась!");

        var player1Units = Player1.GetActiveUnits();
        var player2Units = Player2.GetActiveUnits();

        while (player1Units.Count > 0 && player2Units.Count > 0)
        {
            for (int i = 0; i < Math.Min(player1Units.Count, player2Units.Count); i++)
            {
                ExecuteAttack(player1Units[i], player2Units[i], Player1, Player2);
                if (player2Units[i].IsAlive())
                {
                    ExecuteAttack(player2Units[i], player1Units[i], Player2, Player1);
                }
            }

            // Обновляем списки активных юнитов
            player1Units = Player1.GetActiveUnits();
            player2Units = Player2.GetActiveUnits();
        }

        LogEvent?.Invoke("Битва завершена!");

        // Определяем победителя
        AnnounceWinner();
    }

    // Валидация перед началом битвы
    private bool ValidateBattleSetup()
    {
        if (Player1 == null || Player2 == null)
        {
            LogEvent?.Invoke("Битва не может начаться: один из игроков не определен.");
            return false;
        }

        if (Player1.GetActiveUnits().Count == 0 || Player2.GetActiveUnits().Count == 0)
        {
            LogEvent?.Invoke("Битва не может начаться: у одного из игроков нет активных юнитов.");
            return false;
        }

        return true;
    }

    // Выполнение атаки между двумя юнитами
    private void ExecuteAttack(Unit attacker, Unit target, Player attackerPlayer, Player targetPlayer)
    {
        attacker.Attack(target);
        LogEvent?.Invoke($"{attackerPlayer.Name}'s {attacker.Name} атакует {targetPlayer.Name}'s {target.Name}. У {targetPlayer.Name}'s {target.Name} осталось {Math.Max(0, target.Health)} здоровья.");

        if (!target.IsAlive())
        {
            LogEvent?.Invoke($"{targetPlayer.Name}'s {target.Name} погиб.");
        }
    }

    // Объявление победителя
    private void AnnounceWinner()
    {
        var winner = DetermineWinner();
        if (winner != null)
        {
            LogEvent?.Invoke($"{winner.Name} победил!");
        }
        else
        {
            LogEvent?.Invoke("Битва закончилась вничью.");
        }
    }

    // Метод для определения победителя
    public Player DetermineWinner()
    {
        bool player1HasUnits = Player1.GetActiveUnits().Count > 0;
        bool player2HasUnits = Player2.GetActiveUnits().Count > 0;

        if (player1HasUnits && !player2HasUnits) return Player1;
        if (player2HasUnits && !player1HasUnits) return Player2;

        return null; // Ничья
    }
}
