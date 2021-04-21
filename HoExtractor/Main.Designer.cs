
namespace HoExtractor
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.groupArchives = new System.Windows.Forms.GroupBox();
            this.lstArchives = new System.Windows.Forms.ListView();
            this.groupFiles = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelFiles = new System.Windows.Forms.TableLayoutPanel();
            this.dgvFiles = new System.Windows.Forms.DataGridView();
            this.colAssetId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAssetType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDataSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFlags = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemExport = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.toolStripLoadButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.groupArchives.SuspendLayout();
            this.groupFiles.SuspendLayout();
            this.tableLayoutPanelFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupArchives
            // 
            this.groupArchives.Controls.Add(this.lstArchives);
            this.groupArchives.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupArchives.Location = new System.Drawing.Point(3, 3);
            this.groupArchives.Name = "groupArchives";
            this.groupArchives.Size = new System.Drawing.Size(254, 419);
            this.groupArchives.TabIndex = 0;
            this.groupArchives.TabStop = false;
            this.groupArchives.Text = "Archives";
            // 
            // lstArchives
            // 
            this.lstArchives.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstArchives.FullRowSelect = true;
            this.lstArchives.HideSelection = false;
            this.lstArchives.Location = new System.Drawing.Point(3, 19);
            this.lstArchives.MultiSelect = false;
            this.lstArchives.Name = "lstArchives";
            this.lstArchives.Size = new System.Drawing.Size(248, 397);
            this.lstArchives.TabIndex = 0;
            this.lstArchives.UseCompatibleStateImageBehavior = false;
            this.lstArchives.View = System.Windows.Forms.View.Details;
            this.lstArchives.SelectedIndexChanged += new System.EventHandler(this.LstArchives_SelectedIndexChanged);
            // 
            // groupFiles
            // 
            this.groupFiles.Controls.Add(this.tableLayoutPanelFiles);
            this.groupFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupFiles.Location = new System.Drawing.Point(263, 3);
            this.groupFiles.Name = "groupFiles";
            this.groupFiles.Size = new System.Drawing.Size(534, 419);
            this.groupFiles.TabIndex = 0;
            this.groupFiles.TabStop = false;
            this.groupFiles.Text = "Files";
            // 
            // tableLayoutPanelFiles
            // 
            this.tableLayoutPanelFiles.ColumnCount = 1;
            this.tableLayoutPanelFiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelFiles.Controls.Add(this.dgvFiles, 0, 1);
            this.tableLayoutPanelFiles.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanelFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelFiles.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanelFiles.Name = "tableLayoutPanelFiles";
            this.tableLayoutPanelFiles.RowCount = 2;
            this.tableLayoutPanelFiles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanelFiles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelFiles.Size = new System.Drawing.Size(528, 397);
            this.tableLayoutPanelFiles.TabIndex = 1;
            // 
            // dgvFiles
            // 
            this.dgvFiles.AllowUserToAddRows = false;
            this.dgvFiles.AllowUserToDeleteRows = false;
            this.dgvFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFiles.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAssetId,
            this.colName,
            this.colAssetType,
            this.colDataSize,
            this.colFlags});
            this.dgvFiles.ContextMenuStrip = this.contextMenuStrip;
            this.dgvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFiles.Location = new System.Drawing.Point(3, 37);
            this.dgvFiles.Name = "dgvFiles";
            this.dgvFiles.ReadOnly = true;
            this.dgvFiles.RowHeadersVisible = false;
            this.dgvFiles.RowTemplate.Height = 25;
            this.dgvFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFiles.Size = new System.Drawing.Size(522, 357);
            this.dgvFiles.TabIndex = 0;
            // 
            // colAssetId
            // 
            this.colAssetId.DataPropertyName = "AssetID";
            dataGridViewCellStyle1.Format = "X16";
            dataGridViewCellStyle1.NullValue = "0";
            this.colAssetId.DefaultCellStyle = dataGridViewCellStyle1;
            this.colAssetId.HeaderText = "ID";
            this.colAssetId.Name = "colAssetId";
            this.colAssetId.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colAssetType
            // 
            this.colAssetType.DataPropertyName = "AssetType";
            this.colAssetType.HeaderText = "Type";
            this.colAssetType.Name = "colAssetType";
            this.colAssetType.ReadOnly = true;
            // 
            // colDataSize
            // 
            this.colDataSize.DataPropertyName = "DataSize";
            this.colDataSize.HeaderText = "Size";
            this.colDataSize.Name = "colDataSize";
            this.colDataSize.ReadOnly = true;
            // 
            // colFlags
            // 
            this.colFlags.DataPropertyName = "Flags";
            dataGridViewCellStyle2.Format = "X8";
            dataGridViewCellStyle2.NullValue = null;
            this.colFlags.DefaultCellStyle = dataGridViewCellStyle2;
            this.colFlags.HeaderText = "Flags";
            this.colFlags.Name = "colFlags";
            this.colFlags.ReadOnly = true;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemExport});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(109, 26);
            // 
            // toolStripMenuItemExport
            // 
            this.toolStripMenuItemExport.Name = "toolStripMenuItemExport";
            this.toolStripMenuItemExport.Size = new System.Drawing.Size(108, 22);
            this.toolStripMenuItemExport.Text = "Export";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSelectAll);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtFilter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(522, 28);
            this.panel1.TabIndex = 1;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectAll.Location = new System.Drawing.Point(363, 2);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 3;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.BtnSelectAll_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(444, 2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filter:";
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(42, 2);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(315, 23);
            this.txtFilter.TabIndex = 0;
            this.txtFilter.TextChanged += new System.EventHandler(this.TxtFilter_TextChanged);
            // 
            // toolStripLoadButton
            // 
            this.toolStripLoadButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLoadButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLoadButton.Image")));
            this.toolStripLoadButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripLoadButton.Name = "toolStripLoadButton";
            this.toolStripLoadButton.Size = new System.Drawing.Size(37, 22);
            this.toolStripLoadButton.Text = "Load";
            this.toolStripLoadButton.Click += new System.EventHandler(this.ToolStripLoadButton_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLoadButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 260F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.groupFiles, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.groupArchives, 0, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 1;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(800, 425);
            this.tableLayoutPanelMain.TabIndex = 1;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Main";
            this.Text = "Family Guy Extractor";
            this.groupArchives.ResumeLayout(false);
            this.groupFiles.ResumeLayout(false);
            this.tableLayoutPanelFiles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupArchives;
        private System.Windows.Forms.ListView lstArchives;
        private System.Windows.Forms.GroupBox groupFiles;
        private System.Windows.Forms.DataGridView dgvFiles;
        private System.Windows.Forms.ToolStripButton toolStripLoadButton;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExport;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFiles;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAssetId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAssetType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDataSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFlags;
    }
}

