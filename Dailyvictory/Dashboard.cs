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
            lstHabits.Items.Clear();

            DatabaseHelper db =
                new DatabaseHelper();

            var con = db.GetConnection();

            con.Open();

            string query =
                "SELECT * FROM Habits";

            SQLiteCommand cmd =
                new SQLiteCommand(query, con);

            SQLiteDataReader reader =
                cmd.ExecuteReader();

            while (reader.Read())
            {
                lstHabits.Items.Add(
                    reader["HabitName"].ToString()
                    + " - " +
                    reader["Category"].ToString()
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
            if (lstHabits.SelectedItem == null)
            {
                MessageBox.Show("Select Habit");
                return;
            }

            string habit =
lstHabits.SelectedItem.ToString().Split('-')[0].Trim();

            MessageBox.Show(habit);

            SQLiteConnection con =
                db.GetConnection();

            con.Open();

            string query =
                "DELETE FROM Habits WHERE HabitName=@name";

            SQLiteCommand cmd =
                new SQLiteCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@name", habit);

            int rows =
                cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show(
                rows + " Deleted");

            LoadHabits();
        }

        // COMPLETE + STREAK

        private void btnComplete_Click(object sender, EventArgs e)
        {
            if (lstHabits.SelectedItem == null)
            {
                MessageBox.Show("Select Habit");
                return;
            }

            string habit = lstHabits.SelectedItem.ToString().Split('-')[0].Trim();

            using (var con = db.GetConnection())
            using (var cmd = con.CreateCommand())
            {
                con.Open();
                cmd.CommandText = @"UPDATE Habits
                            SET Streak = Streak + 1, LastCompletedDate=@d
                            WHERE LOWER(TRIM(HabitName)) = LOWER(TRIM(@h))";

                cmd.Parameters.AddWithValue("@d", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@h", habit);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    MessageBox.Show("Habit Completed! Rows affected: " + rows);
                else
                    MessageBox.Show("ERROR: No rows updated! Check habit name: '" + habit + "'");
            }

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