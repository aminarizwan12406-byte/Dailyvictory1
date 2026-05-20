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
    public partial class AddHabitForm : Form
    {
        public AddHabitForm()
        {
            InitializeComponent();
        }
        // SAVE BUTTON
        private void btnSave_Click(object sender, EventArgs e)
        {
            DatabaseHelper db =
        new DatabaseHelper();

            var con = db.GetConnection();

            con.Open();

            string query =
                "INSERT INTO Habits(HabitName,Category,Streak,LastCompletedDate) VALUES(@n,@c,0,'')";

            SQLiteCommand cmd =
                new SQLiteCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@n", txtHabit.Text);

            cmd.Parameters.AddWithValue(
                "@c", cmbCategory.Text);

            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Habit Added");

            this.Close();

        }
        // CANCEL BUTTON
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
