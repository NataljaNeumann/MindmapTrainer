// Vokabel-Trainer v1.0
// Copyright (C) 2019 NataljaNeumann@gmx.de
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MindmapTrainer
{
    //*******************************************************************************************************
    /// <summary>
    /// This class presents a node of the mindmap
    /// </summary>
    //*******************************************************************************************************
    public partial class MindmapNodeView : UserControl
    {
        //===================================================================================================
        /// <summary>
        /// Indicates that the view is in expanded mode
        /// </summary>
        bool m_bExpanded;

        //===================================================================================================
        /// <summary>
        /// The sub-elements of this node
        /// </summary>
        List<MindmapNodeView> m_aSubElements;

        //===================================================================================================
        /// <summary>
        /// The process is in clearing sub-elements, so resizing is ignored
        /// </summary>
        bool m_bInClearSubElements;

        //===================================================================================================
        /// <summary>
        /// The corresponding mindmap node
        /// </summary>
        IMindmapNode m_iNode;

        //===================================================================================================
        /// <summary>
        /// Gets or sets the mindmap node
        /// </summary>
        public IMindmapNode Node
        {
            get
            {
                return m_iNode;
            }
            set
            {
                m_iNode = value;
                ClearSubElements();
                if (m_iNode!=null)
                {
                    m_lblText.Text = m_iNode.Text;

                    if (m_iNode.HasElements)
                        m_btnExpand.Text = "+";
                    else
                        m_btnExpand.Text = ">";
                }
                else
                {
                    m_btnExpand.Text = ">";
                }
                CalcNewSize();
                
            }
        }

        //===================================================================================================
        /// <summary>
        /// Clears sub-views
        /// </summary>
        //===================================================================================================
        private void ClearSubElements()
        {
            if (m_aSubElements != null)
            {
                try
                {
                    m_bInClearSubElements = true;

                    while (m_aSubElements.Count > 0)
                    {
                        MindmapNodeView v = m_aSubElements[m_aSubElements.Count - 1];
                        m_aSubElements.RemoveAt(m_aSubElements.Count - 1);

                        try
                        {
                            this.Controls.Remove(v);
                            v.Dispose();
                        }
                        catch
                        {
                            // ignore
                        }

                    }
                }
                finally
                {
                    m_bInClearSubElements = false;
                }
            }
        }

        //===================================================================================================
        /// <summary>
        /// Constructs a new view
        /// </summary>
        //===================================================================================================
        public MindmapNodeView()
        {
            InitializeComponent();
            m_btnExpand.TabIndex = 1;
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when label changes its size
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        //===================================================================================================
        private void label1_SizeChanged(object sender, EventArgs e)
        {
            CalcNewSize();
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user double clicks on the label 
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        //===================================================================================================
        private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks the button inside the control
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        //===================================================================================================
        private void button1_Click(object sender, EventArgs e)
        {
            m_bExpanded = !m_bExpanded;
            CalcNewSize();

            if (m_bExpanded)
            {
                m_btnExpand.Text = "-";
                Controls.Remove(m_tbxNextItemInput);
                Controls.Add(m_tbxNextItemInput);
                m_tbxNextItemInput.TabIndex = 1000;
            }
            else
            {
                if (m_iNode != null && m_iNode.HasElements)
                    m_btnExpand.Text = "+";
                else
                    m_btnExpand.Text = ">";
            }
        }

        //===================================================================================================
        /// <summary>
        /// Calculates new size of control, including all contents
        /// </summary>
        //===================================================================================================
        private void CalcNewSize()
        {
            Size newSize;
            if (m_bExpanded)
            {
                newSize = new Size(
                    Math.Max(m_lblText.Location.X + m_lblText.Size.Width + m_btnExpand.Location.X,100), 
                    m_btnExpand.Location.Y + m_btnExpand.Size.Height + m_tbxNextItemInput.Size.Height + 
                    m_btnExpand.Location.Y * 3);

                if (m_aSubElements == null)
                    m_aSubElements = new List<MindmapNodeView>();
                if (m_iNode != null)
                {
                    if (m_aSubElements.Count == 0)
                    {

                        int currentY = Math.Max(m_lblText.Size.Height + m_lblText.Location.Y + m_btnExpand.Location.Y, 
                            m_btnExpand.Location.Y * 2 + m_btnExpand.Size.Height);
                        int tabIndex = 2;
                        foreach (IMindmapNode n in m_iNode.Elements)
                        {
                            MindmapNodeView v = new MindmapNodeView();
                            v.Location = new Point(m_btnExpand.Location.X * 2 + m_btnExpand.Size.Width, currentY);
                            v.Node = n;
                            m_aSubElements.Add(v);
                            this.Controls.Add(v);
                            v.Show();
                            v.TabIndex = tabIndex++;
                        }

                        foreach (MindmapNodeView v in m_aSubElements)
                        {
                            v.SizeChanged += new EventHandler(v_SizeChanged);
                            v.Location = new Point(m_btnExpand.Location.X * 2 + m_btnExpand.Size.Width, currentY);
                            currentY = v.Location.Y + v.Size.Height + m_btnExpand.Location.Y;
                            newSize.Height = newSize.Height + v.Size.Height + m_btnExpand.Location.Y;
                            if (newSize.Width<v.Location.X+v.Size.Width)
                                newSize.Width = v.Location.X+v.Size.Width;
                        }

                        Size = newSize;

                    } else
                    {
                        int currentY = Math.Max(m_lblText.Size.Height + m_lblText.Location.Y + m_btnExpand.Location.Y, 
                            m_btnExpand.Location.Y * 2 + m_btnExpand.Size.Height);
                        foreach (MindmapNodeView v in m_aSubElements)
                        {
                            v.Location = new Point(m_btnExpand.Location.X * 2 + m_btnExpand.Size.Width, currentY);
                            currentY = v.Location.Y + v.Size.Height + m_btnExpand.Location.Y;
                            newSize.Height = newSize.Height + v.Size.Height + m_btnExpand.Location.Y;
                            if (newSize.Width < v.Location.X + v.Size.Width)
                                newSize.Width = v.Location.X + v.Size.Width;
                        }
                        Size = newSize;
                    }
                    m_tbxNextItemInput.Show();
                }
                else
                {
                    m_tbxNextItemInput.Hide();

                }
                // search the elements to show inside
            }
            else
            {
                newSize = new Size(m_lblText.Location.X + m_lblText.Size.Width + m_btnExpand.Location.X, 
                    Math.Max(m_lblText.Size.Height + m_lblText.Location.Y + m_btnExpand.Location.Y, 
                    m_btnExpand.Location.Y * 2 + m_btnExpand.Size.Height));
                Size = newSize;
                ClearSubElements();
                m_tbxNextItemInput.Hide();
            }
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when size changes
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        //===================================================================================================
        void v_SizeChanged(object sender, EventArgs e)
        {
            if (!m_bInClearSubElements)
            {
                CalcNewSize();
            }
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when user presses a key down, while in text box
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        //===================================================================================================
        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (e.Control || e.Shift)
                {
                    m_tbxNextItemInput.Text = m_tbxNextItemInput.Text + "\r\n";
                }
                else
                {
                    e.IsInputKey = false;
                    if (m_iNode != null)
                    {
                        m_iNode.AddElement(m_tbxNextItemInput.Text);
                        m_tbxNextItemInput.Text = "";
                        ClearSubElements();
                        CalcNewSize();
                        Focus();
                        m_tbxNextItemInput.TabIndex = 1000;
                        m_tbxNextItemInput.Focus();
                    }
                }
            }
        }


    }
}
