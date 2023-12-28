namespace CyConex.Forms
{
    partial class HTMLEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HTMLEditorForm));
            this.htmlEditControl = new Zoople.HTMLEditControl();
            this.SuspendLayout();
            // 
            // htmlEditControl
            // 
            this.htmlEditControl.AcceptsReturn = true;
            this.htmlEditControl.BaseURL = null;
            this.htmlEditControl.CleanMSWordHTMLOnPaste = true;
            this.htmlEditControl.CodeEditor.Enabled = true;
            this.htmlEditControl.CodeEditor.Font = new System.Drawing.Font("Courier New", 10F);
            this.htmlEditControl.CodeEditor.Locked = false;
            this.htmlEditControl.CodeEditor.TabWidth = 720;
            this.htmlEditControl.CodeEditor.WordWrap = false;
            this.htmlEditControl.CSSText = null;
            this.htmlEditControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlEditControl.DocumentHTML = null;
            this.htmlEditControl.EnableInlineSpelling = false;
            this.htmlEditControl.FontSizesList = "";
            this.htmlEditControl.FontsList = null;
            this.htmlEditControl.HiddenButtons = null;
            this.htmlEditControl.HideDOMToolbar = true;
            this.htmlEditControl.ImageStorageLocation = null;
            this.htmlEditControl.InCodeView = false;
            this.htmlEditControl.IndentAmount = 2;
            this.htmlEditControl.IndentsUseBlockuote = false;
            this.htmlEditControl.LanguageFile = null;
            this.htmlEditControl.LicenceActivationKey = null;
            this.htmlEditControl.LicenceKey = null;
            this.htmlEditControl.LicenceKeyInlineSpelling = null;
            this.htmlEditControl.Location = new System.Drawing.Point(2, 2);
            this.htmlEditControl.Name = "htmlEditControl";
            this.htmlEditControl.Size = new System.Drawing.Size(894, 509);
            this.htmlEditControl.TabIndex = 0;
            this.htmlEditControl.ToolstripImageScalingSize = new System.Drawing.Size(16, 16);
            this.htmlEditControl.UseParagraphAsDefault = false;
            // 
            // HTMLEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 513);
            this.Controls.Add(this.htmlEditControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HTMLEditorForm";
            this.Style.MdiChild.IconHorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.Style.MdiChild.IconVerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            this.Text = "HTML Editor Form";
            this.ResumeLayout(false);

        }

        #endregion

        private Zoople.HTMLEditControl htmlEditControl;
    }
}