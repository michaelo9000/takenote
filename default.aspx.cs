using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
       protected void Page_Load(object sender, EventArgs e)
        {
           errorLabel.Text = ""; 
           if (folderList.SelectedValue == "")
                loadDropDowns(this, e);
        }

       public void loadDropDowns(object sender, EventArgs e)
       {
           string foldersPath = Server.MapPath("folders.txt");
           if (File.Exists(foldersPath))
           {
               folderList.Items.Clear();

               string newItem;
               string[] foldersList = File.ReadAllLines(foldersPath);
               Array.Sort(foldersList);
               for (int i = 0; i < foldersList.Length; i++)
               {
                   newItem = foldersList[i];
                   folderList.Items.Add(newItem);
               }
           }
           else
               errorLabel.Text = "Folders file not found";
        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
            bool overwriting = false;
            string title = titleBox.Text;
            string pageText = textBox.Text;

            string newPath = Server.MapPath("Folders/" + folderLabel.Text + "/" + title + ".txt");
            using (FileStream fs = File.Create(newPath))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(pageText);
                fs.Write(info, 0, info.Length);
            }
            
            string text = "";
            string filePath = Server.MapPath("newPagePre.txt");
            text = file(filePath);
            string preText = text;
            filePath = Server.MapPath("newPageMid.txt");
            text = file(filePath);
            string midText = text;
            filePath = Server.MapPath("newPagePost.txt");
            text = file(filePath);
            string postText = text;

            string newFile = preText + title + midText + title + ".png'/><br/>" + pageText + postText;

            newPath = Server.MapPath("Folders/" + folderLabel.Text + "/"+title+".html");
            using (FileStream fs = File.Create(newPath))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(newFile);
                fs.Write(info, 0, info.Length);
            }

            string pagesFile = Server.MapPath("Folders/" + folderLabel.Text + "/pages.txt");
            if (File.Exists(pagesFile))
            {
                string[] pagesArray = File.ReadAllLines(pagesFile);
                List<string> pagesList = new List<string>(pagesArray);
                foreach (string item in pagesList)
                {
                    if (item == title)
                    overwriting = true;
                }
                if (overwriting != true)
                {
                    pagesList.Add(title);
                    TextWriter writer = new StreamWriter(pagesFile);
                    foreach (string item in pagesList)
                    {
                        writer.WriteLine(item);
                    }
                    writer.Close();
                }
                else
                    errorLabel.Text = "overwritten"; 
            }
            string selectedFolder = folderLabel.Text;
            loadPageDowns(this, e, selectedFolder);
        }

        public string file (string filePath)
        {
            StreamReader streamReader = new StreamReader(filePath);
            string text = streamReader.ReadToEnd();
            streamReader.Close();
            return text;
        }

        protected void redirectButton_Click(object sender, EventArgs e)
        {
            string selected = redirectList.SelectedValue;
            string page = "Folders/" + folderLabel.Text + "/" + selected + ".html";
            string pagePath = Server.MapPath(page);
            if (File.Exists(pagePath))
                Response.Redirect(page);
            else
                errorLabel.Text = "Can't find page, WHOOPS";
        }

        protected void editButton_Click(object sender, EventArgs e)
        {
            string selected = redirectList.SelectedValue;
            string filePath = Server.MapPath("Folders/" + folderLabel.Text + "/" + selected + ".txt");
            string text = file(filePath);
            textBox.Text = text;
            titleBox.Text = selected;
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            string selected = redirectList.SelectedValue;
            string htmlPath = Server.MapPath("Folders/" + folderLabel.Text + "/" + selected + ".html");
            string txtPath = Server.MapPath("Folders/" + folderLabel.Text + "/" + selected + ".txt");
            File.Delete(txtPath);
            File.Delete(htmlPath);

            string pagesFile = Server.MapPath("Folders/" + folderLabel.Text + "/pages.txt");
            if (File.Exists(pagesFile))
            {
                string[] pagesArray = File.ReadAllLines(pagesFile);
                List<string> pagesList = new List<string>(pagesArray);
                TextWriter writer = new StreamWriter(pagesFile);
                    foreach (string item in pagesList)
                    {
                        if (item != selected)
                        writer.WriteLine(item);
                    }
                    writer.Close();
                    string selectedFolder = folderLabel.Text;
                    loadPageDowns(this, e, selectedFolder);
            }
        }

        protected void newFolderButton_Click(object sender, EventArgs e)
        {
            string newPath;
            bool overwriting = false;
            string folderName = newFolderBox.Text;

            if (newFolderBox.Text != "")
            {
                string pagesFile = Server.MapPath("folders.txt");
                if (File.Exists(pagesFile))
                {
                    string[] pagesArray = File.ReadAllLines(pagesFile);
                    List<string> pagesList = new List<string>(pagesArray);
                    foreach (string item in pagesList)
                    {
                        if (item == folderName)
                            overwriting = true;
                    }
                    if (overwriting != true)
                    {
                        pagesList.Add(folderName);
                        TextWriter writer = new StreamWriter(pagesFile);
                        foreach (string item in pagesList)
                        {
                            writer.WriteLine(item);
                        }
                        writer.Close();
                        //Create new folder
                        newPath = Server.MapPath("Folders/" + newFolderBox.Text);
                        Directory.CreateDirectory(newPath);
                        //Create pages.txt within new folder
                        using (FileStream fs = File.Create(newPath + "/pages.txt"))
                        { }
                        //Create _StyleSheet file in new folder
                        string filePath = Server.MapPath("_StyleSheet.css");
                        string stylesheet = (file(filePath));
                        newPath = Server.MapPath("Folders/" + newFolderBox.Text + "/_StyleSheet.css");
                        using (FileStream filestream = File.Create(newPath))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes(stylesheet);
                            filestream.Write(info, 0, info.Length);
                        }
                        //reload folders DDList
                        loadDropDowns(this, e);
                    }
                    else
                        errorLabel.Text = "Folder already exists";
                }
            }
            else
                errorLabel.Text = "Give the folder a name dumgus";
        }

        protected void folderButton_Click(object sender, EventArgs e)
        {
           string selectedFolder = folderList.SelectedValue;
           folderLabel.Text = selectedFolder;
           loadDropDowns(this, e);
           loadPageDowns(this, e, selectedFolder);
        }
        protected void loadPageDowns(object sender, EventArgs e, string selectedFolder)
        {
            string pagesPath = Server.MapPath("Folders/" + selectedFolder + "/pages.txt");
            if (File.Exists(pagesPath))
            {
                pageList.Items.Clear();
                redirectList.Items.Clear();

                string newItem;
                string[] pagesList = File.ReadAllLines(pagesPath);
                Array.Sort(pagesList);
                for (int i = 0; i < pagesList.Length; i++)
                {
                    newItem = pagesList[i];
                    redirectList.Items.Add(newItem);
                    pageList.Items.Add(newItem);
                }
            }
            else
                errorLabel.Text = "Pages file not found";
        }

        protected void deleteEnable_CheckedChanged(object sender, EventArgs e)
        {
            deleteButton.Enabled = !deleteButton.Enabled;
            deleteFolderButton.Enabled = !deleteFolderButton.Enabled;
        }

        protected void deleteFolderButton_Click(object sender, EventArgs e)
        {
            if (folderLabel.Text == "")
                errorLabel.Text = "choose a folder first";
            else
            {
                string selectedFolder = folderLabel.Text;
                string path = Server.MapPath("Folders/" + selectedFolder);
                System.IO.DirectoryInfo di = new DirectoryInfo(path);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                Directory.Delete(path);

                string foldersFile = Server.MapPath("folders.txt");
                if (File.Exists(foldersFile))
                {
                    string[] pagesArray = File.ReadAllLines(foldersFile);
                    List<string> pagesList = new List<string>(pagesArray);
                    TextWriter writer = new StreamWriter(foldersFile);
                    foreach (string item in pagesList)
                    {
                        if (item != selectedFolder)
                            writer.WriteLine(item);
                    }
                    writer.Close();
                    folderLabel.Text = "";
                    pageList.Items.Clear();
                    redirectList.Items.Clear();
                    loadDropDowns(this, e);
                }
            }
        }

        protected void imageSubmit_Click(object sender, EventArgs e)
        {
            string selectedPage = pageList.SelectedValue;
            if (imageUpload.HasFile == false)
                errorLabel.Text = "choose a picture";
            else if (selectedPage == "")
                errorLabel.Text = "open a folder";
            else
            {
                string filePath = Server.MapPath("Folders/" + folderLabel.Text + "/" + selectedPage + ".png");
                imageUpload.SaveAs(filePath);
            }
        }
    }
}
