namespace QuasarShop.Models
{
    public class CreditCard
    {
        public string Name { get; set; }

        public List<CreditCardInstallment> Installments { get; set; } = new List<CreditCardInstallment>();

        public static List<CreditCard> CreateCards() => new[]
        {
            new CreditCard {
                Name = "world"  ,
                Installments = new []
                {
                    new CreditCardInstallment { Number = 2, Rate = 0 },
                    new CreditCardInstallment { Number = 3, Rate = 0 },
                    new CreditCardInstallment { Number = 4, Rate = 0 },
                    new CreditCardInstallment { Number = 5, Rate = 0.05m },
                    new CreditCardInstallment { Number = 6, Rate = 0.06m },
                    new CreditCardInstallment { Number = 9, Rate = 0.065m },
                    new CreditCardInstallment { Number = 12, Rate = 0.07m },
                }.ToList()
             },
            new CreditCard {
                Name = "bonus"  ,
                Installments = new []
                {
                    new CreditCardInstallment { Number = 3, Rate = 0 },
                    new CreditCardInstallment { Number = 6, Rate = 0.05m },
                    new CreditCardInstallment { Number = 9, Rate = 0.057m },
                    new CreditCardInstallment { Number = 12, Rate = 0.065m },
                }.ToList()
             },
            new CreditCard {
                Name = "maximum"  ,
                Installments = new []
                {
                    new CreditCardInstallment { Number = 2, Rate = 0 },
                    new CreditCardInstallment { Number = 3, Rate = 0 },
                    new CreditCardInstallment { Number = 4, Rate = 0 },
                    new CreditCardInstallment { Number = 5, Rate = 0.053m },
                    new CreditCardInstallment { Number = 6, Rate = 0.065m },
                    new CreditCardInstallment { Number = 9, Rate = 0.072m },
                    new CreditCardInstallment { Number = 12, Rate = 0.078m },
                }.ToList()
             },
            new CreditCard {
                Name = "axess"  ,
                Installments = new []
                {
                    new CreditCardInstallment { Number = 2, Rate = 0 },
                    new CreditCardInstallment { Number = 3, Rate = 0 },
                    new CreditCardInstallment { Number = 4, Rate = 0 },
                    new CreditCardInstallment { Number = 5, Rate = 0.053m },
                    new CreditCardInstallment { Number = 6, Rate = 0.065m },
                    new CreditCardInstallment { Number = 9, Rate = 0.072m },
                    new CreditCardInstallment { Number = 12, Rate = 0.078m },
                }.ToList()
             },
            new CreditCard {
                Name = "cardfinans"  ,
                Installments = new []
                {
                    new CreditCardInstallment { Number = 2, Rate = 0 },
                    new CreditCardInstallment { Number = 3, Rate = 0 },
                    new CreditCardInstallment { Number = 4, Rate = 0 },
                    new CreditCardInstallment { Number = 5, Rate = 0.05m },
                    new CreditCardInstallment { Number = 6, Rate = 0.06m },
                    new CreditCardInstallment { Number = 9, Rate = 0.065m },
                    new CreditCardInstallment { Number = 12, Rate = 0.07m },
                }.ToList()
             },
            new CreditCard {
                Name = "advantage"  ,
                Installments = new []
                {
                    new CreditCardInstallment { Number = 3, Rate = 0 },
                    new CreditCardInstallment { Number = 6, Rate = 0.05m },
                    new CreditCardInstallment { Number = 9, Rate = 0.057m },
                    new CreditCardInstallment { Number = 12, Rate = 0.065m },
                }.ToList()
             },
            new CreditCard {
                Name = "bankkart"  ,
                Installments = new []
                {
                    new CreditCardInstallment { Number = 2, Rate = 0 },
                    new CreditCardInstallment { Number = 3, Rate = 0 },
                    new CreditCardInstallment { Number = 4, Rate = 0 },
                    new CreditCardInstallment { Number = 5, Rate = 0.053m },
                    new CreditCardInstallment { Number = 6, Rate = 0.065m },
                    new CreditCardInstallment { Number = 9, Rate = 0.072m },
                    new CreditCardInstallment { Number = 12, Rate = 0.078m },
                }.ToList()
             },
            new CreditCard {
                Name = "paraf"  ,
                Installments = new []
                {
                    new CreditCardInstallment { Number = 2, Rate = 0 },
                    new CreditCardInstallment { Number = 3, Rate = 0 },
                    new CreditCardInstallment { Number = 4, Rate = 0 },
                    new CreditCardInstallment { Number = 5, Rate = 0.053m },
                    new CreditCardInstallment { Number = 6, Rate = 0.065m },
                    new CreditCardInstallment { Number = 9, Rate = 0.072m },
                    new CreditCardInstallment { Number = 12, Rate = 0.078m },
                }.ToList()
             },
        }.ToList();

    }

    public class CreditCardInstallment
    {
        public int Number { get; set; }
        public decimal Rate { get; set; }

        public decimal Amount(decimal price)
        {
            return (price + (price * Rate)) / Number;
        }
        public decimal Total(decimal price)
        {
            return (price + (price * Rate));
        }

    }
}
