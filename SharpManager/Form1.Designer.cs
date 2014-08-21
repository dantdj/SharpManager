namespace SharpManager
{
    partial class Form1
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
            this.B_Back = new System.Windows.Forms.Button();
            this.B_UpALevel = new System.Windows.Forms.Button();
            this.TB_CurrentDirectory = new System.Windows.Forms.TextBox();
            this.FileGridView = new System.Windows.Forms.DataGridView();
            this.ColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDateCreated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDateModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.FileGridView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // B_Back
            // 
            this.B_Back.Location = new System.Drawing.Point(13, 420);
            this.B_Back.Name = "B_Back";
            this.B_Back.Size = new System.Drawing.Size(75, 23);
            this.B_Back.TabIndex = 0;
            this.B_Back.Text = "Back";
            this.B_Back.UseVisualStyleBackColor = true;
            this.B_Back.Click += new System.EventHandler(this.B_Back_Click);
            // 
            // B_UpALevel
            // 
            this.B_UpALevel.Location = new System.Drawing.Point(95, 420);
            this.B_UpALevel.Name = "B_UpALevel";
            this.B_UpALevel.Size = new System.Drawing.Size(75, 23);
            this.B_UpALevel.TabIndex = 1;
            this.B_UpALevel.Text = "Up A Level";
            this.B_UpALevel.UseVisualStyleBackColor = true;
            this.B_UpALevel.Click += new System.EventHandler(this.B_UpALevel_Click);
            // 
            // TB_CurrentDirectory
            // 
            this.TB_CurrentDirectory.Location = new System.Drawing.Point(12, 12);
            this.TB_CurrentDirectory.Name = "TB_CurrentDirectory";
            this.TB_CurrentDirectory.Size = new System.Drawing.Size(451, 20);
            this.TB_CurrentDirectory.TabIndex = 3;
            this.TB_CurrentDirectory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_CurrentDirectory_KeyDown);
            // 
            // FileGridView
            // 
            this.FileGridView.AllowUserToAddRows = false;
            this.FileGridView.AllowUserToDeleteRows = false;
            this.FileGridView.AllowUserToResizeRows = false;
            this.FileGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.FileGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FileGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColName,
            this.ColType,
            this.ColSize,
            this.ColDateCreated,
            this.ColDateModified});
            this.FileGridView.ContextMenuStrip = this.contextMenuStrip1;
            this.FileGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.FileGridView.Location = new System.Drawing.Point(13, 38);
            this.FileGridView.MultiSelect = false;
            this.FileGridView.Name = "FileGridView";
            this.FileGridView.RowHeadersVisible = false;
            this.FileGridView.ShowEditingIcon = false;
            this.FileGridView.Size = new System.Drawing.Size(503, 376);
            this.FileGridView.TabIndex = 4;
            this.FileGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FileGridView_CellContentDoubleClick);
            this.FileGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.FileGridView_CellMouseDown);
            this.FileGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FileGridView_KeyDown);
            // 
            // ColName
            // 
            this.ColName.HeaderText = "Name";
            this.ColName.Name = "ColName";
            // 
            // ColType
            // 
            this.ColType.HeaderText = "Type";
            this.ColType.Name = "ColType";
            // 
            // ColSize
            // 
            this.ColSize.HeaderText = "Size";
            this.ColSize.Name = "ColSize";
            // 
            // ColDateCreated
            // 
            this.ColDateCreated.HeaderText = "Date Created";
            this.ColDateCreated.Name = "ColDateCreated";
            // 
            // ColDateModified
            // 
            this.ColDateModified.HeaderText = "Date Modified";
            this.ColDateModified.Name = "ColDateModified";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.moveToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(105, 92);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // moveToolStripMenuItem
            // 
            this.moveToolStripMenuItem.Name = "moveToolStripMenuItem";
            this.moveToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.moveToolStripMenuItem.Text = "Move";
            this.moveToolStripMenuItem.Click += new System.EventHandler(this.moveToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 468);
            this.Controls.Add(this.FileGridView);
            this.Controls.Add(this.TB_CurrentDirectory);
            this.Controls.Add(this.B_UpALevel);
            this.Controls.Add(this.B_Back);
            this.Name = "Form1";
            this.Text = "SharpManager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.FileGridView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button B_Back;
        private System.Windows.Forms.Button B_UpALevel;
        private System.Windows.Forms.TextBox TB_CurrentDirectory;
        private System.Windows.Forms.DataGridView FileGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDateCreated;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDateModified;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveToolStripMenuItem;
    }
}

