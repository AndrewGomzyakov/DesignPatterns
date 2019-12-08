using System.Reflection.Metadata;

namespace ChainOfResposibility
{
    public enum CurrencyType
    {
        Eur,
        Dollar,
        Ruble
    }

    public interface IBanknote
    {
        CurrencyType Currency { get; }
        string Value { get; }
    } 

    public class Bancomat
    {
        private readonly BanknoteHandler _handler;

        public Bancomat()
        {
            _handler = new TenRubleHandler(null);
            _handler = new TenDollarHandler(_handler);
            _handler = new FiftyDollarHandler(_handler);
            _handler = new HundredDollarHandler(_handler);
        }

        public bool Validate(string banknote)
        {
            return _handler.Validate(banknote);
        } 
    }

    public abstract class BanknoteHandler
    {
        private readonly BanknoteHandler _nextHandler;

        protected BanknoteHandler(BanknoteHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }
        
        protected abstract int Value { get; }
        
        public virtual bool TryGet(int value, out string banknotes)
        {
            if (_nextHandler == null)
            {
                if (value % Value != 0)
                {
                    banknotes = "0";
                    return false;
                }

                banknotes = $"{(value / Value)}*{Value}";
                return true;
            }

            if (_nextHandler.TryGet(value % Value, out var nextBanknotes))
            {
                banknotes = $"{(value / Value)}*{Value}+{nextBanknotes}";
                return true;
            }

            banknotes = nextBanknotes;
            return false;
        }

        public virtual bool Validate(string banknote)
        {
            return _nextHandler != null && _nextHandler.Validate(banknote);
        }
    }

    public abstract class DollarHandlerBase : BanknoteHandler
    {
        public override bool Validate(string banknote)
        {
            if (banknote.EndsWith("$"))
            {
                return true;
            }
            return base.Validate(banknote);
        }

        
        
        protected DollarHandlerBase(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }
    
    public abstract class RubleHandlerBase : BanknoteHandler
    {
        public override bool Validate(string banknote)
        {
            if (banknote.EndsWith("₽"))
            {
                return true;
            }
            return base.Validate(banknote);
        }

        protected RubleHandlerBase(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }

    public abstract class EuroHandlerBase : BanknoteHandler
    {
        public override bool Validate(string banknote)
        {
            if (banknote.EndsWith("€"))
            {
                return true;
            }
            
            return base.Validate(banknote);
        }

        protected EuroHandlerBase(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }
    
    public class TenEuroHandler : EuroHandlerBase
    {
        protected override int Value => 10;

        public TenEuroHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }
    
    public class FiftyEuroHandler : EuroHandlerBase
    {
        protected override int Value => 50;

        public FiftyEuroHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }
    
    public class HundredEuroHandler : EuroHandlerBase
    {
        protected override int Value => 100;

        public HundredEuroHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }
    
    public class TenRubleHandler : RubleHandlerBase
    {
        protected override int Value => 10;

        public TenRubleHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }
    
    public class FiftyRubleHandler : RubleHandlerBase
    {
        protected override int Value => 50;

        public FiftyRubleHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }

    public class HundredRubleHandler : RubleHandlerBase
    {
        protected override int Value => 100;

        public HundredRubleHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }
    
    public class ThousandRubleHandler : RubleHandlerBase
    {
        protected override int Value => 1000;

        public ThousandRubleHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }
    
    public class HundredDollarHandler : DollarHandlerBase
    {
        protected override int Value => 100;

        public HundredDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }

    public class FiftyDollarHandler : DollarHandlerBase
    {
        protected override int Value => 50;

        public FiftyDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }

    public class TenDollarHandler : DollarHandlerBase
    {
        protected override int Value => 10;

        public TenDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }
}
