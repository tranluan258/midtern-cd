using Midterm_Chuyende.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Midterm_Chuyende.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        MidternEntities db = new MidternEntities();
        public ActionResult Index(String id)
        {
            if(Session["Account"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewModelIndex data = new ViewModelIndex();
            int page = 1;
            int pageSize = 3;
            int countPage = db.Posts.ToList().Count;
            if (id != null)
            {
                if (Request["page"] != null)
                {
                    page = Int32.Parse(Request["page"]);
                    data.getAllPost = db.Posts.Where(p => p.topicKey == id).OrderBy(prop => prop.id).Skip((int)((page - 1) * pageSize)).Take((int)pageSize).ToList();
                }
                else
                {
                    data.getAllPost = db.Posts.Where(p => p.topicKey == id).OrderBy(prop => prop.id).Skip((int)((page - 1) * pageSize)).Take((int)pageSize).ToList();
                }
                countPage = db.Posts.Where(p => p.topicKey == id).ToList().Count;
            }
            else
            {
                if (Request["page"] != null)
                {
                    page = Int32.Parse(Request["page"]);
                    data.getAllPost = db.Posts.OrderBy(prop => prop.id).Skip((int)((page - 1) * pageSize)).Take((int)pageSize).ToList();
                }
                else
                {
                    data.getAllPost = db.Posts.OrderBy(prop => prop.id).Skip((int)((page - 1) * pageSize)).Take((int)pageSize).ToList();
                }
            }
            double MaxPage = (double)countPage / (double)pageSize;
            ViewBag.MaxPage = Math.Ceiling(MaxPage);

            data.getAllTopic = db.Topics.ToList();
            String username = Session["Account"].ToString();
            data.getAccount = db.Accounts.FirstOrDefault(a => a.username == username);
            return View(data);
        }

        [HttpPost]
        public JsonResult SearchPost(String id)
        {
            var posts = db.Posts.Where(p => p.namePost.StartsWith(id)).Select(p => new { id = p.id, namePost = p.namePost  }).ToList();
            return Json(new { code = id, msg = posts });
        }

        public ActionResult Detail(int id)
        {
            if (Session["Account"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewModelDetail data = new ViewModelDetail();
            String username = Session["Account"].ToString();
            data.account = db.Accounts.FirstOrDefault(a => a.username == username);
            data.post = db.Posts.FirstOrDefault(p => p.id == id);
            data.comments = db.Comments.Where(c => c.idPost == id).ToList();
            return View(data);
        }
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
    }
}