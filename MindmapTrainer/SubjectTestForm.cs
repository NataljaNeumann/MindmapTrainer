/*  Mindmap-Trainer aims to help people in training for exams
    Copyright (C) 2024-2025 NataljaNeumann@gmx.de

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
    //*******************************************************************************************************
    /// <summary>
    /// This class provides a dialog, so user can tests his/her skills in the loaded mind map domain
    /// </summary>
    //*******************************************************************************************************
    public partial class SubjectTestForm : Form
    {

        //===================================================================================================
        /// <summary>
        /// Constructs a new SubjectTestForm object
        /// </summary>
        //===================================================================================================
        public SubjectTestForm()
        {
            InitializeComponent();
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks the 'Show it' button
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        //===================================================================================================
        private void buttonShow_Click(object sender, EventArgs e)
        {
            m_lblElements.Show();
            m_btnCorrectResult.Show();
            m_btnWrongResult.Show();
            m_btnShow.Hide();
            //AcceptButton = buttonCorrect;
            m_btnCorrectResult.Focus();
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when the size of the tested sub-elements text changes
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        //===================================================================================================
        private void label2_SizeChanged(object sender, EventArgs e)
        {
            if (!m_lblElements.Text.Equals("points"))
            {
                m_lblElements.Location = 
                    new Point((Size.Width - m_lblElements.Size.Width) / 2, m_lblElements.Location.Y);
                Size = new Size(
                    Size.Width, m_lblElements.Location.Y + m_lblElements.Size.Height + 
                    m_lblSubject.Location.Y*2 + m_btnCanel.Size.Height + 
                    (m_lblElements.Location.Y - (m_lblSubject.Size.Height + m_lblSubject.Location.Y)) * 3);
            }
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks the 'It is correct' button
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        //===================================================================================================
        private void buttonCorrect_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user click the 'I was wrong' button
        /// </summary>
        /// <param name="sender">Sende object</param>
        /// <param name="e">Event args</param>
        //===================================================================================================
        private void buttonWrong_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks the 'Cancel' button
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        //===================================================================================================
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
