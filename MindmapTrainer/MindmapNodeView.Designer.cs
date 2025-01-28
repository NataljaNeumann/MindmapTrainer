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


namespace MindmapTrainer
{
    partial class MindmapNodeView
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MindmapNodeView));
            this.m_btnExpand = new System.Windows.Forms.Button();
            this.m_lblText = new System.Windows.Forms.Label();
            this.m_tbxNextItemInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // m_btnExpand
            // 
            resources.ApplyResources(this.m_btnExpand, "m_btnExpand");
            this.m_btnExpand.Name = "m_btnExpand";
            this.m_btnExpand.UseVisualStyleBackColor = true;
            this.m_btnExpand.Click += new System.EventHandler(this.button1_Click);
            // 
            // m_lblText
            // 
            resources.ApplyResources(this.m_lblText, "m_lblText");
            this.m_lblText.Name = "m_lblText";
            this.m_lblText.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDoubleClick);
            this.m_lblText.SizeChanged += new System.EventHandler(this.label1_SizeChanged);
            // 
            // m_tbxNextItemInput
            // 
            resources.ApplyResources(this.m_tbxNextItemInput, "m_tbxNextItemInput");
            this.m_tbxNextItemInput.Name = "m_tbxNextItemInput";
            this.m_tbxNextItemInput.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textBox1_PreviewKeyDown);
            // 
            // MindmapNodeView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_tbxNextItemInput);
            this.Controls.Add(this.m_lblText);
            this.Controls.Add(this.m_btnExpand);
            this.Name = "MindmapNodeView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_btnExpand;
        private System.Windows.Forms.Label m_lblText;
        private System.Windows.Forms.TextBox m_tbxNextItemInput;
    }
}
