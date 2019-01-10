using ArchApp.Context;
using ArchApp.Entities;
using ArchApp.Repository;
using ArchApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArchApp.Controllers
{
    public class RepoController : Controller
    {
        [HttpPost]
        public ActionResult AddKitap(Kitap kitap)
        {
            Kitap kitapEntity = new Kitap();
            
            int saveChangesResult = kitapEntity.KitapKayıt(kitap);

            if (saveChangesResult > 0)
            {
                return RedirectToAction("RepoNotification", "Notification", new
                {
                    notification = "Kayıt Başarılı",
                    alert = "alert alert-success"
                });
            }
            else
            {
                return RedirectToAction("RepoNotification", "Notification", new
                {
                    notification = "Kayıt Başarısız",
                    alert = "alert alert-danger"
                });
            }

            //return Json(new { success = true, responseText = " Sucessfully." }, JsonRequestBehavior.AllowGet);
            //return Json(new { vm, success = true, responseText = " Sucessfully." }, JsonRequestBehavior.AllowGet);
        }
       

        public ActionResult DeletKitap(int id)
        {
            DbContextApp db = new DbContextApp();
            Kitap kitap = new Kitap();
            kitap = db.Kitaplar.Find(id);
            db.Kitaplar.Remove(kitap);
            int saveChangesResult = db.SaveChanges();

            if (saveChangesResult > 0)
            {
                return RedirectToAction("RepoNotification", "Notification", new
                {
                    notification = "Kayıt Silindi",
                    alert = "alert alert-success"
                });

            }
            else
            {
                return RedirectToAction("RepoNotification", "Notification", new
                {
                    notification = "Kayıt Silinemedi",
                    alert = "alert alert-danger"
                });
            }
        }
        public ActionResult DuzenleKitap(int id)
        {
            ViewModel vm = new ViewModel();
            vm = vm.KitapDuzenlePagePrep(id);

            return View("~/Views/Home/index.cshtml", vm);
        }

      

        [HttpPost]
        public ActionResult DuzenleKitap(Kitap kitap)
        {
            kitap.Yazarlar.RemoveAll(c => string.IsNullOrEmpty(c.Adi));
            kitap.Tags.RemoveAll(c => string.IsNullOrEmpty(c.Etiket));
            List<Yazar> yeniYazarlar = kitap.Yazarlar.Where(c => c.Id == 0).ToList();
            List<Tag> yeniEtiketler = kitap.Tags.Where(c => c.Id == 0).ToList();
            kitap.Yazarlar.RemoveAll(c => c.Id == 0);
            kitap.Tags.RemoveAll(c => c.Id == 0);

            //Yapılacak - Bu Kitap başlığından var emin misiniz?    

            Kitap duzenlenenKitap = new Kitap();
            using (DbContextApp db = new DbContextApp())
            {
                duzenlenenKitap = db.Kitaplar/*.AsNoTracking()*/.FirstOrDefault(c => c.Id == kitap.Id);

                AltKategori altKategori = db.AltKategoriler.FirstOrDefault(c => c.Id == kitap.AltKategoriId);
                kitap.AltKategori = altKategori;
            }

            if (duzenlenenKitap != null)
            {

                using (DbContextApp db = new DbContextApp())
                {
                    foreach (var item in kitap.Yazarlar)
                    {
                        item.KitapId = kitap.Id;

                        db.Entry(item).State = EntityState.Modified;
                    }
                    foreach (var item in kitap.Tags)
                    {
                        item.KitapId = kitap.Id;

                        db.Entry(item).State = EntityState.Modified;
                    }


                    db.Entry(kitap).State = EntityState.Modified;

                    if (yeniYazarlar.Count>0)
                    {
                        foreach (var item in yeniYazarlar)
                        {
                            item.KitapId = kitap.Id;
                            db.Yazarlar.Add(item);
                        }

                    }
                    if (yeniEtiketler.Count>0)
                    {
                        foreach (var item in yeniEtiketler)
                        {
                            item.KitapId = kitap.Id;
                            db.Etiketler.Add(item);
                        }

                    }
                  
                    int saveChangesResult = db.SaveChanges();

                    if (saveChangesResult > 0)
                    {
                        return RedirectToAction("RepoNotification", "Notification", new
                        {
                            notification = "Kayıt Düzenlendi",
                            alert = "alert alert-success"
                        });
                    }
                    else
                    {
                        return RedirectToAction("RepoNotification", "Notification", new
                        {
                            notification = "Kayıt Düzenlenemedi",
                            alert = "alert alert-danger"
                        });
                    }
                }
            }


            //try
            //{

            //    int updateSonuc = repoKitap.Update(kitap, duzenlenenKitap);
            //}
            //catch (DbEntityValidationException e)
            //{

            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        Response.Write(string.Format("Entity türü \"{0}\" şu hatalara sahip \"{1}\" Geçerlilik hataları:", eve.Entry.Entity.GetType().Name, eve.Entry.State));
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Response.Write(string.Format("- Özellik: \"{0}\", Hata: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
            //        }
            //        Response.End();
            //    }
            //}

            return RedirectToAction("Index", "Home");
        }
    }
}

