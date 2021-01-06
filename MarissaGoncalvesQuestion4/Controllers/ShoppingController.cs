using MarissaGoncalvesQuestion4.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarissaGoncalvesQuestion4.Controllers
{
    public class ShoppingController : Controller
    {

        Models.ShoppingItemsEntities database = new Models.ShoppingItemsEntities();

        [LoginFilter]
        public ActionResult Index()
        {
            int user_id = int.Parse(Session["user_id"].ToString());
            Models.User theUser = database.Users.SingleOrDefault(u => u.user_id == user_id);

            if(theUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(theUser);
        }

    
        // GET: Shopping/Create
        public ActionResult Create()
        {
            int user_id = int.Parse(Session["user_id"].ToString());
            Models.User theUser = database.Users.SingleOrDefault(u => u.user_id == user_id);

            if (theUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Shopping/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            int user_id = int.Parse(Session["user_id"].ToString());
            Models.User theUser = database.Users.SingleOrDefault(u => u.user_id == user_id);

            if (theUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                Models.Item newItem = new Models.Item
                {
                    item1 = collection["item1"],
                    user_id = theUser.user_id
                };

                if (newItem.item1 == "")
                {
                    return RedirectToAction("Create");
                }

                database.Items.Add(newItem);
                database.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shopping/Edit/5
        public ActionResult Edit(int id)
        {
            int user_id = int.Parse(Session["user_id"].ToString());
            Models.User theUser = database.Users.SingleOrDefault(u => u.user_id == user_id);

            if (theUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Models.Item theItem = database.Items.SingleOrDefault(i => i.item_id == id);
            return View(theItem);
        }

        // POST: Shopping/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            int user_id = int.Parse(Session["user_id"].ToString());
            Models.User theUser = database.Users.SingleOrDefault(u => u.user_id == user_id);

            if (theUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                Models.Item theItem = database.Items.SingleOrDefault(i => i.item_id == id);
                theItem.item1 = collection["item1"];

                if (theItem.item1 == "")
                {
                    return RedirectToAction("Edit");
                }

                database.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        // GET: Shopping/Delete/5
        public ActionResult Delete(int id)
        {
            int user_id = int.Parse(Session["user_id"].ToString());
            Models.User theUser = database.Users.SingleOrDefault(u => u.user_id == user_id);

            if (theUser == null)
            {
                return RedirectToAction("Index", "Home");
            }


            try
            {
                Models.Item theItem = database.Items.SingleOrDefault(i => i.item_id == id);
                database.Items.Remove(theItem);
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
