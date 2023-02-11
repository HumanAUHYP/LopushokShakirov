using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LopushokShakirov.DB
{
    public static class DataAccess
    {
        public static List<Product> GetProducts()
        {
            return db.connection.Product.ToList();
        }
        public static List<Agent> GetAgents()
        {
            return db.connection.Agent.ToList();
        }
        public static List<Material> GetMaterials()
        {
            return db.connection.Material.ToList();
        }
        public static List<MaterialType> GetMaterialTypes()
        {
            return db.connection.MaterialType.ToList();
        }
        public static List<ProductMaterial> GetProductMaterials()
        {
            return db.connection.ProductMaterial.ToList();
        }
        public static List<ProductSale> GetProductSales()
        {
            return db.connection.ProductSale.ToList();
        }
        public static List<ProductType> GetProductTypes()
        {
            return db.connection.ProductType.ToList();
        }
        public static List<Sale> GetSales()
        {
            return db.connection.Sale.ToList();
        }
        public static List<Unit> GetUnits()
        {
            return db.connection.Unit.ToList();
        }
        public static List<Workshop> GetWorkshops()
        {
            return db.connection.Workshop.ToList();
        }
    }
}
