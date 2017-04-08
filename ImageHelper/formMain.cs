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
    public partial class formMain : Form
    {

        // TODO Star rating system for images.
        // TODO Tag implimentation for sorting.
        // TODO Slideshow
        // TODO Form data saving (position / maximize)


        // Constants
        private const String SAVE_DIRECTORY = "DATA\\";
        private const String NAME_SORT = "config_sort.txt";
        private const String NAME_CROP = "config_crop.txt";
        private const String NAME_DELETE = "config_delete.txt";
        private const String NAME_DUPE = "config_dupe.txt";
        private const String PROGRAM_NAME = "ImageHelper";
        private const String VERSION_NUMBER = "0.5";
        private const String CREATOR = "Evan Rettman";
        private const String LICENSE = "MIT License\n\nCopyright (c) 2016 Evan Rettman\n\nPermission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the \"Software\"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:\n\nThe above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.\n\n THE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER, LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.";

        // Variables
        private Panel[] panel_array = new Panel[4];
        
        // Public Variables for Form Passing
        public static String sorter_input = "";
        public static List<String> sorter_output = new List<String>();

        private List<String> get_files(String folder) // Gets all image files within the specified folder
        {
            var ext = new List<string> { ".jpg", ".gif", ".png" };
            return Directory.GetFiles(folder, "*", SearchOption.AllDirectories)
                 .Where(s => ext.Contains(Path.GetExtension(s)))
                 .ToList();
        }

        private void panel_visible(int panel_enable) // Makes the panel visible and all others invisible
        {
            for(int i = 0; i < panel_array.Length; i++)
            {
                if (i == panel_enable)
                {
                    panel_array[i].Visible = true;
                } else 
                {
                    panel_array[i].Visible = false;
                }
            }
    }

        // Flow Layout Panel Stuff

        private void outputflow_resize(FlowLayoutPanel flp) // Button resize for when vertical scroll bar shows up
        {
            if (flp.VerticalScroll.Visible == false)
            {
                foreach (Control tb in flp.Controls)
                {
                    if (tb is TextBox)
                        tb.Size = new Size(222, 20);
                }
            }else
            {
                foreach (Control tb in flp.Controls)
                {
                    if (tb is TextBox)
                        tb.Size = new Size(205, 20);
                }
            }
        }

        private void sort_outputflow_add(String file) // Adds a button/textbox to layout with the specified file path
        {
            TextBox dir = new TextBox();
            Button btn = new Button();
            dir.ReadOnly = true;
            dir.Size = new Size(205, 20);
            btn.Size = new Size(22, 22);
            if (file == null)
            {
                dir.Text = "Click the button to add an output folder.";
                btn.Text = "_";
            } else
            {
                dir.Text = file;
                btn.Text = "☒";
            }
            btn.Click += (s, e) => // handle output folder button dynamic creation click event
            {
                if (btn.Text == "☒")
                {
                    flpSortOutputFolders.Controls.Remove(dir);
                    flpSortOutputFolders.Controls.Remove(btn);
                    dir.Dispose();
                    btn.Dispose();
                    outputflow_resize(flpSortOutputFolders);
                    save_sort();
                }
                else
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    fbd.Description = "Choose an output folder.\nThis is a folder the images are sorted into.";
                    if (Directory.Exists(txtSortInputFolder.Text))
                        fbd.SelectedPath = txtSortInputFolder.Text;
                    fbd.ShowNewFolderButton = false;

                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        var unique = true;
                        foreach(Control c in flpSortOutputFolders.Controls)
                        {
                            if(c is TextBox)
                            {
                                if (c.Text == fbd.SelectedPath)
                                {
                                    unique = false;
                                }
                            }
                        }
                        if (unique)
                        {
                            tsslError.Text = "No Error";
                            dir.Text = fbd.SelectedPath;
                            btn.Text = "☒";
                            save_sort();
                            sort_outputflow_add(null);
                        } else
                        {
                            tsslError.Text = "Non-unique output folder.";
                        }
                    }
                }
            };
            flpSortOutputFolders.Controls.Add(btn);
            flpSortOutputFolders.Controls.Add(dir);
            flpSortOutputFolders.VerticalScroll.Value = flpSortOutputFolders.VerticalScroll.Maximum;
            outputflow_resize(flpSortOutputFolders);
        }

        private void dupe_outputflow_add(String file) // Adds a button/textbox to layout with the specified file path
        {
            TextBox dir = new TextBox();
            Button btn = new Button();
            dir.ReadOnly = true;
            dir.Size = new Size(205, 20);
            btn.Size = new Size(22, 22);
            if (file == null)
            {
                dir.Text = "Click the button to add a folder to the check list.";
                btn.Text = "_";
            }
            else
            {
                dir.Text = file;
                btn.Text = "☒";
            }
            btn.Click += (s, e) => // handle output folder button dynamic creation click event
            {
                if (btn.Text == "☒")
                {
                    flpDupeFolderCheck.Controls.Remove(dir);
                    flpDupeFolderCheck.Controls.Remove(btn);
                    dir.Dispose();
                    btn.Dispose();
                    outputflow_resize(flpDupeFolderCheck);
                    save_sort();
                }
                else
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    fbd.Description = "Choose an output folder.\nThis is a folder the images are sorted into.";
                    if (Directory.Exists(txtSortInputFolder.Text))
                        fbd.SelectedPath = txtSortInputFolder.Text;
                    fbd.ShowNewFolderButton = false;

                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        var unique = true;
                        var given_path = fbd.SelectedPath;
                        foreach (Control c in flpSortOutputFolders.Controls)
                        {
                            if (c is TextBox)
                            {
                                if (c.Text == given_path)
                                {
                                    unique = false;
                                }
                            }
                        }
                        
                        if (unique)
                        {
                            tsslError.Text = "No Error";
                            dir.Text = given_path;
                            btn.Text = "☒";
                            save_sort();
                            dupe_outputflow_add(null);
                        }
                        else
                        {
                            tsslError.Text = "Non-unique output folder given.";
                        }
                    }
                }
            };
            flpDupeFolderCheck.Controls.Add(btn);
            flpDupeFolderCheck.Controls.Add(dir);
            flpDupeFolderCheck.VerticalScroll.Value = flpDupeFolderCheck.VerticalScroll.Maximum;
            outputflow_resize(flpDupeFolderCheck);
        }

        // Save & Load

        private void save_sort() // Saves sort data
        {
            System.IO.StreamWriter f = new System.IO.StreamWriter(SAVE_DIRECTORY + NAME_SORT);
            if (Directory.Exists(txtSortInputFolder.Text))
            {
                f.WriteLine(txtSortInputFolder.Text);
            }
            else
            {
                f.WriteLine("");
            }
            foreach (Control txt in flpSortOutputFolders.Controls)
            {
                if (txt is TextBox)
                    if (Directory.Exists(txt.Text))
                        f.WriteLine(txt.Text);
            }
            f.Close();
        }

        private void load_sort() // Loads sort data
        {
            if (!File.Exists(SAVE_DIRECTORY + NAME_SORT))
            {
                save_sort();
            }
            else
            {
                System.IO.StreamReader f = new System.IO.StreamReader(SAVE_DIRECTORY + NAME_SORT);
                var line = f.ReadLine();
                if (Directory.Exists(line))
                    txtSortInputFolder.Text = line;
                line = f.ReadLine();
                while (line != null)
                {
                    if (Directory.Exists(line))
                        sort_outputflow_add(line);
                    line = f.ReadLine();
                }
                sort_outputflow_add(null);
                f.Close();
            }
        }

        private void save_crop() // Saves crop data
        {
            System.IO.StreamWriter f = new System.IO.StreamWriter(SAVE_DIRECTORY + NAME_CROP);
            f.Close();
        }

        private void load_crop() // Loads crop data
        {
            if (!File.Exists(SAVE_DIRECTORY + NAME_CROP))
            {
                save_crop();
            }
            else
            {
                System.IO.StreamReader f = new System.IO.StreamReader(SAVE_DIRECTORY + NAME_CROP);
                f.Close();
            }
        }

        private void save_delete() // Saves delete data
        {
            System.IO.StreamWriter f = new System.IO.StreamWriter(SAVE_DIRECTORY + NAME_DELETE);
            f.Close();
        }

        private void load_delete() // Loads delete data
        {
            if (!File.Exists(SAVE_DIRECTORY + NAME_DELETE))
            {
                save_delete();
            }
            else
            {
                System.IO.StreamReader f = new System.IO.StreamReader(SAVE_DIRECTORY + NAME_DELETE);
                f.Close();
            }
        }

        private void save_dupe() // Saves dupe data
        {
            System.IO.StreamWriter f = new System.IO.StreamWriter(SAVE_DIRECTORY + NAME_DUPE);
            f.Close();
        }

        private void load_dupe() // Loads dupe data
        {
            if (!File.Exists(SAVE_DIRECTORY + NAME_DUPE))
            {
                save_dupe();
            }
            else
            {
                System.IO.StreamReader f = new System.IO.StreamReader(SAVE_DIRECTORY + NAME_DUPE);
                f.Close();
            }
        } 

        private void save_data() // Save all mode data
        {
            if (!Directory.Exists(SAVE_DIRECTORY))
                Directory.CreateDirectory(SAVE_DIRECTORY);

            // sort save
            save_sort();
            // crop saved
            save_crop();
            // delete save
            save_delete();
            // dupe save
            save_dupe();
        }

        private void load_data() // Loads the game data on start
        {
            load_sort();
            load_crop();
            load_delete();
            load_dupe();
        }

        // Confirm button code

        private void confirm_sort() // Sorting Mode Confirm
        {
            var input = Directory.Exists(txtSortInputFolder.Text);
            var output = true;
            List<String> output_files = new List<String>();
            foreach (Control txt in flpSortOutputFolders.Controls)
            {
                if (txt is TextBox)
                {
                    if (txt.Text != "Click the button to add an output folder.")
                        output_files.Add(txt.Text);
                    if (txt.Text != "Click the button to add an output folder." && !Directory.Exists(txt.Text))
                    {
                        output = false;
                        if (!txt.Text.StartsWith("INVALID PATH: "))
                        {
                            txt.Text = "INVALID PATH: " + txt.Text;
                        }
                    }
                }
            }
            if (input && output && output_files.Count() > 0 && get_files(txtSortInputFolder.Text).Count != 0)
            {
                tsslError.Text = "No Error";
                sorter_input = txtSortInputFolder.Text;
                sorter_output = output_files;
                this.Hide();
                formSorter formSorter = new formSorter();
                formSorter.Text = this.Text + ": Sorter";
                formSorter.ShowDialog();
                this.Show();
                this.Focus();
            }
            else if (input && get_files(txtSortInputFolder.Text).Count == 0)
            {
                tsslError.Text = "There are no files in the input folder.";
            }
            else
            {
                tsslError.Text = "Error occured. Check input and output folders.";
            }
        }

        private void confirm_crop() // Cropping Mode Confirm
        {

        }

        private void confirm_delete() // Deleter Mode Confirm
        {

        }

        private void confirm_dupe() // Dupe Deleter Mode Confirm
        {

        }

        // Form Load

        public formMain()
        {
            InitializeComponent();
        }

        private void formMain_Load(object sender, EventArgs e) // On Load
        {
            this.Name = PROGRAM_NAME + " v" + VERSION_NUMBER;
            this.Text = this.Name;
            this.Height = 300;
            this.Width = 300;
            panel_array = new Panel[4] { panel_imageSort, panel_imageCrop, panel_imageDelete, panel_imageDupe };
            load_data();
            Point p = new Point(12, 67);
            panel_imageSort.Location = p;
            panel_imageCrop.Location = p;
            panel_imageDelete.Location = p;
            panel_imageDupe.Location = p;
            cbModeSelection.Items.Add("Image Sorter");
            cbModeSelection.Items.Add("Image Cropper (WIP)");
            cbModeSelection.Items.Add("Image Deleter");
            cbModeSelection.Items.Add("Image Dupe Deleter");
            cbModeSelection.SelectedIndex = cbModeSelection.Items.IndexOf("Image Sorter");
            //panel_enable(0);
        }

        private void OnProcessExit(object sender, EventArgs e) // Save data when program is closed
        {
            save_data();
        }

        // Menu Strip

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) // Displays about us message bo.
        {
            MessageBox.Show("Program: " + PROGRAM_NAME + " v" + VERSION_NUMBER + "\nDeveloper(s): " + CREATOR + "\n\n" + LICENSE + "\n\nThank you for using my program. :)");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) // Exit Program
        {
            Application.Exit();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) // Save all data sets
        {
            save_data();
        }

        // Main Form Controls

        private void cbModeSelection_SelectedIndexChanged(object sender, EventArgs e) // Change currently displayed mode
        {
            panel_visible(cbModeSelection.SelectedIndex);
        }

        private void btnConfirmConfig_Click(object sender, EventArgs e) // Confirm check for starting mode
        {
            switch(cbModeSelection.SelectedIndex)
            {
                case 0:
                    confirm_sort();
                    break;
                case 1:
                    confirm_crop();
                    break;
                case 2:
                    confirm_delete();
                    break;
                case 3:
                    confirm_dupe();
                    break;
                default:
                    MessageBox.Show("873usfdj49t8 Confirm Config Button Press: This should not appear.");
                    break;
            }
            
        }

        // Sub Panel Controls

        private void btnChangeInput_Click(object sender, EventArgs e) // Change input folder on txtInputFolder
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Choose the input folder.\nThis is the folder images are sorted from.";
            if (Directory.Exists(txtSortInputFolder.Text))
                fbd.SelectedPath = txtSortInputFolder.Text;
            fbd.ShowNewFolderButton = false;

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtSortInputFolder.Text = fbd.SelectedPath;
                save_sort();
            }

        }

    }
}
