using AVSModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repository.Interface
{
    public interface IMaterial
    {
        Task<List<MaterialModel>> GetMaterials();
    }
}
