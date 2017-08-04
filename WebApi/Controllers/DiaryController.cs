using CountingKs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Services;

namespace WebApi.Controllers
{
    public class DiaryController : BaseController
    {
        IIdentityService _identity;
        public DiaryController(ICountingKsRepository repository,IIdentityService identity):base(repository)
        {
            this._identity = identity;
        }

        public HttpResponseMessage Get()
        {
            var diaries = reposotiry.GetDiaries(_identity.UserName).ToList();
            if (diaries == null || diaries.Count > 0)
                return Request.CreateResponse(HttpStatusCode.OK, diaries.Select(d => ModelFactory.Create(d)));
            else
                return Request.CreateResponse(HttpStatusCode.NotFound);
        }
        public HttpResponseMessage Get(DateTime DiaryId)
        {
            var diary = reposotiry.GetDiary(_identity.UserName, DiaryId);
            if (diary != null)
                return Request.CreateResponse(HttpStatusCode.OK, ModelFactory.Create(diary));
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}
