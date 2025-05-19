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
using System.IO;

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
        /// Indicates if we have main picture
        /// </summary>
        bool m_bWithPicture;

        //===================================================================================================
        /// <summary>
        /// A replacement picture to show, when user clicks the "Show answer" button
        /// </summary>
        string m_strReplacementPicture;

        //===================================================================================================
        /// <summary>
        /// Constructs a new SubjectTestForm object
        /// </summary>
        //===================================================================================================
        public SubjectTestForm()
        {
            InitializeComponent();

            // set maximum size of the form
            MaximumSize = new Size(
                        (int)(Screen.PrimaryScreen.Bounds.Width * 0.95),
                        (int)(Screen.PrimaryScreen.Bounds.Height * 0.95)
                    );
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks the 'Show it' button
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void buttonShow_Click(
            object oSender, 
            EventArgs oEventArgs
            )
        {
            if (m_strReplacementPicture != null)
            {
                try
                {
                    m_ctlMainPicture.Image = Image.FromFile(m_strReplacementPicture);
                }
                catch (Exception oEx)
                {
                    MessageBox.Show(oEx.Message);
                }
            }

            m_lblElements.Show();
            m_btnCorrectResult.Show();
            m_btnWrongResult.Show();
            m_btnShow.Hide();
            //AcceptButton = buttonCorrect;
            m_btnCorrectResult.Focus();
        }


        //===================================================================================================
        /// <summary>
        /// Tries to find out if the string represents a valid path
        /// </summary>
        /// <param name="strInput">Input string. Can be abolute or relative path</param>
        /// <returns>The absolute path, represented by input string</returns>
        //===================================================================================================
        public static string ExtractPath(
            string strInput
            )
        {
            try
            {
                // Test for UNC path
                if (Uri.IsWellFormedUriString(strInput, UriKind.Absolute) 
                    && strInput.StartsWith(@"\\"))
                {
                    return strInput;
                }

                // Test for absolute path
                if (Path.IsPathRooted(strInput))
                {
                    return strInput;
                }

                // Test for relative path to pictures directory
                string strCombinedPath = 
                    Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                        strInput);
                if (File.Exists(strCombinedPath))
                {
                    return strCombinedPath;
                }

                // Test for relative path to documents directory
                strCombinedPath = 
                    Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), 
                        strInput);
                if (File.Exists(strCombinedPath))
                {
                    return strCombinedPath;
                }
            }
            catch (Exception)
            {
                // ignore, just return null
            }

            // No valid path found
            return null;
        }

        
        //===================================================================================================
        /// <summary>
        /// Dinamically sets the correct anser text to show
        /// </summary>
        /// <param name="strInputText">Text to show, can contain picture path in first two lines</param>
        //===================================================================================================
        public void SetText(
            string strInputText
            )
        {
            // Process the input text
            string[] astrLines = strInputText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            try
            {
                if (astrLines.Length > 0 && astrLines[0].Length > 5)
                {
                    // Check if the first line is a valid image pat
                    string strImagePath = ExtractPath(astrLines[0].Substring(2));
                    if (strImagePath != null && File.Exists(strImagePath))
                    {
                        Image oOriginalImage = Image.FromFile(strImagePath);

                        // Scale the image proportionally to fit the client width
                        int nMaxPictureWidth = Screen.PrimaryScreen.Bounds.Width * 3 / 4;
                        int nMaxPictureHeight = Screen.PrimaryScreen.Bounds.Height / 2;
                        int nNewPictureWidth = oOriginalImage.Width;
                        int nNewPictureHeight = oOriginalImage.Height;

                        // increase max height, if there aren't that many points to show
                        if (astrLines.Length < 5)
                            nMaxPictureHeight = nMaxPictureHeight * 3 / 2;

                        if (nNewPictureWidth > nMaxPictureWidth)
                        {
                            float scaleFactor = (float)nMaxPictureWidth / nNewPictureWidth;
                            nNewPictureWidth = nMaxPictureWidth;
                            nNewPictureHeight = (int)(nNewPictureHeight * scaleFactor);
                        };

                        if (nNewPictureHeight > nMaxPictureHeight)
                        {
                            float scaleFactor = (float)nMaxPictureHeight / nNewPictureHeight;
                            nNewPictureWidth = (int)(nNewPictureWidth * scaleFactor);
                            nNewPictureHeight = nMaxPictureHeight;
                        };


                        m_ctlMainPicture.Width = nNewPictureWidth;
                        m_ctlMainPicture.Height = nNewPictureHeight;

                        m_lblSubject.BackColor = System.Drawing.SystemColors.ControlDark;

                        m_ctlMainPicture.Image = oOriginalImage;
                        m_ctlMainPicture.Visible = true;
                        m_bWithPicture = true;

                        if (astrLines.Length > 1 && astrLines[1].Length > 5)
                        {
                            string strPathToSecondImage = ExtractPath(astrLines[1].Substring(2));
                            if (strPathToSecondImage != null && File.Exists(strPathToSecondImage))
                            {
                                m_strReplacementPicture = strPathToSecondImage;

                                // Skip the first two lines
                                m_lblElements.Text =
                                    string.Join(Environment.NewLine, astrLines, 2, astrLines.Length - 2);
                                return;
                            }
                        }

                        // Skip the first line
                        m_lblElements.Text =
                            string.Join(Environment.NewLine, astrLines, 1, astrLines.Length - 1);
                        return;
                    }
                }
            }
            catch (Exception)
            {
                // ignore
            }

            // No picture path found
            m_bWithPicture = false;
            m_ctlMainPicture.Visible = false;
            // Use all lines if no valid image path
            m_lblElements.Text = strInputText; 
            
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when the size of the tested sub-elements text changes
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void OnAnswerLabelSizeChanged(
            object oSender, 
            EventArgs oEventArgs
            )
        {
            if (!m_lblElements.Text.Equals("points"))
            {
                int nNewWidth;

                if (m_bWithPicture)
                {
                    nNewWidth = ClientSize.Width > m_ctlMainPicture.Width + 20 ?
                        Size.Width :
                        Size.Width - ClientSize.Width + 20 + m_ctlMainPicture.Width;

                    m_ctlMainPicture.Location = 
                        new Point((nNewWidth - m_ctlMainPicture.Width) / 2, m_ctlMainPicture.Top);
                    m_lblElements.Location =
                        new Point((nNewWidth - m_lblElements.Size.Width) / 2, m_ctlMainPicture.Bottom + 10);
                }
                else
                {
                    nNewWidth = Size.Width;

                    m_lblElements.Location =  
                        new Point((nNewWidth - m_lblElements.Size.Width) / 2, m_lblElements.Location.Y);
                }



                Size oOldSize = Size;
                Size oNewSize = new Size(
                    nNewWidth,
                    m_lblElements.Bottom + 
                    ClientRectangle.Top +
                    m_lblSubject.Top * 6 + m_btnCanel.Height);

                if (m_oOriginalPositions != null)
                    m_oOriginalPositions.Clear();


                if (!m_bWithPicture)
                {
                    if (oNewSize.Width > oNewSize.Height)
                    {
                        ReadyToUseImageInjection("MindmapTrainer-Header.jpg");
                    }
                    else
                    {
                        ReadyToUseSideImageInjection("MindmapTrainer-Sidebar2.jpg");
                    }
                }

                Size = new Size(
                    Size.Width - oOldSize.Width + oNewSize.Width, 
                    Size.Height - oOldSize.Height + oNewSize.Height);
            }
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks the 'It is correct' button
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void OnCorrectClick(
            object oSender, 
            EventArgs oEventArgs
            )
        {
            DialogResult = DialogResult.Yes;
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user click the 'I was wrong' button
        /// </summary>
        /// <param name="oSender">Sende object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void OnWrongClick(
            object oSender, 
            EventArgs oEventArgs
            )
        {
            DialogResult = DialogResult.No;
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks the 'Cancel' button
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void OnCancelClick(
            object oSender, 
            EventArgs oEventArgs
            )
        {
            DialogResult = DialogResult.Cancel;
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks the F1 key
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void OnHelpRequested(object oSender, HelpEventArgs oEventArgs)
        {
            try
            {
                System.Diagnostics.Process.Start(
                    System.IO.Path.Combine(Application.StartupPath, "Readme.html"));
            }
            catch (Exception oEx)
            {
                MessageBox.Show(oEx.Message);
            }

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
        private void ReadyToUseSideImageInjection(
            string strImageName
            )
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
        private void ResizeSideImageAlongWithForm(
            object oSender, 
            EventArgs oEventArgs
            )
        {
            ResizeSideImageAndShiftElements();
        }

        //===================================================================================================
        /// <summary>
        /// Loads an image and resizes it to the width of client area
        /// </summary>
        /// <param name="strImagePath"></param>
        //===================================================================================================
        private void LoadAndResizeSideImage(
            string strImagePath
            )
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
        private void ShiftOtherElementsLeftOrRight(
            int nWidthChange
            )
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
        private void ReadyToUseImageInjection(
            string strImageName
            )
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
        private void ResizeImageAlongWithForm(
            object oSender, 
            EventArgs oEventArgs
            )
        {
            ResizeImageAndShiftElements();
        }

        //===================================================================================================
        /// <summary>
        /// Loads an image and resizes it to the width of client area
        /// </summary>
        /// <param name="strImagePath"></param>
        //===================================================================================================
        private void LoadAndResizeImage(
            string strImagePath
            )
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
        private void ShiftOtherElementsUpOrDown(
            int nNewPictureHeight
            )
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
