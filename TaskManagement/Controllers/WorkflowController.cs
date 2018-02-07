using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Bson;
using Newtonsoft.Json;
using TaskManagement.DAL;

namespace API.Controllers
{
    public class WorkflowController : ApiController
    {
        // GET: api/Workflow
        public HttpResponseMessage Get()
        {
            var listOfFileNames = StorageAccess.GetWorkflowNames();
            // Convert list to json and return as response
            var resp = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(listOfFileNames), System.Text.Encoding.UTF8, "application/json") };
            return resp;
        }

        // GET: api/Workflow/5
        public HttpResponseMessage Get(ObjectId id)
        {
            string text = StorageAccess.GetWorkflow(id);
            var resp = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(text, System.Text.Encoding.UTF8, "application/json") };
            return resp;

        }

        // POST: api/Workflow
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Workflow/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Workflow/5
        public void Delete(int id)
        {
        }
    }
}
