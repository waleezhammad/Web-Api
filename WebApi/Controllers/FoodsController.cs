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
    public class FoodsController : BaseController
    {

        public FoodsController(ICountingKsRepository repository) : base(repository)
        {

        }

        public IEnumerable<FoodModel> Get()
        {
            try
            {

                return reposotiry.GetAllFoodsWithMeasures()
                    .OrderBy(c => c.Id)
                    .Take(25).ToList()
                    .Select(f => ModelFactory.Create(f));

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public FoodModel Get(int id)
        {
            return ModelFactory.Create(reposotiry.GetFood(id));
        }
    }
}
