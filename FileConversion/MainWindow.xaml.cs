using BusinessLayer;
using Microsoft.Win32;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


namespace FileConversion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SourceButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog sourceFileDialog = new OpenFileDialog();        

            sourcePath.Text = GetFolderPath();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            destPath.Text = GetFolderPath();
        }



        private string GetFolderPath()
        {
            string path = "Select Folder";
            System.Windows.Forms.FolderBrowserDialog pathSelector = new System.Windows.Forms.FolderBrowserDialog();
            // This is what will execute if the user selects a folder and hits OK.
            if (pathSelector.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = pathSelector.SelectedPath;
            }
            return path;
        }

        private async void startConversion_Click(object sender, RoutedEventArgs e)
        {
            string selectedType = typeSelected.Text;
            string source = sourcePath.Text;
            string destination = destPath.Text;
            string conversionType = typeSelected.Text; 
            Task<int> task = new Task<int>(() => ReadWriteData(source, destination, conversionType));
            task.Start();

            message.Content = "Plese wait : Started file conversion";

            int status = await task;
            message.Content = "File conversion Completed.";
            
        }


        private int ReadWriteData(string source, string destination, string conversionType)
        {
            IBussinessProcess textWriter;
            if (conversionType == "HTML")
            {
                textWriter = new HtmlWriter();
            }
            else
            {
                textWriter = new XmlWriter();
            }
            
            ReaderClass content = textWriter.ReadWriteTextDataFiles(source);
            textWriter.WriteToOutPutFile(content, destination);
            return 1;
        }
        
    }
}
