using ModelService.windoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windo.Models;

namespace Windo.Repository
{
    interface IBuisnessRepos
    {
        Buisness GetBuisnessById(int id);
        List<Buisness> GetListOfBuisnesses();
        int createBuisness(Buisness model);
        int updateBuisness(Buisness model);
        int deleteBuisness(int id);
    }
}
