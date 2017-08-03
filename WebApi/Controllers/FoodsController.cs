using CountingKs.Data;
using NewApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class FoodsController : ApiController
    {
        public IEnumerable<Food> Get()
        {

            try
            {
                using (CountingKsRepository reposotiry = new CountingKsRepository(new NewApi.Data.CountingKsEntities()))
                {
                    return reposotiry.GetAllFoods()
                        .OrderBy(c=>c.Id)
                        .Take(25)
                        .ToList();
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
