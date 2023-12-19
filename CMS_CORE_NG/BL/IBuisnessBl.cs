using Microsoft.AspNetCore.Http;
using ModelService;
using ModelService.windoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windo.Models;

namespace Windo.BL
{
    interface IBuisnessBl
    {
        BuisnessVm GetBuisnessByEmailId(string email,int? currentBusinessId);
        List<BusinessForCardVM> GetListOfBuisnesses();
        //int createBuisness(BuisnessVm model);
        //פונק היצירה האמיתית
        Task<int> createBuisnessWithFiles(BuisnessVm modelVm, List<IFormFile> WorkFiles);
        //לא צריך עדכון - הוא נכלל ביצירה
        //int updateBuisness(BuisnessVm model);
        int deleteBuisness(int id);
    }
}
