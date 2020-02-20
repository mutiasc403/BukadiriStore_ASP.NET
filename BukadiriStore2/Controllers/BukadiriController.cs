using BukadiriStore2.Context;
using BukadiriStore2.Models;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BukadiriStore2.Controllers
{
    public class BukadiriController : Controller
    {
        private BukadiriContext db = new BukadiriContext();
        //

        //public System.Web.Mvc.ViewResult cekSession()
        //{
        //    if (Session["username"] == null || Session["password"] == null)
        //    {
        //        //Response.Redirect("Login.aspx");
        //        //return RedirectToAction("Login");
        //        return View("Login");
        //    }
        //    else
        //    {
        //        //return RedirectToAction("Index");
        //        return View("Index");
        //    }
        //}

        // GET: /Bukadiri/
        public ActionResult Index()
        {
            //return cekSession();

            if (Session["username"] == null || Session["password"] == null)
            {
                //Response.Redirect("Login.aspx");
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }

            //return View();
        }

        public ActionResult Login()
        {
            //if (Session["username"] != null)
            //{
            //    Session.Abandon();
            //}
            //Session.Abandon();
            //Session["username"] = null;
            Session.RemoveAll();
            return View();
        }

        public ActionResult CekLogIn(Login log)
        {
            //----cara ngeremove session---- (tapi ini harusnya ditaro di return view a.k.a login bukan ceklogin)
            //Session.Contents.RemoveAll();
            //Session.RemoveAll();
            //Session.Clear();
            //Session.Abandon();
            //Session["username"] = null;

            var cek = db.Login.Where(b => b.username.Equals(log.username) && b.password.Equals(log.password));
            int hasil = cek.Count();//cek database, jika ada (1) maka simpan gagal

            if (hasil >= 1) // kalo datanya username dan password ada dan sesuai
            {
                Session["username"] = log.username;
                Session["password"] = log.password;
                return Json(new { success = 1, count = hasil }, JsonRequestBehavior.AllowGet);
            }
            else
            { //hasilnya 0 (kebalikan isinya sama signup)
                return Json(new { success = 2, count = hasil }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SignUp(Login log)
        {
            var cek = db.Login.Where(b => b.username.Equals(log.username));
            int hasil = cek.Count();//cek database, jika ada (1) maka simpan gagal

            if (hasil >= 1)
            {
                return Json(new { success = 2, count = hasil }, JsonRequestBehavior.AllowGet);
            }
            else
            { //hasilnya 0
                db.Login.Add(log);
                db.SaveChanges();//setiap ada perubahan di db harus ada saveChanges
                return Json(new { success = 1, count = hasil }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ResetPass(Login log) //-------------ini belom yg reset-----------------------
        {
            var cek = db.Login.Where(b => b.username.Equals(log.username) && b.password.Equals(log.password));
            int hasil = cek.Count();//cek database, jika ada (1) maka simpan gagal

            if (hasil >= 1)
            {
                return Json(new { success = 2, count = hasil }, JsonRequestBehavior.AllowGet);
            }
            else
            { //hasilnya 0
                db.Login.Add(log);
                db.SaveChanges();//setiap ada perubahan di db harus ada saveChanges
                return Json(new { success = 1, count = hasil }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LogOut()
        {
            Session.RemoveAll();
            return RedirectToAction("Login");
        }

        public ActionResult LoadData()
        {
            var DataHome =
                   from a in db.BukadiriItem
                   join b in db.BukadiriProvinsi on a.kodeProvinsi equals b.kodeProvinsi
                   join c in db.BukadiriPilihan on a.kodePilihan equals c.kodePilihan
                   join d in db.BukadiriLapak on a.kodeLapak equals d.kodeLapak
                   where a.isDelete == 0
                   select new { KodeItem = a.kodeItem, NamaItem = a.namaItem, HargaItem = a.hargaItem, NamaProv = b.namaProvinsi, NamaPilihan = c.namaPilihan, NamaLapak = d.namaLapak }; //produces flat sequence
                    //itu yg kayak KodeItem, NamaItem dll itu yg nanti keliatan di json, yg dipanggil juga di js
            return Json(new { data = DataHome }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Setting()
        {
            //return View();

            if (Session["username"] == null || Session["password"] == null)
            {
                //Response.Redirect("Login.aspx");
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Provinsi()
        {
            //return View(); //kalo pake datatable kan ngeakses LoadProvinsi yg mana di LoadProvinsi bakal ngeakses database, jadi gaperlu pake db.ToList

            if (Session["username"] == null || Session["password"] == null)
            {
                //Response.Redirect("Login.aspx");
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Pilihan()
        {
            //return View();

            if (Session["username"] == null || Session["password"] == null)
            {
                //Response.Redirect("Login.aspx");
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Lapak()
        {
            //return View();

            if (Session["username"] == null || Session["password"] == null)
            {
                //Response.Redirect("Login.aspx");
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Item()
        {
            //return View();

            if (Session["username"] == null || Session["password"] == null)
            {
                //Response.Redirect("Login.aspx");
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }

        public ActionResult LoadDataProvinsi()
        {
            List<BukadiriProvinsi> prov = db.BukadiriProvinsi.ToList<BukadiriProvinsi>(); //cara lain dari db.Biodata.ToList()
            return Json(new { data = prov }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SimpanProvinsi(BukadiriProvinsi prov)
        {
            var cek = db.BukadiriProvinsi.Where(b => b.kodeProvinsi.Equals(prov.kodeProvinsi));
            int hasil = cek.Count();//cek database, jika ada (1) maka simpan gagal

            if (hasil >= 1)
            {
                return Json(new { success = 2, count = hasil }, JsonRequestBehavior.AllowGet);
            }
            else
            { //hasilnya 0
                db.BukadiriProvinsi.Add(prov);
                db.SaveChanges();//setiap ada perubahan di db harus ada saveChanges
                return Json(new { success = 1, count = hasil }, JsonRequestBehavior.AllowGet);
            }

            //db.BukadiriProvinsi.Add(prov);
            //db.SaveChanges();
            //return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DetailProvinsi(string id)
        {
            BukadiriProvinsi prov = db.BukadiriProvinsi.Find(id);
            return Json(new { data = prov }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditProvinsi(string id)
        {
            BukadiriProvinsi prov = db.BukadiriProvinsi.Find(id);
            return Json(new { data = prov }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateProvinsi(BukadiriProvinsi prov)
        {
            db.Entry(prov).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteProvinsi(string id)
        {
            BukadiriProvinsi prov = db.BukadiriProvinsi.Find(id);
            return Json(new { data = prov }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateDeleteProvinsi(BukadiriProvinsi prov)
        {
            var cek = db.BukadiriItem.Where(b => b.kodeProvinsi.Equals(prov.kodeProvinsi) && b.isDelete == 0);
            int hasil = cek.Count();//cek database, jika ada (1) maka simpan gagal

            if (hasil >= 1)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            else
            { //hasilnya 0
                db.Entry(prov).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }

            //db.Entry(prov).State = System.Data.Entity.EntityState.Modified;
            //db.SaveChanges();
            //return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AktifProvinsi(string id)
        {
            var entity = db.BukadiriProvinsi.FirstOrDefault(b => b.kodeProvinsi == id);
            entity.isDelete = 0;
            db.SaveChanges();
            return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportReportProvinsi()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "reportProvinsi.rpt")); //ngeakses folder report, file provreport.rpt
            rd.SetDataSource(db.BukadiriProvinsi.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Provinsi.pdf");
            }
            catch
            {
                throw;
            }
        }

        public ActionResult LoadDataPilihan()
        {
            List<BukadiriPilihan> pil = db.BukadiriPilihan.ToList<BukadiriPilihan>(); //cara lain dari db.Biodata.ToList()
            return Json(new { data = pil }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SimpanPilihan(BukadiriPilihan pil)
        {
            var cek = db.BukadiriPilihan.Where(b => b.kodePilihan.Equals(pil.kodePilihan));
            int hasil = cek.Count();//cek database, jika ada (1) maka simpan gagal

            if (hasil >= 1)
            {
                return Json(new { success = 2, count = hasil }, JsonRequestBehavior.AllowGet);
            }
            else
            { //hasilnya 0
                db.BukadiriPilihan.Add(pil);
                db.SaveChanges();//setiap ada perubahan di db harus ada saveChanges
                return Json(new { success = 1, count = hasil }, JsonRequestBehavior.AllowGet);
            }

            //db.BukadiriPilihan.Add(pil);
            //db.SaveChanges();
            //return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DetailPilihan(string id)
        {
            BukadiriPilihan pil = db.BukadiriPilihan.Find(id);
            return Json(new { data = pil }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditPilihan(string id)
        {
            BukadiriPilihan pil = db.BukadiriPilihan.Find(id);
            return Json(new { data = pil }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdatePilihan(BukadiriPilihan pil)
        {
            db.Entry(pil).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeletePilihan(string id)
        {
            BukadiriPilihan pil = db.BukadiriPilihan.Find(id);
            return Json(new { data = pil }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateDeletePilihan(BukadiriPilihan pil)
        {
            var cek = db.BukadiriItem.Where(b => b.kodePilihan.Equals(pil.kodePilihan) && b.isDelete == 0);
            int hasil = cek.Count();//cek database, jika ada (1) maka simpan gagal

            if (hasil >= 1)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            else
            { //hasilnya 0
                db.Entry(pil).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AktifPilihan(string id)
        {
            var entity = db.BukadiriPilihan.FirstOrDefault(b => b.kodePilihan == id);
            entity.isDelete = 0;
            db.SaveChanges();
            return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportReportPilihan()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "reportPilihan.rpt")); //ngeakses folder report, file provreport.rpt
            rd.SetDataSource(db.BukadiriPilihan.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Pilihan.pdf");
            }
            catch
            {
                throw;
            }
        }

        public ActionResult LoadDataLapak()
        {
            List<BukadiriLapak> lap = db.BukadiriLapak.ToList<BukadiriLapak>(); //cara lain dari db.Biodata.ToList()
            return Json(new { data = lap }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SimpanLapak(BukadiriLapak lap)
        {
            var cek = db.BukadiriLapak.Where(b => b.kodeLapak.Equals(lap.kodeLapak));
            int hasil = cek.Count();//cek database, jika ada (1) maka simpan gagal

            if (hasil >= 1)
            {
                return Json(new { success = 2, count = hasil }, JsonRequestBehavior.AllowGet);
            }
            else
            { //hasilnya 0
                db.BukadiriLapak.Add(lap);
                db.SaveChanges();//setiap ada perubahan di db harus ada saveChanges
                return Json(new { success = 1, count = hasil }, JsonRequestBehavior.AllowGet);
            }
            //db.BukadiriLapak.Add(lap);
            //db.SaveChanges();
            //return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DetailLapak(string id)
        {
            BukadiriLapak lap = db.BukadiriLapak.Find(id);
            return Json(new { data = lap }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditLapak(string id)
        {
            BukadiriLapak lap = db.BukadiriLapak.Find(id);
            return Json(new { data = lap }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateLapak(BukadiriLapak lap)
        {
            db.Entry(lap).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteLapak(string id)
        {
            BukadiriLapak lap = db.BukadiriLapak.Find(id);
            return Json(new { data = lap }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateDeleteLapak(BukadiriLapak lapak)
        {
            var cek = db.BukadiriItem.Where(b => b.kodeLapak.Equals(lapak.kodeLapak) && b.isDelete == 0);
            int hasil = cek.Count();//cek database, jika ada (1) maka simpan gagal

            if (hasil >= 1)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            else
            { //hasilnya 0
                db.Entry(lapak).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AktifLapak(string id)
        {
            var entity = db.BukadiriLapak.FirstOrDefault(b => b.kodeLapak == id);
            entity.isDelete = 0;
            db.SaveChanges();
            return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportReportLapak()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "reportLapak.rpt")); //ngeakses folder report, file provreport.rpt
            rd.SetDataSource(db.BukadiriLapak.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Lapak.pdf");
            }
            catch
            {
                throw;
            }
        }

        public ActionResult LoadDataItem()
        {
            List<BukadiriItem> item = db.BukadiriItem.ToList<BukadiriItem>(); //cara lain dari db.Biodata.ToList()
            return Json(new { data = item }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadDataProvinsiItem()
        {
            //List<BukadiriProvinsi> prov = db.BukadiriProvinsi.ToList<BukadiriProvinsi>(); //cara lain dari db.Biodata.ToList()

            var provitem =
                    from a in db.BukadiriProvinsi
                    where a.isDelete == 0
                    select new { kodeProvinsi = a.kodeProvinsi , namaProvinsi = a.namaProvinsi }; //produces flat sequence
            
            return Json(new { data = provitem }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadDataPilihanItem()
        {
            //List<BukadiriProvinsi> prov = db.BukadiriProvinsi.ToList<BukadiriProvinsi>(); //cara lain dari db.Biodata.ToList()

            var pilitem =
                    from a in db.BukadiriPilihan
                    where a.isDelete == 0
                    select new { kodePilihan = a.kodePilihan , namaPilihan = a.namaPilihan }; //produces flat sequence

            return Json(new { data = pilitem }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadDataLapakItem()
        {
            //List<BukadiriProvinsi> prov = db.BukadiriProvinsi.ToList<BukadiriProvinsi>(); //cara lain dari db.Biodata.ToList()

            var lapitem =
                    from a in db.BukadiriLapak
                    where a.isDelete == 0
                    select new { kodeLapak = a.kodeLapak, namaLapak = a.namaLapak }; //produces flat sequence

            return Json(new { data = lapitem }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SimpanItem(BukadiriItem item)
        {
            var cek = db.BukadiriItem.Where(b => b.kodeItem.Equals(item.kodeItem));
            int hasil = cek.Count();//cek database, jika ada (1) maka simpan gagal

            if (hasil >= 1)
            {
                return Json(new { success = 2, count = hasil }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                db.BukadiriItem.Add(item);
                db.SaveChanges();//setiap ada perubahan di db harus ada saveChanges
                return Json(new { success = 1, count = hasil }, JsonRequestBehavior.AllowGet);
            }

            //db.BukadiriItem.Add(item);
            //db.SaveChanges();
            //return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DetailItem(string id)
        {
            //BukadiriItem item = db.BukadiriItem.Find(id);

            var item =
                    from a in db.BukadiriItem
                    join b in db.BukadiriProvinsi on a.kodeProvinsi equals b.kodeProvinsi
                    join c in db.BukadiriPilihan on a.kodePilihan equals c.kodePilihan
                    join d in db.BukadiriLapak on a.kodeLapak equals d.kodeLapak
                    where a.kodeItem == id // a.kodeItem.Equals(id)
                    //where b.isDelete == 0 && c.isDelete == 0 && d.isDelete == 0
                    select new { kodeItem = a.kodeItem, namaItem = a.namaItem, hargaItem = a.hargaItem, namaProvinsi = b.namaProvinsi, namaPilihan = c.namaPilihan, namaLapak = d.namaLapak, tanggalBuat = a.tanggalBuat, tanggalUbah = a.tanggalUbah, tanggalHapus = a.tanggalHapus }; //produces flat sequence
            
            return Json(new { data = item }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditItem(string id)
        {
            BukadiriItem item = db.BukadiriItem.Find(id);
            return Json(new { data = item }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateItem(BukadiriItem item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteItem(string id)
        {
            BukadiriItem item = db.BukadiriItem.Find(id);
            return Json(new { data = item }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AktifItem(string id)
        {
            var entity = db.BukadiriItem.FirstOrDefault(b => b.kodeItem == id);
            entity.isDelete = 0;
            db.SaveChanges();
            return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportReportItem()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "reportItem.rpt")); //ngeakses folder report, file provreport.rpt
            rd.SetDataSource(db.BukadiriItem.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Item.pdf");
            }
            catch
            {
                throw;
            }
        }
	}
}