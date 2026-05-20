using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dailyvictory
{
    public partial class StatisticsForm : Form
    {
        DatabaseHelper db = new DatabaseHelper();
        public StatisticsForm()
        {
            InitializeComponent();

            lblDate.Text =
                DateTime.Now.ToLongDateString();

            LoadStatistics();
        }


        private void LoadStatistics()
        {
            var con = db.GetConnection();

            con.Open();

            // TOTAL HABITS
            string totalQuery =
                "SELECT COUNT(*) FROM Habits";

            SQLiteCommand cmd1 =
                new SQLiteCommand(totalQuery, con);

            int total =
                Convert.ToInt32(cmd1.ExecuteScalar());
            lblTotal.Text =
                "Total Habits : " + total;

            // BEST STREAK
            string streakQuery =
                "SELECT MAX(Streak) FROM Habits";

            SQLiteCommand cmd2 =
                new SQLiteCommand(streakQuery, con);

            object result =
                cmd2.ExecuteScalar();

            int streak = 0;

            if (result != DBNull.Value)
            {
                streak =
                    Convert.ToInt32(result);
            }

            lblStreak.Text =
                "Best Streak : " + streak;

            // LAST COMPLETED
            string completedQuery =
"SELECT HabitName FROM Habits WHERE LastCompletedDate != '' ORDER BY LastCompletedDate DESC LIMIT 1";
            SQLiteCommand cmd3 = new SQLiteCommand(completedQuery, con);
            object completed = cmd3.ExecuteScalar();
            if (completed != null)
            {
                lblCompleted.Text = "Last Completed : " + completed.ToString();
            }
            con.Close();
        }
        // CLOSE BUTTON

        private void lblStreak_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {

            this.Hide();
        }
    }
}
