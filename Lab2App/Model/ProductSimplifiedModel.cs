using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel;

namespace Lab2App.Model
{

    public class ProductSimplifiedModel { }

    public sealed class ProductSimplified : ObservableObject
    {
        private Guid id;
        private string name = string.Empty;
        private double price;
        private uint stock;

        public Guid Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public double Price
        {
            get { return price; }
            set
            {
                if (price != value)
                {
                    price = value;
                    OnPropertyChanged(nameof(Price));
                }
            }
        }

        public uint Stock
        {
            get { return stock; }
            set
            {
                if (stock != value)
                {
                    stock = value;
                    OnPropertyChanged(nameof(Stock));
                }
            }
        }
    }
}
