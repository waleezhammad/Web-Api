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
    public class DiaryEntryController : BaseController
    {
        IIdentityService _identityService;
        public DiaryEntryController(ICountingKsRepository repository, IIdentityService identityService) : base(repository)
        {
            this._identityService = identityService;
        }

        public HttpResponseMessage Get(DateTime diaryId)
        {
            var diaryEntries = reposotiry.GetDiaryEntries(_identityService.UserName, diaryId).ToList();
            if (diaryEntries != null && diaryEntries.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, diaryEntries.Select(de => ModelFactory.Create(de)));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
        public HttpResponseMessage Get(DateTime diaryId, int id)
        {
            var diary = reposotiry.GetDiaryEntry(_identityService.UserName, diaryId, id);
            if (diary == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, ModelFactory.Create(diary));
        }
    }
}
