using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FPPractice.ApplyingFunctionalPrinciples.FileSystemPracitce
{
    public class FileSystem
    {
        private readonly int _maxCount;

        public FileSystem(int maxCount)
        {
            _maxCount = maxCount;
        }

        public FileAction AddRecord(FileContent sourceFile, string title, string userName, DateTime editTime)
        {
            var fileInfos = Parse(sourceFile.Content);

            if (fileInfos.Count < _maxCount)
            {
                fileInfos.Add(new FileInfo(fileInfos.Count +1, title, userName, editTime));
                var newContent = Serialize(fileInfos);

                return new FileAction(sourceFile.FileName, newContent, ActionType.Update);
            }
            else
            {
                var fileInfo = new FileInfo(1, title, userName, editTime);
                string[] newContent = Serialize(new List<FileInfo> { fileInfo });
                string newFileName = GetNewFileName(sourceFile.FileName);

                return new FileAction(newFileName, newContent, ActionType.Create);
            }
        }

        public IReadOnlyList<FileAction> RemoveRecordByUserName(string userName, FileContent[] fileContents)
        {
            return fileContents
                .Select(file => RemoveRecordUseUserName(file, userName))
                .Where(action => action != null)
                .Select(action => action.Value)
                .ToList();
        }

        private FileAction? RemoveRecordUseUserName(FileContent fileContent, string userName)
        {
            var fileInfos = Parse(fileContent.Content);

            var newContents = fileInfos
                .Where(x => x.UserName != userName)
                .Select((result, index) => new FileInfo(index + 1, result.Title, result.UserName, result.EditTime))
                .ToList();

            // 這邊代表
            if (fileInfos.Count == newContents.Count)
                return null;

            if (newContents.Count == 0)
                return new FileAction(fileContent.FileName, new string[0], ActionType.Delete);

            return new FileAction(fileContent.FileName, Serialize(newContents), ActionType.Update);
        }

        private List<FileInfo> Parse(string[] contents)
        {
            var fileInfos = new List<FileInfo>();

            foreach(var content in contents)
            {
                string[] data = content.Split(';');
                fileInfos.Add(new FileInfo(int.Parse(data[0]), data[1], data[2], DateTime.Parse(data[3])));
            }

            return fileInfos;
        }

        private string[] Serialize(List<FileInfo> fileInfos)
        {
            return fileInfos.Select(x => x.Id+ ";" + x.Title + ";" + x.UserName + ";" + x.EditTime.ToString("yyyy-MM-dd HH:mm:ss")).ToArray();
        }

        private string GetNewFileName(string existingFileName)
        {
            string fileName = Path.GetFileNameWithoutExtension(existingFileName);
            int index = int.Parse(fileName.Split('_')[1]);
            return $"FileInfo_{index + 1}.txt";
        }
    }

    public struct FileContent
    {
        public FileContent(string fileName, string[] content)
        {
            FileName = fileName;
            Content = content;
        }

        public string FileName { get; }
        public string[] Content { get; }
    }

    public struct FileInfo
    {
        public FileInfo(int id, string title, string userName, DateTime editTime)
        {
            Id = id;
            Title = title;
            UserName = userName;
            EditTime = editTime;
        }

        public int Id { get; }
        public string Title { get; }
        public string UserName { get; }
        public DateTime EditTime { get; }
    }

    public struct FileAction
    {
        public FileAction(string fileName, string[] content, ActionType type)
        {
            FileName = fileName;
            Content = content;
            Type = type;
        }

        public string FileName { get; }
        public string[] Content { get; }
        public ActionType Type { get; }
    }

    public enum ActionType
    {
        Create,
        Update,
        Delete
    }
}
