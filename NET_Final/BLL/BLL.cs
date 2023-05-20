using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using NET_Final.DAL;
using NET_Final.DTO;

namespace NET_Final.BLL
{
    internal class BLL
    {
        private static BLL _instance;
        public static BLL Instance
        {
            get
            {
                if (_instance == null) _instance = new BLL();
                return _instance;
            }

            private set { }
        }

        public List<ItemCbb> GetAllPositions()
        {
            try
            {
                List<ItemCbb> list = new List<ItemCbb>();
                foreach (Position p in DAL.DAL.Instance.GetAllPositions())
                {
                    list.Add(new ItemCbb { Value = p.PositionId, Text = p.PositionName });
                }
                return list;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in BLL.GetAllPositions: " + ex.Message);
                return null;
            }
        }

        public List<ItemDgv> GetAllEmployees()
        {
            try
            {
                return DAL.DAL.Instance.GetAllEmployees();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in BLL.GetAllEmployees: " + ex.Message);
                return null;
            }
        }

        public Employee GetEmployee(string id)
        {
            try
            {
                return DAL.DAL.Instance.GetEmployee(id);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in BLL.GetEmployee: " + ex.Message);
                return null;
            }
        }

        public ItemDgv GetEmployeeInfo(string id)
        {
            try
            {
                var e = GetEmployee(id);
                return new ItemDgv
                {
                    Id = e.Id,
                    Name = e.Name,
                    Birthday = e.Birthday,
                    Gender = e.Gender,
                    PositionId = e.PositionId,
                    PositionName = e.Position.PositionName,
                    Salary = (e.Rate + e.Position.PositionRate) * 1.5,
                };
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in BLL.GetEmployeeInfo: " + ex.Message);
                return null;
            }
        }

        public Position GetPosition(int id)
        {
            try
            {
                return DAL.DAL.Instance.GetPosition(id);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in BLL.GetPosition: " + ex.Message);
                return null;
            }
        }

        public List<ItemDgv> GetEmployeesByFilter(List<ItemDgv> list, int positionId = 0, string searchText = "", string sortProp = "NONE")
        {
            try
            {
                if (sortProp == "NONE")
                    return list.Where(e => (e.PositionId == positionId || positionId == 0) && (e.Name.Contains(searchText))).ToList();

                else
                {
                    PropertyInfo propertyInfo = typeof(ItemDgv).GetProperty(sortProp);
                    return list.Where(e => (e.PositionId == positionId || positionId == 0) && (e.Name.Contains(searchText)))
                        .OrderByDescending(e => propertyInfo.GetValue(e)).ToList();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in BLL.GetEmployeesByFilter: " + ex.Message);
                return null;
            }
        }

        public bool IsExistEmployee(string eid)
        {
            try
            {
                return DAL.DAL.Instance.IsExistEmployee(eid);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in BLL.IsExistEmployee: " + ex.Message);
                throw ex;
            }
        }

        public void AddEmployee(Employee e)
        {
            try
            {
                DAL.DAL.Instance.AddEmployee(e);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in BLL.AddEmployee: " + ex.Message);
            }
        }

        public void UpdateEmployee(Employee e)
        {
            try
            {
                DAL.DAL.Instance.UpdateEmployee(e);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in BLL.UpdateEmployee: " + ex.Message);
            }
        }

        public void DeleteEmployee(string eid)
        {
            try
            {
                DAL.DAL.Instance.DeleteEmployee(eid);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in BLL.DeleteEmployee: " + ex.Message);
            }
        }
    }
}
