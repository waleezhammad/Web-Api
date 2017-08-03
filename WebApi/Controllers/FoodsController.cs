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
        CountingKsRepository reposotiry;
        public FoodsController(CountingKsRepository reposotiry)
        {
            this.reposotiry = reposotiry;
        }
        public IEnumerable<Food> Get()
        {
            try
            {

                return reposotiry.GetAllFoods()
                    .OrderBy(c => c.Id)
                    .Take(25)
                    .ToList();

            }
            catch
            {
                return null;
            }
        }
    }
}
