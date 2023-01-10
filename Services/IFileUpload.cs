﻿namespace ProductManagementSystem.Services
{
    public interface IFileUpload
    {
        Task<bool> UploadFile(IFormFile file);

        string FileName { get; set; }
    }
}
