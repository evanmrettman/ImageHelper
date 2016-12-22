using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ImageSorter
{
    public partial class MainWindow : System.Windows.Forms.Form
    {

        String input_folder = "";
        String config_path = Path.Combine(Environment.CurrentDirectory, "config.data");
        List<string> files = null;
        List<string> output_folders = new List<string> { };
        Random rand = new Random();
        
        private void flowlayout_removeAll() // Remove all buttons from flow layout.
        {
            foreach (Control item in flowLayout_outputFolders.Controls)
            {
                flowLayout_outputFolders.Controls.Remove(item);
                item.Dispose();
            }
            flowLayout_outputFolders.Controls.Clear();
        }

        private void flowlayout_add() // Add buttons to flow layout.
        {
            if (output_folders != null) {
                flowlayout_removeAll();
                for (int i = 0; i < output_folders.Count(); i++)
                {
                    Button btn = new Button();
                    btn.Tag = i;
                    //btn.Image = Icon.ExtractAssociatedIcon(output_folders[i]).ToBitmap();
                    btn.Text = output_folders[i].Substring(output_folders[i].LastIndexOf("\\")).Remove(0, 1);
                    //btn.AutoSize = true;
                    btn.Width = 48;
                    btn.Height = 48;
                    btn.TextAlign = ContentAlignment.BottomCenter;
                    btn.Click += new EventHandler(this.button_outputFolder_Click);
                    flowLayout_outputFolders.Controls.Add(btn);
                }
            }
        }

        private void save_data() // Saves data to config.data
        {
            var width = 800;
            var height = 608;
            var maximized = false;
            if (this.WindowState == FormWindowState.Maximized)
            {
                maximized = true;
            } else
            {
                if (this.Width > width)
                {
                    width = this.Width;
                }
                if (this.Height > height)
                {
                    height = this.Height;
                }
            }
            System.IO.StreamWriter f = new System.IO.StreamWriter(config_path);
            f.WriteLine(input_folder);
            f.WriteLine(width);
            f.WriteLine(height);
            f.WriteLine(maximized);
            for(int i = 0; i < output_folders.Count; i++)
            {
                f.WriteLine(output_folders[i]);
            }
            f.Close();
        }

        private void load_data() // Loads data from config.data
        {
            if (File.Exists(config_path))
            {
                System.IO.StreamReader f = new System.IO.StreamReader(config_path);
                input_folder = f.ReadLine();
                this.Width = Convert.ToInt32(f.ReadLine());
                this.Height = Convert.ToInt32(f.ReadLine());
                var maximized = Convert.ToBoolean(f.ReadLine());
                var line = f.ReadLine();
                while (line != null)
                {
                    if (Directory.Exists(line)) 
                    {
                        output_folders.Add(line);
                    }
                    line = f.ReadLine();
                }
                f.Close();

                if (output_folders.Count > 0)
                {
                    flowlayout_add();
                }

                if (maximized == true)
                {
                    this.WindowState = FormWindowState.Maximized;
                } else
                {
                    this.WindowState = FormWindowState.Normal;
                }
            }
            else
            {
                MessageBox.Show("No previous configuration data exists.\nCreating configuration file.");
                save_data();
            }
        }

        private void get_image() // Gets an image from the folder
        {
            if (imageBox.Image != null)
            {
                imageBox.Image.Dispose(); // cleanup
            }
            if (files.Count > 0)
            {
                try
                {
                    //rand.Next(files.Count())
                    //var randNum = rand.Next(files.Count());
                    imageBox.Image = Image.FromFile(files[0]); // set new image to file
                    statusStrip_label_currentImage.Text = "Current Image: " + files[0].Substring(files[0].LastIndexOf("\\")).Remove(0,1);
                    statusStrip_label_currentDimensions.Text = "Dimensions: " + imageBox.Image.Width + "x" + imageBox.Height;
                } catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            else
            {
                imageBox.Image = null;
                MessageBox.Show("There are no more images to choose from!");
            }
        }

        private void update_input() // Called to update the input_folder
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Choose the new Input Folder.\nThis is the folder that images are sorted from.";
            fbd.SelectedPath = input_folder;
            fbd.ShowNewFolderButton = false;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                input_folder = fbd.SelectedPath;
                input_setup();
                get_image();
            }
        }

        private void input_setup() // Called when input_folder changes.
        {
            statusStrip_label_inputFolder.Text = "Input Folder: "+input_folder;
            var ext = new List<string> { ".jpg", ".gif", ".png" };
            files = Directory.GetFiles(input_folder, "*", SearchOption.AllDirectories)
                 .Where(s => ext.Contains(Path.GetExtension(s)))
                 .ToList();
        }

        private void OnProcessExit(object sender, EventArgs e)
        {
            save_data();
        }

        public MainWindow() // OnLoad
        {
            InitializeComponent();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            flowlayout_add();
            load_data();
            while (input_folder == "")
            {
                update_input();
            }
            input_setup();
            get_image();
        }

        private void image_box_Click(object sender, EventArgs e)
        {
            get_image();
        }

        private void inputFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void input_display_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_inputFolder_Click(object sender, EventArgs e)
        {
            update_input();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button_outputAdd_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Choose the new Input Folder.\nThis is the folder that images are sorted from.";
            fbd.SelectedPath = input_folder;
            fbd.ShowNewFolderButton = false;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                output_folders.Add(fbd.SelectedPath);
                flowlayout_add();
            }
        }

        private void button_outputFolder_Click(object sender, EventArgs e)// Click event for dyanimcally generated buttons
        {
            if (files.Count > 0)
            {
                Button btn = sender as Button;
                String input_file = files[0];
                String output_file = output_folders[Convert.ToInt32(btn.Tag)] + "\\" + files[0].Substring(files[0].LastIndexOf("\\")).Remove(0, 1);
                files.RemoveAt(0);
                get_image();
                if (File.Exists(input_file))
                {
                    File.Move(input_file, output_file);
                }
            }
        }

        private void button_outputDelete_Click(object sender, EventArgs e)
        {
            output_folders.Clear();
            flowlayout_add();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (files.Count > 0)
            {
                var file = files[0];
                files.RemoveAt(0);
                get_image();
                if (File.Exists(file)) {
                    File.Delete(file);
                }
            }
            get_image();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
