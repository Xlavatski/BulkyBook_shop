using BulkyBook.Models.APIModels;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HttpApiController : Controller
    {
        public HttpClient httpClient;

        public HttpApiController(IHttpClientFactory factory)
        {
            httpClient = factory.CreateClient();
        }

        public IActionResult Index() 
        {
            return View();
        }



        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");

            var result = await httpClient.GetAsync("/posts");

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();

                var instances = JsonConvert.DeserializeObject<List<PostDto>>(content);
                //var instances = JsonConvert.DeserializeObject<PostDto>(content);

                //return Ok(content);
                return Json(new { data = instances });
            }

            return BadRequest(result.StatusCode);
        }

        [HttpGet]
        public async Task<IActionResult> TestJedan(long id)
        {
            httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");

            var result = await httpClient.GetAsync("/posts/"+id);

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();

                var instances = JsonConvert.DeserializeObject<PostDto>(content);

                //return Ok(content);
                return Json(new { data = instances });
            }

            return BadRequest(result.StatusCode);
        }



        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {

            httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");

            var result = await httpClient.DeleteAsync("/posts/" + id);

            if (result.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Delete Successful" });
            }

            return Json(new { success = false, message = "Delete Not Successful" });
        }

        #endregion
    }
}
