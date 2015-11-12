// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Form1.cs" company="">
//   
// </copyright>
// <summary>
//   The form 1.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Printer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing.Printing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    using TAF.Utility;

    /// <summary>
    /// The form 1.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// The path.
        /// </summary>
        private readonly string path = @"documents\";

        private List<ListViewItem> Items = new List<ListViewItem>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {

            InitializeComponent();
            imageList1.Images.Add(System.Drawing.Image.FromFile(@"Icons\excel.png"));
            imageList1.Images.Add(System.Drawing.Image.FromFile(@"Icons\word.png"));
            imageList1.Images.Add(System.Drawing.Image.FromFile(@"Icons\pdf.png"));
            imageList1.Images.Add(System.Drawing.Image.FromFile(@"Icons\image.png"));
            imageList1.Images.Add(System.Drawing.Image.FromFile(@"Icons\empty.png"));
            LoadData();
            listView1.Items.AddRange(Items.OrderBy(i => i.Text).ToArray());
            listView1.SelectedIndexChanged += ListView1_SelectedIndexChanged;

        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                var filePath = listView1.SelectedItems[0].Tag.ToString();
                Print(filePath);
            }

        }

        private void ListView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var filePath = e.Item.Tag.ToString();
            Print(filePath);

        }




        /// <summary>
        /// The text box 1_ text changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listView1.SelectedIndexChanged -= ListView1_SelectedIndexChanged;
            var newItems = Items.Where(i => i.Text.Contains(textBox1.Text.Trim()) || i.Text.GetChineseSpell().Contains(textBox1.Text.Trim().GetChineseSpell())).ToList();
            listView1.Items.Clear();
            listView1.Items.AddRange(newItems.OrderBy(i => i.Text).ToArray());

            listView1.SelectedIndexChanged += ListView1_SelectedIndexChanged;

        }


        /// <summary>
        /// The load data.
        /// </summary>
        public void LoadData()
        {

            Items.Clear();
            var dir = new DirectoryInfo(path);
            foreach (var d in dir.GetFiles())
            {
                var ext = Path.GetExtension(path + d.Name);
                var item = new ListViewItem { Text = d.Name, Tag = this.path + d.Name };
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".gif")
                {
                    item.ImageIndex = 3;
                }
                else if (ext == ".pdf")
                {
                    item.ImageIndex = 2;
                }
                else if (ext == ".doc" || ext == ".docx")
                {
                    item.ImageIndex = 1;
                }
                else if (ext == ".xls" || ext == ".xlsx")
                {
                    item.ImageIndex = 0;
                }
                else
                {
                    item.ImageIndex = 4;
                }

                if (ext.In(new List<string> { ".jpg", ".jpeg", ".gif", ".pdg", ".doc", ".docx", ".xls", ".xlsx", ".txt", ".pdf" }))
                {
                    Items.Add(item);
                }
            }
        }

        private void 导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "选择文件";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                listView1.SelectedIndexChanged -= ListView1_SelectedIndexChanged;
                var names = openFileDialog1.FileNames;
                names.ForEach(
                              n =>
                              {
                                  File.Copy(n, Directory.GetCurrentDirectory() + @"\" + path + Path.GetFileName(n), true);
                              });
                LoadData();
                listView1.Items.Clear();
                listView1.Items.AddRange(Items.OrderBy(i => i.Text).ToArray());
                listView1.SelectedIndexChanged += ListView1_SelectedIndexChanged;
            }
        }

        private void Print(string filePath)
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.CreateNoWindow = false;
                proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.FileName = filePath;//打印文件路径（本地完整路径包括文件名和后缀名）
                proc.StartInfo.Verb = "print";
                proc.Start();
                proc.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }
}