using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageHelper
{
    public partial class formSorter : Form
    {

        // Variables

        private String input_folder = "";
        private List<String> output_folders = new List<String>();

        // Custom Functions

        private List<String> get_files()
        {
            var ext = new List<string> { ".jpg", ".gif", ".png" };
            var files = Directory.GetFiles(input_folder, "*", SearchOption.AllDirectories)
                 .Where(s => ext.Contains(Path.GetExtension(s)))
                 .ToList();
            tsslCount.Text = "Images Left: " + files.Count.ToString();
            tsslFolder.Text = "Folder: " + input_folder;
            tsslFile.Text = "File: " +files[0].Substring(files[0].LastIndexOf("\\")).Remove(0, 1);
            return files;
        }

        private void buttons_add()
        {
            for (int i = 0; i < output_folders.Count; i++)
            {
                Button btn = new Button();
                btn.Size = new Size(56, 56);//new Size(44, 44);
                btn.Text = output_folders[i].Substring(output_folders[i].LastIndexOf("\\")).Remove(0, 1);
                btn.Tag = i;
                btn.Click += (s, e) =>
                {
                    var files = get_files();
                    var source = files[0];
                    var dest = output_folders[(int) btn.Tag] + "\\" + files[0].Substring(files[0].LastIndexOf("\\")).Remove(0, 1);
                    var n = 0;
                    while (File.Exists(dest))
                    {
                        var dest_noEXT = dest.Substring(0, dest.LastIndexOf("."));
                        var dest_ext = dest.Substring(dest.LastIndexOf("."));
                        if (dest_noEXT.EndsWith(" (" + n + ")"))
                        {
                            dest = dest_noEXT+ " (" + ++n + ")" + dest_ext;
                        } else
                        {
                            dest = dest_noEXT + " (" + n + ")" + dest_ext;
                        }
                    }
                    try
                    {
                        if (files.Count > 1)
                        {
                            get_image(1);
                            File.Move(source, dest);
                        } else
                        {
                            ibFile.Image.Dispose();
                            File.Move(source, dest);
                            this.Close();
                        }
                    } catch (Exception error)
                    {
                        MessageBox.Show(error.ToString());
                    }
                };
                flpOutputFolders.Controls.Add(btn);
            }
        }

        private void get_image()
        {
            get_image(0);
        }

        private void get_image(int index)
        {
            var files = get_files();
            if (ibFile.Image != null)
            {
                ibFile.Image.Dispose(); // cleanup
            }
            if (files.Count > 0)
            {
                ibFile.Image = Image.FromFile(files[index]); // set new image to file
            } else
            {
                this.Close();
            }
        }

        // Form Load

        public formSorter()
        {
            InitializeComponent();
        }

        private void formSorter_Load(object sender, EventArgs e)
        {
            input_folder = formMain.sorter_input;
            output_folders = formMain.sorter_output;
            this.Size = new Size(800, 608);
            this.Hide();
            buttons_add();
            get_image();
        }

        private void formSorter_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        // Main Form Controls
        
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var files = get_files();
            try
            {
                if (files.Count > 1)
                {
                    get_image(1);
                    File.Delete(files[0]);
                }
                else
                {
                    ibFile.Image.Dispose();
                    File.Delete(files[0]);
                    this.Close();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        private void ibFile_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == MouseButtons.Right)
                System.Diagnostics.Process.Start("explorer.exe", "/select, \"" + get_files()[0] + "\"");
        }
    }
}
