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
            if (File.Exists(fileToDelete))
            {
                File.Delete(Application.persistentDataPath + '/' + fileToDelete);
            }
        }
    }
}
