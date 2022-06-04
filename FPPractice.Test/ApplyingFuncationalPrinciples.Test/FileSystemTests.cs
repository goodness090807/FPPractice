using FPPractice.ApplyingFunctionalPrinciples.FileSystemPracitce;
using System;
using Xunit;

namespace FPPractice.Test.ApplyingFuncationalPrinciples.Test
{
    public class FileSystemTests
    {
        [Fact]
        public void AddRecord_add_a_record_to_exsit_file_if_not_overflowed()
        {
            var fileSystem = new FileSystem(3);
            var dateTime = DateTime.Now;

            var file = new FileContent("FileInfo_1.txt", new string[]
            {
                "1;測試標題;蔡家誠;2022-05-22 16:10:00"
            });

            var fileAction = fileSystem.AddRecord(file, "測試標題2", "蔡家誠", dateTime);

            Assert.Equal(ActionType.Update, fileAction.Type);
            Assert.Equal("FileInfo_1.txt", fileAction.FileName);
            Assert.Equal(new[] 
            {
                "1;測試標題;蔡家誠;2022-05-22 16:10:00",
                $"2;測試標題2;蔡家誠;{dateTime.ToString("yyyy-MM-dd HH:mm:ss")}"
                
            }, fileAction.Content);
        }

        [Fact]
        public void AddRecord_add_a_record_to_exsit_file_if_overflowed()
        {
            var fileSystem = new FileSystem(2);
            var dateTime = DateTime.Now;

            var file = new FileContent("FileInfo_1.txt", new string[]
            {
                "1;測試標題;蔡家誠;2022-05-22 16:10:00",
                "2;測試標題2;蔡家誠;2022-05-22 21:10:00"
            });

            var fileAction = fileSystem.AddRecord(file, "測試標題3", "蔡家誠", dateTime);

            Assert.Equal(ActionType.Create, fileAction.Type);
            Assert.Equal("FileInfo_2.txt", fileAction.FileName);
            Assert.Equal(new[]
            {
                $"1;測試標題3;蔡家誠;{dateTime.ToString("yyyy-MM-dd HH:mm:ss")}"

            }, fileAction.Content);
        }

        [Fact]
        public void RemoveRecord_removes_record_from_userName_with_file()
        {
            var fileSystem = new FileSystem(5);

            var file = new FileContent("FileInfo_1.txt", new string[]
            {
                "1;測試標題;蔡家誠;2022-05-22 16:10:00",
                "2;測試標題2;蔡家誠;2022-05-22 21:10:00",
                "3;測試標題4;蔡家家;2022-05-22 21:10:00"
            });

            var actions = fileSystem.RemoveRecordByUserName("蔡家誠", new[] { file });

            Assert.Equal(1, actions.Count);
            Assert.Equal("FileInfo_1.txt", actions[0].FileName);
            Assert.Equal(ActionType.Update, actions[0].Type);
            Assert.Equal(new[]
            {
                "1;測試標題4;蔡家家;2022-05-22 21:10:00"
            }, actions[0].Content);
        }

        [Fact]
        public void RemoveRecord_removes_whole_file_if_it_doesnt_contain_anything()
        {

            var fileSystem = new FileSystem(5);

            var file = new FileContent("FileInfo_1.txt", new string[]
            {
                "1;測試標題;蔡家誠;2022-05-22 16:10:00"
            });

            var actions = fileSystem.RemoveRecordByUserName("蔡家誠", new[] { file });

            Assert.Equal(1, actions.Count);
            Assert.Equal("FileInfo_1.txt", actions[0].FileName);
            Assert.Equal(ActionType.Delete, actions[0].Type);
        }

        [Fact]
        public void RemoveRecord_does_not_do_anything_if_not_userName_Equal()
        {
            var fileSystem = new FileSystem(5);

            var file = new FileContent("FileInfo_1.txt", new string[]
            {
                "1;測試標題;蔡家誠;2022-05-22 16:10:00"
            });

            var actions = fileSystem.RemoveRecordByUserName("蔡家7誠", new[] { file });

            Assert.Equal(0, actions.Count);
        }
    }
}
