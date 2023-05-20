using NET_Final.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_Final.DAL
{
    internal class DAL
    {
        private static DAL _instance;
        public static DAL Instance
        {
            get
            {
                if (_instance == null) _instance = new DAL();
                return _instance;
            }

            private set { }
        }

        public List<Position> GetAllPositions()
        {
            List<Position> list = new List<Position>();
            using (Model model = new Model())
            {
                list = model.Positions.ToList();
            }
            return list;
        }

        public List<ItemDgv> GetAllEmployees() 
        {
            List<ItemDgv> list = new List<ItemDgv>();
            using (Model model = new Model())
            {
                var tlist = model.Employees.Select(e => new
                {
                    Id = e.Id,
                    Name = e.Name,
                    Birthday = e.Birthday,
                    Gender = e.Gender,
                    Salary = Math.Round((e.Rate + e.Position.PositionRate) * 1.5, 2),
                    PositionId = e.PositionId,
                    PositionName = e.Position.PositionName,
                }).ToList();

                foreach (var e in tlist)
                {
                    list.Add(new ItemDgv
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Birthday = e.Birthday,
                        Gender = e.Gender,
                        Salary = e.Salary,
                        PositionId = e.PositionId,
                        PositionName = e.PositionName
                    });
                }
            }
            return list;
        }

        public Employee GetEmployee(string id)
        {
            using (Model model = new Model())
            {
                return model.Employees.FirstOrDefault(e => e.Id == id);
            }
        }

        public Position GetPosition(int id)
        {
            using (Model model = new Model())
            {
                return model.Positions.FirstOrDefault(p => p.PositionId == id);
            }
        }

        public bool IsExistEmployee(string eid)
        {
            using (Model model = new Model())
            {
                return model.Employees.Any(e => e.Id == eid);
            }
        }

        public void AddEmployee(Employee e)
        {
            using (Model model = new Model())
            {
                model.Employees.Add(e);
                model.SaveChanges();
            }
        }

        public void UpdateEmployee(Employee e)
        {
            using (Model model = new Model())
            {
                var _e = model.Employees.Single(em => em.Id == e.Id);
                _e.Id = e.Id;
                _e.Name = e.Name;
                _e.Birthday = e.Birthday;
                _e.Gender = e.Gender;
                _e.PositionId = e.PositionId;
                model.SaveChanges();
            }
        }

        public void DeleteEmployee(string eid)
        {
            using (Model model = new Model())
            {
                var e = model.Employees.Single(em => em.Id == eid);
                model.Employees.Remove(e);
                model.SaveChanges();
            }
        }
    }
}
