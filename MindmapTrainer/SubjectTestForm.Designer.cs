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


namespace MindmapTrainer
{
    partial class SubjectTestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubjectTestForm));
            this.m_lblSubject = new System.Windows.Forms.Label();
            this.m_lblElements = new System.Windows.Forms.Label();
            this.m_btnShow = new System.Windows.Forms.Button();
            this.m_btnCorrectResult = new System.Windows.Forms.Button();
            this.m_btnWrongResult = new System.Windows.Forms.Button();
            this.m_btnCanel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_lblSubject
            // 
            this.m_lblSubject.AccessibleDescription = null;
            this.m_lblSubject.AccessibleName = null;
            resources.ApplyResources(this.m_lblSubject, "m_lblSubject");
            this.m_lblSubject.Name = "m_lblSubject";
            // 
            // m_lblElements
            // 
            this.m_lblElements.AccessibleDescription = null;
            this.m_lblElements.AccessibleName = null;
            resources.ApplyResources(this.m_lblElements, "m_lblElements");
            this.m_lblElements.Name = "m_lblElements";
            this.m_lblElements.SizeChanged += new System.EventHandler(this.label2_SizeChanged);
            // 
            // m_btnShow
            // 
            this.m_btnShow.AccessibleDescription = null;
            this.m_btnShow.AccessibleName = null;
            resources.ApplyResources(this.m_btnShow, "m_btnShow");
            this.m_btnShow.BackgroundImage = null;
            this.m_btnShow.Name = "m_btnShow";
            this.m_btnShow.UseVisualStyleBackColor = true;
            this.m_btnShow.Click += new System.EventHandler(this.buttonShow_Click);
            // 
            // m_btnCorrectResult
            // 
            this.m_btnCorrectResult.AccessibleDescription = null;
            this.m_btnCorrectResult.AccessibleName = null;
            resources.ApplyResources(this.m_btnCorrectResult, "m_btnCorrectResult");
            this.m_btnCorrectResult.BackgroundImage = null;
            this.m_btnCorrectResult.Name = "m_btnCorrectResult";
            this.m_btnCorrectResult.UseVisualStyleBackColor = true;
            this.m_btnCorrectResult.Click += new System.EventHandler(this.buttonCorrect_Click);
            // 
            // m_btnWrongResult
            // 
            this.m_btnWrongResult.AccessibleDescription = null;
            this.m_btnWrongResult.AccessibleName = null;
            resources.ApplyResources(this.m_btnWrongResult, "m_btnWrongResult");
            this.m_btnWrongResult.BackgroundImage = null;
            this.m_btnWrongResult.Name = "m_btnWrongResult";
            this.m_btnWrongResult.UseVisualStyleBackColor = true;
            this.m_btnWrongResult.Click += new System.EventHandler(this.buttonWrong_Click);
            // 
            // m_btnCanel
            // 
            this.m_btnCanel.AccessibleDescription = null;
            this.m_btnCanel.AccessibleName = null;
            resources.ApplyResources(this.m_btnCanel, "m_btnCanel");
            this.m_btnCanel.BackgroundImage = null;
            this.m_btnCanel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCanel.Name = "m_btnCanel";
            this.m_btnCanel.UseVisualStyleBackColor = true;
            this.m_btnCanel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // SubjectTestForm
            // 
            this.AcceptButton = this.m_btnShow;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.CancelButton = this.m_btnCanel;
            this.Controls.Add(this.m_btnCanel);
            this.Controls.Add(this.m_btnWrongResult);
            this.Controls.Add(this.m_btnCorrectResult);
            this.Controls.Add(this.m_btnShow);
            this.Controls.Add(this.m_lblElements);
            this.Controls.Add(this.m_lblSubject);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SubjectTestForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_btnShow;
        private System.Windows.Forms.Button m_btnCorrectResult;
        private System.Windows.Forms.Button m_btnWrongResult;
        private System.Windows.Forms.Button m_btnCanel;
        public System.Windows.Forms.Label m_lblSubject;
        public System.Windows.Forms.Label m_lblElements;
    }
}