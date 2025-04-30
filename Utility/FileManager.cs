using IoFile = System.IO.File;
namespace VialambreAppTest1.Utility
{
    public class FileManager
    {
        public static async Task<string> CopyFile(IFormFile file, string uploadFolder)
        {
            var extention = Path.GetExtension(file.FileName);
            string newName = Guid.NewGuid().ToString() + extention;
            var fileDest = Path.Combine(uploadFolder, newName);
            using (var fileStream = new FileStream(fileDest, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return newName;
        }
        public static void Deletefile(string filePath)
        {
            if (filePath != null)
            {
                if (IoFile.Exists(filePath))
                {
                    IoFile.Delete(filePath);
                }
            }
        }

        public static async Task<string> CopyFileCostos(IFormFile filesCostos, string UploadFolderCostos)
        {
            var extention = Path.GetExtension(filesCostos.FileName);
            string newName = Guid.NewGuid().ToString() + extention;
            var fileDest = Path.Combine(UploadFolderCostos, newName);
            using (var fileStream = new FileStream(fileDest, FileMode.Create))
            {
                await filesCostos.CopyToAsync(fileStream);
            }
            return newName;
        }

        public static void DeletefileCostos(string filePath)
        {
            if (filePath != null)
            {
                if (IoFile.Exists(filePath))
                {
                    IoFile.Delete(filePath);
                }
            }
        }

        public static async Task<string> CopyFileDesign(IFormFile filesDesign, string UploadFolderDesign)
        {
            var extention = Path.GetExtension(filesDesign.FileName);
            string newName = Guid.NewGuid().ToString() + extention;
            var fileDest = Path.Combine(UploadFolderDesign, newName);
            using (var fileStream = new FileStream(fileDest, FileMode.Create))
            {
                await filesDesign.CopyToAsync(fileStream);
            }
            return newName;
        }
        public static void DeletefileDesign(string filePath)
        {
            if (filePath != null)
            {
                if (IoFile.Exists(filePath))
                {
                    IoFile.Delete(filePath);
                }
            }
        }

        public static async Task<string> CopyFileRespCostos(IFormFile filesCostos, string uploadFolder)
        {
            var extention = Path.GetExtension(filesCostos.FileName);
            string newName = Guid.NewGuid().ToString() + extention;
            var fileDest = Path.Combine(uploadFolder, newName);
            using (var fileStream = new FileStream(fileDest, FileMode.Create))
            {
                await filesCostos.CopyToAsync(fileStream);
            }
            return newName;
        }
        public static void DeletefileRespCostos(string filePath)
        {
            if (filePath != null)
            {
                if (IoFile.Exists(filePath))
                {
                    IoFile.Delete(filePath);
                }
            }
        }

        public static async Task<string> CopyFilePieza(IFormFile filePieza, string UploadFolderPieza)
        {
            var extention = Path.GetExtension(filePieza.FileName);
            string newName = Guid.NewGuid().ToString() + extention;
            var fileDest = Path.Combine(UploadFolderPieza, newName);
            using (var fileStream = new FileStream(fileDest, FileMode.Create))
            {
                await filePieza.CopyToAsync(fileStream);
            }
            return newName;
        }

        public static void DeletefilePieza(string filePath)
        {
            if (filePath != null)
            {
                if (IoFile.Exists(filePath))
                {
                    IoFile.Delete(filePath);
                }
            }
        }
    }
}
