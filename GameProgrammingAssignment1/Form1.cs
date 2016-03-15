using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameProgrammingAssignment1
{
    public partial class Form1 : Form
    {
        const int NUM_OF_SEATS = 15;
        int rowMax = 5;
        int colMax = 3;
        int row = 0;
        int col = 0;
        int seatsAvailable = NUM_OF_SEATS;
        string selectError = "";
        bool rowSelected = false;
        bool colSelected = false;
        string[] waitingList = new string[10] { "", "", "", "", "", "", "", "", "", ""};
        string[,] seatmap = new string[5, 3] { { "", "", "" }, { "", "", "" }, { "", "", "" }, { "", "", "" }, { "", "", "" } };
        public Form1()
        {
            InitializeComponent();
            listRow.Items.Add("0");
            listRow.Items.Add("1");
            listRow.Items.Add("2");
            listRow.Items.Add("3");
            listRow.Items.Add("4");
            listCol.Items.Add("0");
            listCol.Items.Add("1");
            listCol.Items.Add("2");
        }

        
        private void listRow_SelectedIndexChanged(object sender, EventArgs e)
        {
            row = int.Parse(listRow.GetItemText(listRow.SelectedItem));
            rowSelected = true;
        }

        private void listCol_SelectedIndexChanged(object sender, EventArgs e)
        {
            col = int.Parse(listCol.GetItemText(listCol.SelectedItem));
            colSelected = true;
        }
        bool checkSelect()
        {
            if(rowSelected == true && colSelected == true)
            {
                return true;
            }
            if (rowSelected != true && colSelected == true)
            {
                selectError = "You haven't selected a row.";
                return false;
            }
            if (rowSelected == true && colSelected != true)
            {
                selectError = "You haven't selected a column.";
                return false;
            }
            else
            {
                selectError = "You haven't selected a row. \nYou haven't selected a column.";
                return false;
            }
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            if (checkSelect())
            {
                if (txtName.Text != "")
                {
                    if (seatsAvailable > 0)
                    {
                        if (seatmap[row, col] == "")
                        {
                            seatmap[row, col] = txtName.Text;
                            seatsAvailable--;
                            MessageBox.Show("You have succesfully booked the seat.");
                        }
                        else
                        {
                            MessageBox.Show("That seat has been taken, choose another seat and try again.");
                        }
                    }
                    else
                    {
                        if (waitingList[waitingList.Length - 1] != "")
                        {
                            MessageBox.Show("Waiting list is full.");
                        }
                        else
                        {
                            for (int i = 0; i < waitingList.Length; i++)
                            {
                                if (waitingList[i] == "")
                                {
                                    waitingList[i] = txtName.Text;
                                    MessageBox.Show("You have been added to the waiting list");
                                    break;
                                }
                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("You haven't input the name!");
                } 
            }
            else
            {
                MessageBox.Show(selectError);
            }
        }


        private void btnStatus_Click(object sender, EventArgs e)
        {
            if (checkSelect())
            {
                if (seatmap[row, col] != "")
                {
                    txtStatus.Text = "Unavailable";
                }
                else
                {
                    txtStatus.Text = "Available";
                } 
            }
            else
            {
                MessageBox.Show(selectError);
            }
        }
        private void btnSeat_Click(object sender, EventArgs e)
        {
            string result = "";
            for (int i = 0; i < rowMax; i++)
            {
                for (int j = 0; j < colMax; j++)
                {
                    result += "["+ i + ", "+ j + "]:"+ seatmap[i,j]+ "\n";
                }
            }
            rtbSeat.Text = result;
        }

        private void btmCancel_Click(object sender, EventArgs e)
        {
            if (checkSelect())
            {
                if (seatmap[row, col] != "")
                {
                    seatmap[row, col] = "";
                    seatsAvailable++;
                    if (waitingList[0] != "")
                    {
                        seatmap[row, col] = waitingList[0];
                        for (int i = 0; i < waitingList.Length - 1; i++)
                        {
                            waitingList[i] = waitingList[i + 1];
                        }
                        seatsAvailable--;
                    }
                    MessageBox.Show("You have succesfully canceled the seat.");
                }
                else
                {
                    MessageBox.Show("That seat is already empty!");
                } 
            }
            else
            {
                MessageBox.Show(selectError);
            }
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            seatsAvailable = 0;
            for (int i = 0; i < rowMax; i++)
            {
                for (int j = 0; j < colMax; j++)
                {
                    seatmap[i, j]= "Ziming";
                }
            }
        }

        private void btnAddWait_Click(object sender, EventArgs e)
        {
            if(seatsAvailable > 0)
            {
                MessageBox.Show("We still have seats left for you.");
            }
            else
            {
                if(waitingList[waitingList.Length-1] !="")
                {
                    MessageBox.Show("Waiting list is full.");
                }
                else
                {
                    for (int i = 0; i < waitingList.Length; i++)
                    {
                        if (waitingList[i] == "")
                        {
                            waitingList[i] = txtName.Text;
                            MessageBox.Show("You have been added to the waiting list");
                            break;
                        }
                    }
                }
            }
        }

        private void btnShowWait_Click(object sender, EventArgs e)
        {
            string WaitResult = "";
            for (int i = 0; i < waitingList.Length; i++)
            {
                WaitResult+=""+(i+1)+":"+waitingList[i]+"\n";
            }
            rtbWait.Text = WaitResult;
        }

        private void btnRealDebug_Click(object sender, EventArgs e)
        {
            MessageBox.Show("" + seatsAvailable);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            string WaitResult = "";
            for (int i = 0; i < waitingList.Length; i++)
            {
                WaitResult += "" + (i + 1) + ":" + waitingList[i] + "\n";
            }
            rtbWait.Text = WaitResult;
            string result = "";
            for (int i = 0; i < rowMax; i++)
            {
                for (int j = 0; j < colMax; j++)
                {
                    result += "[" + i + ", " + j + "]:" + seatmap[i, j] + "\n";
                }
            }
            rtbSeat.Text = result;
        }
    }
}
