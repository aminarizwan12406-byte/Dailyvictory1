using Dailyvictory;
using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Dailyvictory
{
    public partial class Dashboard : Form
    {
        DatabaseHelper db = new DatabaseHelper();

        public Dashboard()
        {
            InitializeComponent();
            lblDate.Text =
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // BUTTON EVENTS MANUALLY CONNECT
            btnAdd.Click += btnAdd_Click;
            LoadHabits();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadHabits();
        }

        // LOAD HABITS
        private void LoadHabits()
        {
            dgvHabits.Rows.Clear();
            dgvHabits.Columns.Clear();

            dgvHabits.Columns.Add("HabitName", "Habit Name");
            dgvHabits.Columns.Add("Category", "Category");
            dgvHabits.Columns.Add("Streak", "Streak");
            dgvHabits.Columns.Add("Date", "Last Completed");

            var con = db.GetConnection();

            con.Open();

            string query = "SELECT * FROM Habits";

            SQLiteCommand cmd =
                new SQLiteCommand(query, con);

            SQLiteDataReader reader =
                cmd.ExecuteReader();

            while (reader.Read())
            {
                dgvHabits.Rows.Add(
                    reader["HabitName"].ToString(),
                    reader["Category"].ToString(),
                    reader["Streak"].ToString(),
                    reader["LastCompletedDate"].ToString()
                );
            }

            con.Close();
        }

        // ADD HABIT
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddHabitForm a =
     new AddHabitForm();

            a.ShowDialog();
            LoadHabits();
        }

        // DELETE HABIT
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvHabits.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select Habit");
                return;
            }

            string habit =
                dgvHabits.SelectedRows[0]
                .Cells[0].Value.ToString();

            var con = db.GetConnection();

            con.Open();

            string query =
                "DELETE FROM Habits WHERE HabitName=@name";

            SQLiteCommand cmd =
                new SQLiteCommand(query, con);

            cmd.Parameters.AddWithValue("@name", habit);

            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Habit Deleted");

            LoadHabits();
        }

        // COMPLETE + STREAK

        private void btnComplete_Click(object sender, EventArgs e)
        {
            if (dgvHabits.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select Habit");
                return;
            }

            string habit =
                dgvHabits.SelectedRows[0]
                .Cells[0].Value.ToString();

            var con = db.GetConnection();

            con.Open();

            string query =
                "UPDATE Habits SET " +
                "Streak = Streak + 1, " +
                "LastCompletedDate=@d " +
                "WHERE HabitName=@h";

            SQLiteCommand cmd =
                new SQLiteCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@d",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            cmd.Parameters.AddWithValue("@h", habit);

            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Habit Completed");

            LoadHabits();
        }

        // Stats
        private void btnStatistics_Click(object sender, EventArgs e)
        {
            StatisticsForm s =
               new StatisticsForm();

            s.ShowDialog();
        }
        // Logout
        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm l =
                new LoginForm();

            l.Show();

            this.Hide();
        }


    }
}
