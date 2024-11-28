/*  Mindmap-Trainer aims to help people in training for exams
    Copyright (C) 2024 NataljaNeumann@gmx.de

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation; either version 2 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License along
    with this program; if not, write to the Free Software Foundation, Inc.,
    51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
*/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MindmapTrainer
{
    public partial class SubjectTestForm : Form
    {
        public SubjectTestForm()
        {
            InitializeComponent();
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            label2.Show();
            buttonCorrect.Show();
            buttonWrong.Show();
            buttonShow.Hide();
            //AcceptButton = buttonCorrect;
            buttonCorrect.Focus();
        }

        private void label2_SizeChanged(object sender, EventArgs e)
        {
            if (!label2.Text.Equals("points"))
            {
                label2.Location = new Point((Size.Width - label2.Size.Width) / 2, label2.Location.Y);
                Size = new Size(Size.Width, label2.Location.Y + label2.Size.Height + label1.Location.Y*2 + buttonCancel.Size.Height + (label2.Location.Y - (label1.Size.Height + label1.Location.Y)) * 3);
            }
        }

        private void buttonCorrect_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void buttonWrong_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
