using System;
using System.IO;

using NUnit.Framework;

using SerializationApp.Components;
using SerializationApp.Serializers;

namespace SerializationAppTests
{
    [TestFixture]
    public class SerializerTests
    {
        private DirectoryComponent expected;

        private string filePath;


        [Test]
        [TestCase("binary", "file.txt")]
        [TestCase("xml", "file.txt")]      
        public void SerializeDeserializeTests(string type, string filePath)
        {
            this.filePath = filePath; 

            CreateRealDirectory(expected, string.Empty);
            Serializer serializer = SerializerStaticFactory.CreateSerializer(type, filePath, expected.Name);
            serializer.Serialize();
            var actual = serializer.Deserialize();

            Assert.AreEqual(expected, actual);
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            var currentPath = Directory.GetCurrentDirectory();

            var root = new DirectoryComponent() { Name = $"{currentPath}\\TestDirectory", Depth = 0, IsLeaf = false, IsRoot = true };
            root.Components.Add(new FileComponent("txt1", 0, 0, DateTime.Now, FileAttributes.Archive, false));
            root.Components.Add(new FileComponent("bin1", 0, 0, DateTime.Now, FileAttributes.Archive, false));
            root.Components.Add(new FileComponent("xml1", 0, 0, DateTime.Now, FileAttributes.Archive, false));

            var nested1 = new DirectoryComponent() { Name = "Nested1", Depth = 1, IsLeaf = false };
            nested1.Components.Add(new FileComponent("txt2", 0, 1, DateTime.Now, FileAttributes.Archive, false));
            nested1.Components.Add(new FileComponent("bin2", 0, 1, DateTime.Now, FileAttributes.Archive, false));
            nested1.Components.Add(new FileComponent("xml2", 0, 1, DateTime.Now, FileAttributes.Archive, false));

            var nested11 = new DirectoryComponent() { Name = "Nested11", Depth = 2, IsLeaf = true };
            nested11.Components.Add(new FileComponent("nestedFile1", 0, 2, DateTime.Now, FileAttributes.Archive, true));

            nested1.Components.Add(nested11);
            root.Components.Add(nested1);

            var nested2 = new DirectoryComponent() { Name = "Nested2", Depth = 1, IsLeaf = true };
            nested2.Components.Add(new FileComponent("txt2", 0, 1, DateTime.Now, FileAttributes.Archive, false));
            nested2.Components.Add(new FileComponent("bin2", 0, 1, DateTime.Now, FileAttributes.Archive, false));
            nested2.Components.Add(new FileComponent("xml2", 0, 1, DateTime.Now, FileAttributes.Archive, false));

            var nested22 = new DirectoryComponent() { Name = "Nested22", Depth = 2, IsLeaf = true };
            nested22.Components.Add(new FileComponent("nestedFile1", 0, 2, DateTime.Now, FileAttributes.Archive, true));

            nested2.Components.Add(nested22);
            root.Add(nested2);

            expected = root;
        }

        [TearDown]
        public void TearDown()
        {
            //delete created file and directory to avoid trash holding   
            if (File.Exists(filePath))
                File.Delete(filePath);

            if(Directory.Exists(expected.Name))
                Directory.Delete(expected.Name, true);
        }

        private void CreateRealDirectory(DirectoryComponent directory, string parentPath)
        {
            var path = String.IsNullOrEmpty(parentPath)
                ? directory.Name
                : parentPath + @"\" + directory.Name;

            var root = Directory.CreateDirectory(path);
            foreach (var item in directory.Components)
            {
                if (item is DirectoryComponent)
                    CreateRealDirectory(item as DirectoryComponent, root.FullName);
                else
                {
                    var temp = item as FileComponent;

                    var file = new FileInfo(root.FullName + @"\" + item.Name);
                    file.Create().Dispose();
                    file.CreationTime = temp.CreationTime;
                    file.Attributes = temp.Attributes;
                }
            }
        }   
    }   
}
