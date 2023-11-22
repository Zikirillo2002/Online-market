﻿using Lesson11.Models;
using Newtonsoft.Json;

namespace Lesson11.Services
{
    public class SalesService
    {
        private List<Sale> _sales = new List<Sale>();

        public SalesService()
        {
            LoadDataFromJson();
        }

        public IEnumerable<Sale> GetCustomers() => _sales;

        public void Create(Sale sale)
        {
            _sales.Add(sale);
            SaveDataToJson();
        }

        public Sale FindById(int id) => _sales.FirstOrDefault(c => c.Id == id);

        public void Update(Sale saleToUpdate)
        {
            var sale = FindById(saleToUpdate.Id);
            if (sale != null)
            {
                sale.CustomerId = saleToUpdate.CustomerId;
                sale.Date = saleToUpdate.Date;
                SaveDataToJson();
            }
        }

        public void Delete(int id)
        {
            var sale = FindById(id);
            if (sale != null)
            {
                _sales.Remove(sale);
                SaveDataToJson();
            }
        }

        private void SaveDataToJson()
        {
            string json = JsonConvert.SerializeObject(_sales, Formatting.Indented);
            File.WriteAllText("sales.json", json);
        }

        private void LoadDataFromJson()
        {
            if (File.Exists("sales.json"))
            {
                string json = File.ReadAllText("sales.json");
                _sales = JsonConvert.DeserializeObject<List<Sale>>(json);
            }
        }
    }
}
