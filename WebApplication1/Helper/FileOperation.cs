using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace WebApplication1.Helper
{
    public static class FileOperation
    {
        public static string BrowseFile(HttpPostedFile httpPostedFile, string ImageType)
        {
          
            string hdfAvatarUrl = "";
            
               
                if (httpPostedFile != null)
                {
                    if (IsImage(httpPostedFile) == false)
                    {
                        string error = "#E#Invalid file type!";                        
                        return error;
                    }

                    string fileExtension, randomString, sourceFileName;


                    randomString = Guid.NewGuid().ToString();
                       
                    sourceFileName = httpPostedFile.FileName;
                    fileExtension = httpPostedFile.FileName.Split('.')[1];
                    hdfAvatarUrl = "temp/" + randomString + "." + fileExtension;
                    string destinationPath = GetDiskPathForUploadedImages + hdfAvatarUrl;    
                    httpPostedFile.SaveAs(destinationPath);       

                    if (CheckFileSize(ImageType, destinationPath)==false)
                    {
                       
                        string error = "#E#Maximum Image size with in " +  AvaterimageMaxW.ToString() + "x" +  AvaterimageMaxH.ToString() + " and minimum image size " + AvaterimageMinW.ToString() + "x" + AvaterimageMinH.ToString();
                         File.Delete(destinationPath);
                        return error;
                    }
                }

                return GetDiskPathForUploadedImages + hdfAvatarUrl;
        
        }
        private static int AvaterimageMinH { get; set; }
        private static int AvaterimageMinW { get; set; }
        private static int AvaterimageMaxW { get; set; }
        private static int AvaterimageMaxH { get; set; }
        private static string GetDiskPathForUploadedImages { get; set; }

        public static bool IsImage(HttpPostedFile postedFile)
        {
            try
            {
                using (var bitmap = new System.Drawing.Bitmap(postedFile.InputStream))
                {
                    if (bitmap.Size.IsEmpty)
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    




        private static bool CheckFileSize(string ImageType, string file)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(file);

           

            if (AvaterimageMinW > img.Width || img.Width > AvaterimageMaxW)
            {
                img.Dispose();
             return false;
            }

            if (AvaterimageMinH > img.Height || img.Height > AvaterimageMaxH)
            {
                img.Dispose();
                return false;
            }
            img.Dispose();
            return true;
        }

        public static string  basicOperation(string userType, string tempUrl)
        {
            string pictureUrl;
            pictureUrl = userType + "/" + Guid.NewGuid().ToString() + "." + GetFileExtension(tempUrl);
            System.IO.File.Copy(GetDiskPathForUploadedImages + tempUrl, GetDiskPathForUploadedImages + pictureUrl);
            System.IO.File.Delete(GetDiskPathForUploadedImages + tempUrl);
            return pictureUrl;
        }

        public static void DeleteOperation(string imageUrl)
        {
            var fileName = GetDiskPathForUploadedImages + imageUrl;
            FileInfo info = new FileInfo(fileName);

                    
            System.IO.File.Delete(fileName);

        }
        private static string GetFileExtension(string stringData)
        {
            int i, len, j;
            string res;
            res = "";
            j = -1;
            len = stringData.Length;
            for (i = len - 1; i >= 0; i--)
            {
                if (stringData[i] == '.')
                {
                    j = i + 1;
                    break;
                }
            }
            for (i = j; i < len; i++)
            {
                res += stringData[i];
            }
            return res;
        }

        public static string ImageCrop(string imagepath,int imageX,int imageY,int imageW,int imageH)
        {
            string imageCroppedPath = "";

            System.Drawing.Image orgImg = System.Drawing.Image.FromFile(imagepath);

            Rectangle CropArea = new Rectangle(
            Convert.ToInt32(imageX),
            Convert.ToInt32(imageY),
            Convert.ToInt32(imageW),
            Convert.ToInt32(imageH));
            FileInfo aa = new FileInfo(imagepath);
           
            try
            {
                Bitmap bitMap = new Bitmap(CropArea.Width, CropArea.Height);
                using (Graphics g = Graphics.FromImage(bitMap))
                {
                    g.DrawImage(orgImg, new Rectangle(0, 0, bitMap.Width, bitMap.Height), CropArea, GraphicsUnit.Pixel);
                }
                string  cropFileName = "";

               string randomString = RandomGenerator.GetRandomAlphaNumericStringOfParticularLength(SiteUtility.GetRandomStringLength());
               string fileExtension = aa.FullName.Split('.')[aa.FullName.Split('.').Length-1];
                 cropFileName = "temp/" + randomString + "." + fileExtension;
                string cropFilePath = SiteUtility.GetDiskPathForUploadedImages() + cropFileName;
               
                bitMap.Save(cropFilePath);
                imageCroppedPath = cropFileName;
            }
            catch (Exception ex)
            {
                throw;
            }

            return imageCroppedPath;


        }
    }
}
