using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DesnaInfo.Business.UserService;
using DesnaInfo.LevelFetcher;
using DesnaInfo.Viber.Classes;
using DesnaInfo.Viber.Classes.Helper;
using DesnaInfo.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DesnaInfoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IHttpClientFactory _httpClientFactory;
        private IConfiguration _configuration;

        public InfoController(IUserService userService, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _userService = userService;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        #region HTTP methods

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAll();
            return Ok(users?.ToList());
        }

        [HttpPost(template: "AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserViewModel user)
        {
            var result = await _userService.RemoveUser(user.MessengerId);
            //var result = await _userService.AddUser(user);
            return Ok(result);
        }

        [HttpPost(template: "OnSubscribedWebHook")]
        public async Task<IActionResult> WebHookTest([FromBody] CallbackData callbackData)
        {
            var eventType = callbackData.Event;

            switch (eventType.ToLower())
            {
                case Constants.CONVERSATION_STARTED:
                    return WelcomeMessage();
                case Constants.SUBSCRIBED:
                    return await OnSubscribed(callbackData);
                case Constants.UNSUBSCRIBED:
                    return await OnUnSubscribed(callbackData);
                case Constants.MESSAGE:
                    return await OnMessage(callbackData);
            }
            await Task.Delay(100);
            return Ok();
        }





        #endregion

        #region Helper methods
        private async Task<IActionResult> OnMessage(CallbackData callbackData)
        {
            if (callbackData?.Sender != null)
            {
                UserViewModel model = new UserViewModel
                {
                    Id = 0,
                    MessengerId = callbackData.Sender.Id,
                    CreatedAt = DateTime.Now
                };
                var res = await _userService.AddUser(model);
                var sendRes = await SendMessage(model.MessengerId);
                return Ok();
            }
            return Ok();
        }

        [HttpPost(template: "Broadcast")]
        public async Task<IActionResult> BroadCast([FromBody] string text)
        {
            var users = await _userService.GetAll();
            if (users == null) return Ok();
            var usersIds = users.Select(u => u.MessengerId).ToArray();

            var res = await SendBroadcast(text, usersIds);

            return Ok(res);

        }
        private async Task<IActionResult> OnSubscribed(CallbackData callbackData)
        {
            if (callbackData?.User != null)
            {
                UserViewModel model = new UserViewModel
                {
                    Id = 0,
                    MessengerId = callbackData.User.Id,
                    CreatedAt = DateTime.Now
                };
                var res = await _userService.AddUser(model);
                return Ok();
            }
            return Ok();
        }



        private async Task<IActionResult> OnUnSubscribed(CallbackData callbackData)
        {
            if (callbackData?.User_Id != null)
            {
                var res = await _userService.RemoveUser(callbackData.User_Id);
                return Ok(res);
            }
            return Ok();
        }

        private IActionResult WelcomeMessage()
        {
            var cbd = new WelcomeMessage()
            {
                Sender = new Sender
                {
                    Name = "DesnaInfo"
                },
                Tracking_Data = "Track",
                Type = "text",
                Text = "Привет, я DesnaInfo бот. \r\n Я буду информировать Вас об измениении уровня воды в реке Десна в г. Чернигов \r\n" +
                       "Отправьте мне любое сообщение, чтобы подписаться."
            };

            return Ok(cbd);
        }
        private async Task<string> SendMessage(string modelMessengerId)
        {
            var levelsString = await GetLevelString();

            RegularMessage message = new RegularMessage
            {
                Receiver = modelMessengerId,
                MinApiVersion = 2,
                Sender = new Sender { Name = Constants.DESNAINFO },
                Tracking_Data = "trackData",
                Type = "text",
                Text = levelsString
            };

            return await SendMessageToViber(message, Links.SENDMESSAGE);
        }
        private async Task<string> SendBroadcast(string text, string[] usersIds)
        {
            var levelsString = await GetLevelString();

            BroadcastMessage message = new BroadcastMessage
            {
                Broadcast_List = usersIds,
                Min_Api_Version = 2,
                Type = "text",
                Sender = new Sender
                {
                    Name = Constants.DESNAINFO
                },
                Text = levelsString
            };

            return await SendMessageToViber(message, Links.BROADCASTMESSAGE);

        }

        private async Task<string> SendMessageToViber(object message, string apiUrl)
        {
            using var client = _httpClientFactory.CreateClient();
            var headerName = _configuration.GetSection("Viber").GetValue<string>("HeaderName");
            var headerValue = _configuration.GetSection("Viber").GetValue<string>("HeaderValue");


            client.DefaultRequestHeaders.Add(headerName, headerValue);
            var response = await client.PostAsync(apiUrl,
                new StringContent(
                    JsonConvert.SerializeObject(message, Formatting.None,
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }), Encoding.UTF8));

            string st = null;
            if (response.IsSuccessStatusCode)
            {
                st = await response.Content.ReadAsStringAsync();
            }

            return st;
        }

        private static async Task<string> GetLevelString()
        {
            StringBuilder builder = new StringBuilder();
            var levels = await Fetcher.FetchAsync();
            foreach (var waterLevel in levels)
            {
                builder.AppendLine(
                    $" дата: {waterLevel.Date} уровень: {waterLevel.AbsoluteLevel}, прирост: {waterLevel.Change}");
            }

            return builder.ToString();
        }

        #endregion
    }
}
