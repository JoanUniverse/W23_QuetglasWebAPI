using Dapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using W20_QuetglasWebAPI.Models;

namespace W20_QuetglasWebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Message")]
    public class MessageController : ApiController
    {
        [HttpPost]
        [Route("InsertMessage")]
        public IHttpActionResult InsertMessage(MessageModel message)
        {
            string authenticatedAspNetUserId = RequestContext.Principal.Identity.GetUserId();
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {
                string sql = "INSERT INTO dbo.Messages(PlayerId, Message) " +
                    $"VALUES ('{authenticatedAspNetUserId}', '{message.Message}')";
                try
                {
                    cnn.Execute(sql);
                }
                catch (Exception ex)
                {
                    return BadRequest("Error inserting message in database: " + ex.Message);
                }

                return Ok();
            }
        }

        [HttpPost]
        [Route("GetMessages")]
        public List<MessageModel> GetMessages(PlayerModel player)
        {
            string authenticatedAspNetUserId = RequestContext.Principal.Identity.GetUserId();
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {
                string sql = $"SELECT * FROM dbo.Messages " +
                    $"WHERE MessageTime > '{player.LastLogin.ToString("MM/dd/yyyy HH:mm:ss", DateTimeFormatInfo.InvariantInfo)}'";
                var messages = cnn.Query<MessageModel>(sql).ToList();
                return messages;
            }
        }
    }
}
