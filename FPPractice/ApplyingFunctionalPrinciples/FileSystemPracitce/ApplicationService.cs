using System;
using System.IO;
using System.Linq;
using static FPPractice.ApplyingFunctionalPrinciples.FileSystemPracitce.FileSystem;

namespace FPPractice.ApplyingFunctionalPrinciples.FileSystemPracitce
{
    /// <summary>
    /// 這一部分是來做結合的
    /// </summary>
    public class ApplicationService
    {
        private readonly string _directoryName;
        private readonly FileSystem _fileSystem;
        private readonly Persister _persister;

        public ApplicationService(string directoryName)
        {
            _directoryName = directoryName;
        }

        public void RemoveRecordByUserName(string userName)
        {
            FileContent[] fileContents = _persister.ReadDirectory(_directoryName);
            var actions = _fileSystem.RemoveRecordByUserName(userName, fileContents);
            _persister.ApplyChanges(actions);
        }

        public void AddRecord(string title, string userName, DateTime editTime)
        {
            var fileInfo = new DirectoryInfo(_directoryName)
                .GetFiles()
                .OrderByDescending(x => x.LastWriteTime)
                .First();

            var file = _persister.ReadFile(fileInfo.Name);
            var action = _fileSystem.AddRecord(file, title, userName, editTime);

            _persister.ApplyChange(action);
        }
    }
}
