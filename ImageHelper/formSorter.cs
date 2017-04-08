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
        private List<String> files = new List<String>();

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

        private void dispose_image()
        {
            if (ibFile.Image != null)
            {
                ibFile.Image.Dispose();
            }
        }

        private void buttons_add()
        {
            for (int i = 0; i < output_folders.Count; i++)
            {
                Button btn = new Button();
                btn.Size = new Size(56, 56);//new Size(44, 44);
                btn.Text = output_folders[i].Substring(output_folders[i].LastIndexOf("\\")).Remove(0, 1); // display text based on folder name
                btn.Tag = i; // button tag
                btn.Click += (s, e) => // button click event
                {
                    var source = files[0]; //grab shown image
                    var dest = output_folders[(int) btn.Tag] + "\\" + files[0].Substring(files[0].LastIndexOf("\\")).Remove(0, 1); // final destination for picture
                    var n = 0; // number counter for duplicate file name
                    while (File.Exists(dest)) // rename file if file exists in destination directory
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
                    try // try to move file
                    {
                        if (files.Count > 1) // if more images exist
                        {
                            get_image(1); // get next image
                            files.RemoveAt(0); // remove current image from list
                            File.Move(source, dest); // move image to destination
                        } else 
                        {
                            dispose_image(); // clear image on window
                            File.Move(source, dest); // move image to destination
                            this.Close(); // close the sorting window
                        }
                    } catch (Exception error) // error occured
                    {
                        MessageBox.Show(error.ToString()); // print error
                    }
                };
                flpOutputFolders.Controls.Add(btn); // add button to 
            }
        }

        private void get_image()
        {
            get_image(0);
        }

        private void get_image(int index)
        {
            dispose_image(); // image cleanup
            if (files.Count > 0) // if images are left
            {
                ibFile.Image = Image.FromFile(files[index]); // set new image to file
            } else
            {
                this.Close(); // close window because no images are left (failsafe)
            }
        }

        // Form Load

        public formSorter()
        {
            InitializeComponent();
        }

        private void formSorter_Load(object sender, EventArgs e)
        {
            input_folder = formMain.sorter_input; // get input folders
            output_folders = formMain.sorter_output; // get output folders
            this.Size = new Size(800, 608); // set size (default)
            this.Hide(); // hide main form
            files = get_files(); // get files from input folder
            buttons_add(); // add all buttons to canvas
            get_image(); // get first image
        }

        private void formSorter_FormClosing(object sender, FormClosingEventArgs e)
        {
            dispose_image(); // cleanup image
        }

        // Main Form Controls
        
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (files.Count > 1)
                {
                    get_image(1); // get next image in list
                    File.Delete(files[0]); // delete file
                    files.RemoveAt(0); // remove current image from list
                }
                else
                {
                    dispose_image(); // image cleanup
                    File.Delete(files[0]); // delete file
                    this.Close(); // close form
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString()); // error occured when deleting image
            }
        }

        private void ibFile_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e; // get mouse button event
            if (me.Button == MouseButtons.Right) // if right click
                System.Diagnostics.Process.Start("explorer.exe", "/select, \"" + get_files()[0] + "\""); // open file in explorer
        }

    }
}
