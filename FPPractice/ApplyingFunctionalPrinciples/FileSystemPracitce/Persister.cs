using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static FPPractice.ApplyingFunctionalPrinciples.FileSystemPracitce.FileSystem;

namespace FPPractice.ApplyingFunctionalPrinciples.FileSystemPracitce
{
    public class Persister
    {
        public FileContent ReadFile(string fileName)
        {
            return new FileContent(fileName, File.ReadAllLines(fileName));
        }

        public FileContent[] ReadDirectory(string directoryName)
        {
            return Directory
                .GetFiles(directoryName)
                .Select(file => ReadFile(file))
                .ToArray();
        }

        public void ApplyChanges(IReadOnlyList<FileAction> fileActions)
        {
            foreach(var fileAction in fileActions)
            {
                switch (fileAction.Type)
                {
                    case ActionType.Create:
                    case ActionType.Update:
                        File.WriteAllLines(fileAction.FileName, fileAction.Content);
                        continue;
                    case ActionType.Delete:
                        File.Delete(fileAction.FileName);
                        continue;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        /// <summary>
        /// 這邊實現一個的
        /// </summary>
        public void ApplyChange(FileAction fileAction)
        {
            ApplyChanges(new List<FileAction>{ fileAction });
        }
    }
}
