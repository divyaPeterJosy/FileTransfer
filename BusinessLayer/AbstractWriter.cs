using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public abstract class AbstractWriter : IBussinessProcess
    {
        
        public ReaderClass ReadWriteTextDataFiles(string inputPath)
        {
            ReaderClass readData = new ReaderClass();
            StringBuilder fileNames = new StringBuilder();
            StringBuilder codes = new StringBuilder();
            StringBuilder descriptions = new StringBuilder();
            FileInfo file;
            List<string> datas = new List<string>();

            string[] fileAry = Directory.GetFiles(inputPath, "*.txt");
            foreach (string filePath in fileAry)
            {
                file = new FileInfo(filePath);
                fileNames.Append(file.Name);
                using (TextReader tr = new StreamReader(filePath))
                {
                    string line;
                    string lineCode = "";
                    bool nextLine = true;
                    while ((line = tr.ReadLine()) != null)
                    {
                        nextLine = true;
                        if (line == "[Code]")
                        {
                            nextLine = false;
                            lineCode = "Code";
                        }
                        else if (line == "[Description]")
                        {
                            nextLine = false;
                            lineCode = "Desc";
                        }
                        else if (line == "[Data]")
                        {
                            nextLine = false;
                            lineCode = "Data";
                        }
                        if (nextLine)
                        {
                            if (lineCode == "Code")
                            {
                                codes.Append(line);
                            }
                            else if (lineCode == "Desc")
                            {
                                descriptions.Append(line);
                            }
                            else if (lineCode == "Data")
                            {
                                datas.Add(line);
                            }
                        }

                    }

                    tr.Close();
                    tr.Dispose();
                }
            }
            readData.FileNames = string.Join(",", fileNames);
            readData.Code = string.Join(",", codes);
            readData.FileDescription = string.Join(",", descriptions);
            readData.Datas = datas;
            return readData;
        }

        public abstract void WriteToOutPutFile(ReaderClass content, string outPutPath);        
    }
}
