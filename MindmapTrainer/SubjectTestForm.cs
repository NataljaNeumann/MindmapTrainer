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

                Size oOldSize = Size;
                Size oNewSize = new Size(
                    Size.Width, m_lblElements.Location.Y + m_lblElements.Size.Height + 
                    m_lblSubject.Location.Y*2 + m_btnCanel.Size.Height + 
                    (m_lblElements.Location.Y - (m_lblSubject.Size.Height + m_lblSubject.Location.Y)) * 3);

                if (m_oOriginalPositions != null)
                    m_oOriginalPositions.Clear();

                
                if (oNewSize.Width > oNewSize.Height)
                {
                    ReadyToUseImageInjection("MindmapTrainer-Header.jpg");
                }
                else
                {
                    ReadyToUseSideImageInjection("MindmapTrainer-Sidebar2.jpg");
                }

                Size = new Size(Size.Width - oOldSize.Width + oNewSize.Width, Size.Height - oOldSize.Height + oNewSize.Height);
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

        #region side image injection part
        //===================================================================================================
        /// <summary>
        /// Picture box control
        /// </summary>
        private PictureBox m_ctlPictureBox;
        //===================================================================================================
        /// <summary>
        /// Image
        /// </summary>
        private System.Drawing.Image m_oLoadedImage;
        //===================================================================================================
        /// <summary>
        /// A dictionary with positions of other elements
        /// </summary>
        private Dictionary<Control, int> m_oOriginalPositions;

        //===================================================================================================
        /// <summary>
        /// Loads an image from application startup path and shows it at the top of the window
        /// </summary>
        /// <param name="strName">Name of the image, without directory specifications</param>
        //===================================================================================================
        private void ReadyToUseSideImageInjection(string strImageName)
        {
            string strImagePath = System.IO.Path.Combine(Application.StartupPath, strImageName);
            if (System.IO.File.Exists(strImagePath))
            {
                m_oOriginalPositions = new Dictionary<Control, int>();
                foreach (Control ctl in Controls)
                {
                    m_oOriginalPositions[ctl] = ctl.Left;
                }

                m_ctlPictureBox = new PictureBox();
                m_ctlPictureBox.Location = this.ClientRectangle.Location;
                m_ctlPictureBox.Size = new Size(0, 0);
                Controls.Add(m_ctlPictureBox);

                LoadAndResizeSideImage(strImagePath);

                this.Resize += new EventHandler(ResizeSideImageAlongWithForm);
            }
        }

        //===================================================================================================
        /// <summary>
        /// Resizes image along with the form
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void ResizeSideImageAlongWithForm(object oSender, EventArgs oEventArgs)
        {
            ResizeSideImageAndShiftElements();
        }

        //===================================================================================================
        /// <summary>
        /// Loads an image and resizes it to the width of client area
        /// </summary>
        /// <param name="strImagePath"></param>
        //===================================================================================================
        private void LoadAndResizeSideImage(string strImagePath)
        {
            m_oLoadedImage = Image.FromFile(strImagePath);
            ResizeSideImageAndShiftElements();
        }

        //===================================================================================================
        /// <summary>
        /// Resizes image and shifts other elements
        /// </summary>
        //===================================================================================================
        private void ResizeSideImageAndShiftElements()
        {
            if (m_oLoadedImage != null)
            {
                if (WindowState != FormWindowState.Minimized)
                {
                    float fAspectRatio = (float)m_oLoadedImage.Width / (float)m_oLoadedImage.Height;

                    int nNewHeight = this.ClientSize.Height;
                    if (nNewHeight != 0)
                    {
                        int nNewWidth = (int)(nNewHeight * fAspectRatio);

                        int nWidthChange = nNewWidth - m_ctlPictureBox.Width;

                        this.m_ctlPictureBox.Image = new Bitmap(m_oLoadedImage, nNewWidth, nNewHeight);
                        this.m_ctlPictureBox.Size = new Size(nNewWidth, nNewHeight);

                        if (nWidthChange != 0)
                        {
                            this.Width += nWidthChange;
                            ShiftOtherElementsLeftOrRight(nWidthChange);
                        }
                    }
                }
            }
        }

        //===================================================================================================
        /// <summary>
        /// Shifts elements, apart from the image box up or down
        /// </summary>
        /// <param name="nNewPictureWidth">New width of the picture</param>
        //===================================================================================================
        private void ShiftOtherElementsLeftOrRight(int nWidthChange)
        {
            foreach (Control ctl in m_oOriginalPositions.Keys)
            {
                if ((ctl.Anchor & AnchorStyles.Right) == AnchorStyles.None)
                {
                    ctl.Left += nWidthChange;
                }
                else
                    if ((ctl.Anchor & AnchorStyles.Left) != AnchorStyles.None)
                    {
                        ctl.Left +=  nWidthChange;
                        //ctl.Width -= nWidthChange;
                    }
            }
        }
        #endregion

        #region image injection part
        //===================================================================================================
        /// <summary>
        /// Loads an image from application startup path and shows it at the top of the window
        /// </summary>
        /// <param name="strName">Name of the image, without directory specifications</param>
        //===================================================================================================
        private void ReadyToUseImageInjection(string strImageName)
        {
            string strImagePath = System.IO.Path.Combine(Application.StartupPath, strImageName);
            if (System.IO.File.Exists(strImagePath))
            {
                m_oOriginalPositions = new Dictionary<Control, int>();
                foreach (Control ctl in Controls)
                {
                    m_oOriginalPositions[ctl] = ctl.Top;
                }

                m_ctlPictureBox = new PictureBox();
                m_ctlPictureBox.Location = this.ClientRectangle.Location;
                m_ctlPictureBox.Size = new Size(0, 0);
                Controls.Add(m_ctlPictureBox);

                LoadAndResizeImage(strImagePath);

                this.Resize += new EventHandler(ResizeImageAlongWithForm);
            }
        }

        //===================================================================================================
        /// <summary>
        /// Resizes image along with the form
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void ResizeImageAlongWithForm(object oSender, EventArgs oEventArgs)
        {
            ResizeImageAndShiftElements();
        }

        //===================================================================================================
        /// <summary>
        /// Loads an image and resizes it to the width of client area
        /// </summary>
        /// <param name="strImagePath"></param>
        //===================================================================================================
        private void LoadAndResizeImage(string strImagePath)
        {
            m_oLoadedImage = Image.FromFile(strImagePath);
            ResizeImageAndShiftElements();
        }

        //===================================================================================================
        /// <summary>
        /// Resizes image and shifts other elements
        /// </summary>
        //===================================================================================================
        private void ResizeImageAndShiftElements()
        {
            if (m_oLoadedImage != null)
            {
                if (WindowState != FormWindowState.Minimized)
                {
                    float fAspectRatio = (float)m_oLoadedImage.Width / (float)m_oLoadedImage.Height;

                    int nNewWidth = this.ClientSize.Width;
                    if (nNewWidth != 0)
                    {
                        int nNewHeight = (int)(nNewWidth / fAspectRatio);

                        int nHeightChange = nNewHeight - m_ctlPictureBox.Height;

                        this.m_ctlPictureBox.Image = new Bitmap(m_oLoadedImage, nNewWidth, nNewHeight);
                        this.m_ctlPictureBox.Size = new Size(nNewWidth, nNewHeight);

                        this.Height += nHeightChange;
                        ShiftOtherElementsUpOrDown(nNewHeight);
                    }
                }
            }
        }

        //===================================================================================================
        /// <summary>
        /// Shifts elements, apart from the image box up or down
        /// </summary>
        /// <param name="nNewPictureHeight">New height of the picture</param>
        //===================================================================================================
        private void ShiftOtherElementsUpOrDown(int nNewPictureHeight)
        {
            foreach (Control ctl in m_oOriginalPositions.Keys)
            {
                if ((ctl.Anchor & AnchorStyles.Bottom) == AnchorStyles.None)
                    ctl.Top = m_oOriginalPositions[ctl] + nNewPictureHeight;
            }
        }
        #endregion
    }
}
