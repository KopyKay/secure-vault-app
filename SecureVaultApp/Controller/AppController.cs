using api_access;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using SecureVaultApp.Converter;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vault.Dtos;
using vault.Models;
using Windows.Storage;
using Windows.Storage.Pickers;
using System.Diagnostics;

namespace SecureVaultApp.Controller
{
    public class AppController
    {
        private ApiService _apiService;
        private FileBlobConverter _blobConverter;
        public List<File> userFiles { get; private set; }
        public List<Credential> userCredentials { get; private set; }

        public AppController()
        {
            _apiService = new ApiService();
            _blobConverter = new FileBlobConverter();
            userFiles = new List<File>();
            userCredentials = new List<Credential>();

            //LoadUserData();
        }

        #region blob-file converter methods
        public byte[] ConvertFileToBlob(StorageFile file)
        {
            var blobData = _blobConverter.SaveFileAsBlob(file);

            return blobData;
        }

        public void ConvertBlobToFile(byte[] blobData, string destinationPath)
        {
            _blobConverter.SaveBlobAsFileAsync(blobData, destinationPath);
        }
        #endregion

        #region api service methods
        public void ResetToken()
        {
            _apiService.ClearAuthToken();
        }

        public string GetToken()
        {
            return _apiService.GetAuthToken();
        }

        private async void LoadUserData()
        {
            userFiles = await GetUserFilesAsync();
            userCredentials = await GetUserCredentialsAsync();
        }

        public async Task<bool> TryUserSignInAsync(string userEmail, string userPassword, string userKey)
        {
            var userCredentials = new UserCredentialsDTO
            {
                Email = userEmail,
                Password = userPassword,
                Key = userKey
            };

            await _apiService.RequestAndSetTokenAsync(userCredentials);

            var token = _apiService.GetAuthToken();

            if (token == null)
                return false;

            return true;
        }

        public async Task<string> TryCreateNewUserAsync(string userEmail, string userPassword, string userConfirmPassowrd)
        {
            var newUser = new User
            {
                Email = userEmail,
                Password = userPassword
            };

            var userKey = await _apiService.PostUserAsync(newUser);

            return userKey;
        }

        public async Task<User> GetUserAccountAsync()
        {
            var user = await _apiService.GetUserAsync();

            return user;
        }

        public async Task<List<File>> GetUserFilesAsync()
        {
            var files = await _apiService.GetFilesAsync();

            return files;
        }

        public async Task<List<Credential>> GetUserCredentialsAsync()
        {
            var credentials = await _apiService.GetCredentialsAsync();

            return credentials;
        }

        public async Task<File> UploadFileAsync(File file)
        {
            return await _apiService.PostFileAsync(file);
        }

        public async Task<Credential> UploadCredentialAsync(Credential credential)
        {
            return await _apiService.PostCredentialAsync(credential);
        }

        public async Task<bool> TryDeleteFileAsync(int id)
        {
            return await _apiService.DeleteFileAsync(id);
        }

        public async Task<bool> TryDeleteCredentialAsync(int id)
        {
            return await _apiService.DeleteCredentialAsync(id);
        }
        #endregion

        #region WinUI methods
        public async void DisplayErrorDialog(XamlRoot xamlRoot, string message)
        {
            var dialog = new ContentDialog
            {
                XamlRoot = xamlRoot,
                Style = (Style)Application.Current.Resources["DefaultContentDialogStyle"],
                Title = "Something goes wrong!",
                Content = message,
                CloseButtonText = "OK",
                DefaultButton = ContentDialogButton.Close
            };

            await dialog.ShowAsync();
        }

        public async Task<StorageFile> OpenFilePickerAsync()
        {
            var openPicker = new FileOpenPicker();
            var window = View.Window.AuthorizationWindow._mainWindow;
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.FileTypeFilter.Add("*");

            var pickedFile = await openPicker.PickSingleFileAsync();

            return pickedFile;
        }

        public async Task<string> GetFolderPathAsync()
        {
            var openPicker = new FolderPicker();
            var window = View.Window.AuthorizationWindow._mainWindow;
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

            openPicker.SuggestedStartLocation = PickerLocationId.Downloads;
            openPicker.FileTypeFilter.Add("*");

            var folderPath = await openPicker.PickSingleFolderAsync();

            if (folderPath == null)
                return string.Empty;

            return folderPath.Path;
        }
        #endregion
    }
}
