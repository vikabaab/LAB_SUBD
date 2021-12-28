using BalanceProject2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
namespace BalanceProject2.Controllers
{
    public class HomeController : Controller
    {
        BalanceDb db = new BalanceDb();
        //Работа с ролями
        private bool YesOrNoRoles(string role)
        {
            return db.Roles.Any(dp => dp.Name == role);
        }
        public ActionResult ListRoles()
        {
            List<Roles> GetListRole = db.Roles.ToList();
            return View(GetListRole);
        }
        [HttpGet]
        public ActionResult AddRoles()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddRoles(Roles roles)
        {
            if (!YesOrNoRoles(Convert.ToString(roles.Name)))
            {
                if (!roles.ToString().Equals(0))
                {
                    db.Roles.Add(roles);
                    db.SaveChanges();
                    return RedirectToAction("ListRoles");
                }
                else
                {
                    ViewBag.ErrorMessage = "Введите данные";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Данная роль уже имеется";
                return View();
            }
        }
        public ActionResult DeleteRoles(int id)
        {
            Roles b = db.Roles.Find(id);
            if (b != null)
            {
                db.Roles.Remove(b);
                db.SaveChanges();
            }
            return RedirectToAction("ListRoles");
        }
        [HttpGet]
        public ActionResult UpdateRoles(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Roles roles = db.Roles.Find(id);
            if (roles != null)
            {
                return View(roles);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult UpdateRoles(Roles roles)
        {
            if (!YesOrNoRoles(Convert.ToString(roles.Name)))
            {
                try
                {
                    db.Entry(roles).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ListRoles");
                }
                catch
                {
                    ViewBag.ErrorMessage = "Введите данные";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Данная роль уже имеется";
                return View();
            }
        }
        //Работа с пользователями
        private bool YesOrNoLogin(string login)
        {
            return db.Users.Any(dp => dp.Login == login);
        }
        public ActionResult ListUsers()
        {
            List<Users> GetListRole = db.Users.ToList();
            return View(GetListRole);
        }
        public ActionResult DeleteUsers(int id)
        {
            Users i = db.Users.Find(id);
            if (i != null)
            {
                db.Users.Remove(i);
                db.SaveChanges();
            }
            return RedirectToAction("ListUsers");
        }
        [HttpGet]
        public ActionResult UpdateUsers(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Users users = db.Users.Find(id);
            if (users != null)
            {
                IEnumerable<SelectListItem> roles = db.Roles.Select(b => new SelectListItem { Value = b.Id_Role.ToString(), Text = b.Name });
                ViewData["Id_Role"] = roles;
                return View(users);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult UpdateUsers(Users users)
        {
            try
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListUsers");
            }
            catch
            {
                ViewBag.ErrorMessage = "Введите данные";
                return View();
            }
        }
        [HttpGet]
        public ActionResult AddUsers()
        {
            IEnumerable<SelectListItem> roles = db.Roles.Select(b => new SelectListItem { Value = b.Id_Role.ToString(), Text = b.Name });
            ViewData["Id_Role"] = roles;
            return View();
        }
        [HttpPost]
        public ActionResult AddUsers(Users users)
        {
            if (!YesOrNoLogin(Convert.ToString(users.Login)))
            {
                try
                {
                    db.Users.Add(users);
                    db.SaveChanges();
                    return RedirectToAction("AddUsers", new Roles());
                }
                catch
                {
                    ViewBag.ErrorMessage = "Введите данные";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Такой логин уже есть";
                return View();
            }
        }
        //Лицевой счёт
        public ActionResult ListPA()
        {
            List<Personal_accounts> GetListRole = db.Personal_accounts.ToList();
            return View(GetListRole);
        }
        [HttpGet]
        public ActionResult AddPA()
        {
            IEnumerable<SelectListItem> user = db.Users.Select(b => new SelectListItem { Value = b.Id_User.ToString(), Text = b.Login });
            ViewData["Id_User"] = user;
            return View();
        }
        public ActionResult DeletePA(int id)
        {
            Personal_accounts i = db.Personal_accounts.Find(id);
            if (i != null)
            {
                db.Personal_accounts.Remove(i);
                db.SaveChanges();
            }
            return RedirectToAction("ListPA");
        }
        [HttpGet]
        public ActionResult UpdatePA(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Personal_accounts personal = db.Personal_accounts.Find(id);
            if (personal != null)
            {
                IEnumerable<SelectListItem> users = db.Users.Select(b => new SelectListItem { Value = b.Id_User.ToString(), Text = b.Login });
                ViewData["Id_User"] = users;
                return View(personal);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult UpdatePA(Personal_accounts personal)
        {
            try
            {
                db.Entry(personal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListPA");
            }
            catch
            {
                ViewBag.ErrorMessage = "Введите данные";
                return View();
            }
        }
        [HttpPost]
        public ActionResult AddPA(Personal_accounts personal_Accounts)
        {
                try
                {
                    db.Personal_accounts.Add(personal_Accounts);
                    db.SaveChanges();
                    return RedirectToAction("AddPA", new Users());
                }
                catch
                {
                    ViewBag.ErrorMessage = "Введите данные";
                    return View();
                }
        }
        //Начисления
        public ActionResult ListH()
        {
            List<Headings> GetListRole = db.Headings.ToList();
            return View(GetListRole);
        }
        [HttpGet]
        public ActionResult AddH()
        {
            IEnumerable<SelectListItem> pa = db.Personal_accounts.Select(b => new SelectListItem { Value = b.Id_Personal_accounts.ToString(), Text = b.Number.ToString() });
            ViewData["Id_Personal_accounts"] = pa;
            return View();
        }
        public ActionResult DeleteH(int id)
        {
            Headings i = db.Headings.Find(id);
            if (i != null)
            {
                db.Headings.Remove(i);
                db.SaveChanges();
            }
            return RedirectToAction("ListH");
        }
        [HttpGet]
        public ActionResult UpdateH(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Headings headings = db.Headings.Find(id);
            if (headings != null)
            {
                IEnumerable<SelectListItem> personal_accounts = db.Personal_accounts.Select(b => new SelectListItem { Value = b.Id_Personal_accounts.ToString(), Text = b.Name });
                ViewData["Id_Personal_accounts"] = personal_accounts;
                return View(headings);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult UpdateH(Headings headings)
        {
            try
            {
                db.Entry(headings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListH");
            }
            catch
            {
                ViewBag.ErrorMessage = "Введите данные";
                return View();
            }
        }
        [HttpPost]
        public ActionResult AddH(Headings headings)
        {
            try
            {
                db.Headings.Add(headings);
                db.SaveChanges();
                return RedirectToAction("AddH", new Personal_accounts());
            }
            catch
            {
                ViewBag.ErrorMessage = "Введите данные";
                return View();
            }
        }
        //Палтежи
        public ActionResult ListP()
        {
            List<Payments> GetListRole = db.Payments.ToList();
            return View(GetListRole);
        }
        [HttpGet]
        public ActionResult AddP()
        {
            IEnumerable<SelectListItem> pa = db.Personal_accounts.Select(b => new SelectListItem { Value = b.Id_Personal_accounts.ToString(), Text = b.Number.ToString() });
            ViewData["Id_Personal_accounts"] = pa;
            return View();
        }
        public ActionResult DeleteP(int id)
        {
            Payments i = db.Payments.Find(id);
            if (i != null)
            {
                db.Payments.Remove(i);
                db.SaveChanges();
            }
            return RedirectToAction("ListP");
        }
        [HttpGet]
        public ActionResult UpdateP(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Payments personal = db.Payments.Find(id);
            if (personal != null)
            {
                IEnumerable<SelectListItem> pa = db.Personal_accounts.Select(b => new SelectListItem { Value = b.Id_Personal_accounts.ToString(), Text = b.Number.ToString() });
                ViewData["Id_Personal_accounts"] = pa;
                return View(personal);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult UpdateP(Payments payments)
        {
            try
            {
                db.Entry(payments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListP");
            }
            catch
            {
                ViewBag.ErrorMessage = "Введите данные";
                return View();
            }
        }
        [HttpPost]
        public ActionResult AddP(Payments payments)
        {
            try
            {
                db.Payments.Add(payments);
                db.SaveChanges();
                return RedirectToAction("AddP", new Personal_accounts());
            }
            catch
            {
                ViewBag.ErrorMessage = "Введите данные";
                return View();
            }
        }
        //Палтежи
        public ActionResult ListS()
        {
            List<Saldo> GetListRole = db.Saldo.ToList();
            return View(GetListRole);
        }
        [HttpGet]
        public ActionResult AddS()
        {
            IEnumerable<SelectListItem> pa = db.Personal_accounts.Select(b => new SelectListItem { Value = b.Id_Personal_accounts.ToString(), Text = b.Number.ToString() });
            ViewData["Id_Personal_accounts"] = pa;
            return View();
        }
        public ActionResult DeleteS(int id)
        {
            Saldo i = db.Saldo.Find(id);
            if (i != null)
            {
                db.Saldo.Remove(i);
                db.SaveChanges();
            }
            return RedirectToAction("ListS");
        }
        [HttpGet]
        public ActionResult UpdateS(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Saldo personal = db.Saldo.Find(id);
            if (personal != null)
            {
                IEnumerable<SelectListItem> pa = db.Personal_accounts.Select(b => new SelectListItem { Value = b.Id_Personal_accounts.ToString(), Text = b.Number.ToString()});
                ViewData["Id_Personal_accounts"] = pa;
                return View(personal);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult UpdateS(Saldo saldo)
        {
            try
            {
                db.Entry(saldo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListS");
            }
            catch
            {
                ViewBag.ErrorMessage = "Введите данные";
                return View();
            }
        }
        [HttpPost]
        public ActionResult AddS(Saldo saldo)
        {
            try
            {
                db.Saldo.Add(saldo);
                db.SaveChanges();
                return RedirectToAction("AddS", new Personal_accounts());
            }
            catch
            {
                ViewBag.ErrorMessage = "Введите данные";
                return View();
            }
        }
        //Авторизация
        [HttpGet]
        public ActionResult Authorization()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorization(Users SV)
        {
            Users sv = db.Users.FirstOrDefault(x => x.Login == SV.Login && x.Password == SV.Password);
            if (sv != null)
                if (sv.Id_Role == 5)
                {
                    return RedirectToAction("AdminPanel");
                }
                else
                {
                    return RedirectToAction("AddPA");
                }
                ViewBag.Admin = "Пользователь не найден";
            return View();
        }
        public ActionResult AdminPanel()
        {
            return View();
        }
        public ActionResult SaldoView()
        {
            return View();
        }
    }
}