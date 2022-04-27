using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.BL.Helper
{
    public static class UploadFileHelper
    {
        public static string FileUploader(string FolderPath , IFormFile File)
        {
            try
            {

                // 1) Get The Physical Path (Directory)
                string PhysicalPath = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot", FolderPath);

                // 2) Get File Name
                string FileName = Guid.NewGuid() + Path.GetFileName(File.FileName);

                // 3) Merge Physical Path + File Name
                string FinalPath = Path.Combine(PhysicalPath, FileName);

                // 4) Save The File As Streams "Data Over Time"
                using (var stream = new FileStream(FinalPath, FileMode.Create))
                {
                    File.CopyTo(stream);
                }

                return FileName;

            }
            catch (Exception ex)
            {
                return ex.Message;    
            }
        }


        public static string FileRemover(string FolderPath, string FileName)
        {
            try
            {

                if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory() ,"wwwroot", FolderPath , FileName)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", FolderPath, FileName));
                }

                return "Removed";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


}
}
