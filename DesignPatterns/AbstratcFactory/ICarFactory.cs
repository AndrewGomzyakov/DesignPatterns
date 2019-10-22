using System;

namespace AbstratcFactory
{
    public interface IEngine
    {
        string Name { get; }
        int Power { get; }
    }

    public interface IBody
    {
        Guid ID { get; }
        string Color { get; }
    }
    
    public interface ICarFactory
    {
        IEngine CreateEngine();
        IBody CreateBody();
    }

    public class BmwEngine : IEngine
    {
        public string Name => "M70";
        public int Power => 305;
    }

    public class AudiEngine : IEngine
    {
        public string Name => "3.2 VR6 24v FSI (EA390)";
        public int Power => 247;
    }
    
    public class BmwBody : IBody
    {       
        private readonly Guid _id;
        
        public BmwBody()
        {
            _id = Guid.NewGuid();
        }
        public Guid ID => _id;
        public string Color => "red";
    }
    
    public class AudiBody : IBody
    {
        private readonly Guid _id;
        
        public AudiBody()
        {
            _id = Guid.NewGuid();
        }
        public Guid ID => _id;
        public string Color => "blue";
    }

    public class BMWCarFactory : ICarFactory
    {
        public IEngine CreateEngine()
        {
            return new BmwEngine();
        }

        public IBody CreateBody()
        {
            return new BmwBody();
        }
    }
    
    public class AudiCarFactory : ICarFactory
    {
        public IEngine CreateEngine()
        {
            return new AudiEngine();
        }

        public IBody CreateBody()
        {
            return new AudiBody();
        }
    }
}