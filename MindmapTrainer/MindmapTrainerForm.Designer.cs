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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MindmapTrainerForm));
            this.m_oMenuStrip = new System.Windows.Forms.MenuStrip();
            this.m_ctlNewMindmapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ctlOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ctlTrainingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ctlIntensiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ctlAboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ctlAboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ctlLicenseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ctlToggleGUI = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ctlPanel = new System.Windows.Forms.Panel();
            this.m_tbxEditNodeText = new System.Windows.Forms.TextBox();
            this.m_ctlTreeView = new System.Windows.Forms.TreeView();
            this.m_btnHiddenAcceptButton = new System.Windows.Forms.Button();
            this.m_ctlMindmapNodeView = new MindmapTrainer.MindmapNodeView();
            this.m_dlgSaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.m_dlgOpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.m_oMenuStrip.SuspendLayout();
            this.m_ctlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_oMenuStrip
            // 
            this.m_oMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ctlNewMindmapToolStripMenuItem,
            this.m_ctlOpenToolStripMenuItem,
            this.m_ctlTrainingToolStripMenuItem,
            this.m_ctlIntensiveToolStripMenuItem,
            this.m_ctlAboutToolStripMenuItem,
            this.m_ctlToggleGUI});
            resources.ApplyResources(this.m_oMenuStrip, "m_oMenuStrip");
            this.m_oMenuStrip.Name = "m_oMenuStrip";
            // 
            // m_ctlNewMindmapToolStripMenuItem
            // 
            this.m_ctlNewMindmapToolStripMenuItem.Name = "m_ctlNewMindmapToolStripMenuItem";
            resources.ApplyResources(this.m_ctlNewMindmapToolStripMenuItem, "m_ctlNewMindmapToolStripMenuItem");
            this.m_ctlNewMindmapToolStripMenuItem.Click += new System.EventHandler(this.newMindmapToolStripMenuItem_Click);
            // 
            // m_ctlOpenToolStripMenuItem
            // 
            this.m_ctlOpenToolStripMenuItem.Name = "m_ctlOpenToolStripMenuItem";
            resources.ApplyResources(this.m_ctlOpenToolStripMenuItem, "m_ctlOpenToolStripMenuItem");
            this.m_ctlOpenToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // m_ctlTrainingToolStripMenuItem
            // 
            this.m_ctlTrainingToolStripMenuItem.Name = "m_ctlTrainingToolStripMenuItem";
            resources.ApplyResources(this.m_ctlTrainingToolStripMenuItem, "m_ctlTrainingToolStripMenuItem");
            this.m_ctlTrainingToolStripMenuItem.Click += new System.EventHandler(this.trainingToolStripMenuItem_Click);
            // 
            // m_ctlIntensiveToolStripMenuItem
            // 
            this.m_ctlIntensiveToolStripMenuItem.Name = "m_ctlIntensiveToolStripMenuItem";
            resources.ApplyResources(this.m_ctlIntensiveToolStripMenuItem, "m_ctlIntensiveToolStripMenuItem");
            this.m_ctlIntensiveToolStripMenuItem.Click += new System.EventHandler(this.intensivelyToolStripMenuItem_Click);
            // 
            // m_ctlAboutToolStripMenuItem
            // 
            this.m_ctlAboutToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.m_ctlAboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ctlAboutMenuItem,
            this.m_ctlLicenseMenuItem});
            this.m_ctlAboutToolStripMenuItem.Name = "m_ctlAboutToolStripMenuItem";
            resources.ApplyResources(this.m_ctlAboutToolStripMenuItem, "m_ctlAboutToolStripMenuItem");
            // 
            // m_ctlAboutMenuItem
            // 
            this.m_ctlAboutMenuItem.Name = "m_ctlAboutMenuItem";
            resources.ApplyResources(this.m_ctlAboutMenuItem, "m_ctlAboutMenuItem");
            this.m_ctlAboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // m_ctlLicenseMenuItem
            // 
            this.m_ctlLicenseMenuItem.Name = "m_ctlLicenseMenuItem";
            resources.ApplyResources(this.m_ctlLicenseMenuItem, "m_ctlLicenseMenuItem");
            this.m_ctlLicenseMenuItem.Click += new System.EventHandler(this.licenseMenuItem_Click);
            // 
            // m_ctlToggleGUI
            // 
            resources.ApplyResources(this.m_ctlToggleGUI, "m_ctlToggleGUI");
            this.m_ctlToggleGUI.Name = "m_ctlToggleGUI";
            this.m_ctlToggleGUI.Click += new System.EventHandler(this.OnToggleGUIClick);
            // 
            // m_ctlPanel
            // 
            resources.ApplyResources(this.m_ctlPanel, "m_ctlPanel");
            this.m_ctlPanel.Controls.Add(this.m_tbxEditNodeText);
            this.m_ctlPanel.Controls.Add(this.m_ctlTreeView);
            this.m_ctlPanel.Controls.Add(this.m_btnHiddenAcceptButton);
            this.m_ctlPanel.Controls.Add(this.m_ctlMindmapNodeView);
            this.m_ctlPanel.Name = "m_ctlPanel";
            // 
            // m_tbxEditNodeText
            // 
            this.m_tbxEditNodeText.AcceptsReturn = true;
            resources.ApplyResources(this.m_tbxEditNodeText, "m_tbxEditNodeText");
            this.m_tbxEditNodeText.Name = "m_tbxEditNodeText";
            this.m_tbxEditNodeText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_tbxEditNodeText_KeyDown);
            this.m_tbxEditNodeText.Leave += new System.EventHandler(this.OnNodeEditTextBoxFocusLeft);
            // 
            // m_ctlTreeView
            // 
            resources.ApplyResources(this.m_ctlTreeView, "m_ctlTreeView");
            this.m_ctlTreeView.Name = "m_ctlTreeView";
            this.m_ctlTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.m_ctlTreeView_BeforeExpand);
            this.m_ctlTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_ctlTreeView_AfterSelect);
            this.m_ctlTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.m_ctlTreeView_NodeMouseClick);
            this.m_ctlTreeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_ctlTreeView_KeyDown);
            // 
            // m_btnHiddenAcceptButton
            // 
            this.m_btnHiddenAcceptButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.m_btnHiddenAcceptButton, "m_btnHiddenAcceptButton");
            this.m_btnHiddenAcceptButton.Name = "m_btnHiddenAcceptButton";
            this.m_btnHiddenAcceptButton.UseVisualStyleBackColor = true;
            this.m_btnHiddenAcceptButton.Click += new System.EventHandler(this.hiddenAcceptButton_Click);
            // 
            // m_ctlMindmapNodeView
            // 
            resources.ApplyResources(this.m_ctlMindmapNodeView, "m_ctlMindmapNodeView");
            this.m_ctlMindmapNodeView.Name = "m_ctlMindmapNodeView";
            this.m_ctlMindmapNodeView.Node = null;
            // 
            // m_dlgSaveFileDialog1
            // 
            this.m_dlgSaveFileDialog1.DefaultExt = "MindMap.xml";
            resources.ApplyResources(this.m_dlgSaveFileDialog1, "m_dlgSaveFileDialog1");
            // 
            // m_dlgOpenFileDialog1
            // 
            this.m_dlgOpenFileDialog1.DefaultExt = "MindMap.xml";
            this.m_dlgOpenFileDialog1.FileName = "openFileDialog1";
            resources.ApplyResources(this.m_dlgOpenFileDialog1, "m_dlgOpenFileDialog1");
            // 
            // MindmapTrainerForm
            // 
            this.AcceptButton = this.m_btnHiddenAcceptButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.m_ctlPanel);
            this.Controls.Add(this.m_oMenuStrip);
            this.MainMenuStrip = this.m_oMenuStrip;
            this.Name = "MindmapTrainerForm";
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MindmapTrainerForm_MouseMove);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.OnHelpRequested);
            this.m_oMenuStrip.ResumeLayout(false);
            this.m_oMenuStrip.PerformLayout();
            this.m_ctlPanel.ResumeLayout(false);
            this.m_ctlPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip m_oMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem m_ctlNewMindmapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_ctlOpenToolStripMenuItem;
        private MindmapNodeView m_ctlMindmapNodeView;
        private System.Windows.Forms.Panel m_ctlPanel;
        private System.Windows.Forms.ToolStripMenuItem m_ctlTrainingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_ctlIntensiveToolStripMenuItem;
        private System.Windows.Forms.Button m_btnHiddenAcceptButton;
        private System.Windows.Forms.SaveFileDialog m_dlgSaveFileDialog1;
        private System.Windows.Forms.OpenFileDialog m_dlgOpenFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem m_ctlAboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_ctlAboutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_ctlLicenseMenuItem;
        private System.Windows.Forms.TreeView m_ctlTreeView;
        private System.Windows.Forms.TextBox m_tbxEditNodeText;
        private System.Windows.Forms.ToolStripMenuItem m_ctlToggleGUI;
    }
}

