using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waangxuapi.Core.DAL.IHelper;
using wangxuapi.Core.Model;
using Wangxuapi.Core.Model.Model;

namespace Waangxuapi.Core.DAL.Helper
{
    public  class CityDAL : BaseDAL<User>, ICityDAL
    {
        private readonly EFDbContext _DbContext;
        public CityDAL(EFDbContext db) : base(db)
        {
            this._DbContext = db;
        }
    }
}
