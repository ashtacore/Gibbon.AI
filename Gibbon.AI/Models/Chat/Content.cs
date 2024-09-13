using System;
using System.IO;

namespace Gibbon.AI.Models.Chat
{
    public class Content
    {
        public ContentType ContentType { get; set; }
        
        public string Text { get; set; }

        private string _imageBase64 = string.Empty; 
        public ImageSource ImageSource { get; private set; }
        public string ImageURI
        {
            get => this.ImageURI;
            set
            {
                ImageURI = value;
                if (value.StartsWith("http", StringComparison.CurrentCultureIgnoreCase))
                {
                    ImageSource = ImageSource.URL;
                    _imageBase64 = string.Empty;
                    ImageMimeType = string.Empty;
                }
                else if (string.IsNullOrEmpty(value) == false)
                {
                    ImageSource = ImageSource.Base64;
                    ConvertImageToBase64AndRecordMimeType();
                }
                else
                {
                    ImageSource = ImageSource.None;
                    _imageBase64 = string.Empty;
                    ImageMimeType = string.Empty;
                }
            } 
        }
        public string Image => string.IsNullOrEmpty(_imageBase64) ? ImageURI : _imageBase64;
        public string ImageMimeType { get; private set; }

        public Content(ContentType contentType, string textOrImageUri)
        {
            this.ContentType = ContentType.Text;

            if (this.ContentType == ContentType.Text)
            {
                this.Text = textOrImageUri;
                this.ImageURI = string.Empty;
            }
            else if (this.ContentType == ContentType.Image)
            {
                this.ImageURI = textOrImageUri;
            }
            
        }
        
        private void ConvertImageToBase64AndRecordMimeType()
        {
            try
            {
                byte[] imageArray = File.ReadAllBytes(ImageURI);
                _imageBase64 = Convert.ToBase64String(imageArray);
                ImageMimeType = DetermineMimeType(imageArray);
            }
            catch (Exception ex)
            {
                _imageBase64 = string.Empty;
                ImageMimeType = string.Empty;
            }
        }
        
        private static string DetermineMimeType(byte[] fileBytes)
        {
            if (fileBytes.Length >= 12)
            {
                // Check for JPEG
                if (fileBytes[0] == 0xFF && fileBytes[1] == 0xD8)
                {
                    return "image/jpeg";
                }
        
                // Check for PNG
                if (fileBytes[0] == 0x89 && fileBytes[1] == 0x50 && fileBytes[2] == 0x4E && fileBytes[3] == 0x47 &&
                    fileBytes[4] == 0x0D && fileBytes[5] == 0x0A && fileBytes[6] == 0x1A && fileBytes[7] == 0x0A)
                {
                    return "image/png";
                }
        
                // Check for GIF
                if (fileBytes[0] == 0x47 && fileBytes[1] == 0x49 && fileBytes[2] == 0x46 &&
                    fileBytes[3] == 0x38 && (fileBytes[4] == 0x37 || fileBytes[4] == 0x39) && fileBytes[5] == 0x61)
                {
                    return "image/gif";
                }
        
                // Check for WebP
                if (fileBytes[0] == 0x52 && fileBytes[1] == 0x49 && fileBytes[2] == 0x46 && fileBytes[3] == 0x46 &&
                    fileBytes[8] == 0x57 && fileBytes[9] == 0x45 && fileBytes[10] == 0x42 && fileBytes[11] == 0x50)
                {
                    return "image/webp";
                }
            }
    
            return "application/octet-stream"; // Default unknown binary data
        }
    }
    
    public enum ContentType
    {
        Text,
        Image
    }

    public enum ImageSource
    {
        Base64,
        URL,
        None
    }
}