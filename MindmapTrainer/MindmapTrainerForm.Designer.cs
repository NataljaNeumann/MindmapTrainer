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
    partial class MindmapTrainerForm
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.neueMindmapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.öffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trainierenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.intensivToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.hiddenAcceptButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.mindmapNodeView1 = new MindmapTrainer.MindmapNodeView();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.licenseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.licenseInUserLanguageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.neueMindmapToolStripMenuItem,
            this.öffnenToolStripMenuItem,
            this.trainierenToolStripMenuItem,
            this.intensivToolStripMenuItem,
            this.infoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(707, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // neueMindmapToolStripMenuItem
            // 
            this.neueMindmapToolStripMenuItem.Name = "neueMindmapToolStripMenuItem";
            this.neueMindmapToolStripMenuItem.ShortcutKeyDisplayString = "Strg+N";
            this.neueMindmapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.neueMindmapToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.neueMindmapToolStripMenuItem.Text = "&Neu";
            this.neueMindmapToolStripMenuItem.Click += new System.EventHandler(this.neueMindmapToolStripMenuItem_Click);
            // 
            // öffnenToolStripMenuItem
            // 
            this.öffnenToolStripMenuItem.Name = "öffnenToolStripMenuItem";
            this.öffnenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.öffnenToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.öffnenToolStripMenuItem.Text = "&Öffnen";
            this.öffnenToolStripMenuItem.Click += new System.EventHandler(this.öffnenToolStripMenuItem_Click);
            // 
            // trainierenToolStripMenuItem
            // 
            this.trainierenToolStripMenuItem.Name = "trainierenToolStripMenuItem";
            this.trainierenToolStripMenuItem.ShortcutKeyDisplayString = "Strg+T";
            this.trainierenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.trainierenToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.trainierenToolStripMenuItem.Text = "&Trainieren";
            this.trainierenToolStripMenuItem.Click += new System.EventHandler(this.trainierenToolStripMenuItem_Click);
            // 
            // intensivToolStripMenuItem
            // 
            this.intensivToolStripMenuItem.Name = "intensivToolStripMenuItem";
            this.intensivToolStripMenuItem.ShortcutKeyDisplayString = "Strg+I";
            this.intensivToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.intensivToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.intensivToolStripMenuItem.Text = "&Intensiv";
            this.intensivToolStripMenuItem.Click += new System.EventHandler(this.intensivToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.hiddenAcceptButton);
            this.panel1.Controls.Add(this.mindmapNodeView1);
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(707, 432);
            this.panel1.TabIndex = 2;
            // 
            // hiddenAcceptButton
            // 
            this.hiddenAcceptButton.Location = new System.Drawing.Point(663, 201);
            this.hiddenAcceptButton.Name = "hiddenAcceptButton";
            this.hiddenAcceptButton.Size = new System.Drawing.Size(32, 23);
            this.hiddenAcceptButton.TabIndex = 2;
            this.hiddenAcceptButton.Text = "...";
            this.hiddenAcceptButton.UseVisualStyleBackColor = true;
            this.hiddenAcceptButton.Click += new System.EventHandler(this.hiddenAcceptButton_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "MindMap.xml";
            this.saveFileDialog1.Filter = "Mind Map Dateien|*.MindMap.xml";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "MindMap.xml";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Mind Map Dateien|*.MindMap.xml";
            // 
            // mindmapNodeView1
            // 
            this.mindmapNodeView1.Location = new System.Drawing.Point(3, 3);
            this.mindmapNodeView1.Name = "mindmapNodeView1";
            this.mindmapNodeView1.Node = null;
            this.mindmapNodeView1.Size = new System.Drawing.Size(97, 31);
            this.mindmapNodeView1.TabIndex = 1;
            this.mindmapNodeView1.Visible = false;
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.infoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMenuItem,
            this.licenseMenuItem,
            this.licenseInUserLanguageMenuItem});
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.infoToolStripMenuItem.Text = "Info";
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.Size = new System.Drawing.Size(277, 22);
            this.aboutMenuItem.Text = "Über Mindmap-Trainer";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // licenseMenuItem
            // 
            this.licenseMenuItem.Name = "licenseMenuItem";
            this.licenseMenuItem.Size = new System.Drawing.Size(277, 22);
            this.licenseMenuItem.Text = "Lizenz (GPL 2, bindend)";
            this.licenseMenuItem.Click += new System.EventHandler(this.licenseMenuItem_Click);
            // 
            // licenseInUserLanguageMenuItem
            // 
            this.licenseInUserLanguageMenuItem.Name = "licenseInUserLanguageMenuItem";
            this.licenseInUserLanguageMenuItem.Size = new System.Drawing.Size(277, 22);
            this.licenseInUserLanguageMenuItem.Text = "Lizenztext auf Deutsch (nicht bindend)";
            this.licenseInUserLanguageMenuItem.Click += new System.EventHandler(this.licenseInUserLanguageMenuItem_Click);
            // 
            // MindmapTrainerForm
            // 
            this.AcceptButton = this.hiddenAcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 460);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MindmapTrainerForm";
            this.Text = "Mindmap-Trainer";
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MindmapTrainerForm_MouseMove);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem neueMindmapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem öffnenToolStripMenuItem;
        private MindmapNodeView mindmapNodeView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem trainierenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem intensivToolStripMenuItem;
        private System.Windows.Forms.Button hiddenAcceptButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem licenseMenuItem;
        private System.Windows.Forms.ToolStripMenuItem licenseInUserLanguageMenuItem;
    }
}

