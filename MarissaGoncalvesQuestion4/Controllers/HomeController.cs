using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MarissaGoncalvesQuestion4.Controllers
{
    public class HomeController : Controller
    {

        Models.ShoppingItemsEntities database = new Models.ShoppingItemsEntities();

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }


        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {

            string username = collection["username"];
            Models.User theUser = database.Users.SingleOrDefault(u => u.username.Equals(username));

            if(theUser != null && Crypto.VerifyHashedPassword(theUser.password_hash, collection["password_hash"]))
            {
                Session["user_id"] = theUser.user_id;
                return RedirectToAction("Index", "Shopping");
            }

            else
            {
                return View();
            }
        }




        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(FormCollection collection)
        {

            string username = collection["username"];
            Models.User theUser = database.Users.SingleOrDefault(u => u.username.Equals(username));

            if (theUser != null)
            {
                return RedirectToAction("Register");
            }

            try
            {
                Models.User newUser = new Models.User
                {
                    username = collection["username"],
                    password_hash = Crypto.HashPassword(collection["password_hash"])
                };

                database.Users.Add(newUser);
                database.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }
}
