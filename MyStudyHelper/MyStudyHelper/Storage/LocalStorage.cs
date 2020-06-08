using PCLStorage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace MyStudyHelper.Storage
{
    public class LocalStorage
    {
        // read a text file from the app's local folder
        public static async Task<string> ReadTextFileAsync(string _filename)
        {
            // declare an empty variable to be filled later
            string result = null;

            // see if the file exists
            try
            {
                // get hold of the file system
                IFolder rootFolder = PCLStorage.FileSystem.Current.LocalStorage;

                // create a folder, if one does not exist already
                IFolder folder = await rootFolder.CreateFolderAsync("StudyApp", CreationCollisionOption.OpenIfExists);

                // create a file, overwriting any existing file
                IFile file = await rootFolder.CreateFileAsync(_filename, CreationCollisionOption.OpenIfExists);

                // populate the file with some text
                result = await file.ReadAllTextAsync();
            }
            // if the file doesn't exist
            catch (Exception ex)
            {
                // Output to debugger
                Debug.WriteLine(ex);
            }

            // return the contents of the file
            return result;
        }

        // write a text file to the app's local folder
        public static async Task<string> WriteTextFileAsync(string _filename, string _content)
        {
            // declare an empty variable to be filled later
            string result = null;
            try
            {
                // get hold of the file system
                IFolder rootFolder = PCLStorage.FileSystem.Current.LocalStorage;

                // create a folder, if one does not exist already
                IFolder folder = await rootFolder.CreateFolderAsync("StudyApp", CreationCollisionOption.OpenIfExists);

                // create a file, overwriting any existing file
                IFile file = await rootFolder.CreateFileAsync(_filename, CreationCollisionOption.ReplaceExisting);

                // populate the file with some text
                await file.WriteAllTextAsync(_content);

                result = _content;
            }
            // if there was a problem
            catch (Exception ex)
            {
                // Output to debugger
                Debug.WriteLine(ex);
            }

            // return the contents of the file
            return result;
        }
    }
}