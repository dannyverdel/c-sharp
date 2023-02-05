using System;
using Bogus;
using Newtonsoft.Json;

namespace CSharpDemos.ClassLibrary
{
	public class InvokeBogus : IInvokeMethod
	{
        public void InvokeMethod()
        {
            List<BogusModel> model_faker = new BogusModelFaker().Generate(1000);

            Console.WriteLine(JsonConvert.SerializeObject(model_faker.First()));
        }
    }

    public class BogusModelFaker : Faker<BogusModel>
    {
        BogusAddressFaker _address_faker = new BogusAddressFaker();
        public BogusModelFaker()
        {
            int id = 1;

            Random rnd = new Random();

            UseSeed(rnd.Next(10000))
                .RuleFor(mf => mf.Id, _ => id++)
                .RuleFor(mf => mf.Name, f => f.Person.FullName)
                .RuleFor(mf => mf.Email, f => f.Person.Email)
                .RuleFor(mf => mf.Phone, f => f.Phone.PhoneNumberFormat())
                .RuleFor(mf => mf.Address, _ => _address_faker.Generate(1).First().OrNull(_, .1f));
        }
    }

    public class BogusAddressFaker : Faker<BogusAddressModel>
    {
        public BogusAddressFaker()
        {
            Random rnd = new Random();

            UseSeed(rnd.Next(10000))
                .RuleFor(x => x.City, f => f.Address.City())
                .RuleFor(x => x.Zipcode, f => f.Address.ZipCode())
                .RuleFor(x => x.Address, f => f.Address.StreetAddress())
                .RuleFor(x => x.Housenumber, f => f.Address.BuildingNumber())
                .RuleFor(x => x.Country, f => f.Address.Country());
        }
    }

    public class BogusModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public BogusAddressModel? Address { get; set; }
    }

    public class BogusAddressModel
    {
        public string? City { get; set; }
        public string? Zipcode { get; set; }
        public string? Address { get; set; }
        public string? Housenumber { get; set; }
        public string? Country { get; set; }
    }
}

