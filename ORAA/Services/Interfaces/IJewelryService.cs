using Microsoft.AspNetCore.Mvc;
using ORAA.Core;
using ORAA.Data;
using ORAA.DTO;
using ORAA.Models;

namespace ORAA.Services.Interfaces
{
    public interface IJewelryService
    {
        Task<ApiResponse<List<JeweleryDTO>>> SearchJewelryAsync(JewelerySearchRequest request);
    }
}
