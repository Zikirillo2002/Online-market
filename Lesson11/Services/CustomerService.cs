using Lesson11.Models;
using Newtonsoft.Json;

namespace Lesson11.Services
{
    public class CustomerService
    {
        private List<Customer> _customers = new List<Customer>();

        public CustomerService()
        {
            LoadDataFromJson();
        }

        public IEnumerable<Customer> GetCustomers() => _customers;

        public void Create(Customer customer)
        {
            _customers.Add(customer);
            SaveDataToJson();
        }

        public Customer FindById(int id) => _customers.FirstOrDefault(c => c.Id == id);

        public void Update(Customer customerToUpdate)
        {
            var customer = FindById(customerToUpdate.Id);
            if (customer != null)
            {
                customer.FirstName = customerToUpdate.FirstName;
                customer.LastName = customerToUpdate.LastName;
                customer.PhoneNumber = customerToUpdate.PhoneNumber;
                SaveDataToJson();
            }
        }

        public void Delete(int id)
        {
            var customer = FindById(id);
            if (customer != null)
            {
                _customers.Remove(customer);
                SaveDataToJson();
            }
        }

        private void SaveDataToJson()
        {
            string json = JsonConvert.SerializeObject(_customers, Formatting.Indented);
            File.WriteAllText("customers.json", json);
        }

        private void LoadDataFromJson()
        {
            if (File.Exists("customers.json"))
            {
                string json = File.ReadAllText("customers.json");
                _customers = JsonConvert.DeserializeObject<List<Customer>>(json);
            }
        }
    }
}

