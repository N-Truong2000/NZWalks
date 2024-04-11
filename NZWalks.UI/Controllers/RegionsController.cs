using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models.Dto;
using System.Net;
using System.Text;
using System.Text.Json;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        private readonly string Url = "https://localhost:7035/api";
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = new List<RegionDto>();
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpResponseMessage = await client.GetAsync($"{Url}/v1/regions/getall");
                httpResponseMessage.EnsureSuccessStatusCode();
                var response = await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>();

                list.AddRange(response);
            }
            catch (Exception ex)
            {

                throw;
            }

            return View(list);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"{Url}/v1/regions/create"),
                    Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"),
                };
                var httpResponseMessage = await client.SendAsync(httpRequestMessage);
                //httpResponseMessage.EnsureSuccessStatusCode();
                if (httpResponseMessage.StatusCode==HttpStatusCode.BadRequest)
                {
                    var errorContent = await httpResponseMessage.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", errorContent);
                    return View(model);

                }

                var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();
                if (response is not null)
                {
                    return RedirectToAction("Index", "Regions");
                }
                else
                {
                    // Trường hợp không có phản hồi hợp lệ từ máy chủ
                    ModelState.AddModelError("", "Không thể thêm khu vực. Vui lòng thử lại sau.");
                    return View(model);
                }
            }
            catch (HttpRequestException ex)
            {
                // Xử lý lỗi từ giao tiếp mạng
                ModelState.AddModelError("", "Lỗi kết nối mạng. Vui lòng kiểm tra kết nối của bạn và thử lại sau.");
                return View(model);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi chung
                ModelState.AddModelError("", "Đã xảy ra lỗi. Vui lòng thử lại sau hoặc liên hệ với quản trị viên.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<RegionDto>($"{Url}/v2/regions/GetById/{id}");
            if (response != null)
            {
                return View(response);
            }
            return View(id);
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] RegionDto model)
        {
            var client = _httpClientFactory.CreateClient();
            var htppRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Patch,
                RequestUri = new Uri($"{Url}/v1/regions/Update/{model.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"),
            };
            var httpReponseMessage= await client.SendAsync(htppRequestMessage);
            httpReponseMessage.EnsureSuccessStatusCode();
            var reponse= await htppRequestMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (reponse is not null)
            {
                 return RedirectToAction("Index", "Regions");
            }
            return View(reponse);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto model)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpReponseMessage = await client.DeleteAsync($"{Url}/v1/regions/delete/{model.Id}");
                httpReponseMessage.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Regions");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
