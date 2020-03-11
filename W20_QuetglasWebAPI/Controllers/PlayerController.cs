using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using W20_QuetglasWebAPI.Models;
using Microsoft.AspNet.Identity;
using System.Globalization;

namespace W20_QuetglasWebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Player")]
    public class PlayerController : ApiController
    {
        // POST api/Player/RegisterPlayer
        [HttpPost]
        [Route("RegisterPlayer")]
        public IHttpActionResult RegisterPlayer(PlayerModel player)
        {
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {
                string sql = "INSERT INTO dbo.Players(Id, Name, Email, BirthDay) " +
                    $"VALUES ('{player.Id}', '{player.Name}', '{player.Email}', '{player.BirthDay}')";
                try
                {
                    cnn.Execute(sql);
                }
                catch (Exception ex)
                {
                    return BadRequest("Error inserting player in database: " + ex.Message);
                }

                return Ok();
            }
        }

        // GET api/Player/Info
        [HttpGet]
        [Route("Info")]
        public PlayerModel GetPlayerInfo()
        {
            string authenticatedAspNetUserId = RequestContext.Principal.Identity.GetUserId();
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {
                string sql = $"SELECT Id, Name, Email, BirthDay, LastLogin FROM dbo.Players " +
                    $"WHERE Id LIKE '{authenticatedAspNetUserId}'";
                var player = cnn.Query<PlayerModel>(sql).FirstOrDefault();
                return player;
            }
        }

        [HttpPost]
        [Route("UpdateLastLogin")]
        public IHttpActionResult UpdateLastLogin()
        {
            string authenticatedAspNetUserId = RequestContext.Principal.Identity.GetUserId();
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {
                string time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
                string sql = $"UPDATE dbo.Players SET LastLogin = '{time}' " +
                    $"WHERE Id = '{authenticatedAspNetUserId}'";
                try
                {
                    cnn.Execute(sql);
                }
                catch (Exception ex)
                {
                    return BadRequest("Error updateing player LastLogin in database: " + ex.Message);
                }

                return Ok();
            }
        }
    }
}
