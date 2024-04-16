using System.Net;
using System.Xml;
using tparf.api.Entities;
using tparf.api.Extensions;
using tparf.api.Interfaces;
using tparf.dto.Categories;
using tparf.dto.Manufacturer;
using tparf.dto.Product;
using tparf.dto.Product.Characteristics;
using tparf.dto.Product.Descriptions;

namespace tparf.api.ManufacturerSources
{
    public class Petropump
    {
        private IManufacturerRepository _manufacturer;
        private ICategoryRepository _category;
        private IProductRepository _product;
        private ICurrencyRepository _currency;
        public Petropump(ICategoryRepository category, IProductRepository product, IManufacturerRepository manufacturer, ICurrencyRepository currency)
        {
            _category = category;
            _product = product;
            _manufacturer = manufacturer;
            _currency = currency;
        }

        public async Task Download()
        {
            string url = "https://petropump.ru/bitrix/catalog_export/partners.yml";
            WebRequest request = WebRequest.Create(@"https://petropump.ru/bitrix/catalog_export/partners.yml");
            using (var response = request.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            {
                using (FileStream outputStream = File.OpenWrite("petropump.xml"))
                {
                    responseStream.CopyTo(outputStream);
                }
            }
            Console.WriteLine("Complete");
            //await AddCategories();
            await AddManufacturer();
        }
        public async Task<bool> CategoryExist(string categoryName)
        {
            if (categoryName == "Оборудование для перекачки и учета ГСМ")
            {
               return true;
            }
            return false;
        }

        public async Task<List<CreateCategoryDto>> AddCategories()
        {
            bool petropampCatExist = false;
            
            XmlDocument xmlDoc = new XmlDocument();
            string XMLpath = Directory.GetCurrentDirectory() + @"\petropump.xml";
            xmlDoc.Load(XMLpath);

            List<Category> categories = await _category.GetCategories();

            foreach (var cat in categories)
            {
                if (await CategoryExist(cat.Name) == true)
                {
                    petropampCatExist=true;
                }
            }
            if (petropampCatExist == false)
            {
                var petropump = new CreateCategoryDto
                {
                    Id = 0,
                    Name = "Оборудование для перекачки и учета ГСМ",
                    ImageUrl = "",
                    IconCss = "",
                    ParentId = 0,
                };
                await _category.AddNewCategory(petropump);
            }

            categories = await _category.GetAllCategories();
            List<CreateCategoryDto> petropumpCats = new List<CreateCategoryDto>();

            XmlNodeList CList = xmlDoc.GetElementsByTagName("category");
            foreach (XmlNode CNode in CList)
            {

                if (CNode.Attributes.GetNamedItem("parentId") == null)
                {
                    CreateCategoryDto petropumpCat = new()
                    {
                        Id = System.Convert.ToInt64(CNode.Attributes.GetNamedItem("id").Value),
                        Name = CNode.InnerText,
                        IconCss = "",
                        ImageUrl = "",
                        ParentId = categories.Where(c=>c.Name == "Оборудование для перекачки и учета ГСМ").Select(c=>c.Id).SingleOrDefault(),
                    };
                    petropumpCats.Add(petropumpCat);
                    await _category.AddNewCategory(petropumpCat);
                }

                Console.WriteLine(CNode.InnerText.ToString());
            }

            categories = await _category.GetAllCategories();
            List<CreateCategoryDto> petropumpCatsChild = new List<CreateCategoryDto>();

            foreach (XmlNode CNode in CList)
            {
                if (CNode.Attributes.GetNamedItem("parentId") != null)
                {
                    var parentId = System.Convert.ToInt64(CNode.Attributes.GetNamedItem("parentId").Value);
                    CreateCategoryDto Parent = petropumpCats.FirstOrDefault(c=>c.Id == parentId);
                    CreateCategoryDto petropumpCat = new()
                    {
                        Id = System.Convert.ToInt64(CNode.Attributes.GetNamedItem("id").Value),
                        Name = CNode.InnerText,
                        IconCss = "",
                        ImageUrl = "",
                        ParentId = categories.Where(c => c.Name == Parent.Name).Select(c => c.Id).SingleOrDefault(),
                    };
                    await _category.AddNewCategory(petropumpCat);
                    petropumpCat.ParentId = parentId;
                    petropumpCats.Add(petropumpCat);
                }
            }
            return petropumpCats;
        }

        public async Task AddManufacturer()
        {
            XmlDocument xmlDoc = new XmlDocument();
            string XMLpath = Directory.GetCurrentDirectory() + @"\petropump.xml";
            xmlDoc.Load(XMLpath);

            List<ManufacturerDto> manufacturers = new List<ManufacturerDto>();
            List<string> manufacturerName= new List<string>();

            XmlNodeList offerList = xmlDoc.SelectNodes("//offer");
            foreach (XmlNode offerNode in offerList)
            {
                var Name = offerNode.SelectSingleNode("vendor")?.InnerText;
                manufacturerName.Add(Name);
            }
            var manName = manufacturerName.Distinct();
            foreach (var name in manName)
            {
                await _manufacturer.AddNewManufacturer(new ManufacturerDto
                {
                    Id = 0,
                    Name = name,
                    IconCss = "",
                    ImageUrl = ""
                });
            }

        }

        public async Task AddProducts()
        {
            await Download();
            List<Manufacturer> manufacturers = await _manufacturer.GetManufacturers();
            List<Сurrencies> сurrencies = await _currency.GetCurrencies();
            await AddManufacturer();
            List<CreateCategoryDto> petropumpCats = await AddCategories();
            List<Category> categories = await _category.GetAllCategories();
            XmlDocument xmlDoc = new XmlDocument();

            // Путь к вашему XML файлу
            string XMLpath = Directory.GetCurrentDirectory() + @"\petropump.xml";

            // Загружаем XML файл
            xmlDoc.Load(XMLpath);
            XmlNodeList offerNodeList = xmlDoc.SelectNodes("//offer");
            foreach(XmlNode offer in offerNodeList)
            {
                string article = "";
                if (offer.SelectSingleNode("param[@name='Артикул']") != null)
                    article = offer.SelectSingleNode("param[@name='Артикул']").InnerText;
                string image = "";
                
                if (offer.SelectSingleNode("picture") != null)
                {
                    image = offer.SelectSingleNode("picture").InnerText;
                }                    
                var vendorName = offer.SelectSingleNode("vendor")?.InnerText;
                var currencyName = offer.SelectSingleNode("currencyId")?.InnerText;
                var categoryId = Convert.ToInt64(offer.SelectSingleNode("categoryId")?.InnerText);
                var cat = petropumpCats.Where(c=>c.Id == categoryId).FirstOrDefault();
                CreateProductDto petropumpProduct = new()
                {
                    Id = 0,
                    Name = offer.SelectSingleNode("name").InnerText,
                    Article = article,
                    ImageUrl = image,
                    Price = Convert.ToDecimal(offer.SelectSingleNode("price").InnerText),
                    Discount = 0,
                    ManufacturerId = manufacturers.Where(c => c.Name == vendorName).Select(i => i.Id).Single(),
                    СategoryId = categories.Where(c => c.Name == cat.Name).Select(i => i.Id).Single(),
                    CurrencyId = сurrencies.Where(cur => cur.Name == currencyName).Select(i => i.Id).Single(),
                };
                
                if (petropumpProduct != null)
                {
                    await _product.AddNewProduct(petropumpProduct);                                                  
                    
                    var product = await _product.GetProductByName(petropumpProduct.Name);

                    XmlNodeList paramNodeList = offer.SelectNodes("param");
                    foreach (XmlNode paramNode in paramNodeList)
                    {
                        // Получаем значение атрибута "name"
                        string paramName = paramNode.Attributes["name"]?.Value;

                        // Получаем значение элемента <param>
                        string paramValue = paramNode.InnerText;

                        // Создаем экземпляр класса CharacteristicDto и добавляем его в список характеристик
                        CharacteristicDto characterictic = new()
                        {
                            Id = 0,
                            Name = paramName,
                            Value = paramValue,
                            ProductId = product.Id
                        };
                        await _product.AddNewCharacteristic(characterictic);                                                                 
                        
                    }

                    if (offer.SelectSingleNode("description") != null)
                    {
                        string description = offer.SelectSingleNode("description")?.InnerText;
                        
                        DescriptionDto dto = new()
                        {
                            Id = 0,
                            Text = description,
                            Title = "",
                            ProductId = product.Id
                        };
                        await _product.AddNewDescription(dto);

                        
                    }
                    

                }

                
                Console.WriteLine("---------------------------------------------------------");

            }

        }
    }
}
