using System;
using PCLStorage;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MyStudyHelper.Storage
{
    public class LocalStorage
    {
        //Read a text file from the app's local folder
        public static async Task<string> ReadTextFileAsync(string _filename)
        {
            //Declare an empty variable to be filled later
            string result = null;

            //See if the file exists
            try
            {
                //Get hold of the file system
                IFolder rootFolder = PCLStorage.FileSystem.Current.LocalStorage;

                //Create a folder, if one does not exist already
                IFolder folder = await rootFolder.CreateFolderAsync("StudyApp", CreationCollisionOption.OpenIfExists);

                //Create a file, overwriting any existing file
                IFile file = await rootFolder.CreateFileAsync(_filename, CreationCollisionOption.OpenIfExists);

                //Populate the file with some text
                result = await file.ReadAllTextAsync();
            }
            //If the file doesn't exist
            catch (Exception ex)
            {
                //Output to debugger
                Debug.WriteLine(ex);
            }
            //Return the contents of the file
            return result;
        }

        //Write a text file to the app's local folder
        public static async Task<string> WriteTextFileAsync(string _filename, string _content)
        {
            //Declare an empty variable to be filled later
            string result = null;
            try
            {
                //Get hold of the file system
                IFolder rootFolder = PCLStorage.FileSystem.Current.LocalStorage;

                //Create a folder, if one does not exist already
                IFolder folder = await rootFolder.CreateFolderAsync("StudyApp", CreationCollisionOption.OpenIfExists);

                //Create a file, overwriting any existing file
                IFile file = await rootFolder.CreateFileAsync(_filename, CreationCollisionOption.ReplaceExisting);

                //Populate the file with some text
                await file.WriteAllTextAsync(_content);

                result = _content;
            }
            //If there was a problem
            catch (Exception ex)
            {
                //Output to debugger
                Debug.WriteLine(ex);
            }
            //Return the contents of the file
            return result;
        }
    }
}