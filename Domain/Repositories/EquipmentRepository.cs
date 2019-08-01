using Domain.Context;
using Domain.Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{

    public class EquipmentRepository : BaseRepository<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(DataContext context) : base(context) { }

        public IQueryable<EquipmentModel> GetWithPagination(string sortOrder, string searchString)
        {
            var query = this.context.Equipments.Select(i => new EquipmentModel
            {
                SerialNumber = i.SerialNumber,
                Name = i.Name,
                Image = i.Image,
                NextControlDate = i.NextControlDate.ToString(),
                Id = i.Id
            });
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(s => s.Name);
                    break;
                case "serialNumber_desc":
                    query = query.OrderBy(s => s.SerialNumber);
                    break;
                case "date_desc":
                    query = query.OrderByDescending(s => s.NextControlDate);
                    break;
                default:  // Name ascending 
                    query = query.OrderBy(s => s.Name);
                    break;
            }
            return query;
        }
    }

    public interface IEquipmentRepository : IRepository<Equipment>
    {
        IQueryable<EquipmentModel> GetWithPagination(string sortOrder, string searchString);
    }
}
