using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace Lab2Project
{
    public partial class Form1 : Form
    {
        private Ranker _ranker;
        private List<Vacancy> _vacancies;

        public Form1()
        {
            InitializeComponent();
            _ranker = new Ranker(new List<string>());
            _vacancies = new List<Vacancy>();
        }

        // Обработчик кнопки "Поиск"
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var skills = textBox1.Text.Split(',').Select(s => s.Trim()).ToList();

                // Если навыки не введены, выбрасываем исключение
                if (skills.Count == 0 || string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    throw new ArgumentException("Не указаны навыки для поиска");
                }

                _ranker = new Ranker(skills);
                _vacancies = new MockDataCollector().GetVacancies();
                _vacancies = _ranker.RankList(_vacancies);
                dataGridView1.DataSource = _vacancies;
                label1.Text = $"Статус: Найдено {_vacancies.Count} вакансий";
            }
            catch (Exception ex)
            {
                label1.Text = $"Статус: Ошибка - {ex.Message}";
                dataGridView1.DataSource = null; // Очищаем таблицу
            }
        }

        // Обработчик кнопки "Сохранить"
        private void button2_Click(object sender, EventArgs e)
        {
            if (_vacancies == null || _vacancies.Count == 0)
            {
                MessageBox.Show("Нет данных для сохранения!");
                return;
            }

            try
            {
                File.WriteAllText("vacancies.txt", string.Join("\n", _vacancies.Select(v => v.Name)));
                label1.Text = "Статус: Данные сохранены в vacancies.txt";
            }
            catch (Exception ex)
            {
                label1.Text = "Статус: Ошибка сохранения - " + ex.Message;
            }
        }
    }
}