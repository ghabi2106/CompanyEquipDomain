using Domain.Entities;
using Models;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepository;

        public EquipmentService(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;

        }

        public IQueryable<EquipmentModel> GetWithPagination(string sortOrder, string searchString)
        {
            return _equipmentRepository.GetWithPagination(sortOrder, searchString);
        }

        public Equipment GetById(int id)
        {
            return _equipmentRepository.GetById(id);
        }

        public int Insert(Equipment equipment)
        {
            _equipmentRepository.Insert(equipment);
            _equipmentRepository.SaveChanges();
            return equipment.Id;
        }

        public int Update(Equipment oldEquipment, Equipment equipment)
        {
            oldEquipment.Name = equipment.Name;
            oldEquipment.SerialNumber = equipment.SerialNumber;
            oldEquipment.Image = equipment.Image;
            oldEquipment.NextControlDate = equipment.NextControlDate;
            int rows = _equipmentRepository.SaveChanges();
            return rows;
        }

        public int Delete(Equipment equipment)
        {
            _equipmentRepository.Remove(equipment);
            int rows = _equipmentRepository.SaveChanges();
            return rows;
        }
    }

    public interface IEquipmentService
    {
        IQueryable<EquipmentModel> GetWithPagination(string sortOrder, string searchString);
        Equipment GetById(int id);
        int Insert(Equipment equipment);
        int Update(Equipment oldEquipment, Equipment equipment);
        int Delete(Equipment equipment);
    }
}
