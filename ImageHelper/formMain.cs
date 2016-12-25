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

        // Variables

        private const String SAVE_DIRECTORY = "DATA\\";
        private const String SAVE_SORTER = "config_sort.txt";
        private const String SAVE_CROPPER = "config_crop.txt";
        private const String SAVE_DELETE = "config_delete.txt";
        private const String SAVE_DUPE = "config_dupe.txt";
        private const String PROGRAM_NAME = "ImageHelper";
        private const String VERSION_NUMBER = "0.2";
        private const String CREATOR = "Evan Rettman";
        private const String LICENSE = "MIT License\n\nCopyright (c) 2016 Evan Rettman\n\nPermission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the \"Software\"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:\n\nThe above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.\n\n THE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER, LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.";

        // value passing for other forms
        public static String sorter_input = "";
        public static List<String> sorter_output = new List<String>();

        // Custom Functions

        private List<String> get_files(String folder)
        {
            var ext = new List<string> { ".jpg", ".gif", ".png" };
            return Directory.GetFiles(folder, "*", SearchOption.AllDirectories)
                 .Where(s => ext.Contains(Path.GetExtension(s)))
                 .ToList();
        }

        private void panel_enable(int panel)
        {
            switch (panel)
            {
                case 0:
                    panel_imageSorter.Visible = true;
                    panel_imageCropper.Visible = false;
                    panel_imageDeleter.Visible = false;
                    panel_imageDupeDeleter.Visible = false;
                    break;
                case 1:
                    panel_imageSorter.Visible = false;
                    panel_imageCropper.Visible = true;
                    panel_imageDeleter.Visible = false;
                    panel_imageDupeDeleter.Visible = false;
                    break;
                case 2:
                    panel_imageSorter.Visible = false;
                    panel_imageCropper.Visible = false;
                    panel_imageDeleter.Visible = true;
                    panel_imageDupeDeleter.Visible = false;
                    break;
                case 3:
                    panel_imageSorter.Visible = false;
                    panel_imageCropper.Visible = false;
                    panel_imageDeleter.Visible = false;
                    panel_imageDupeDeleter.Visible = true;
                    break;
                default:
                    panel_imageSorter.Visible = true;
                    panel_imageCropper.Visible = false;
                    panel_imageDeleter.Visible = false;
                    panel_imageDupeDeleter.Visible = false;
                    break;
            }
    }

        private void outputflow_resize()
        {
            if (flpOutputFolders.VerticalScroll.Visible == false)
            {
                foreach (Control tb in flpOutputFolders.Controls)
                {
                    if (tb is TextBox)
                        tb.Size = new Size(222, 20);
                }
            }
            else
            {
                foreach (Control tb in flpOutputFolders.Controls)
                {
                    if (tb is TextBox)
                        tb.Size = new Size(205, 20);
                }
            }
        }
        
        private void outputflow_add(String file)
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
                    flpOutputFolders.Controls.Remove(dir);
                    flpOutputFolders.Controls.Remove(btn);
                    dir.Dispose();
                    btn.Dispose();
                    outputflow_resize();
                    save_data(0);
                }
                else
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    fbd.Description = "Choose an output folder.\nThis is a folder the images are sorted into.";
                    if (Directory.Exists(txtInputFolder.Text))
                        fbd.SelectedPath = txtInputFolder.Text;
                    fbd.ShowNewFolderButton = false;

                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        var unique = true;
                        foreach(Control c in flpOutputFolders.Controls)
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
                            save_data(0);
                            outputflow_add();
                        } else
                        {
                            tsslError.Text = "Non-unique output folder.";
                        }
                    }
                }
            };
            flpOutputFolders.Controls.Add(btn);
            flpOutputFolders.Controls.Add(dir);
            flpOutputFolders.VerticalScroll.Value = flpOutputFolders.VerticalScroll.Maximum;
            outputflow_resize();
        }

        private void outputflow_add()
        {
            outputflow_add(null);
        }

        private void save_data(int data_set)
        {
            if (!Directory.Exists(SAVE_DIRECTORY))
                Directory.CreateDirectory(SAVE_DIRECTORY);
            switch (data_set)
            {
                case 0:
                    System.IO.StreamWriter f0 = new System.IO.StreamWriter(SAVE_DIRECTORY + SAVE_SORTER);
                    if (Directory.Exists(txtInputFolder.Text))
                    {
                        f0.WriteLine(txtInputFolder.Text);
                    } else
                    {
                        f0.WriteLine("");
                    }
                    foreach(Control txt in flpOutputFolders.Controls)
                    {
                        if (txt is TextBox)
                            if (Directory.Exists(txt.Text))
                                f0.WriteLine(txt.Text);
                    }
                    f0.Close();
                    break;
                case 1:
                    System.IO.StreamWriter f1 = new System.IO.StreamWriter(SAVE_DIRECTORY + SAVE_CROPPER);
                    f1.Close();
                    break;
                case 2:
                    System.IO.StreamWriter f2 = new System.IO.StreamWriter(SAVE_DIRECTORY + SAVE_DELETE);
                    f2.Close();
                    break;
                case 3:
                    System.IO.StreamWriter f3 = new System.IO.StreamWriter(SAVE_DIRECTORY + SAVE_DUPE);
                    f3.Close();
                    break;
                default:
                    // idk
                    break;
            }
        }

        private void load_data()
        {
            if (!File.Exists(SAVE_DIRECTORY+SAVE_SORTER))
            {
                save_data(0);
            } else
            {
                System.IO.StreamReader f0 = new System.IO.StreamReader(SAVE_DIRECTORY + SAVE_SORTER);
                var line = f0.ReadLine();
                if (Directory.Exists(line))
                    txtInputFolder.Text = line;
                line = f0.ReadLine();
                while (line != null)
                {
                    if (Directory.Exists(line))
                        outputflow_add(line);
                    line = f0.ReadLine();
                }
                outputflow_add();
                f0.Close();
            }
            if (!File.Exists(SAVE_DIRECTORY+SAVE_CROPPER))
            {
                save_data(1);
            } else
            {
                System.IO.StreamReader f1 = new System.IO.StreamReader(SAVE_DIRECTORY + SAVE_CROPPER);
                f1.Close();
            }
            if (!File.Exists(SAVE_DIRECTORY + SAVE_DELETE))
            {
                save_data(2);
            } else
            {
                System.IO.StreamReader f2 = new System.IO.StreamReader(SAVE_DIRECTORY + SAVE_DELETE);
                f2.Close();
            }
            if (!File.Exists(SAVE_DIRECTORY+SAVE_DUPE))
            {
                save_data(3);
            } else
            {
                System.IO.StreamReader f3 = new System.IO.StreamReader(SAVE_DIRECTORY + SAVE_DUPE);
                f3.Close();
            }
        }

        // Form Load

        public formMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Name = PROGRAM_NAME + " v" + VERSION_NUMBER;
            this.Text = this.Name;
            this.Height = 300;
            this.Width = 300;
            cbModeSelection.Items.Add("Image Sorter");
            //cbModeSelection.Items.Add("Image Cropper");
            //cbModeSelection.Items.Add("Image Deleter");
            //cbModeSelection.Items.Add("Image Dupe Deleter");
            cbModeSelection.SelectedIndex = cbModeSelection.Items.IndexOf("Image Sorter");
            Point p = new Point(12, 67);
            panel_imageSorter.Location = p;
            panel_imageCropper.Location = p;
            panel_imageDeleter.Location = p;
            panel_imageDupeDeleter.Location = p;
            panel_enable(0);
            load_data();

        }

        private void OnProcessExit(object sender, EventArgs e)
        {
            save_data(0);
            save_data(1);
            save_data(2);
            save_data(3);
        }

        // Menu Strip

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) // Displays about us message box.
        {
            MessageBox.Show("Program: " + PROGRAM_NAME + " v" + VERSION_NUMBER + "\nDeveloper(s): " + CREATOR + "\n\n" + LICENSE + "\n\nThank you for using my program. :)");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save_data(cbModeSelection.SelectedIndex);
        }

        // Main Form Controls

        private void cbModeSelection_SelectedIndexChanged(object sender, EventArgs e) // Change currently displayed mode
        {
            switch(cbModeSelection.SelectedIndex)
            {
                case 0:
                    panel_enable(0);
                    break;
                case 1:
                    panel_enable(1);
                    break;
                case 2:
                    panel_enable(2);
                    break;
                case 3:
                    panel_enable(3);
                    break;
                default:
                    panel_enable(1);
                    break;
            }
        }

        private void btnConfirmConfig_Click(object sender, EventArgs e)
        {
            switch(cbModeSelection.SelectedIndex)
            {
                case 0:
                    var input = Directory.Exists(txtInputFolder.Text);
                    var output = true;
                    List<String> output_files = new List<String>();
                    foreach (Control txt in flpOutputFolders.Controls)
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
                    if (input && output && output_files.Count() > 0 && get_files(txtInputFolder.Text).Count != 0)
                    {
                        tsslError.Text = "No Error";
                        sorter_input = txtInputFolder.Text;
                        sorter_output = output_files;
                        this.Hide();
                        formSorter formSorter = new formSorter();
                        formSorter.Text = this.Text + ": Sorter";
                        formSorter.ShowDialog();
                        this.Show();
                        this.Focus();
                    } else if(get_files(txtInputFolder.Text).Count == 0)
                    {
                        tsslError.Text = "There are no files in the input folder.";
                    } else
                    {
                        tsslError.Text = "Error occured. Check input and output folders.";
                    }
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    //????
                    break;
            }
            
        }

        // Sub Panel Controls

        private void btnChangeInput_Click(object sender, EventArgs e) // Change input folder on txtInputFolder
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Choose the input folder.\nThis is the folder images are sorted from.";
            if (Directory.Exists(txtInputFolder.Text))
                fbd.SelectedPath = txtInputFolder.Text;
            fbd.ShowNewFolderButton = false;

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtInputFolder.Text = fbd.SelectedPath;
                save_data(0);
            }

        }

    }
}
