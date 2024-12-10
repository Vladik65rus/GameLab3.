using System;
using System.IO;
using System.Collections.Generic;

public class GameController
{
    private GameState _currentGameState; // Текущее состояние игры
    private readonly Action<string> _logToUI; // Делегат для логирования сообщений в интерфейс

    public GameController(Action<string> logToUI)
    {
        _logToUI = logToUI ?? throw new ArgumentNullException(nameof(logToUI));
        StartNewGame();
    }

    /// <summary>
    /// Начинает новую игру.
    /// </summary>
    public void StartNewGame()
    {
        var player1 = new Player("Player 1", 100, 50);
        var player2 = new Player("Player 2", 100, 50);

        // Добавляем юнитов игрокам
        player1.Units.Add(new Unit("Warrior", 100, 20, 10));
        player1.Units.Add(new Unit("Archer", 80, 25, 5));
        player2.Units.Add(new Unit("Mage", 70, 30, 8));
        player2.Units.Add(new Unit("Knight", 120, 15, 12));

        _currentGameState = new GameState
        {
            Battlefield = new Battlefield(player1, player2)
            {
                LogEvent = _logToUI
            }
        };

        _logToUI?.Invoke("Игра началась! Игроки получили начальные юниты.");
    }

    /// <summary>
    /// Возвращает игрока 1.
    /// </summary>
    public Player GetPlayer1()
    {
        return _currentGameState?.Battlefield.Player1;
    }

    /// <summary>
    /// Возвращает юниты игрока 1.
    /// </summary>
    public List<Unit> GetPlayer1Units()
    {
        return _currentGameState?.Battlefield.Player1.Units;
    }

    /// <summary>
    /// Возвращает игрока 2.
    /// </summary>
    public Player GetPlayer2()
    {
        return _currentGameState?.Battlefield.Player2;
    }

    /// <summary>
    /// Возвращает юниты игрока 2.
    /// </summary>
    public List<Unit> GetPlayer2Units()
    {
        return _currentGameState?.Battlefield.Player2.Units;
    }

    /// <summary>
    /// Запускает битву между игроками.
    /// </summary>
    public void StartBattle()
    {
        if (_currentGameState == null)
        {
            _logToUI?.Invoke("Ошибка: Игра не инициализирована.");
            return;
        }

        _currentGameState.Battlefield.StartBattle();
    }

    /// <summary>
    /// Сохраняет текущее состояние игры в файл.
    /// </summary>
    public void SaveGameStateToFile()
    {
        if (_currentGameState == null)
        {
            _logToUI?.Invoke("Ошибка: Нет текущего состояния игры для сохранения.");
            return;
        }

        try
        {
            const string filePath = "gameState.dat";
            _currentGameState.Save(filePath);
            _logToUI?.Invoke("Игра успешно сохранена.");
        }
        catch (Exception ex)
        {
            _logToUI?.Invoke($"Ошибка при сохранении игры: {ex.Message}");
        }
    }

    /// <summary>
    /// Загружает состояние игры из файла.
    /// </summary>
    public void LoadGameStateFromFile()
    {
        try
        {
            const string filePath = "gameState.dat";
            if (File.Exists(filePath))
            {
                _currentGameState = GameState.Load(filePath);
                _logToUI?.Invoke("Игра успешно загружена.");
            }
            else
            {
                _logToUI?.Invoke("Ошибка: Файл сохранения не найден.");
            }
        }
        catch (Exception ex)
        {
            _logToUI?.Invoke($"Ошибка при загрузке игры: {ex.Message}");
        }
    }
}
