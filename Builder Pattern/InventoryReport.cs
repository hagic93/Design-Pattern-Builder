using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Builder_Pattern
{
        public class FurnitureItem
        {
            public string Name;
            public double Price;
            public double Height;
            public double Width;
            public double Weight;

            public FurnitureItem(string productName, double price, double height, double width, double weight)
            {
                this.Name = productName;
                this.Price = price;
                this.Height = height;
                this.Width = width;
                this.Weight = weight;
            }
        }

        public class InventoryReport
        {
            public string TitleSection;
            public string DimensionsSection;
            public string LogisticsSection;

            public string Debug()
            {
                return new StringBuilder().AppendLine(TitleSection).AppendLine(DimensionsSection).AppendLine(LogisticsSection).ToString();
            }
        }

    public interface IFurnitureInventoryBuilder
    {
        IFurnitureInventoryBuilder AddTitle();
        IFurnitureInventoryBuilder AddDimensions();
        IFurnitureInventoryBuilder AddLogistics(DateTime dateTime);
        InventoryReport GetDailyReport();
    }

    public class DialyReportBuilder: IFurnitureInventoryBuilder
    {
        private InventoryReport _report;
        private IEnumerable<FurnitureItem> _items;

        public DialyReportBuilder(IEnumerable<FurnitureItem> items)
        {
            Reset();
            _items = items;
        }

        public IFurnitureInventoryBuilder AddTitle()
        {
            _report.TitleSection = "------ Daily Inventory Report -------\n\n";
            return this;
        }

        public IFurnitureInventoryBuilder AddDimensions()
        {
            _report.DimensionsSection = string.Join(Environment.NewLine, _items.Select(product => 
            $"Product: {product.Name} \nPrice: {product.Price} \n" + 
            $"Height: {product.Height} x Width: {product.Width} -> Weight: {product.Weight}"));

            return this;
        }

        public IFurnitureInventoryBuilder AddLogistics(DateTime dateTime)
        {
            _report.LogisticsSection = $"Report generated on {dateTime}";

            return this;
        }

        public InventoryReport GetDailyReport()
        {
            InventoryReport finishedReport = _report;
            Reset();

            return finishedReport;
        }

        public void Reset()
        {
            _report = new InventoryReport();
        }
    }

    public class InventoryBuildDirector
    {
        private IFurnitureInventoryBuilder _builder;

        public InventoryBuildDirector(IFurnitureInventoryBuilder concreteBuilder)
        {
            _builder = concreteBuilder;
        }

        public void BuildCompleteReport()
        {
            _builder.AddTitle();
            _builder.AddDimensions();
            _builder.AddLogistics(DateTime.Now);
        }
    }

}
