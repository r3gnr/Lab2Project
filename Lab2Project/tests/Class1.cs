using FlaUI.Core;
using FlaUI.UIA3;
using FlaUI.Core.AutomationElements;
using NUnit.Framework;
using System.IO;

[TestFixture]
public class UITests
{
    private Application _app;
    private UIA3Automation _automation;
    private FlaUI.Core.AutomationElements.Window _mainWindow;

    [SetUp]
    public void Setup()
    {
        // Запуск приложения
        _app = Application.Launch(@"C:\Users\r3gn\source\repos\Lab2Project\Lab2Project\bin\Debug\Lab2Project.exe");
        _automation = new UIA3Automation();
        _mainWindow = _app.GetMainWindow(_automation);
    }

    // Тест 1: Успешный поиск вакансий
    [Test]
    public void Search_ValidSkills_ShowsVacancies()
    {
        // Ввод навыков
        var skillsBox = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("textBox1")).AsTextBox();
        skillsBox.Text = "C#, SQL";

        // Нажатие кнопки "Поиск"
        var searchButton = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("button1")).AsButton();
        searchButton.Click();

        // Проверка, что таблица обновилась
        var grid = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("dataGridView1")).AsDataGridView();
        Assert.That(grid.Rows.Length, Is.GreaterThan(0), "Таблица не содержит данных после поиска");
    }

    // Тест 2: Сохранение списка в файл
    [Test]
    public void SaveButton_Click_CreatesFile()
    {
        // Выполняем поиск, чтобы заполнить список
        var skillsBox = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("textBox1")).AsTextBox();
        skillsBox.Text = "C#";
        var searchButton = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("button1")).AsButton();
        searchButton.Click();

        // Нажимаем кнопку "Сохранить"
        var saveButton = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("button2")).AsButton();
        saveButton.Click();

        // Проверяем файл
        Assert.That(File.Exists("vacancies.txt"), Is.True, "Файл не создан");
    }

        // Тест 3: Обработка пустого ввода навыков
        [Test]
    public void Search_EmptySkills_ShowsErrorMessage()
    {
        // Очистка поля ввода
        var skillsBox = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("textBox1")).AsTextBox();
        skillsBox.Text = "";

        // Нажатие кнопки "Поиск"
        var searchButton = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("button1")).AsButton();
        searchButton.Click();

        // Проверка сообщения об ошибке
        var statusLabel = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("label1")).AsLabel();
        Assert.That(statusLabel.Text, Does.Contain("Ошибка").IgnoreCase, "Сообщение об ошибке не отображается");
    }

    [TearDown]
    public void TearDown()
    {
        // Закрытие приложения и очистка
        _automation.Dispose();
        _app.Close();
        if (File.Exists("vacancies.txt")) File.Delete("vacancies.txt");
    }
}