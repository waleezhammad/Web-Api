using CountingKs.Data;
using NewApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class FoodsController : ApiController
    {
        ICountingKsRepository reposotiry;
        ModelFactory modelFactory;
        public FoodsController(ICountingKsRepository reposotiry)
        {
            this.reposotiry = reposotiry;
            modelFactory = new ModelFactory();
        }
        public IEnumerable<FoodModel> Get()
        {
            try
            {

                return reposotiry.GetAllFoodsWithMeasures()
                    .OrderBy(c => c.Id)
                    .Take(25).ToList()
                    .Select(f => modelFactory.Create(f))
                    ;

            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public FoodModel Get(int id)
        {
            return modelFactory.Create(reposotiry.GetFood(id));
        }
    }
}
