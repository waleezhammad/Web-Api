using CountingKs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public abstract class BaseController : ApiController
    {
        protected ICountingKsRepository reposotiry;
        ModelFactory modelFactory;


        public BaseController(ICountingKsRepository reposotiry)
        {
            this.reposotiry = reposotiry;
            // modelFactory = new ModelFactory();
        }

        public ModelFactory ModelFactory
        {
            get
            {
                if (modelFactory == null)
                    modelFactory = new ModelFactory(this.Request, reposotiry);
                return modelFactory;
            }
        }
    }
}
