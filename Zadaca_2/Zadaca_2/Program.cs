using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Zadaca_2
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Потребно е да направиш конзолна апликација која рекуpзивно ќе брише фајлови со одредена екстензија во одреден директориум.
            Освен тоа, потребно е во посебен текст фајл да ги запише фајловите кои ги избришала.

            Пример:
            Внесете директориум:
                (Корисникот внесува C:\Temp)
            Внесете екстензија на документите кои треба да се избришат:
                (Корисникот внесува txt)

            Апликацијата соодветно треба да ги избрише сите .txt фајлови во C:\Temp и во секој подфолдер во него доколку постои таков фајл, 
            а избришаните фајлови да ги испише во друг фајл.*/

            string directory;
            string extension;
            List<string> files = new List<string>();
            string fileName = "ErasedFiles.txt";
            Console.Write("Enter directory name: ");
            directory = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Enter extension: ");
            extension = Console.ReadLine();
            extension = "*." + extension;

            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(directory);

            files = WalkDirectoryTree(di, extension);

            string pathString = System.IO.Path.Combine(directory, fileName);

            using (System.IO.FileStream fs = System.IO.File.Create(pathString)) ;
            System.IO.File.WriteAllLines(pathString, files);

            Console.WriteLine("Press any key");
            Console.ReadKey();
        }

        static List<string> WalkDirectoryTree(System.IO.DirectoryInfo dir, string ext)
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;
            List<string> fileNames = new List<string>();

            files = dir.GetFiles(ext);

            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    Console.WriteLine(fi.FullName);
                    string fileName = fi.FullName.Substring(dir.FullName.Length + 1);
                    fileNames.Add(fileName);
                    try
                    {
                        fi.Delete();
                    }
                    catch (System.IO.IOException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                subDirs = dir.GetDirectories();

                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    fileNames.AddRange(WalkDirectoryTree(dirInfo, ext));
                }
            }
            
            return fileNames;
        }
    }
}
