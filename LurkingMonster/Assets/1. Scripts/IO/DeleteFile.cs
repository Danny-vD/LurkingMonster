using System.IO;
using UnityEngine;

namespace IO
{
    public class DeleteFile : MonoBehaviour
    {
        [SerializeField]
        private string fileToDelete = string.Empty;

        public void RemoveFile()
        {
            string path = $"{Application.persistentDataPath}/{fileToDelete}";
            print(path);
            
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
