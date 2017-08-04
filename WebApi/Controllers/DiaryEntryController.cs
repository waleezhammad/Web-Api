using CountingKs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
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

        public HttpResponseMessage Post(DateTime diaryId, [FromBody]DiaryEntryModel model)
        {
            try
            {
                if(model == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Could not insert null values");
                if (model.Quantity == default(double))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Quantity Must have value");


                var entity = ModelFactory.Parse(model);
                if(entity==null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Could not parse");

                var diary = reposotiry.GetDiary(_identityService.UserName,diaryId);

                //if (diary.DiaryEntries.Any(c => c.Measure.Id == entity.Measure.Id))
                //    return Request.CreateResponse(HttpStatusCode.BadRequest, "Could not duplicate Measures");

                diary.DiaryEntries.Add(entity);

                if (reposotiry.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, ModelFactory.Create(diary));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Could not save to database");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(DateTime diaryId,int id)
        {

            try
            {
                var diary = reposotiry.GetDiary(_identityService.UserName, diaryId);
                if (diary == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Diary could not be found");
                if (!diary.DiaryEntries.Any(d => d.Id == id))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Diary entry is not found");

                reposotiry.DeleteDiaryEntry(id);

                if (reposotiry.SaveAll())
                    return Request.CreateResponse(HttpStatusCode.OK);
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "could not save to db");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
