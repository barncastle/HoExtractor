using HoLib;
using HoLib.Sections;
using System;
using System.IO;
using System.Windows.Forms;

namespace HoExtractor
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void ToolStripLoadButton_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            dgvFiles.DataSource = null;

            lstArchives.Clear();
            lstArchives.View = View.Details;
            lstArchives.FullRowSelect = true;
            lstArchives.SuspendLayout();

            if (lstArchives.Columns.Count == 0)
            {
                var vw = SystemInformation.VerticalScrollBarWidth;
                lstArchives.Columns.Add("Archive Files", lstArchives.Width - vw - 5);
            }

            var paths = Directory.GetFiles(dialog.SelectedPath, "*.ho", SearchOption.AllDirectories);

            foreach (var path in paths)
            {
                var archive = new Archive(path);
                lstArchives.Items.Add(new ListViewItem(archive.Section.Name)
                {
                    Tag = archive
                });
            }

            lstArchives.ResumeLayout();
        }

        private void LstArchives_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstArchives.SelectedItems.Count == 0)
                return;

            var selectedItem = lstArchives.SelectedItems[0];
            var archive = (Archive)selectedItem.Tag;

            dgvFiles.DataSource = new BindingSource()
            {
                DataSource = new AssetEntryView(archive.Assets)
                {
                    Filter = txtFilter.Text
                }
            };
        }

        private void TxtFilter_TextChanged(object sender, EventArgs e)
        {
            if (dgvFiles.DataSource is BindingSource source)
            {
                dgvFiles.SuspendLayout();
                source.Filter = txtFilter.Text;
                dgvFiles.ResumeLayout();
            }
        }

        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            dgvFiles.SelectAll();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (dgvFiles.SelectedRows.Count == 0)
                return;
            if (lstArchives.SelectedItems.Count == 0)
                return;

            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            var selectedItem = lstArchives.SelectedItems[0];
            var archive = (Archive)selectedItem.Tag;
            using var stream = File.OpenRead(archive.ArchivePath);

            foreach (DataGridViewRow row in dgvFiles.SelectedRows)
            {
                var entry = row.DataBoundItem as AssetEntry;
                archive.Extract(entry, dialog.SelectedPath, stream);
            }
        }
    }
}
