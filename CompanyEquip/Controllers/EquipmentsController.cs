using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Library.Converter;
using Models;
using PagedList;
using Services;

namespace CompanyEquip.Controllers
{
    public class EquipmentsController : Controller
    {
        public IEquipmentService equipmentService;

        public EquipmentsController(IEquipmentService equipmentService)
        {
            this.equipmentService = equipmentService;
        }

        // Index: Equipments
        #region Index
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SerialNumber = sortOrder == "Date" ? "date_desc" : "serialNumber_desc" ;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var equipments = equipmentService.GetWithPagination(sortOrder, searchString);

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(equipments.ToPagedList(pageNumber, pageSize));
        }
        #endregion Index

        // Details: Equipments
        #region Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int ID = id ?? 0;
            var equipment = equipmentService.GetById(ID);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            var equipmentModel = new EquipmentModel();
            equipmentModel.Id = equipment.Id;
            equipmentModel.SerialNumber = equipment.SerialNumber;
            equipmentModel.Name = equipment.Name;
            equipmentModel.NextControlDate = DateConverter.DatetTimeToString(equipment.NextControlDate);
            equipmentModel.Image = equipment.Image;
            return View(equipmentModel);
        }
        #endregion Details

        // Create: Equipments
        #region Create
        public ActionResult Create()
        {
            EquipmentModel equipmentModel = new EquipmentModel();
            return View(equipmentModel);
        }

        [HttpPost]
        public ActionResult Create(EquipmentModel equipmentModel, HttpPostedFileBase ImageEquip)
        {
            var equipment = new Equipment();
            equipment.Name = equipmentModel.Name;
            equipment.SerialNumber = equipmentModel.SerialNumber;
            equipment.NextControlDate = DateConverter.StringToDateTime(equipmentModel.NextControlDate);
            if (ImageEquip != null && ImageEquip.ContentLength > 0)
            {
                equipment.Image = new byte[ImageEquip.ContentLength]; // ImageEquip to store image in binary formate  
                ImageEquip.InputStream.Read(equipment.Image, 0, ImageEquip.ContentLength);
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var inserted = equipmentService.Insert(equipment);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(equipmentModel);
        }
        #endregion

        // Edit: Equipments
        #region Edit      
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int ID = id ?? 0;
            var equipment = equipmentService.GetById(ID);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            var equipmentModel = new EquipmentModel();
            equipmentModel.Id = equipment.Id;
            equipmentModel.SerialNumber = equipment.SerialNumber;
            equipmentModel.Name = equipment.Name;
            equipmentModel.NextControlDate = DateConverter.DatetTimeToString(equipment.NextControlDate);
            equipmentModel.Image = equipment.Image;
            return View(equipmentModel);
        }

        [HttpPost]
        public ActionResult Edit(EquipmentModel equipmentModel, HttpPostedFileBase ImageEquip)
        {
            if (ModelState.IsValid)
            {
                var oldEquipment = equipmentService.GetById(equipmentModel.Id);
                var equipment = new Equipment();
                equipment.Name = equipmentModel.Name;
                equipment.SerialNumber = equipmentModel.SerialNumber;
                equipment.NextControlDate = DateConverter.StringToDateTime(equipmentModel.NextControlDate);
                if (ImageEquip != null && ImageEquip.ContentLength > 0)
                {
                    equipment.Image = new byte[ImageEquip.ContentLength]; // ImageEquip to store image in binary formate  
                    ImageEquip.InputStream.Read(equipment.Image, 0, ImageEquip.ContentLength);
                }

                /* Other Method : Insert image into folder and insert image path into database and display image in 
                 * view from image folder based on path given(stored) in database. */
                //if (ImageEquip == null)
                //{
                //    string ImageName = System.IO.Path.GetFileName(ImageEquip.FileName); //file2 to store path and url  
                //    string physicalPath = Server.MapPath("~/img/" + ImageName);
                //    // save image in folder  
                //    ImageEquip.SaveAs(physicalPath);
                //    equip.PathPhoto = "img/" + ImageName;
                //}

                var updated = equipmentService.Update(oldEquipment, equipment);
                return RedirectToAction("Index");
            }

            return View(equipmentModel);
        }
        #endregion Edit

        // Delete: Equipments
        #region Delete
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            var ID = id ?? 0;
            var equipment = equipmentService.GetById(ID);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var equipment = equipmentService.GetById(id);
                var deleted = equipmentService.Delete(equipment);
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
        #endregion Delete
    }
}