using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json.Linq;

namespace prjToolist.Controllers {
    [RoutePrefix("api/test")]
    public class ValuesController : ApiController {
        private readonly FEUNMLEntities db = new FEUNMLEntities();
        public int str { get; set; }

        [HttpPost]
        [Route("listPost")]
        [EnableCors("*", "*", "*")]
        public IEnumerable<user> ttt() {
            //public List<Student> Get() {
            var api = from p in db.users
                select p;
            //user.Add*()
            return api.ToList();
        }

        // GET api/values
        [HttpGet]
        [Route("listGet")]
        public IEnumerable<user> Get() {
            //public List<Student> Get() {

            var api = from p in db.users
                select p;

            //user.Add*()
            return api.ToList();

            /*
            return new List<Student> {
                new Student {
                    Id = 100,
                    Name = "小AAA明"
                },
                new Student {
                    Id = 101,
                    Name = "小華"
                }
            };
            */
        }

        // GET api/values/5
        public IEnumerable<user> Get(int id) {
            var api = from p in db.users
                where p.id == id
                select p;
            return api;
        }

        [HttpPost]
        // POST api/values
        public HttpResponseMessage Post([FromBody] string createUser) {
            var controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            var jo = JObject.Parse(createUser);
            var name = jo["name"].ToString();
            var email = jo["email"].ToString();
            var pwd = jo["password"].ToString();
            var createdTime = jo["created"].ToString();
            //string updatedTime = jo["updated"].ToString();
            var authorityNew = jo["authority"].ToString();
            var createMember = new user();
            createMember.name = name;
            createMember.email = email;
            createMember.password = pwd;
            createMember.created = DateTime.Parse(createdTime);
            //createMember.updated = DateTime.Parse(updatedTime);
            createMember.authority = int.Parse(authorityNew);
            db.users.Add(createMember);
            db.SaveChanges();
            var result = new {
                STATUS = true,
                MSG = "成功"
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // PUT api/values/5
        public HttpResponseMessage Put(int id, [FromBody] string updateUser) {
            var controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            var jo = JObject.Parse(updateUser);
            //string memberId = jo["id"].ToString();
            var name = jo["name"].ToString();
            var email = jo["email"].ToString();
            var pwd = jo["password"].ToString();
            var updatedTime = jo["updated"].ToString();
            var authorityNew = jo["authority"].ToString();
            var updateMember = db.users.FirstOrDefault(p => p.id == id);

            //user createMember = new user();
            updateMember.name = name;
            updateMember.email = email;
            updateMember.password = pwd;
            updateMember.updated = DateTime.Parse(updatedTime);
            updateMember.authority = int.Parse(authorityNew);
            db.SaveChanges();
            var result = new {
                STATUS = true,
                MSG = "成功"
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // DELETE api/values/5
        public void Delete(int id) {
        }


        public class Student {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}