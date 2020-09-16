using MPConstruction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPConstruction.Services
{
    public class MockDataStore : IDataStore<Equipment>
    {
        readonly List<Equipment> Equipments;

        public MockDataStore()
        {
            Equipments = new List<Equipment>()
            {
                new Equipment { Id = "ID-2010", Name = "Hammer", Comments = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", ImageUrl = "" }
            };
        }

        public async Task<bool> AddItemAsync(Equipment Equipment)
        {
            Equipments.Add(Equipment);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Equipment Equipment)
        {
            var oldEquipment = Equipments.Where((Equipment arg) => arg.Id == Equipment.Id).FirstOrDefault();
            Equipments.Remove(oldEquipment);
            Equipments.Add(Equipment);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldEquipment = Equipments.Where((Equipment arg) => arg.Id == id).FirstOrDefault();
            Equipments.Remove(oldEquipment);

            return await Task.FromResult(true);
        }

        public async Task<Equipment> GetItemAsync(string id)
        {
            return await Task.FromResult(Equipments.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Equipment>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(Equipments);
        }
    }
}
