using ArchApp.Context;
using ArchApp.Entities;
using ArchApp.Models;
using ArchApp.Repository;
using ArchApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArchApp.Controllers
{
    public class RepoController : Controller
    {
        [HttpPost]
        public ActionResult AddKitap(KitapViewModel kvm)
        {
            Kitap kitapEntity = new Kitap();

            if (kvm.File != null && kvm.File.ContentLength > 0)
            {
                var allowedExtensions = new[] { ".pdf", ".doc", ".docx" };
                var allowedFileSize = 210000000;
                var checkextension = Path.GetExtension(kvm.File.FileName).ToLower();
                if (!allowedExtensions.Contains(checkextension))
                {

                    return RedirectToAction("RepoNotification", "Notification", new
                    {
                        notification = "Eklenen Dosya Uzantısı Hatalı",
                        alert = "alert alert-danger"
                    });
                }
                if (kvm.File.ContentLength > allowedFileSize)
                {
                    return RedirectToAction("RepoNotification", "Notification", new
                    {
                        notification = "Dosya Boyutu İzin Verilen Sınırın Üzerinde",
                        alert = "alert alert-danger"
                    });
                }
                kvm.Kitap.AttachedFileName = Path.GetFileName(kvm.File.FileName);
                kvm.Kitap.AttachedFilePath = Path.Combine(Server.MapPath("~/UploadedFiles/"), kvm.Kitap.AttachedFileName);
                kvm.File.SaveAs(kvm.Kitap.AttachedFilePath);
            }

            int saveChangesResult = kitapEntity.KitapKayıt(kvm.Kitap);

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
            KitapViewModel vm = new KitapViewModel();
            vm = vm.KitapDuzenlePagePrep(id);

            return View("~/Views/Home/index.cshtml", vm);
        }



        [HttpPost]
        public ActionResult DuzenleKitap(KitapViewModel kvm)
        {
            Kitap duzenlenenKitap = new Kitap();
            using (DbContextApp db = new DbContextApp())
            {
                duzenlenenKitap = db.Kitaplar/*.AsNoTracking()*/.Find(kvm.Kitap.Id);

                AltKategori altKategori = db.AltKategoriler.FirstOrDefault(c => c.Id == kvm.Kitap.AltKategoriId);
                kvm.Kitap.AltKategori = altKategori;
            }

            kvm.Kitap.Yazarlar.RemoveAll(c => string.IsNullOrEmpty(c.Adi));
            kvm.Kitap.Tags.RemoveAll(c => string.IsNullOrEmpty(c.Etiket));
            List<Yazar> yeniYazarlar = kvm.Kitap.Yazarlar.Where(c => c.Id == 0).ToList();
            List<Tag> yeniEtiketler = kvm.Kitap.Tags.Where(c => c.Id == 0).ToList();
            kvm.Kitap.Yazarlar.RemoveAll(c => c.Id == 0);
            kvm.Kitap.Tags.RemoveAll(c => c.Id == 0);

            

            if (kvm.File != null && kvm.File.ContentLength > 0)
            {
                FileActions fileAct = new FileActions();
                fileAct = fileAct.FileCheck(kvm.File);
                if (fileAct.FileCheckRes)
                {
                    kvm.Kitap.AttachedFileName = Path.GetFileName(kvm.File.FileName);
                    kvm.Kitap.AttachedFilePath = Path.Combine(Server.MapPath("~/UploadedFiles/"), kvm.Kitap.AttachedFileName);
                    kvm.File.SaveAs(kvm.Kitap.AttachedFilePath);
                }
                else
                {
                    kvm = kvm.KitapDuzenlePagePrep(kvm.Kitap.Id);
                    ModelState.AddModelError("custom error", fileAct.FileCheckResult);
                    return View("~/Views/Home/index.cshtml", kvm);
                }
            }
            else
            {
                if (duzenlenenKitap.AttachedFileName != null && duzenlenenKitap.AttachedFilePath != null)
                {
                    kvm.Kitap.AttachedFileName = duzenlenenKitap.AttachedFileName;
                    kvm.Kitap.AttachedFilePath = duzenlenenKitap.AttachedFilePath;
                }
            }      

            //Yapılacak - Bu Kitap başlığından var emin misiniz?    

            

            if (duzenlenenKitap != null)
            {

                using (DbContextApp db = new DbContextApp())
                {
                    foreach (var item in kvm.Kitap.Yazarlar)
                    {
                        item.KitapId = kvm.Kitap.Id;

                        db.Entry(item).State = EntityState.Modified;
                    }
                    foreach (var item in kvm.Kitap.Tags)
                    {
                        item.KitapId = kvm.Kitap.Id;

                        db.Entry(item).State = EntityState.Modified;
                    }


                    db.Entry(kvm.Kitap).State = EntityState.Modified;

                    if (yeniYazarlar.Count > 0)
                    {
                        foreach (var item in yeniYazarlar)
                        {
                            item.KitapId = kvm.Kitap.Id;
                            db.Yazarlar.Add(item);
                        }

                    }
                    if (yeniEtiketler.Count > 0)
                    {
                        foreach (var item in yeniEtiketler)
                        {
                            item.KitapId = kvm.Kitap.Id;
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
        public ActionResult RemoveAttachment(int id)
        {
            using (DbContextApp db = new DbContextApp())
            {
                Kitap duzenlenenKitap = db.Kitaplar.Find(id);
                duzenlenenKitap.AttachedFileName = null;
                duzenlenenKitap.AttachedFilePath = null;
                db.Entry(duzenlenenKitap).State = EntityState.Modified;
                int saveChangesResult = db.SaveChanges();

                if (saveChangesResult > 0)
                {

                    KitapViewModel kvm = new KitapViewModel();
                    kvm = kvm.KitapDuzenlePagePrep(id);

                    return View("~/Views/Home/index.cshtml", kvm);
                }
                else
                {
                    return RedirectToAction("RepoNotification", "Notification", new
                    {
                        notification = "Ek Kaldırılamadı, Lütfen Tekrar Deneyiniz",
                        alert = "alert alert-danger"
                    });
                }
            }
            

           
            
        }
    }
}

