using System;
using System.Windows.Forms;

namespace GameLab3
{
    public partial class MainForm : Form
    {
        private GameController _gameController; // Контроллер игры
        private TextBox textBoxLog; // Поле для логов
        private ListBox listBoxPlayer1; // Список юнитов игрока 1
        private ListBox listBoxPlayer2; // Список юнитов игрока 2
        private Label labelPlayer1Health; // Здоровье игрока 1
        private Label labelPlayer1Gold; // Золото игрока 1
        private Label labelPlayer2Health; // Здоровье игрока 2
        private Label labelPlayer2Gold; // Золото игрока 2
        private Button buttonStartGame; // Кнопка "Начать игру"
        private Button buttonSaveGame; // Кнопка "Сохранить игру"
        private Button buttonLoadGame; // Кнопка "Загрузить игру"

        public MainForm()
        {
            InitializeComponent(); // Инициализация компонентов формы
            InitializeGameController(); // Инициализация контроллера игры
        }

        /// <summary>
        /// Инициализация контроллера игры.
        /// </summary>
        private void InitializeGameController()
        {
            _gameController = new GameController(LogToTextBox);
        }

        /// <summary>
        /// Логирует сообщение в текстовое поле.
        /// </summary>
        private void LogToTextBox(string message)
        {
            if (textBoxLog.InvokeRequired)
            {
                textBoxLog.Invoke((Action)(() => textBoxLog.AppendText(message + Environment.NewLine)));
            }
            else
            {
                textBoxLog.AppendText(message + Environment.NewLine);
            }
        }

        /// <summary>
        /// Обновляет интерфейс пользователя.
        /// </summary>
        private void UpdateUI()
        {
            // Очищаем логи
            textBoxLog.Clear();

            // Обновляем списки юнитов
            listBoxPlayer1.Items.Clear();
            listBoxPlayer2.Items.Clear();

            foreach (var unit in _gameController.GetPlayer1Units())
            {
                listBoxPlayer1.Items.Add($"{unit.Name} - HP: {unit.Health}, ATK: {unit.AttackPower}");
            }

            foreach (var unit in _gameController.GetPlayer2Units())
            {
                listBoxPlayer2.Items.Add($"{unit.Name} - HP: {unit.Health}, ATK: {unit.AttackPower}");
            }

            // Обновляем информацию о здоровье и золоте
            var player1 = _gameController.GetPlayer1();
            var player2 = _gameController.GetPlayer2();

            labelPlayer1Gold.Text = $"Золото: {player1.Gold}";
            labelPlayer1Health.Text = $"Здоровье: {player1.Health}";

            labelPlayer2Gold.Text = $"Золото: {player2.Gold}";
            labelPlayer2Health.Text = $"Здоровье: {player2.Health}";
        }

        /// <summary>
        /// Обработчик кнопки "Начать игру".
        /// </summary>
        private void ButtonStartGame_Click(object sender, EventArgs e)
        {
            try
            {
                _gameController.StartNewGame();
                UpdateUI();
                LogToTextBox("Игра началась!");
            }
            catch (Exception ex)
            {
                LogToTextBox($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик кнопки "Сохранить игру".
        /// </summary>
        private void ButtonSaveGame_Click(object sender, EventArgs e)
        {
            try
            {
                _gameController.SaveGameStateToFile();
                LogToTextBox("Игра сохранена.");
            }
            catch (Exception ex)
            {
                LogToTextBox($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик кнопки "Загрузить игру".
        /// </summary>
        private void ButtonLoadGame_Click(object sender, EventArgs e)
        {
            try
            {
                _gameController.LoadGameStateFromFile();
                UpdateUI();
                LogToTextBox("Игра загружена.");
            }
            catch (Exception ex)
            {
                LogToTextBox($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Метод для инициализации компонентов формы.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxLog = new TextBox();
            this.listBoxPlayer1 = new ListBox();
            this.listBoxPlayer2 = new ListBox();
            this.labelPlayer1Health = new Label();
            this.labelPlayer1Gold = new Label();
            this.labelPlayer2Health = new Label();
            this.labelPlayer2Gold = new Label();
            this.buttonStartGame = new Button();
            this.buttonSaveGame = new Button();
            this.buttonLoadGame = new Button();

            // Настройка компонентов
            this.SuspendLayout();

            // TextBox Log
            this.textBoxLog.Location = new System.Drawing.Point(10, 250);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(360, 100);

            // ListBox Player1
            this.listBoxPlayer1.Location = new System.Drawing.Point(10, 10);
            this.listBoxPlayer1.Size = new System.Drawing.Size(150, 150);

            // ListBox Player2
            this.listBoxPlayer2.Location = new System.Drawing.Point(220, 10);
            this.listBoxPlayer2.Size = new System.Drawing.Size(150, 150);

            // Label Player1 Health
            this.labelPlayer1Health.Location = new System.Drawing.Point(10, 170);
            this.labelPlayer1Health.Size = new System.Drawing.Size(150, 20);

            // Label Player1 Gold
            this.labelPlayer1Gold.Location = new System.Drawing.Point(10, 200);
            this.labelPlayer1Gold.Size = new System.Drawing.Size(150, 20);

            // Label Player2 Health
            this.labelPlayer2Health.Location = new System.Drawing.Point(220, 170);
            this.labelPlayer2Health.Size = new System.Drawing.Size(150, 20);

            // Label Player2 Gold
            this.labelPlayer2Gold.Location = new System.Drawing.Point(220, 200);
            this.labelPlayer2Gold.Size = new System.Drawing.Size(150, 20);

            // Button Start Game
            this.buttonStartGame.Location = new System.Drawing.Point(10, 360);
            this.buttonStartGame.Size = new System.Drawing.Size(100, 30);
            this.buttonStartGame.Text = "Начать игру";
            this.buttonStartGame.Click += new EventHandler(this.ButtonStartGame_Click);

            // Button Save Game
            this.buttonSaveGame.Location = new System.Drawing.Point(120, 360);
            this.buttonSaveGame.Size = new System.Drawing.Size(100, 30);
            this.buttonSaveGame.Text = "Сохранить игру";
            this.buttonSaveGame.Click += new EventHandler(this.ButtonSaveGame_Click);

            // Button Load Game
            this.buttonLoadGame.Location = new System.Drawing.Point(230, 360);
            this.buttonLoadGame.Size = new System.Drawing.Size(100, 30);
            this.buttonLoadGame.Text = "Загрузить игру";
            this.buttonLoadGame.Click += new EventHandler(this.ButtonLoadGame_Click);

            // MainForm
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.listBoxPlayer1);
            this.Controls.Add(this.listBoxPlayer2);
            this.Controls.Add(this.labelPlayer1Health);
            this.Controls.Add(this.labelPlayer1Gold);
            this.Controls.Add(this.labelPlayer2Health);
            this.Controls.Add(this.labelPlayer2Gold);
            this.Controls.Add(this.buttonStartGame);
            this.Controls.Add(this.buttonSaveGame);
            this.Controls.Add(this.buttonLoadGame);
            this.Name = "MainForm";
            this.Text = "Автошахматы";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        /// <summary>
        /// Освобождение ресурсов.
        /// </summary>
        /// 
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
