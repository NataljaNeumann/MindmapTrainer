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
    public partial class MindmapNodeView : UserControl
    {
        bool _bExpanded;

        List<MindmapNodeView> _subElements;

        IMindmapNode _node;
        public IMindmapNode Node
        {
            get
            {
                return _node;
            }
            set
            {
                _node = value;
                ClearSubElements();
                if (_node!=null)
                {
                    label1.Text = _node.Text;

                    if (_node.HasElements)
                        button1.Text = "+";
                    else
                        button1.Text = ">";
                }
                else
                {
                    button1.Text = ">";
                }
                CalcNewSize();
                
            }
        }

        private void ClearSubElements()
        {
            if (_subElements != null)
            {
                foreach (MindmapNodeView v in _subElements)
                {
                    this.Controls.Remove(v);
                    v.Dispose();
                }
                _subElements.Clear();
            }
        }

        public MindmapNodeView()
        {
            InitializeComponent();
            button1.TabIndex = 1;
        }

        private void label1_SizeChanged(object sender, EventArgs e)
        {
            CalcNewSize();
        }

        private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            _bExpanded = !_bExpanded;
            CalcNewSize();

            if (_bExpanded)
            {
                button1.Text = "-";
                Controls.Remove(textBox1);
                Controls.Add(textBox1);
                textBox1.TabIndex = 1000;
            }
            else
            {
                if (_node != null && _node.HasElements)
                    button1.Text = "+";
                else
                    button1.Text = ">";
            }
        }

        private void CalcNewSize()
        {
            Size newSize;
            if (_bExpanded)
            {
                newSize = new Size(Math.Max(label1.Location.X + label1.Size.Width + button1.Location.X,100), label1.Location.Y + label1.Size.Height + textBox1.Size.Height + button1.Location.Y * 3);
                if (_subElements == null)
                    _subElements = new List<MindmapNodeView>();
                if (_node != null)
                {
                    if (_subElements.Count == 0)
                    {

                        int currentY = Math.Max(label1.Size.Height + label1.Location.Y + button1.Location.Y, button1.Location.Y * 2 + button1.Size.Height);
                        int tabIndex = 2;
                        foreach (IMindmapNode n in _node.Elements)
                        {
                            MindmapNodeView v = new MindmapNodeView();
                            v.Location = new Point(button1.Location.X * 2 + button1.Size.Width, currentY);
                            v.Node = n;
                            _subElements.Add(v);
                            this.Controls.Add(v);
                            v.Show();
                            v.TabIndex = tabIndex++;
                        }

                        foreach (MindmapNodeView v in _subElements)
                        {
                            v.SizeChanged += new EventHandler(v_SizeChanged);
                            v.Location = new Point(button1.Location.X * 2 + button1.Size.Width, currentY);
                            currentY = v.Location.Y + v.Size.Height + button1.Location.Y;
                            newSize.Height = newSize.Height + v.Size.Height + button1.Location.Y;
                            if (newSize.Width<v.Location.X+v.Size.Width)
                                newSize.Width = v.Location.X+v.Size.Width;
                        }

                        Size = newSize;

                    } else
                    {
                        int currentY = Math.Max(label1.Size.Height + label1.Location.Y + button1.Location.Y, button1.Location.Y * 2 + button1.Size.Height);
                        foreach (MindmapNodeView v in _subElements)
                        {
                            v.Location = new Point(button1.Location.X * 2 + button1.Size.Width, currentY);
                            currentY = v.Location.Y + v.Size.Height + button1.Location.Y;
                            newSize.Height = newSize.Height + v.Size.Height + button1.Location.Y;
                            if (newSize.Width < v.Location.X + v.Size.Width)
                                newSize.Width = v.Location.X + v.Size.Width;
                        }
                        Size = newSize;
                    }
                    textBox1.Show();
                }
                else
                {
                    textBox1.Hide();

                }
                // search the elements to show inside
            }
            else
            {
                newSize = new Size(label1.Location.X + label1.Size.Width + button1.Location.X, Math.Max(label1.Size.Height + label1.Location.Y + button1.Location.Y, button1.Location.Y * 2 + button1.Size.Height));
                Size = newSize;
                ClearSubElements();
                textBox1.Hide();
            }
        }

        void v_SizeChanged(object sender, EventArgs e)
        {
            CalcNewSize();
        }

        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (e.Control || e.Shift)
                {
                    textBox1.Text = textBox1.Text + "\r\n";
                }
                else
                {
                    e.IsInputKey = false;
                    if (_node != null)
                    {
                        _node.AddElement(textBox1.Text);
                        textBox1.Text = "";
                        ClearSubElements();
                        CalcNewSize();
                        Focus();
                        textBox1.TabIndex = 1000;
                        textBox1.Focus();
                    }
                }
            }
        }


    }
}
