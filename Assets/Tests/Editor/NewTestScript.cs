using System.IO;
using NUnit.Framework;

namespace Tests.Editor
{
    public class AssetTest 
    {

        [Test] //Простой тест
        public void AssetValidate() //Название теста
        {
            
            var assetDirectoryPath = "Assets/Editor/"; //По этому пути
            var filePaths = Directory.GetFiles(assetDirectoryPath, "*.cs"); //Находим файлы с расширением

            foreach (var path in filePaths)
            {
                Validate(path); //Проверяем путь для каждого файла
            }
        }

        private void Validate(string path)
        {
            var fileName = Path.GetFileName(path); //Забираем имена файлов
        

            Assert.IsNotNull($"{fileName} => asset is null"); //Утверждаем, что имена файлов существуют
        
        }
    }
}